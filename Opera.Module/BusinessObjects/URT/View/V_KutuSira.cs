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
    [Persistent("V_KutuSira")]//[NonPersistent]
    [ImageName("BO_Organization"), ModelDefault("DefaultListViewShowAutoFilterRow", "True"), NavigationItem(false),
XafDefaultProperty("Barkod"), XafDisplayName("Kutu Siralar")]
    public class V_KutuSira : XPLiteObject
    {
        /* A.Barkod, M.MalzemeKod, M.MalzemeAd, 
         * U.Birim, SUM(U.Miktar) AS Miktar2, 
         * SUM(U.Miktar) AS Miktar, SUM(D.Kalan) AS Toplam2, SUM(D.Kalan) AS Toplam,
         * MAX(U.BitisTarihi) AS Tarih, 
                      MAX(U.BitisTarihi) AS Tarih2, MAX(U.BitisTarihi) AS Zaman, ISNULL(A.PartiNo, '') AS PartiNo, 
         * ISNULL(A.PaletSiraNo, 0) AS PaletSiraNo, ISNULL(C.CariKod, '') AS EXPR1, ISNULL(C.CariAd, '') 
                      AS CariAd, ISNULL(L.MusteriKod, '') AS MusteriKod, ISNULL(L.MusteriAd, '') AS MusteriAd, ISNULL(K.KullaniciKod, '') AS PersonelKod, ISNULL(K.KullaniciAd, '') AS PersonelAd, ISNULL(U.EkAlan1, 
                      '999 KG') AS EXPR2, ISNULL(U.EkAlan2, '999 KG') AS EXPR3, ISNULL(U.EkAlan3, '999 KG') AS EXPR4, ISNULL(U.EkAlan4, '999 KG') AS EXPR5, MIN(U.OID) AS UretimId, U.Ambalaj AS AmbalajId, 
                      ISNULL(C.CariId, 0) AS EXPR6, U.MalzemeId, U.KutuSiraNo*/
        [Key]
        public int UretimId { get; set; }
        public int KutuSiraNo { get; set; }
        public string Barkod { get; set; }
        public int MalzemeId { get; set; }
        public string MalzemeKod { get; set; }
        public string MalzemeAd { get; set; }
        public string Birim { get; set; }

        public decimal Miktar2 { get; set; }
        public decimal Miktar { get; set; }
        public decimal Toplam2 { get; set; }
        public decimal Toplam { get; set; }

        public string EkAlan1 { get; set; }
        public string EkAlan2 { get; set; }
        public string EkAlan3 { get; set; }
        public string EkAlan4 { get; set; }
        //public int TasarimGrupId { get; set; }

        public int CariId { get; set; }
        public string CariKod { get; set; }
        public string CariAd { get; set; }
        public int AmbalajId { get; set; }
        public int PaletSiraNo{ get; set; }

        //public int IstasyonId { get; set; }
        //public string IstasyonKod { get; set; }
       // public int OperasyonId { get; set; }
       // public int OperasyonNo { get; set; }
       // public string OperasyonKod { get; set; }
       // public int BirimId { get; set; }
        public string PartiNo { get; set; }

        public string PersonelKod { get; set; }
        public string PersonelAd{ get; set; }
        
        
        

        public V_KutuSira() { }
        public V_KutuSira(Session session) : base(session) { }
    }
}

/* sql


ALTER VIEW [dbo].[V_KutuSira]
AS
SELECT     A.Barkod, M.MalzemeKod, M.MalzemeAd, U.Birim, SUM(U.Miktar) AS Miktar2, SUM(U.Miktar) AS Miktar, SUM(D.Kalan) AS Toplam2, SUM(D.Kalan) AS Toplam, MAX(U.BitisTarihi) AS Tarih, 
                      MAX(U.BitisTarihi) AS Tarih2, MAX(U.BitisTarihi) AS Zaman, ISNULL(A.PartiNo, '') AS PartiNo, ISNULL(A.PaletSiraNo, 0) AS PaletSiraNo, ISNULL(C.CariKod, '') AS EXPR1, ISNULL(C.CariAd, '') 
                      AS CariAd, ISNULL(L.MusteriKod, '') AS MusteriKod, ISNULL(L.MusteriAd, '') AS MusteriAd, ISNULL(K.KullaniciKod, '') AS PersonelKod, ISNULL(K.KullaniciAd, '') AS PersonelAd, ISNULL(U.EkAlan1, 
                      '999 KG') AS EXPR2, ISNULL(U.EkAlan2, '999 KG') AS EXPR3, ISNULL(U.EkAlan3, '999 KG') AS EXPR4, ISNULL(U.EkAlan4, '999 KG') AS EXPR5, MIN(U.OID) AS UretimId, U.Ambalaj AS AmbalajId, 
                      ISNULL(C.CariId, 0) AS EXPR6, U.MalzemeId, U.KutuSiraNo
FROM         dbo.Ambalajlar AS A WITH (NOLOCK) INNER JOIN
                      dbo.UretimOperasyonlari AS U WITH (NOLOCK) ON A.OID = U.Ambalaj LEFT OUTER JOIN
                      dbo.Malzemeler AS M WITH (NOLOCK) ON U.MalzemeId = M.MalzemeId LEFT OUTER JOIN
                      dbo.AmbalajDetaylari AS D WITH (NOLOCK) ON A.OID = D.Ambalaj LEFT OUTER JOIN
                      dbo.MusteriMalzemeleri AS L WITH (NOLOCK) ON U.MalzemeId = L.MalzemeId AND L.CariId = U.CariId LEFT OUTER JOIN
                      dbo.Kullanicilar AS K WITH (NOLOCK) ON U.Olusturan = K.KullaniciId LEFT OUTER JOIN
                      dbo.Cariler AS C WITH (NOLOCK) ON U.CariId = C.CariId
GROUP BY C.CariId, U.Ambalaj, A.Barkod, U.MalzemeId, M.MalzemeKod, M.MalzemeAd, U.Birim, A.PartiNo, A.PaletSiraNo, C.CariKod, C.CariAd, L.MusteriKod, L.MusteriAd, K.KullaniciKod, K.KullaniciAd, 
                      U.EkAlan1, U.EkAlan2, U.EkAlan3, U.EkAlan4, U.KutuSiraNo

GO




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