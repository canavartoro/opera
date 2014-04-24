using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Xml.Serialization;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [DeferredDeletion(false), OptimisticLocking(false), DefaultClassOptions, ModelDefault("DefaultListViewShowAutoFilterRow", "True"),
    XafDefaultProperty("Oid"), NavigationItem(false), ImageName("BO_Attention")]
    public class SistemLoglari : MikrobarLoglari
    {
        public SistemLoglari() { }
        public SistemLoglari(Session session) : base(session) { }

    }

}
