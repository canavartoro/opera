using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.DC;
using System.Xml.Serialization;

namespace Mikrobar.Module.BusinessObjects
{
    [NonPersistent,DeferredDeletion(false), OptimisticLocking(false), DefaultClassOptions, XafDefaultProperty("Ad"),
    NavigationItem(false), ImageName("BO_Role")]
    public class Tasarimlar : XPLiteObject
    {
        public string Ad { get; set; }

        //#region Ortak Alanlar
        //[ModelDefault("AllowEdit", "false"), ReadOnly(true), XmlIgnore()]
        //public int Olusturan { get; set; }
        //[ModelDefault("AllowEdit", "false"), ReadOnly(true), XmlIgnore()]
        //public DateTime OlusturmaTarihi { get; set; }
        //[ModelDefault("AllowEdit", "false"), ReadOnly(true), XmlIgnore()]
        //public int Guncelleyen { get; set; }
        //[ModelDefault("AllowEdit", "false"), ReadOnly(true), XmlIgnore()]
        //public DateTime GuncellemeTarihi { get; set; }
        //[Size(DbSize.ModulLenght), ModelDefault("AllowEdit", "false"), ReadOnly(true), XmlIgnore()]
        //public string KaynakModul { get; set; }
        //[Description("Kaydýn oluþtuðu uygulama"), XmlIgnore()]
        //public KaynakProgram KaynakProgram { get; set; }
        //[Size(DbSize.CihazNoLenght), ModelDefault("AllowEdit", "false"), Browsable(false), ReadOnly(true), XmlIgnore()]
        //public string CihazNo { get; set; }
        //[ModelDefault("AllowEdit", "false"), ReadOnly(true), XmlIgnore()]
        //public bool Entegre { get; set; }
        //public KayitDurumu Durum { get; set; }
        //#endregion

        public Tasarimlar() { }
        public Tasarimlar(Session session) : base(session) { }

    }

}
