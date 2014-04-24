using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace Mikrobar.Module.BusinessObjects
{
    public class V_AmbalajHareketleriOzet : XPLiteObject
    {
        public int OID { get; set; }
        [Key]
        public string MalzemeKod { get; set; }
        public string Birim { get; set; }
        [DbType(" DECIMAL(18,4) ")]
        public decimal Miktar { get; set; }

        public V_AmbalajHareketleriOzet() { }
        public V_AmbalajHareketleriOzet(Session session) : base(session) { }

    }
}
