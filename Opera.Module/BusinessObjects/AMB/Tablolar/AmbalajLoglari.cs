using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;

namespace Mikrobar.Module.BusinessObjects
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [DeferredDeletion(false), OptimisticLocking(false), DefaultClassOptions,
    XafDefaultProperty("Oid"), NavigationItem(false), ImageName("BO_Attention")]
    public class AmbalajLoglari : MikrobarLoglari
    {
        public AmbalajLoglari() { }
        public AmbalajLoglari(Session session) : base(session) { }
    }
}
