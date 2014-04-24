using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace Mikrobar.Module.BusinessObjects
{
    [NonPersistent]
    public class V_SevkEmirleri : XPLiteObject
    {

        public string SevkEmirNo { get; set; }
        public string CariKod { get; set; }
        public string CariAd { get; set; }
        public string CariMalzemeKod { get; set; }
        public string MalzemeKod { get; set; }     
        public string MalzemeAd { get; set; }           
        public string Birim { get; set; }
        public decimal Miktar { get; set; }
        public decimal Okunan { get; set; }
        public decimal Kalan { get; set; }
        public decimal StokMiktar { get; set; }
        public decimal Gonderilen { get; set; }     

        public string OzelKod { get; set; }
        public string DepoKod { get; set; }
        public string SiparisNo {get; set; }
        public int SevkEmriId { get; set; }

        [Key]
        public int SevkEmriDetayId { get; set; }
        public int CariId { get; set; }
        public int BirimId { get; set; }
        public int MalzemeId { get; set; }
        public int SiraNo { get; set; }
        public int DepoId { get; set; }
        public int HizmetKartId { get; set; }
        public int SatirTipi { get; set; }
        public DateTime TeslimTarih { get; set; }
        public DateTime OlusturmaTarih{ get; set; }
        /*gridden seçmel için*/
        public bool Secim { get; set; }
        public string Aciklama { get; set; }        
        public string PartiNo { get; set; }
        [NonPersistent]
        public int PartiId { get; set; }

        public V_SevkEmirleri(Session session) : base(session) { }
        public V_SevkEmirleri() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
