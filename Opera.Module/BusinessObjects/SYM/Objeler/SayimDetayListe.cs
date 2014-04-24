using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace Mikrobar.Module.BusinessObjects
{
    [NonPersistent]
    public class SayimDetayListe : XPBaseObject
    {
        public string MalzemeKod { get; set; }
        public string MalzemeAd { get; set; }
        public decimal OkunanMiktar { get; set; }
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public string Birim2 { get; set; }
        public int SayimEmri { get; set; }
        public int KullaniciId { get; set; }
        public string DepoKod { get; set; }
        public string RafKod { get; set; }
        public string Barkod { get; set; }
        public KayitDurumu Durum { get; set; }
        public SayimIslemTip IslemTip { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public int Oid { get; set; }
        public string LotKod { get; set; }
    }
}
