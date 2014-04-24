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
    [NonPersistent,DeferredDeletion(false), OptimisticLocking(false), DefaultClassOptions, XafDefaultProperty("YaziciAd"),
    NavigationItem(false), ImageName("BO_Role")]
    public class Yazicilar : XPLiteObject
    {
        public string YaziciAd { get; set; }


        public Yazicilar() { }
        public Yazicilar(Session session) : base(session) { }

    }

}
