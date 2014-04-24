using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [NonPersistent]
    [ImageName("BO_Organization"), ModelDefault("DefaultListViewShowAutoFilterRow", "True"),
  XafDefaultProperty("DurusKod"), XafDisplayName("Uretim Durus Detaylari"), NavigationItem(false)]
    public class V_UretimDuruslari : XPLiteObject
    {

        public string DurusKod { get; set; }
        public string DurusAd { get; set; }
        public string DurusTip { get; set; }
        public int DurusTanimId { get; set; }
        public bool IsEmriBaglanti { get; set; }
        public string Aciklama { get; set; }

        [Key]
        public int UretimDurusId { get; set; }
        public int UretimOperasyon { get; set; }
        public int UretimId { get; set; }
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}")]
        public DateTime BaslangicTarihi { get; set; }
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}")]
        public DateTime BitisTarihi { get; set; }
        public int DurusSuresi { get; set; }
        public int IstasyonId { get; set; }
        public string IstasyonKod { get; set; }
        public string IstasyonAd { get; set; }
        public int Durum { get; set; }


        public V_UretimDuruslari(Session session) : base(session) { }
        public V_UretimDuruslari() : base(Session.DefaultSession) { }
    }

    /*oracle
     CREATE or Replace VIEW "V_UretimDuruslar"
AS
   SELECT NVL ("UretimOperasyonlari"."OID", 0) AS "UretimOperasyon",
          "UretimDuruslari"."OID" AS "UretimDurusId",
          NVL ("UretimOperasyonlari"."Uretim", 0) AS "UretimId",
          "Duruslar"."DurusKod", "Duruslar"."DurusAd",
          NVL ("Duruslar"."IsEmriBaglanti", 0) AS "IsEmriBaglanti",
          "Duruslar"."DurusTip", '' AS "DurusAciklama",
          "UretimDuruslari"."BaslangicTarihi",
          "Duruslar"."DurusId" AS "DurusTanimId",
          "UretimDuruslari"."Aciklama", "UretimDuruslari"."BitisTarihi",
          "UretimDuruslari"."DurusSuresi", "UretimDuruslari"."Durum",
          "UretimDuruslari"."IstasyonId", "UretimDuruslari"."IstasyonKod", "Istasyonlar"."IstasyonAd"
     FROM "UretimDuruslari" LEFT OUTER JOIN "UretimOperasyonlari"
          ON "UretimOperasyonlari"."OID" = "UretimDuruslari"."UretimOperasyon"
          LEFT OUTER JOIN "Duruslar"
          ON "UretimDuruslari"."DurusNedeniId" = "Duruslar"."DurusId"
          LEFT OUTER JOIN "Istasyonlar" ON "Istasyonlar"."IstasyonId" = "UretimDuruslari"."IstasyonId"
          WHERE "UretimDuruslari"."Durum" = 0
     */

}
