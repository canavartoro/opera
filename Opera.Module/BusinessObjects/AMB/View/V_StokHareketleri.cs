using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace Mikrobar.Module.BusinessObjects
{
    public class V_StokHareketleri : XPLiteObject
    {
        public int OID { get; set; }
        public int DepoId { get; set; }
        public string DepoKod { get; set; }
        [DbType(" DECIMAL(18,4) ")]
        public decimal Miktar { get; set; }
        [DbType(" DECIMAL(18,4) ")]
        public decimal Miktar2 { get; set; }
        public string Birim { get; set; }
        public string Birim2 { get; set; }
        [Key]
        public int MalzemeId { get; set; }
        public string MalzemeKod { get; set; }
        public int BirimId { get; set; }
        public int Birim2Id { get; set; }

        public V_StokHareketleri() { }
        public V_StokHareketleri(Session session) : base(session) { }
    }
}
