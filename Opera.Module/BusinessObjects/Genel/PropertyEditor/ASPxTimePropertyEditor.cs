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
    public class ASPxTimePropertyEditor : ASPxPropertyEditor
    {
        private const string TimeFormat = "HH:mm";

        public ASPxTimePropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }

        public new ASPxTimeEdit Editor
        {
            get { return (ASPxTimeEdit)base.Editor; }
        }

        private void SelectedDateChangedHandler(object source, EventArgs e)
        {
            try
            {
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
                return DateTime.Now.ToString(TimeFormat);
            }
            return ((DateTime)base.PropertyValue).ToString(TimeFormat);
        }

        protected override void SetupControl(WebControl control)
        {
            base.SetupControl(control);
            if (control is ASPxTimeEdit)
            {
                ASPxTimeEdit aSPxDateEdit = (ASPxTimeEdit)control;
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
            return new ASPxTimeEdit();
        }
        public override void BreakLinksToControl(bool unwireEventsOnly)
        {
            if (this.Editor != null)
            {
                this.Editor.DateChanged -= new EventHandler(this.SelectedDateChangedHandler);
            }
            base.BreakLinksToControl(unwireEventsOnly);
        }

        private void FixYear(ASPxTimeEdit editor)
        {
            if (editor == null)
                return;

            //if (editor.Value is DateTime)
            //{
            //    DateTime time = (DateTime)editor.Value;
            //    editor.Value = time.AddYears(Math.Max(2000 - time.Year, 0));
            //}
        }

        /*public ASPxTimePropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }

        public new ASPxTimeEdit Editor
        {
            get { return (ASPxTimeEdit)base.Editor; }
        }
        private void SelectedDateChangedHandler(object source, EventArgs e)
        {
            try
            {
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
                return string.Empty;
            }
            return ((DateTime)base.PropertyValue).ToString(TimeFormat);
        }
        protected override void SetupControl(WebControl control)
        {
            base.SetupControl(control);
            if (control is ASPxTimeEdit)
            {
                ASPxTimeEdit aSPxDateEdit = (ASPxTimeEdit)control;
                //aSPxDateEdit.CalendarProperties.DisplayFormatString = base.DisplayFormat;
                aSPxDateEdit.EditFormat = EditFormat.ModelDefault;
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
            return new ASPxTimeEdit();
        }
        public override void BreakLinksToControl(bool unwireEventsOnly)
        {
            if (this.Editor != null)
            {
                this.Editor.DateChanged -= new EventHandler(this.SelectedDateChangedHandler);
            }
            base.BreakLinksToControl(unwireEventsOnly);
        }
        */
    }
}
