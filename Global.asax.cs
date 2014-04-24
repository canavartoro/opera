using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using DevExpress.ExpressApp.Web;
using System.Configuration;
using DevExpress.Xpo.DB;
using DevExpress.Xpo;

namespace Opera
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            #region Mikrobar Baglanti
            string ConnStr = ConfigurationManager.ConnectionStrings["MIKROBAR"].ConnectionString;
            DevExpress.Xpo.Metadata.XPDictionary dictionary = new DevExpress.Xpo.Metadata.ReflectionDictionary();
            XpoDefault.DataLayer = XpoDefault.GetDataLayer(ConnStr, AutoCreateOption.DatabaseAndSchema);
            DevExpress.Xpo.XpoDefault.Session = new DevExpress.Xpo.Session(XpoDefault.DataLayer);

            if (ConfigurationManager.AppSettings["TestApp"] != null)
            {
                if (ConfigurationManager.AppSettings["TestApp"] == "1")
                {
                    new Session(XpoDefault.DataLayer).UpdateSchema();
                }
            }

            DevExpress.Xpo.Helpers.SessionStateStack.SuppressCrossThreadFailuresDetection = true;
            DevExpress.Xpo.Session.GlobalSuppressExceptionOnReferredObjectAbsentInDataStore = true;

            if (HttpContext.Current != null && HttpContext.Current.Server != null)
            {
                Utillity.APP_PATH = HttpContext.Current.Request.PhysicalApplicationPath;
                //APP_PATH = HttpContext.Current.Server.MapPath("..");
                //APP_PATH = HttpContext.Current.Server.MapPath("~");
                System.Diagnostics.Trace.WriteLine(Utillity.APP_PATH);
            }

            #endregion
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            try
            {
                #region Mikrobar Config
                //if (ConfigurationManager.ConnectionStrings["ERP"] != null)
                //{
                //    MikrobarApp.ProviderName = ConfigurationManager.ConnectionStrings["ERP"].ProviderName;
                //    MikrobarApp.ErpConnectionString = ConfigurationManager.ConnectionStrings["ERP"].ConnectionString;
                //}
                //else
                //    throw new MikrobarException("ERP Baglanti bilgisi on tanimli olmalidir!", 75, "Global");

                //if (ConfigurationManager.ConnectionStrings["MIKROBAR"] != null)
                //{
                //    MikrobarApp.MikrobarProviderName = ConfigurationManager.ConnectionStrings["MIKROBAR"].ProviderName;
                //    MikrobarApp.ConnectionString = ConfigurationManager.ConnectionStrings["MIKROBAR"].ConnectionString;
                //    if (!string.IsNullOrEmpty(MikrobarApp.ConnectionString) && MikrobarApp.ConnectionString.IndexOf("XpoProvider") != -1)
                //    {
                //        int start = MikrobarApp.ConnectionString.IndexOf(';') + 1;
                //        int lenght = MikrobarApp.ConnectionString.Length - start;
                //        MikrobarApp.ConnectionString = MikrobarApp.ConnectionString.Substring(start, lenght);
                //    }
                //}
                //else
                //    throw new MikrobarException("MIKROBAR Baglanti bilgisi on tanimli olmalidir!", 96, "Global");

                //if (ConfigurationManager.AppSettings["AppServerInfo"] != null)
                //{
                //    MikrobarApp.AppServerInfo = ConfigurationManager.AppSettings["AppServerInfo"];
                //}
                //else
                //    Trace.WriteLine("\"AppServerInfo\" Tanımlı degil!");

                //if (ConfigurationManager.AppSettings["WebServUrl"] != null)
                //{
                //    MikrobarApp.WebServUrl = ConfigurationManager.AppSettings["WebServUrl"];
                //}
                //else
                //    Trace.WriteLine("\"WebServUrl\" Tanımlı degil!");

                //if (ConfigurationManager.AppSettings["PrintServer"] != null)
                //{
                //    MikrobarApp.PrintServer = ConfigurationManager.AppSettings["PrintServer"];
                //}
                //else
                //    throw new MikrobarException("PrintServer Baglanti bilgisi on tanimli olmalidir!", 97, "Global");

                //if (ConfigurationManager.AppSettings["DokumanUrl"] != null)
                //{
                //    MikrobarApp.DokumanUrl = ConfigurationManager.AppSettings["DokumanUrl"];
                //}
                //else
                //    Trace.WriteLine("\"DokumanUrl\" Tanımlı degil!");
                #endregion

                string ConnStr = ConfigurationManager.ConnectionStrings["MIKROBAR"].ConnectionString;
                DevExpress.Xpo.Session session = new DevExpress.Xpo.Session(XpoDefault.DataLayer);
                HttpContext.Current.Session["session"] = session;
                Application["UserCount"] = Convert.ToInt32(Application["UserCount"]) + 1;
            }
            catch (Exception exc)
            {
                System.Diagnostics.Trace.WriteLine("-----------------Global.Session_Start------------------------------>>!");
                System.Diagnostics.Trace.WriteLine(exc.Message);
                System.Diagnostics.Trace.WriteLine("----------------------------------------------->>!");
                System.Diagnostics.Trace.WriteLine(exc.StackTrace);
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}