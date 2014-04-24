using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opera
{
    public class Utillity
    {
        public static string APP_PATH = "";

        public static DevExpress.Xpo.Session Session
        {
            get
            {
                if (HttpContext.Current == null) return null;
                if (HttpContext.Current.Session == null) return null;

                return (HttpContext.Current.Session["session"] as DevExpress.Xpo.Session);
            }

        }

        public static string SessionId
        {
            get
            {
                if (HttpContext.Current == null) return string.Empty;
                if (HttpContext.Current.Session == null) return string.Empty;
                return HttpContext.Current.Session.SessionID;
            }
        }

        public static string AppsPath
        {
            get
            {
                try
                {
                    if (!string.IsNullOrEmpty(APP_PATH))
                        return string.Format("{0}Apps\\", APP_PATH);

                    return HttpContext.Current.Server.MapPath("../") + "Apps\\";
                }
                catch (Exception exc)
                {
                    System.Diagnostics.Trace.WriteLine("Mikrobar.Utility.AppsPath");
                    System.Diagnostics.Trace.WriteLine(exc.Message);
                    System.Diagnostics.Trace.WriteLine(exc.StackTrace);
                    System.Diagnostics.Trace.WriteLine("");
                }
                return "";
            }
        }
    }
}