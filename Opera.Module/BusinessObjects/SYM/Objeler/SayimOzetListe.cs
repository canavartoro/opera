using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace MainDemo.Module.BusinessObjects
{

    public class SayimOzetListe
    {
        public string Birim { get; set; }
        public decimal OkunanMiktar { get; set; }
        public string MalzemeKod { get; set; }
        public string MalzemeAd { get; set; }
        public int SayimEmriId { get; set; }
        public int KullaniciId { get; set; }
        public string DepoKod { get; set; }
        public string RafKod { get; set; }
        public KayitDurumu Durum { get; set; }
        public SayimIslemTip IslemTip { get; set; }
    }
}