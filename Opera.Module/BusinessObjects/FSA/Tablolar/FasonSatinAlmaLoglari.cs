using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;

namespace Mikrobar.Module.BusinessObjects
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [DeferredDeletion(false), OptimisticLocking(false), DefaultClassOptions,
    XafDefaultProperty("Oid"), NavigationItem(false), ImageName("BO_Attention")]
    public class FasonSatinAlmaLoglari : MikrobarLoglari
    {
        public FasonSatinAlmaLoglari() { }
        public FasonSatinAlmaLoglari(Session session) : base(session) { }
    }
}
