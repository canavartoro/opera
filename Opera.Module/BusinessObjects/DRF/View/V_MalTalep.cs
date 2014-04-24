using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

namespace Mikrobar.Module.BusinessObjects
{
    [NonPersistent, NavigationItem(false)]
    public class V_MalTalep : XPLiteObject
    {
        public int DepoId { get; set; }
        public string DepoKod { get; set; }
        public int HareketId { get; set; }
        public string HareketKod { get; set; }
        public string OnayKod { get; set; }
        [Key]
        public int MalTalepId { get; set; }
        public DateTime SevkTarihi { get; set; }
        public DateTime IrsaliyeTarihi { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public List<V_MalTalepDetay> MalTlpDetay { get; set; }
        public V_MalTalep() { }
        public V_MalTalep(Session session) : base(session) { }


    }
}
