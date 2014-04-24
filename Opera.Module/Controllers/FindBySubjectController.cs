using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Actions;

namespace Mikrobar.Module.Controllers
{
	public partial class FindBySubjectController : ViewController {
		public FindBySubjectController()
			: base() {
			InitializeComponent();
			RegisterActions(components);
		}
		private void FindBySubjectAction_Execute(object sender, ParametrizedActionExecuteEventArgs e) {
			IObjectSpace objectSpace = Application.CreateObjectSpace();
			string paramValue = e.ParameterCurrentValue as string;
			if(!string.IsNullOrEmpty(paramValue)) {
				paramValue = "%" + paramValue + "%";
			}
			object obj = objectSpace.FindObject(((ListView)View).ObjectTypeInfo.Type,
				new BinaryOperator("Subject", paramValue, BinaryOperatorType.Like));
			if(obj != null) {
				e.ShowViewParameters.CreatedView = Application.CreateDetailView(objectSpace, obj);
			}
		}
	}
}
