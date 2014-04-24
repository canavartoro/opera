using System;
using System.Xml.Serialization;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace Mikrobar.Module.BusinessObjects
{
    [OptimisticLocking(false), DeferredDeletion(false), XafDefaultProperty("OlculenDeger")]
    [DefaultClassOptions,NavigationItem(false)]
    public class OlcumDegerleri : XPObject
    { 
        [VisibleInDetailView(false), VisibleInListView(false)]
        public int KontrolGrupId { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public int KontrolNoktasiId { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public string KontrolNoktasiKodu { get; set; }

        public string KontrolNoktasiAciklama { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public string Aciklama { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public int EkipmanId { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public string EkipmanKodu { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public string EkipmanAciklamasi { get; set; }
         
        public decimal MaxDeger { get; set; } 
        public decimal MinDeger { get; set; }  
        public decimal OlculenDeger { get; set; }
        public decimal NominalDeger { get; set; }
        public OlcumOnayRed OnayRed { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public string NitelOlcumDeger { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public string NitelOlcumDeger2 { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public int NitelOlcumDegerId { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public int NitelOlcumDegerId2 { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public string Ornek { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public int OrnekNo { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public int BirimId { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public string BirimKodu { get; set; }

        [XmlIgnore(), Association("OlcumDegeri_Emir.Olcumler"), Persistent("SurecEmriId")]
        public SurecEmirleri SurecEmri { get; set; }

        [PersistentAlias("Iif(SurecEmri is null, 0, SurecEmri.Oid)")]
        public int SurecEmriId { get { return Convert.ToInt32(EvaluateAlias("SurecEmriId")); } }

        public OlcumDegerleri() { }
        public OlcumDegerleri(Session session) : base(session) { }
    }
}
