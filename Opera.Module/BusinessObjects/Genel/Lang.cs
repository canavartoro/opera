using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Threading;
using Mikrobar.Module.BusinessObjects;

namespace Mikrobar
{
    public class Lang
    {
        public Lang()
        { 
        }

        private static CultureInfo _Culture;
        private static CultureInfo _Culturetr;
        private static IniFile _iniFileTr;

        public static CultureInfo CultureInfoTr
        {
            get
            {
                if (_Culturetr == null)
                {
                    _Culturetr = CultureInfo.GetCultureInfo("tr-TR");
                }
                return _Culturetr;
            }

        }

        public static CultureInfo CultureInfoEn
        {
            get { return _Culture; }
        }

        public static IniFile IniFileTr
        {
            get
            {
                if (_iniFileTr == null)
                {
                    _iniFileTr = new IniFile("Mesajlar.ini");
                }
                return _iniFileTr;
            }
        }

        public static string Mesaj(Mesaj msj)
        {
            try
            {
                if (object.ReferenceEquals(msj, null)) return "";

            return IniFileTr.Read(msj.Kategori, msj.ToString(), msj.HataMesaj);   
            }
            catch (Exception exc)
            {
                ////SistemLog.Instance.HataLog(1011, 62, "Lang/Mesaj", exc.Message, "");
            }
            return "Mesaj cozulemedi! (2) ";
        }

        public static string Mesaj(Mesaj msj, params object[] args)
        {            
            try
            {
                if (object.ReferenceEquals(msj, null)) return "";

            string strMesaj = IniFileTr.Read(msj.Kategori, msj.ToString(), msj.HataMesaj);
            if (!object.ReferenceEquals(null, args))
                return string.Format(strMesaj, args);
            else
                return strMesaj;
            }
            catch (Exception exc)
            {
                //SistemLog.Instance.HataLog(1011, 72, "Lang/Mesaj", exc.Message, "");
            }
            return "Mesaj cozulemedi! ";
        }

        public static void LoadLanguage()
        {
            _Culture = new System.Globalization.CultureInfo("en-US");

            Thread.CurrentThread.CurrentUICulture = _Culture;
            Thread.CurrentThread.CurrentCulture = _Culture;

            //DateTimeFormatInfo info = _Culture.DateTimeFormat;
            //int[] ARR = { 3, 2, 2 };

            //System.Globalization.DateTimeFormatInfo dateTimeInfo = _Culture.DateTimeFormat;
            //System.Globalization.NumberFormatInfo NumberInfo = new System.Globalization.NumberFormatInfo();


            //dateTimeInfo.DateSeparator = ".";
            //dateTimeInfo.LongDatePattern = "dd.MM.yyyy HH:mm:ss";
            //dateTimeInfo.ShortDatePattern = "dd.MM.yyyy";
            //dateTimeInfo.LongTimePattern = "HH:mm:ss";
            //dateTimeInfo.ShortTimePattern = "HH:mm";
            //NumberInfo.CurrencySymbol = "Tl";
            //NumberInfo.CurrencyDecimalDigits = 2;
            //NumberInfo.CurrencyDecimalSeparator = ",";
            //NumberInfo.CurrencyGroupSizes = ARR;
            //NumberInfo.CurrencyGroupSeparator = ".";
            //NumberInfo.PositiveInfinitySymbol = " ";
            ////dateTimeInfo.SetAllDateTimePatterns = "dd/MM/yyyy,hh:mm:ss tt";
            //_Culture.DateTimeFormat = dateTimeInfo;
            //_Culture.NumberFormat = NumberInfo;            

            /*System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            ci.NumberFormat.NumberDecimalSeparator = ",";

            Thread.CurrentThread.CurrentCulture = ci;*/
            //CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            //if (currentCulture.Name == "tr-TR")
            //{
            //    _Culture = new CultureInfo("en-US");
                
            //    Thread.CurrentThread.CurrentUICulture = _Culture;
            //    Thread.CurrentThread.CurrentCulture = _Culture;
            //}   
        }
    }
}
