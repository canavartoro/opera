using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Web.ASPxEditors;
using System.Globalization;
using System.Web.UI.WebControls;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using System.Collections;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Editors;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.Web.Editors;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Filtering;

namespace Mikrobar.Module.BusinessObjects
{
    [PropertyEditor(typeof(System.Object), false)]
    public class ASPxSearchEditButtonPropertyEditor : ASPxObjectPropertyEditorBase
    {
        

        Type objeType;
        IModelMemberViewItem viewIteminfo;
        ASPxButtonEdit xButtonEditControl = null;
        public ASPxSearchEditButtonPropertyEditor(
        Type objectType, IModelMemberViewItem info)
            : base(objectType, info)
        {
            objeType = objectType;
            viewIteminfo = info;
        }

        public override void Setup(IObjectSpace objectSpace, XafApplication application)
        {
            base.Setup(objectSpace, application);
            helper = new WebLookupEditorHelper(application, objectSpace, MemberInfo.MemberTypeInfo, Model);
        }

        protected override void SetupControl(WebControl control)
        {
            if (ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.Edit)
            {
                EditButton xEditButton = new EditButton();
                xEditButton.Text = "...";
                xEditButton.Enabled = true;

                xButtonEditControl = new ASPxButtonEdit();
                xButtonEditControl.AllowUserInput = true;
                xButtonEditControl.AutoPostBack = true;
                xButtonEditControl.BackColor = System.Drawing.Color.LightGoldenrodYellow;
                xButtonEditControl.Buttons.Add(xEditButton);
                xButtonEditControl.ValueChanged += new EventHandler(xButtonEditControl_ValueChanged);
                //xButtonEditControl.ValueChanged += new EventHandler(ExtendedEditValueChangedHandler);
                //xButtonEditControl.TextChanged += new EventHandler(xButtonEditControl_TextChanged);
                xButtonEditControl.ButtonClick += new ButtonEditClickEventHandler(xSpinEditControl_ButtonClick);
                if (CurrentObject != null)
                {
                    object val = MemberInfo.GetValue(CurrentObject);
                    if (val != null)
                    {
                        MemberInfo.SetValue(CurrentObject, val);
                        xButtonEditControl.Text = Helper.GetDisplayText(val, EmptyValue, DisplayFormat);
                    }
                }
            }            
        }
        
        void xButtonEditControl_ValueChanged(object sender, EventArgs e)
        {
            ASPxButtonEdit editButton = sender as ASPxButtonEdit;
            if (editButton != null)
            {
                object val = null;
                if (!string.IsNullOrEmpty(editButton.Text))
                {
                    SearchCriteriaBuilder criteriaBuilder = new SearchCriteriaBuilder();
                    criteriaBuilder.TypeInfo = Helper.LookupObjectTypeInfo;
                    criteriaBuilder.SearchInStringPropertiesOnly = false;
                    criteriaBuilder.IncludeNonPersistentMembers = false;
                    criteriaBuilder.SetSearchProperties(Helper.DisplayMember.Name);
                    criteriaBuilder.SearchText = editButton.Text;
                    criteriaBuilder.SearchMode = SearchMode.SearchInProperty;
                    val = ((IObjectSpace)View.ObjectSpace).FindObject(Helper.LookupObjectType,criteriaBuilder.BuildCriteria());
                    if (val != null)
                    {                        
                        MemberInfo.SetValue(CurrentObject, val);
                        editButton.Text = Helper.GetDisplayText(val, EmptyValue, DisplayFormat);
                    }
                }
                if (CurrentObject != null)
                {
                    val = MemberInfo.GetValue(CurrentObject);
                    if (val != null)
                    {
                        editButton.Text = Helper.GetDisplayText(val, EmptyValue, DisplayFormat);
                    }
                }
                if (object.ReferenceEquals(null, val))
                {
                    MemberInfo.SetValue(CurrentObject, null);
                    editButton.Text = string.Empty;
                }
            }
        }

