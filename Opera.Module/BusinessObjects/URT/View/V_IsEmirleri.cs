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
    [Persistent("V_IsEmirleri")]
    [ImageName("BO_Organization"),
    ModelDefault("DefaultListViewAllowDelete", "False"), ModelDefault("DefaultListViewAllowEdit", "False"), ModelDefault("DefaultListViewAllowNew", "False"),
    ModelDefault("DefaultListViewShowAutoFilterRow", "True"), ModelDefault("IsCreatableItem", "False"),
  XafDefaultProperty("IsEmriNo"), XafDisplayName("Is Emri Detaylari"), NavigationItem(false)]
    [ReferansTablo("pub.erp_master", SistemTipi = SistemTipi.Progress,
        SqlSorgu = @"select m.erp_masterno as ""IsEmriId"", d.erp_detayno as ""IsEmriDetayId"", m.isemri_no as ""IsEmriNo"", 0 as ""ReceteId"", 
cast(s.rowid as int) as ""MalzemeId"", s.stok_kod as ""MalzemeKodu"", s.stok_ad as ""MalzemeAdi"", cast(b.rowid as int) as ""BirimId"", 
b.birim as ""BirimKodu"", m.pmiktar as ""PlanlananMiktar"", cast(i.rowid as int) as ""IstasyonId"", i.istasyon_kod as ""IstasyonKodu"", 
i.istasyon_ad as ""IstasyonAdi"", m.rota_kod as ""RotaKod"", d.sira_no as ""OperasyonNo"", cast(o.rowid as int) as ""OperasyonId"", 
o.operasyon_kod as ""OperasyonKodu"", o.operasyon_ad as ""OperasyonAdi"", d.ismer_kod as ""IsMerkeziKodu"", m.aciklama as ""Aciklama"",
cast(t.rowid as int) as ""IsEmriTipId"", t.tip_kod as ""IsEmriTipKodu"", t.tip_ad as ""IsEmriTipAdi"", m.parti_kod as ""DigerKod1"", 
cast(m.bas_tarih as date) as ""BaslangicTarihi"", m.bas_saat as ""BaslangicSaat"", cast(m.bitis_tarih as date) as ""BitisTarihi"", 
m.bitis_saat as ""BitisSaat"", cast(m.create_date as date) as ""AcilmaTarihi""
from pub.erp_master m, pub.erp_detay d, pub.stok_kart s, pub.birim b, pub.is_istasyon i, 
pub.operasyon_kart2 o, pub.isemri_tip t where m.firma_kod = '#FirmaKod#' 
and d.firma_kod = m.firma_kod  and d.erp_masterno = m.erp_masterno and s.firma_kod = m.firma_kod and d.stok_kod = s.stok_kod
and b.firma_kod = m.firma_kod and b.birim = d.birim and i.firma_kod = m.firma_kod and i.istasyon_kod = d.istasyon_kod 
and o.firma_kod = m.firma_kod and o.operasyon_kod = d.operasyon_kod and t.firma_kod = m.firma_kod and t.tip_kod = m.tip_kod",
    SqlWhere = @" and m.erp_masterno = {0} and cast(o.rowid as int) = {1} ", QueryType = QueryType.Yoksa)]
    [ReferansTablo("TBLISEMRI", SqlSorgu = "SELECT * FROM [Barset].[V_IsEmriDetaylari] WITH (NOLOCK) WHERE ( 1 = 1 ) ", SqlWhere = " AND IsEmriDetayId = {0} ", SistemTipi = SistemTipi.Netsis, QueryType = QueryType.Yoksa)]
    public class V_IsEmirleri : XPLiteObject
    {
        public string IsEmriNo { get; set; }

        [VisibleInLookupListView(true)]
        public string MalzemeKod { get; set; }

        [VisibleInLookupListView(true)]
        public string MalzemeAd { get; set; }

        [VisibleInLookupListView(true)]
        public string Birim { get; set; }

        [VisibleInLookupListView(true)]
        public decimal PlanlananMiktar { get; set; }

        [VisibleInLookupListView(true)]
        [NonPersistent]
        public decimal UretilenMiktar { get; set; }

        [NonPersistent, VisibleInLookupListView(true)]
        public decimal UretilenNetMiktar { get; set; }

        [VisibleInLookupListView(true)]
        public decimal FireMiktari { get; set; }

        [NonPersistent, XmlIgnore()]
        public decimal TransferEdilebilecekMiktari { get; set; }

        [VisibleInLookupListView(true)]
        public string IstasyonKod { get; set; }

        [VisibleInLookupListView(true)]
        public string IstasyonAd { get; set; }

        [VisibleInLookupListView(true)]
        public string OperasyonKod { get; set; }

        [VisibleInLookupListView(true)]
        public string OperasyonAd { get; set; }

        [VisibleInLookupListView(true)]
        public string IsMerkeziKod { get; set; }

        [VisibleInLookupListView(true)]
        public string IsMerkeziAd { get; set; }

        [VisibleInLookupListView(true)]
        public string Aciklama { get; set; }

        [XmlIgnore()]
        public string DigerId { get; set; }

        [XmlIgnore()]
        public string RotaKod { get; set; }

        [VisibleInLookupListView(true)]
        public string IsEmriTipKod { get; set; }

        [VisibleInLookupListView(true)]
        public string IsEmriTipAd { get; set; }

        [XmlIgnore()]
        public string OzelKod { get; set; }

        [XmlIgnore()]
        public string DigerKod1 { get; set; }

        [XmlIgnore(), ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}"), VisibleInLookupListView(true)]
        public DateTime BaslangicTarihi { get; set; }

        [XmlIgnore(), ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}"), VisibleInLookupListView(true)]
        public string BaslangicSaat { get; set; }

        [XmlIgnore(), ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}"), VisibleInLookupListView(true)]
        public DateTime BitisTarihi { get; set; }

        [XmlIgnore()]
        public string BitisSaat { get; set; }

        [XmlIgnore()]
        public DateTime AcilmaTarihi { get; set; }

        public int MalzemeId { get; set; }

        public int BirimId { get; set; }
        [XmlIgnore()]
        public int IsYeriId { get; set; }

        public int IsMerkeziId { get; set; }

        [XmlIgnore()]
        public int FirmaId { get; set; }

        [XmlIgnore()]
        public int EntegreId { get; set; }

        [XmlIgnore()]
        public int RenkId { get; set; }

        [XmlIgnore(), VisibleInLookupListView(true)]
        public string CizimNo { get; set; }

        [ReferansAlan("m.erp_masterno", SistemTipi = SistemTipi.Progress, KeyField = true, KeyIndex = 0)]
        public int IsEmriId { get; set; }

        [Key]
        public int IsEmriDetayId { get; set; }

        public int IstasyonId { get; set; }

        [ReferansAlan("cast(o.rowid as int)", SistemTipi = SistemTipi.Progress, KeyField = true, KeyIndex = 1)]
        public int OperasyonId { get; set; }

        [NonPersistent]
        public int OperasyonNo { get; set; }

        public int IsEmriTipId { get; set; }

        [XmlIgnore()]
        public int UrunRotaId { get; set; }

        [XmlIgnore()]
        public int RotaId { get; set; }

        [XmlIgnore()]
        public int ReceteId { get; set; }

        [XmlIgnore()]
        public bool Entegre { get; set; }

        public bool IlkOperasyon { get; set; }

        public bool SonOperasyon { get; set; }

        public bool TemelTas { get; set; }

        public bool HayaletOperasyon { get; set; }

        public string OncekiOperasyon { get; set; }

        public string SonrakiOperasyon { get; set; }

        [VisibleInLookupListView(true)]
        public bool StokUpdate { get; set; }

        public V_IsEmirleri() { }
        public V_IsEmirleri(Session session) : base(session) { }
    }

    /*oracle
     
CREATE OR REPLACE VIEW "V_IsEmirleri"
AS
SELECT     
    "IsEmirleri"."IsEmriNo" AS "IsEmriNo", "Malzemeler"."MalzemeKod" AS "MalzemeKod", "Malzemeler"."MalzemeAd" AS "MalzemeAd", "Birimler"."Birim" AS "Birim", 
    "IsEmirleri"."PlanlananMiktar" AS "PlanlananMiktar", "IsEmirleri"."FireMiktari" AS "FireMiktari", "Istasyonlar"."IstasyonKod" AS "IstasyonKod", "Istasyonlar"."IstasyonAd" AS "IstasyonAd", 
    "Operasyonlar"."OperasyonKod" AS "OperasyonKod", "Operasyonlar"."OperasyonAd" AS "OperasyonAd", "Istasyonlar"."IsMerkeziKod" AS "IsMerkeziKod", "Istasyonlar"."IsMerkeziAd" AS "IsMerkeziAd", 
    "IsEmirleri"."Aciklama" AS "Aciklama", "IsEmirleri"."DigerId" AS "DigerId", "IsEmriDetaylari"."RotaKod" AS "RotaKod", "IsEmirleri"."TipKod" AS "IsEmriTipKod", 
    "IsEmirleri"."TipKod" AS "IsEmriTipAd", "IsEmirleri"."OzelKod" AS "OzelKod", "IsEmirleri"."DigerKod1" AS "DigerKod1", "IsEmirleri"."BaslangicTarihi" AS "BaslangicTarihi", 
    "IsEmirleri"."BaslangicSaat" AS "BaslangicSaat", "IsEmirleri"."BitisTarihi" AS "BitisTarihi", "IsEmirleri"."BitisSaat" AS "BitisSaat", "IsEmirleri"."AcilmaTarihi" AS "AcilmaTarihi", 
    "IsEmirleri"."IsEmriId" AS "IsEmriId", "IsEmirleri"."MalzemeId" AS "MalzemeId", "IsEmirleri"."BirimId" AS "BirimId", "IsEmirleri"."IsYeriId" AS "IsYeriId", "IsEmirleri"."IsMerkeziId" AS "IsMerkeziId", 
    "IsEmirleri"."FirmaId" AS "FirmaId", "IsEmirleri"."EntegreId" AS "EntegreId", CASE WHEN "IsEmriDetaylari"."IsEmriDetayId" IS NULL THEN "IsEmirleri"."IsEmriId" ELSE "IsEmriDetaylari"."IsEmriDetayId" END AS "IsEmriDetayId", 
    "IsEmriDetaylari"."IstasyonId" AS "IstasyonId", "IsEmriDetaylari"."OperasyonId" AS "OperasyonId", "IsEmirleri"."IsEmriTipId" AS "IsEmriTipId", "IsEmirleri"."UrunRotaId" AS "UrunRotaId", 
    "IsEmirleri"."RotaId" AS "RotaId", "IsEmirleri"."ReceteId" AS "ReceteId", NVL("IsEmirleri"."Entegre", 0) AS "Entegre", 
    NVL("IsEmriDetaylari"."IlkOperasyon", 0) AS "IlkOperasyon", NVL("IsEmriDetaylari"."SonOperasyon", 0) AS "SonOperasyon", 
    NVL("IsEmriDetaylari"."TemelTas", 0) AS "TemelTas", NVL("IsEmriDetaylari"."HayaletOperasyon", 0) AS "HayaletOperasyon", 
    "IsEmriDetaylari"."OncekiOperasyon" AS "OncekiOperasyon", "IsEmriDetaylari"."SonrakiOperasyon" AS "SonrakiOperasyon", NVL("IsEmriDetaylari"."StokUpdate", 0) AS "StokUpdate", "IsEmirleri"."RenkId" AS "RenkId", "IsEmirleri"."CizimNo" AS "CizimNo"
FROM "IsEmirleri" LEFT OUTER JOIN "IsEmriDetaylari" ON "IsEmirleri"."IsEmriId" = "IsEmriDetaylari"."IsEmriId" LEFT OUTER JOIN
                      "Birimler" ON "IsEmirleri"."BirimId" = "Birimler"."BirimId" LEFT OUTER JOIN
                      "Malzemeler" ON "IsEmirleri"."MalzemeId" = "Malzemeler"."MalzemeId" LEFT OUTER JOIN
                      "Istasyonlar" ON "IsEmriDetaylari"."IstasyonId" = "Istasyonlar"."IstasyonId" LEFT OUTER JOIN
                      "Operasyonlar" ON "IsEmriDetaylari"."OperasyonId" = "Operasyonlar"."OperasyonId"
     */
}
