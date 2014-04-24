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
    [Persistent("V_UretimQuery01")]//[NonPersistent]
    [ImageName("BO_Organization"), ModelDefault("DefaultListViewShowAutoFilterRow", "True"), NavigationItem(false),
XafDefaultProperty("UretimId"), XafDisplayName("Uretim Detaylari")]
    public class V_UretimQuery01 : XPLiteObject
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

        //[XmlIgnore()]
        //public string Aciklama2 { get; set; }
        [XmlIgnore()]
        public int UretimDurum { get; set; }

        public int VardiyaId { get; set; }
        public string VardiyaKod { get; set; }
        public string VardiyaAciklama { get; set; }
        public string OperasyonAd { get; set; }
        public int CariId { get; set; }
        public string CariKod { get; set; }
        public string CariAd { get; set; }

        public int AmbalajId { get; set; }
        public string Barkod { get; set; }

        public V_UretimQuery01() { }
        public V_UretimQuery01(Session session) : base(session) { }
    }
}

/* sql


ALTER VIEW [dbo].[V_UretimQuery01]
AS
SELECT     dbo.UretimOperasyonlari.OID AS UretimId, dbo.UretimOperasyonlari.IsEmriId, dbo.UretimOperasyonlari.IsEmriNo, 
                      dbo.UretimOperasyonlari.IstasyonId, dbo.UretimOperasyonlari.IstasyonKod, dbo.UretimOperasyonlari.MalzemeId, 
                      dbo.UretimOperasyonlari.OperasyonId, dbo.UretimOperasyonlari.OperasyonNo, dbo.UretimOperasyonlari.OperasyonKod, 
                      dbo.UretimOperasyonlari.BirimId, dbo.UretimOperasyonlari.Birim, dbo.UretimOperasyonlari.Miktar, dbo.UretimOperasyonlari.NetMiktar, 
                      dbo.UretimOperasyonlari.FireMiktari, dbo.f_UretilenMiktar(dbo.UretimOperasyonlari.IsEmriId, dbo.UretimOperasyonlari.OperasyonId) AS UretilenMiktar,
                       ISNULL(dbo.IsEmirleri.PlanlananMiktar, 0) AS PlanlananMiktar, dbo.UretimOperasyonlari.BaslangicTarihi, dbo.UretimOperasyonlari.BitisTarihi, 
                      dbo.UretimOperasyonlari.UretimSuresi, dbo.UretimOperasyonlari.NetUretimSuresi, dbo.UretimOperasyonlari.KatSayi1, 
                      dbo.UretimOperasyonlari.KatSayi2, dbo.UretimOperasyonlari.Aciklama, dbo.UretimOperasyonlari.Aciklama2, 
                      dbo.UretimOperasyonlari.Durum AS UretimDurum, dbo.Vardiyalar.VardiyaId, dbo.Vardiyalar.VardiyaKod, dbo.Vardiyalar.Aciklama AS VardiyaAciklama, 
                      dbo.UretimOperasyonlari.Ambalaj AS AmbalajId, dbo.UretimOperasyonlari.IsEmriDetayId, dbo.Malzemeler.MalzemeKod, dbo.Malzemeler.MalzemeAd, 
                      dbo.Operasyonlar.OperasyonAd, dbo.Ambalajlar.Barkod, ISNULL(dbo.Cariler.CariKod, '') AS CariKod, ISNULL(dbo.Cariler.CariAd, '') AS CariAd, 
                      dbo.Cariler.CariId
FROM         dbo.UretimOperasyonlari WITH (NOLOCK) LEFT OUTER JOIN
                      dbo.Cariler WITH (NOLOCK) ON dbo.UretimOperasyonlari.CariId = dbo.Cariler.CariId LEFT OUTER JOIN
                      dbo.Operasyonlar WITH (NOLOCK) ON dbo.UretimOperasyonlari.OperasyonId = dbo.Operasyonlar.OperasyonId LEFT OUTER JOIN
                      dbo.IsEmirleri WITH (NOLOCK) ON dbo.UretimOperasyonlari.IsEmriId = dbo.IsEmirleri.IsEmriId LEFT OUTER JOIN
                      dbo.Malzemeler WITH (NOLOCK) ON dbo.UretimOperasyonlari.MalzemeId = dbo.Malzemeler.MalzemeId LEFT OUTER JOIN
                      dbo.Vardiyalar WITH (NOLOCK) ON dbo.UretimOperasyonlari.Vardiya = dbo.Vardiyalar.VardiyaId LEFT OUTER JOIN
                      dbo.Ambalajlar WITH (NOLOCK) ON dbo.UretimOperasyonlari.Ambalaj = dbo.Ambalajlar.OID
WHERE     (dbo.UretimOperasyonlari.Durum = 0)




 */


/*
 Oracle
 * 
 CREATE OR REPLACE VIEW barset."V_UretimQuery01" AS
SELECT     "UretimOperasyonlari"."OID" AS "UretimId", "UretimOperasyonlari"."IsEmriId", "UretimOperasyonlari"."IsEmriNo", "UretimOperasyonlari"."IstasyonId", "UretimOperasyonlari"."IstasyonKod",
                      "UretimOperasyonlari"."MalzemeId", "UretimOperasyonlari"."OperasyonId", "UretimOperasyonlari"."OperasyonNo", "UretimOperasyonlari"."OperasyonKod", 
                      "UretimOperasyonlari"."BirimId", "UretimOperasyonlari"."Birim", "UretimOperasyonlari"."Miktar", "UretimOperasyonlari"."NetMiktar", 
                      "UretimOperasyonlari"."FireMiktari", F_URETILENMIKTAR("UretimOperasyonlari"."IsEmriId", "UretimOperasyonlari"."OperasyonId") AS "UretilenMiktar", 
                      NVL("IsEmirleri"."PlanlananMiktar", 0) AS "PlanlananMiktar", "UretimOperasyonlari"."BaslangicTarihi", "UretimOperasyonlari"."BitisTarihi", 
                      "UretimOperasyonlari"."UretimSuresi", "UretimOperasyonlari"."NetUretimSuresi", "UretimOperasyonlari"."KatSayi1", "UretimOperasyonlari"."KatSayi2", 
                      "UretimOperasyonlari"."Aciklama", "UretimOperasyonlari"."Aciklama2", "UretimOperasyonlari"."Durum" AS "UretimDurum", "Vardiyalar"."VardiyaId", 
                      "Vardiyalar"."VardiyaKod", "Vardiyalar"."Aciklama" AS "VardiyaAciklama", "UretimOperasyonlari"."Ambalaj" AS "AmbalajId", "UretimOperasyonlari"."IsEmriDetayId", 
                      "Malzemeler"."MalzemeKod", "Malzemeler"."MalzemeAd", "Operasyonlar"."OperasyonAd"
FROM         "UretimOperasyonlari" LEFT OUTER JOIN
                      "Operasyonlar" ON "UretimOperasyonlari"."OperasyonId" = "Operasyonlar"."OperasyonId" LEFT OUTER JOIN
                      "IsEmirleri" ON "UretimOperasyonlari"."IsEmriId" = "IsEmirleri"."IsEmriId" LEFT OUTER JOIN
                      "Malzemeler" ON "UretimOperasyonlari"."MalzemeId" = "Malzemeler"."MalzemeId" LEFT OUTER JOIN
                      "Vardiyalar" ON "UretimOperasyonlari"."Vardiya" = "Vardiyalar"."VardiyaId"
WHERE     ("UretimOperasyonlari"."Durum" = 0)
 */