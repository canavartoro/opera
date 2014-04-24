using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.ExpressApp.Editors;
using DevExpress.Web.ASPxEditors;

namespace Mikrobar.Module.BusinessObjects
{
    public partial class ViewController1 : ViewController<DetailView>
    {
        public ViewController1()
        {
            TargetObjectType = typeof(Aletler);
        }

        protected override void OnActivated()
        {
            ObjectSpace.ObjectChanged += new EventHandler<ObjectChangedEventArgs>(ObjectSpace_ObjectChanged);
            base.OnActivated();
        }

        void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e)
        {
            if (e.Object == View.CurrentObject && e.PropertyName == "Istasyon")
            {
                ASPxPropertyEditor propertyEditor = (ASPxPropertyEditor)View.FindItem("Istasyon");
                if (propertyEditor != null && propertyEditor.Editor != null)
                {
                    propertyEditor.ViewEditMode = ViewEditMode.Edit;
                    ChangeAutoComplete(propertyEditor);
                }
                else
                {
                    propertyEditor.ControlCreated += new EventHandler<EventArgs>(propertyEditor_ControlCreated);
                }
            }
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            if (View.ViewEditMode == ViewEditMode.Edit)
            {
                ASPxPropertyEditor propertyEditor = (ASPxPropertyEditor)View.FindItem("Istasyon");
                if (propertyEditor != null && propertyEditor.Editor != null)
                {
                    ChangeAutoComplete(propertyEditor);
                }
                else
                {
                    propertyEditor.ControlCreated += new EventHandler<EventArgs>(propertyEditor_ControlCreated);
                }
            }
        }
        void propertyEditor_ControlCreated(object sender, EventArgs e)
        {
            ChangeAutoComplete((ASPxPropertyEditor)sender);
        }
        private void ChangeAutoComplete(ASPxPropertyEditor propertyEditor)
        {
            if (propertyEditor.Editor is ASPxTextBoxBase)
            {
                ((ASPxTextBoxBase)propertyEditor.Editor).AutoCompleteType = System.Web.UI.WebControls.AutoCompleteType.LastName;
            }
        }
    }
}
