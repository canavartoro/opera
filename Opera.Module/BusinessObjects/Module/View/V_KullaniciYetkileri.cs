using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [NonPersistent, ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    public class V_KullaniciYetkileri : XPLiteObject
    {
        public string KullaniciKod { get; set; }
        public string KullaniciAd { get; set; }
        public string KullaniciSoyad { get; set; }
        public string Departman { get; set; }
        public string GrupKod { get; set; }
        public string GrupAciklama { get; set; }
        public string MenuKod { get; set; }
        public string MenuAd { get; set; }
        public int UstMenuId { get; set; }
        public string Ekran { get; set; }
        public string DllModul { get; set; }
        public bool Giris { get; set; }
        public bool Yazma { get; set; }
        public bool Guncelleme { get; set; }
        public bool Silme { get; set; }
        public int KullaniciId { get; set; }
        public int KullaniciDetaylariId { get; set; }
        public int GrupId { get; set; }
        public int RefId { get; set; }

        [Key]
        public int GrupDetaylariId { get; set; }
        public int MenuId { get; set; }
        public int HareketId { get; set; }
        public int RafId { get; set; }
        public CihazTip CihazTip { get; set; }
        public bool Durum { get; set; }

        public V_KullaniciYetkileri(Session session) : base(session) { }
        public V_KullaniciYetkileri() { }
    }
}


/*  mssql
 * 
 SELECT     TOP (100) PERCENT dbo.Kullanicilar.KullaniciKod, dbo.Kullanicilar.KullaniciAd, dbo.Kullanicilar.Aciklama AS KullaniciSoyad, dbo.Kullanicilar.Departman, 
                      dbo.Gruplar.GrupKod, dbo.Gruplar.GrupAciklama, ISNULL(dbo.Menuler.MenuKod, N'') AS MenuKod, ISNULL(dbo.Menuler.Aciklama, '') AS MenuAd, 
                      ISNULL(dbo.Menuler.UstMenuId, 0) AS UstMenuId, ISNULL(dbo.Menuler.Ekran, '') AS Ekran, dbo.GrupDetaylari.Giris, dbo.GrupDetaylari.Yazma, 
                      dbo.GrupDetaylari.Guncelleme, dbo.GrupDetaylari.Silme, dbo.KullaniciDetaylari.OID AS KullaniciDetaylariId, dbo.Gruplar.OID AS GrupId, 
                      dbo.GrupDetaylari.OID AS GrupDetaylariId, dbo.GrupDetaylari.MenuId, dbo.GrupDetaylari.HareketId, dbo.GrupDetaylari.DepoId AS RafId, dbo.Menuler.CihazTip, 
                      dbo.Menuler.Durum, dbo.Kullanicilar.KullaniciId AS Oid, dbo.Kullanicilar.KullaniciId2 AS RefId, dbo.Kullanicilar.KullaniciId, dbo.Menuler.DllModul
FROM         dbo.Gruplar WITH (NOLOCK) INNER JOIN
                      dbo.KullaniciDetaylari WITH (NOLOCK) ON dbo.Gruplar.OID = dbo.KullaniciDetaylari.GrupId INNER JOIN
                      dbo.GrupDetaylari WITH (NOLOCK) ON dbo.GrupDetaylari.GrupId = dbo.Gruplar.OID INNER JOIN
                      dbo.Kullanicilar WITH (NOLOCK) ON dbo.KullaniciDetaylari.KullaniciId = dbo.Kullanicilar.KullaniciId LEFT OUTER JOIN
                      dbo.Menuler WITH (NOLOCK) ON dbo.GrupDetaylari.MenuId = dbo.Menuler.OID
ORDER BY GrupId
 */


/*oracle
 CREATE OR REPLACE VIEW "V_KullaniciYetkileri" AS
SELECT
"Kullanicilar"."KullaniciKod",
"Kullanicilar"."KullaniciAd",
"Kullanicilar"."Aciklama"  AS "KullaniciSoyad",
"Kullanicilar"."Departman",
"Gruplar"."GrupKod",
"Gruplar"."GrupAciklama",
"Menuler"."MenuKod" AS "MenuKod",
"Menuler"."Aciklama" AS "MenuAd",
"Menuler"."UstMenuId" AS "UstMenuId",
"Menuler"."Ekran" AS "Ekran",
"Menuler"."DllModul",
"GrupDetaylari"."Giris",
"GrupDetaylari"."Yazma",
"GrupDetaylari"."Guncelleme",
"GrupDetaylari"."Silme",
"KullaniciDetaylari"."OID" as "KullaniciDetaylariId",
"Gruplar"."OID" as "GrupId",
"GrupDetaylari"."OID" as "GrupDetaylariId",
"GrupDetaylari"."MenuId",
"GrupDetaylari"."HareketId",
"GrupDetaylari"."DepoId" as "RafId",
"Menuler"."CihazTip",
"Menuler"."Durum",
"Kullanicilar"."KullaniciId" as "Oid",
"Kullanicilar"."KullaniciId",
"Kullanicilar"."KullaniciId2" as "RefId",
"Kullanicilar"."KullaniciId2"
 FROM "Gruplar" INNER JOIN "KullaniciDetaylari"  ON "Gruplar"."OID" = "KullaniciDetaylari"."GrupId"
INNER JOIN "GrupDetaylari"   ON "GrupDetaylari"."GrupId" = "Gruplar"."OID"
INNER JOIN "Kullanicilar"  ON "KullaniciDetaylari"."KullaniciId" = "Kullanicilar"."KullaniciId"
LEFT OUTER JOIN "Menuler"   ON "GrupDetaylari"."MenuId" = "Menuler"."OID"
WHERE "Menuler"."Durum" = 1;
 */