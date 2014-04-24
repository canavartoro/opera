using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace Mikrobar.Module.BusinessObjects
{

    [NonPersistent]
    public class V_SiparisEk : XPLiteObject
    {
        public DateTime TeslimTarihi { get; set; }
        public decimal KalanMiktar { get; set; }
        public string SiparisNo { get; set; }                                        
        public string DepoKod { get; set; }
               
        public int SiparisId { get; set; }

        [Key]        
        public int SiparisDetayId { get; set; }
        
        public V_SiparisEk(Session session) : base(session) { }
        public V_SiparisEk() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }


}
