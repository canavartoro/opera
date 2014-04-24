using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mikrobar.Module.BusinessObjects
{
    public class CustomAuthentication : AuthenticationBase, IAuthenticationStandard
    {
        private CustomLogonParameters logonParameters;
        public CustomAuthentication()
        {
            logonParameters = new CustomLogonParameters();
        }

        public override void Logoff()
        {
            base.Logoff();
            logonParameters = new CustomLogonParameters();
        }

        public override void ClearSecuredLogonParameters()
        {
            logonParameters.Password = "";
            base.ClearSecuredLogonParameters();
        }

        public override object Authenticate(IObjectSpace objectSpace)
        {
            CustomLogonParameters ModelDefaultLogonParameters = logonParameters as CustomLogonParameters;
            if (ModelDefaultLogonParameters.SistemKullanici == null)
                throw new ArgumentNullException("Kullanici");
            if (!ModelDefaultLogonParameters.SistemKullanici.ComparePassword(ModelDefaultLogonParameters.SistemKullanici, ModelDefaultLogonParameters.Password))
                throw new AuthenticationException(
                    ModelDefaultLogonParameters.SistemKullanici.UserName, "Password mismatch.");
            return objectSpace.GetObjectByKey<SistemKullanicilari>(ModelDefaultLogonParameters.SistemKullanici.Oid);
        }

        public override IList<Type> GetBusinessClasses()
        {
            return new Type[] { typeof(CustomLogonParameters) };
        }

        public override bool AskLogonParametersViaUI
        {
            get { return true; }
        }

        public override object LogonParameters
        {
            get { return logonParameters; }
        }

        public override bool IsLogoffEnabled
        {
            get { return true; }
        }
    }
}
