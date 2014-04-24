using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;
using DevExpress.Web.ASPxEditors;
using System.Web.UI.WebControls;

namespace Mikrobar.Module.BusinessObjects
{
    [PropertyEditor(typeof(object), false)]//EditorAliases.LookupPropertyEditor)]
    public class URTASPxLookupPropertyEditor : ASPxLookupPropertyEditor
    {
        public URTASPxLookupPropertyEditor(Type objectType, IModelMemberViewItem model)
            : base(objectType, model) 
        {
        }

        protected override void SetupControl(WebControl control) {
            base.SetupControl(control);

            if (control is ASPxLookupDropDownEdit)
            {
                ASPxComboBox combo = ((ASPxLookupDropDownEdit)control).DropDown;
                if (combo == null)
                {
                    DevExpress.Persistent.Base.Tracing.Tracer.LogWarning("Cannot find ASPxComboBox to ModelDefaultize it!");
                }
                else
                {
                    combo.ClientSideEvents.DropDown = "function(s, e) { s.SelectAll(); } ";
                    combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                }
            }
        }
    }
}
