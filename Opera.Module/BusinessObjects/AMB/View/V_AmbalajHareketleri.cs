using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace Mikrobar.Module.BusinessObjects
{
    public class V_AmbalajHareketleri : XPLiteObject
    {
        public int OID { get; set; }
        [Key]
        public string Barkod { get; set; }
        public string DepoKod { get; set; }
        [DbType(" DECIMAL(18,4) ")]
        public decimal Miktar { get; set; }
        [DbType(" DECIMAL(18,4) ")]
        public decimal Miktar2 { get; set; }
        public string Birim { get; set; }
        public string Birim2 { get; set; }
        public string MalzemeKod { get; set; }
        public string MalzemeAd { get; set; }
        public bool Durum { get; set; }

        public V_AmbalajHareketleri() { }
        public V_AmbalajHareketleri(Session session) : base(session) { }
    }
}
