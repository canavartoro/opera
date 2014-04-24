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

namespace Mikrobar.Module.BusinessObjects
{
    [PropertyEditor(typeof(string),false)]
    public class ModelDefaultStringEditor : ASPxPropertyEditor
    {
        ASPxButtonEdit xButtonEditControl = null;
        public ModelDefaultStringEditor(
        Type objectType, IModelMemberViewItem info)
            : base(objectType, info) { }

        protected override void SetupControl(WebControl control)
        {
            if (ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.Edit)
            {
                //foreach (CultureInfo culture in CultureInfo.GetCultures(
                //CultureTypes.InstalledWin32Cultures))
                //{
                //    ((ASPxComboBox)control).Items.Add(culture.EnglishName + "(" + culture.Name + ")");
                //}
            }
        }
        protected override WebControl CreateEditModeControlCore()
        {
            EditButton xEditButton = new EditButton();
            xEditButton.Text = "...";
            xEditButton.Enabled = true;

            xButtonEditControl = new ASPxButtonEdit();
            xButtonEditControl.AllowUserInput = true;
            xButtonEditControl.Buttons.Add(xEditButton);
            xButtonEditControl.ValueChanged += new EventHandler(ExtendedEditValueChangedHandler);
            xButtonEditControl.TextChanged += new EventHandler(xButtonEditControl_TextChanged);
            xButtonEditControl.ButtonClick += new ButtonEditClickEventHandler(xSpinEditControl_ButtonClick);
            return xButtonEditControl;
        }

        void xButtonEditControl_TextChanged(object sender, EventArgs e)
        {
            ASPxButtonEdit editButton = sender as ASPxButtonEdit;
            if (editButton != null)
            {
                //if (string.IsNullOrEmpty(editButton.Text))
                //{

                //    CurrentObject = ((ObjectSpace)View.ObjectSpace).Session.FindObject<Istasyonlar>(new BinaryOperator("IstasyonKod", string.Format("%{0}%"), BinaryOperatorType.Like));
                //}
                //else
                //{
                //    CurrentObject = null;
                //}
            }
        }

        public new ASPxButtonEdit Editor
        {
            get { return (ASPxButtonEdit)base.Editor; }
        }

        protected override void OnValueStoring(object newValue)
        {
            base.OnValueStoring(newValue);
        }

        protected override string GetPropertyDisplayValue()
        {
            if (object.ReferenceEquals(base.PropertyValue, null))
            {
                return string.Empty;
            }
            return base.PropertyValue.ToString();
        }

        protected override void SetImmediatePostDataScript(string script)
        {
            this.Editor.ClientSideEvents.ValueChanged = script;
        }

        void xSpinEditControl_ButtonClick(object source, ButtonEditClickEventArgs e)
        {
            WebApplication app = WebWindow.CurrentRequestWindow.Application;
            IObjectSpace objectSpace = app.CreateObjectSpace();
            string listViewId = app.FindListViewId(typeof(Istasyonlar));
            ShowViewParameters svp = new ShowViewParameters();
            svp.CreatedView = app.CreateListView(listViewId, app.CreateCollectionSource(objectSpace, typeof(Istasyonlar), listViewId), true);

            app.CreateListView(app.FindListViewId(typeof(Istasyonlar)), new CollectionSource(objectSpace, typeof(Istasyonlar)), false);

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
            //SayimEmirleri emir = ((DetailView)WebWindow.CurrentRequestWindow.View).CurrentObject as SayimEmirleri;
            IList selectedObjects = ((ListView)sender).SelectedObjects;
            if (selectedObjects != null && selectedObjects.Count > 0)
            {
                ///CurrentObject = selectedObjects[0];
                //IEnumerable<V_IsEmirleri> vraf = selectedObjects.Cast<V_IsEmirleri>();
                //int[] rafIds = vraf.Select(x => x.RafId).ToArray();
            }
        }

        public override void BreakLinksToControl(bool unwireEventsOnly)
        {
            if (xButtonEditControl != null)
            {
                xButtonEditControl.ValueChanged -= new EventHandler(ExtendedEditValueChangedHandler);
            }
            base.BreakLinksToControl(unwireEventsOnly);
        }
    }
}
