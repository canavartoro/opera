using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    public class V_TransferRapor : XPLiteObject
    {
        public int OID { get; set; }
        public int AmbalajHareket { get; set; }
        public string Barkod { get; set; }
        [Key]
        public int MalzemeId { get; set; }
        public string MalzemeAd { get; set; }
        public string MalzemeKod { get; set; }
        [DbType(" DECIMAL(18,4) ")]
        public decimal Miktar { get; set; }
        public int BirimId { get; set; }
        public string Birim { get; set; }
        public int RafId { get; set; }
        public int DepoId { get; set; }
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}")]
        public DateTime OlusturmaTarihi { get; set; }
        public bool Durum { get; set; }


        public V_TransferRapor() { }
        public V_TransferRapor(Session session) : base(session) { }
    }
}
