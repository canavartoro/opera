using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;

namespace Mikrobar.Module.BusinessObjects
{

    [NonPersistent]
    public class V_SiparisOkunanlar : XPLiteObject
    {
        #region Ithalat
        public int Id { get; set; }
        public int YuklemeId { get; set; }
        public string YuklemeNo { get; set; }
        public int DosyaId { get; set; }
        public string DosyaNo { get; set; }
        #endregion

        public string CariMalzemeKod { get; set; }
        public string MalzemeKod { get; set; }
        public string DepoKod { get; set; }
        
        public decimal Miktar { get; set; }
        public decimal Okunan { get; set; }
        public decimal Kalan { get; set; }
        public decimal Gonderilen { get; set; } //=> Siparişden Sevkiyat için Önceden gönderilen miktarı tutmak için eklenmiştir (Gökhan)
        public string BirimKod { get; set; } 

        public string MalzemeAd { get; set; }                                        
        public string OzelKod { get; set; }
        public string SiparisNo { get; set; }        
        public int SiparisId { get; set; }

        [Key]        
        public int SiparisDetayId { get; set; }
        public int CariId { get; set; }
        public string CariKod { get; set; }
        public string CariAd { get; set; }
        public int BirimId { get; set; }
        public int MalzemeId { get; set; }
        public int SiraNo { get; set; }
        public int DepoId { get; set; }
        public int HizmetKartId { get; set; }
        public int SatirTipi { get; set; }
        public DateTime TeslimTarihi { get; set; }

        [Description("Cikilan kasa ")]
        public List<OkunanKasa> OkunanKasalar { get; set; }

        public int IsEmriId { get; set; }
        public string IsEmriNo { get; set; }
        public int IsEmriDetayId { get; set; }
        public int OperasyonId { get; set; }
        public int OperasyonNo { get; set; }
        public decimal FazlaSipMiktar { get; set; }

        public V_SiparisOkunanlar(Session session) : base(session) { }
        public V_SiparisOkunanlar() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

    public struct OkunanKasa
    {
        public int AmbalajId { get; set; }
        public decimal CikilanMiktar { get; set; }
    }


}
