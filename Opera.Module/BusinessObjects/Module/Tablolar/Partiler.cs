using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mikrobar.Module.BusinessObjects
{
    [NonPersistent]
    public class Partiler
    {
        public int LotId { get; set; }
        public string LotKod { get; set; }
        public int MalzemeId { get; set; }
        public string MalzemeKod { get; set; }
        public string MalzemeAd { get; set; }
        public DateTime BaslangicTarih { get; set; }
        public DateTime BitisTarih { get; set; }
        public decimal MinumumMiktar { get; set; }
        public decimal MaksimumMiktar { get; set; }
        public bool Pasif { get; set; }
        public string Aciklama { get; set; }
        public string Aciklama2 { get; set; }
        public string Aciklama3 { get; set; }
    }
}
