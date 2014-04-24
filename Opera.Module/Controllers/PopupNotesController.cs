using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.BaseImpl;
using Mikrobar.Module.BusinessObjects;

namespace Mikrobar.Module.Controllers
{
	public partial class PopupNotesController : ViewController {
		public PopupNotesController()
			: base() {
			InitializeComponent();
			RegisterActions(components);
		}
		private void ShowNotesAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs args) {
			DemoTask task = (DemoTask)View.CurrentObject;
			View.ObjectSpace.SetModified(task);
			foreach(Note note in args.PopupWindow.View.SelectedObjects) {
				if(!string.IsNullOrEmpty(task.Description)) {
					task.Description += Environment.NewLine;
				}
				task.Description += note.Text;
			}
            ViewItem item = ((DetailView)View).FindItem("Description");
			((PropertyEditor)item).ReadValue();
			if(View is DetailView && ((DetailView)View).ViewEditMode == ViewEditMode.View) {
				View.ObjectSpace.CommitChanges();
			}
		}
        private void ShowNotesAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs args)
        {
			IObjectSpace objectSpace = Application.CreateObjectSpace();
			args.View = Application.CreateListView(Application.FindListViewId(typeof(Note)), new CollectionSource(objectSpace, typeof(Note)), true);
		}
	}
}
