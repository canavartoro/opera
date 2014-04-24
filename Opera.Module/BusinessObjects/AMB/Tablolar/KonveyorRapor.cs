using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Diagnostics;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    public class KonveyorRapor
    {
        public int AmbalajHareket { get; set; }
        public string Birim { get; set; }
        public string MalzemeKod { get; set; }
        public string Barkod { get; set; }
        [DbType(" DECIMAL(18,4) ")]
        public decimal Miktar { get; set; }
        public string HedefDepo { get; set; }
        public string KaynakDepo { get; set; }
        [ModelDefault("AllowEdit", "false"), ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}")]
        public DateTime OlusturmaTarihi { get; set; }
    }
}
