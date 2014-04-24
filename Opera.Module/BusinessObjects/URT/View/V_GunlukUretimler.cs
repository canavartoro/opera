using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{    
    public struct V_GunlukUretimlerKey
    {
        [Persistent("MalzemeKod"), Browsable(false)]
        public string MalzemeKod;
        [Persistent("MalzemeAd"), Browsable(false)]
        public string MalzemeAd;
        [Persistent("Miktar"), Browsable(false)]
        public decimal Miktar;
        [Persistent("Aylik"), Browsable(false)]
        public decimal Aylik;
    }
    [ModelDefault("AllowNew", "false"), ModelDefault("AllowEdit", "false"),
    ModelDefault("Caption", "Gunluk Uretim Bilgisi"),
    ModelDefault("AllowDelete", "false"), ModelDefault("AllowAdd", "false")]
    [DefaultClassOptions, Persistent("V_GunlukUretimler"), NavigationItem(false), ImageName("BO_Organization")]
    public class V_GunlukUretimler : XPLiteObject
    {
        public V_GunlukUretimler(Session session) : base(session) { }

        [Key, Persistent, Browsable(false)]
        public V_GunlukUretimlerKey Key;
        public string MalzemeKod { get { return Key.MalzemeKod; } }
        public string MalzemeAd { get { return Key.MalzemeAd; } }
        [ModelDefault("DisplayFormat", "{0:0.000}")]
        public decimal Miktar { get { return Key.Miktar; } }
        [ModelDefault("DisplayFormat", "{0:0.000}")]
        public decimal Aylik { get { return Key.Aylik; } }
    }
}
