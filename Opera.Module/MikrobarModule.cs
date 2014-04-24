using System;
using System.Reflection;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo.Metadata;
using DevExpress.Xpo;
using System.Configuration;
using DevExpress.ExpressApp.ViewVariantsModule;
using Mikrobar.Module.BusinessObjects;

namespace Mikrobar.Module
{
	public sealed partial class MikrobarModule : ModuleBase {
		public MikrobarModule() {
			InitializeComponent();
		}
        public override void Setup(XafApplication application) {
            base.Setup(application);
            application.CreateCustomLogonWindowObjectSpace += application_CreateModelDefaultLogonWindowObjectSpace;
            ((ViewVariantsModule)application.Modules.FindModule(typeof(ViewVariantsModule))).GenerateVariantsNode = false;
        }

        void application_CreateModelDefaultLogonWindowObjectSpace(object sender, CreateCustomLogonWindowObjectSpaceEventArgs e)
        {
            IObjectSpace objectSpace = ((XafApplication)sender).CreateObjectSpace();
            ((CustomLogonParameters)e.LogonParameters).ObjectSpace = objectSpace;
            e.ObjectSpace = objectSpace;
        }
        static MikrobarModule()
        {
			/*Note that you can specify the required format in a configuration file:
			<appSettings>
			   <add key="FullAddressFormat" value="{Country.Name} {City} {Street}">
			   <add key="FullAddressPersistentAlias" value="Country.Name+City+Street">
			   ...
			</appSettings>

			... and set the specified format here in code:
			Address.SetFullAddressFormat(ConfigurationManager.AppSettings["FullAddressFormat"], ConfigurationManager.AppSettings["FullAddressPersistentAlias"]);
			*/

			Person.SetFullNameFormat("{LastName} {FirstName} {MiddleName}", "concat(FirstName, MiddleName, LastName)");
			Address.SetFullAddressFormat("City: {City}, Street: {Street}", "concat(City, Street)");
		}
	}
}
