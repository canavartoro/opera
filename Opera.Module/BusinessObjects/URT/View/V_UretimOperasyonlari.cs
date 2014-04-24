using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [NonPersistent]
    [ImageName("BO_Organization"), ModelDefault("DefaultListViewShowAutoFilterRow", "True"), NavigationItem(false),
    XafDefaultProperty("UretimId"), XafDisplayName("Uretim Detaylari")]
    public class V_UretimOperasyonlari : XPLiteObject
    {
        [Key]
        public int UretimId { get; set; }
        public int IsEmriId { get; set; }
        public string MalzemeKod { get; set; } 
        public string MalzemeAd { get; set; }
        public int IsEmriDetayId { get; set; }
        public string IsEmriNo { get; set; }
        public int IstasyonId { get; set; }
        public string IstasyonKod { get; set; }
        public int MalzemeId { get; set; }
        public int OperasyonId { get; set; }
        public int OperasyonNo { get; set; }
        public string OperasyonKod { get; set; }
        public int BirimId { get; set; }
        public string Birim { get; set; }
        public decimal Miktar { get; set; }
        [XmlIgnore()]
        public decimal NetMiktar { get; set; }
        public decimal FireMiktari { get; set; }
        public decimal PlanlananMiktar { get; set; }
        public decimal UretilenMiktar { get; set; }
        public decimal AmbalajMiktar { get; set; }
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}")]
        public DateTime BaslangicTarihi { get; set; }
        [XmlIgnore()]
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}")]
        public DateTime BitisTarihi { get; set; }
        [XmlIgnore()]
        public decimal UretimSuresi { get; set; }
        [XmlIgnore()]
        public decimal NetUretimSuresi { get; set; }
        public decimal KatSayi1 { get; set; }
        public decimal KatSayi2 { get; set; }
        public string Aciklama { get; set; }
       
        public string Aciklama2 { get; set; }
        public string Aciklama3 { get; set; }
        public string Aciklama4 { get; set; }
        public string Aciklama5 { get; set; }

        public string EkAlan1 { get; set; }
        public string EkAlan2 { get; set; }
        public string EkAlan3 { get; set; }
        public string EkAlan4 { get; set; }

        public int TasarimGrupId { get; set; }

        [XmlIgnore()]
        public KayitDurumu UretimDurum { get; set; }

        public int VardiyaId { get; set; }
        public string VardiyaKod { get; set; }
        public string VardiyaAciklama { get; set; }
        public string OperasyonAd { get; set; }
        public int CariId { get; set; }
        public string CariKod { get; set; }
        public string CariAd { get; set; }

        public int AmbalajId { get; set; }
        public string Barkod { get; set; }
        public V_Ambalajlar Ambalaj { get; set; }
        public List<V_UretimDuruslari> UretimDuruslari { get; set; }
        public List<UretimMalzemeleri> UretimMalzemeleri { get; set; }
        
        public V_UretimOperasyonlari() { }
        public V_UretimOperasyonlari(Session session) : base(session) { }
    }
}