        protected override WebControl CreateEditModeControlCore()
        {
            if (xButtonEditControl == null)
            {
                EditButton xEditButton = new EditButton();
                xEditButton.Text = "...";
                xEditButton.Enabled = true;

                xButtonEditControl = new ASPxButtonEdit();
                xButtonEditControl.AllowUserInput = true;
                xButtonEditControl.AutoPostBack = true;
                xButtonEditControl.BackColor = System.Drawing.Color.LightGoldenrodYellow;
                xButtonEditControl.Buttons.Add(xEditButton);
                xButtonEditControl.ValueChanged += new EventHandler(xButtonEditControl_ValueChanged);
                xButtonEditControl.ButtonClick += new ButtonEditClickEventHandler(xSpinEditControl_ButtonClick);
                if (CurrentObject != null)
                {
                    object val = MemberInfo.GetValue(CurrentObject);
                    if (val != null)
                    {
                        MemberInfo.SetValue(CurrentObject, val);
                        xButtonEditControl.Text = Helper.GetDisplayText(val, EmptyValue, DisplayFormat);
                    }
                }
            }
            return xButtonEditControl;
        }

        public new ASPxButtonEdit Editor
        {
            get { return (ASPxButtonEdit)base.Editor; }
        }

        protected override void OnValueStoring(object newValue)
        {
            base.OnValueStoring(newValue);
        }

        protected override void SetImmediatePostDataScript(string script)
        {
            this.Editor.ClientSideEvents.ValueChanged = script;
        }

        void xSpinEditControl_ButtonClick(object source, ButtonEditClickEventArgs e)
        {
            WebApplication app = WebWindow.CurrentRequestWindow.Application;
            IObjectSpace objectSpace = app.CreateObjectSpace();
            string listViewId = app.FindListViewId(viewIteminfo.ModelMember.Type);
            ShowViewParameters svp = new ShowViewParameters();
            svp.CreatedView = app.CreateListView(listViewId, app.CreateCollectionSource(objectSpace, viewIteminfo.ModelMember.Type, listViewId), true);

            app.CreateListView(listViewId, new CollectionSource(objectSpace, viewIteminfo.ModelMember.Type), false);

            svp.TargetWindow = TargetWindow.NewModalWindow;
            svp.Context = TemplateContext.PopupWindow;
            svp.CreateAllControllers = true;
            DialogController dialogController = app.CreateController<DialogController>();
            dialogController.ViewClosed += new EventHandler(dialogController_ViewClosed);
            svp.Controllers.Add(dialogController);
            WebWindow.CurrentRequestWindow.Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(WebWindow.CurrentRequestWindow, null));
        }

        void dialogController_ViewClosed(object sender, EventArgs e)
        {
            IList selectedObjects = ((ListView)sender).SelectedObjects;
            if (selectedObjects != null && selectedObjects.Count > 0)
            {
                object val = helper.ObjectSpace.GetObject(selectedObjects[0]);
                MemberInfo.SetValue(CurrentObject, val);
                if (xButtonEditControl != null)
                    xButtonEditControl.Text = Helper.GetDisplayText(val, EmptyValue, DisplayFormat);
            }
            else
            {
                MemberInfo.SetValue(CurrentObject, null);
                if (xButtonEditControl != null)
                    xButtonEditControl.Text = string.Empty;
            }
        }

        public override void BreakLinksToControl(bool unwireEventsOnly)
        {
            if (xButtonEditControl != null)
            {
                //xButtonEditControl.ValueChanged -= new EventHandler(xButtonEditControl_ValueChanged);
            }
            base.BreakLinksToControl(unwireEventsOnly);
        }

        protected override void ReadEditModeValueCore()
        {
        }

        protected override object GetControlValueCore()
        {
            return null;
        }

        private WebLookupEditorHelper helper;
        public LookupEditorHelper Helper
        {
            get { return helper; }
        }

    }
}
