using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Web.TestScripts;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.ExpressApp.Model;
using DevExpress.Web.ASPxEditors;
using System.Web.UI.WebControls;

namespace Mikrobar.Module.BusinessObjects
{
    //[PropertyEditor(typeof(DateTime), false)]
    //[PropertyEditor(typeof(DateTimeOffset), true)]
    public class ASPxDatePropertyEditor : ASPxPropertyEditor
    {
        private const string TimeFormat = "dd.MM.yyyy";

        public ASPxDatePropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }

        public new ASPxDateEdit Editor
        {
            get { return (ASPxDateEdit)base.Editor; }
        }

        private void SelectedDateChangedHandler(object source, EventArgs e)
        {
            try
            {
                //FixYear(source as ASPxTimeEdit);

                base.EditValueChangedHandler(source, e);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.StackTrace);
            }
        }

        protected override string GetPropertyDisplayValue()
        {
            if (object.Equals(base.PropertyValue, DateTime.MinValue) || !(base.PropertyValue is DateTime))
            {
                return "";
            }
            return ((DateTime)base.PropertyValue).Date.ToString(TimeFormat);
        }

        protected override void SetupControl(WebControl control)
        {
            base.SetupControl(control);
            if (control is ASPxDateEdit)
            {
                ASPxDateEdit aSPxDateEdit = (ASPxDateEdit)control;
                //aSPxDateEdit.CalendarProperties.DisplayFormatString = base.DisplayFormat;
                aSPxDateEdit.EditFormat = EditFormat.Custom;
                aSPxDateEdit.EditFormatString = TimeFormat;//base.EditMask;
                aSPxDateEdit.DisplayFormatString = TimeFormat;
                //aSPxDateEdit.CalendarProperties.DaySelectedStyle.CssClass = "ASPxSelectedItem";
                aSPxDateEdit.DateChanged += new EventHandler(this.SelectedDateChangedHandler);
            }
        }
        protected override void SetImmediatePostDataScript(string script)
        {
            this.Editor.ClientSideEvents.ValueChanged = script;
        }
        protected override WebControl CreateEditModeControlCore()
        {
            return new ASPxDateEdit();
        }
        public override void BreakLinksToControl(bool unwireEventsOnly)
        {
            if (this.Editor != null)
            {
                this.Editor.DateChanged -= new EventHandler(this.SelectedDateChangedHandler);
            }
            base.BreakLinksToControl(unwireEventsOnly);
        }

        private void FixYear(ASPxDateEdit editor)
        {
            if (editor == null)
                return;

            //if (editor.Value is DateTime)
            //{
            //    DateTime time = (DateTime)editor.Value;
            //    editor.Value = time.AddYears(Math.Max(2000 - time.Year, 0));
            //}
        }
    }
}
