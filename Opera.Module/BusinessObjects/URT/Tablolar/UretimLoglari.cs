using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Xml.Serialization;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;

namespace Mikrobar.Module.BusinessObjects
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [DeferredDeletion(false), OptimisticLocking(false), DefaultClassOptions, 
    XafDefaultProperty("Oid"), NavigationItem(false), ImageName("BO_Attention")]
    public class UretimLoglari : MikrobarLoglari
    {
        public UretimLoglari() { }
        public UretimLoglari(Session session) : base(session) { }
    }
}
