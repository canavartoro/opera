using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Xml.Serialization;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using System.Diagnostics;
using DevExpress.ExpressApp.Web;
using Mikrobar;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
///
namespace Mikrobar.Module.BusinessObjects
{
    [MikrobarTablo(1, "MALZEME", "GNL", "STOK KART TANIMLARI TABLOSU")]
    [ReferansTablo("pub.stok_kart",
        SqlSorgu = @"select cast(s.rowid as int) as ""MalzemeId"", s.stok_kod as ""MalzemeKod"", s.stok_ad as ""MalzemeAd"", s.stok_ad2 as ""MalzemeAd2"", cast(b.rowid as int) as ""BirimId"", s.birim as ""Birim"", s.kdv_oran as ""KdvOran"", s.min_stok as ""MinStok"", s.max_stok as ""MaxStok"", s.int_sira as ""HammaddeTakip"", s.fazla_sipyuzde AS ""FazlaSipMiktar"", case when urun_tip = 'Hammadde' then 1 when urun_tip = 'Y.Mamul' then 2 when urun_tip = 'Mamul' then 3 else 0 end as ""Tip"", urun_tip as ""TipKod""  from pub.stok_kart s, pub.birim b where s.firma_kod = '#FirmaKod#' and s.birim = b.birim and s.firma_kod = b.firma_kod ",
        SqlWhere = " and cast(s.rowid as int) = {0} and s.stok_kod like '{1}%'", SistemTipi = SistemTipi.Progress, QueryType = QueryType.Yoksa)]
    [ReferansTablo("TBLSTSABIT", SqlSorgu = @"SELECT * FROM Barset.V_Malzemeler WHERE ( 1 = 1 ) ", SqlWhere = " AND MalzemeId = {0} ", SistemTipi = SistemTipi.Netsis, QueryType = QueryType.Yoksa)]
    [ReferansTablo("INVD_ITEM", SistemTipi.WebErp, QueryType = QueryType.Yoksa, SqlSorgu = @"SELECT DISTINCT CAST(M.ITEM_ID AS INTEGER) AS ""MalzemeId"", M.ITEM_CODE AS ""MalzemeKod"", M.ITEM_NAME AS ""MalzemeAd"", M.ITEM_NAME2 AS ""MalzemeAd2"", B.UNIT_ID AS ""BirimId"", U.UNIT_CODE AS ""Birim"", B.CAT_CODE1_ID AS ""HammaddeTakipId"", NVL(G.CAT_CODE, '2') AS ""HammaddeTakip"", C.INV_ITEM_CLASS AS ""Tip"", C.ITEM_CLASS_CODE AS ""TipKod"" FROM UYUMSOFT.INVD_BRANCH_ITEM B INNER JOIN UYUMSOFT.INVD_ITEM M ON B.ITEM_ID = M.ITEM_ID LEFT OUTER JOIN UYUMSOFT.INVD_UNIT U ON B.UNIT_ID = U.UNIT_ID LEFT OUTER JOIN UYUMSOFT.GNLD_CATEGORY G ON B.CAT_CODE1_ID = G.CAT_CODE_ID LEFT OUTER JOIN UYUMSOFT.INVD_ITEM_CLASS C ON B.ITEM_CLASS_ID = C.ITEM_CLASS_ID WHERE B.CO_ID = #Firma# AND B.BRANCH_ID = #Isyeri# ORDER BY M.ITEM_CODE"),
    DebuggerDisplay("MalzemeId = {MalzemeId}, MalzemeKod = {MalzemeKod}, BirimId = {BirimId}, Birim = {Birim}, TipKod = {TipKod}"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    [DeferredDeletion(false), OptimisticLocking(false), DefaultClassOptions, XafDefaultProperty("MalzemeKod"), NavigationItem(false), ImageName("BO_Role")]
    public class Malzemeler : XPBaseObject
    {
        [ReferansAlan("cast(rowid as int)", SistemTipi = SistemTipi.Progress, KeyField = true)]
        [ModelDefault("DisplayFormat", "d"), Key(AutoGenerate = false), Persistent("MalzemeId"), VisibleInLookupListView(true)]
        public int MalzemeId { get; set; }

        [ReferansAlan("stok_kod", SistemTipi = SistemTipi.Progress)]
        [Size(DbSize.KodLenght), Indexed(Name = "malz", Unique = false), VisibleInLookupListView(true)]
        public string MalzemeKod { get; set; }

        [ReferansAlan("stok_ad", SistemTipi = SistemTipi.Progress)]
        [Size(DbSize.AdLenght), VisibleInLookupListView(true)]
        public string MalzemeAd { get; set; }

        [ReferansAlan("stok_ad2", SistemTipi = SistemTipi.Progress)]
        [Size(DbSize.AdLenght), VisibleInLookupListView(true)]
        public string MalzemeAd2 { get; set; }

        [VisibleInLookupListView(true)]
        public int BirimId { get; set; }

        [ReferansAlan("birim", SistemTipi = SistemTipi.Progress)]
        [Size(DbSize.KodLenght), VisibleInLookupListView(true)]
        public string Birim { get; set; }

        [ReferansAlan("kdv_oran", SistemTipi = SistemTipi.Progress)]
        [DbType(" DECIMAL(18,4) "), VisibleInLookupListView(true)]
        public decimal KdvOran { get; set; }

        [ReferansAlan("min_stok", SistemTipi = SistemTipi.Progress)]
        [DbType(" DECIMAL(18,4) "), VisibleInLookupListView(true)]
        public decimal MinStok { get; set; }

        [ReferansAlan("max_stok", SistemTipi = SistemTipi.Progress)]
        [DbType(" DECIMAL(18,4) "), VisibleInLookupListView(true)]
        public decimal MaxStok { get; set; }

        [ReferansAlan("fazla_sipyuzde", SistemTipi = SistemTipi.Progress)]
        [DbType(" DECIMAL(18,4) "), ModelDefault("DisplayFormat", "{0:0.##}%"), VisibleInLookupListView(true)]
        public decimal FazlaSipMiktar { get; set; }

        [VisibleInLookupListView(true)]
        public int MalzemeGrupId { get; set; }

        [VisibleInLookupListView(true)]
        public int MalzemeGrup2Id { get; set; }

        [ReferansAlan("case when urun_tip = 'Hammadde' then 1 when urun_tip = 'Y.Mamul' then 2 when urun_tip = 'Mamul' then 3 else 0 end", SistemTipi = SistemTipi.Progress)]
        [Persistent("Tip"), VisibleInLookupListView(true)]
        public MalzemeTip Tip { get; set; }

        [ReferansAlan("urun_tip", SistemTipi = SistemTipi.Progress)]
        [Size(DbSize.KodLenght), Persistent("TipKod"), VisibleInLookupListView(true)]
        public string TipKod { get; set; }

        [ReferansAlan("int_sira", SistemTipi = SistemTipi.Progress)]
        [XmlIgnore(), Persistent("HammaddeTakip"),
        Description("İş emrine üretim kaydi girilirken sistem bu alana bakarak malzeme ariyor, Malzeme cikilmamissa bu alan kontrol ediliyor."), VisibleInLookupListView(true)]
        public HammaddeTakip HammaddeTakip { get; set; }

        [Description("Kalite kontrol yapilacak mi?")]
        public bool KaliteKontrol { get; set; }

        public string LotKod { get; set; }
        public int LotId { get; set; }

        [XmlIgnore(), Association(@"SayilmayacakMalzemeler.Malzeme.MalzemeId"), NoForeignKey]
        public XPCollection<SayilmayacakMalzemeler> SayilmayacakMalzemeler
        {
            get { return GetCollection<SayilmayacakMalzemeler>(@"SayilmayacakMalzemeler"); }
        }

        [XmlIgnore(), Association(@"MalzemeDokumanlari.Malzeme.MalzemeId"), NoForeignKey]
        public XPCollection<MalzemeDokumanlari> MalzemeDokumanlari
        {
            get { return GetCollection<MalzemeDokumanlari>(@"MalzemeDokumanlari"); }
        }

        [XmlIgnore(), Association(@"TasarimGruplari.Malzeme.MalzemeId"), NoForeignKey, VisibleInDetailView(false)]
        public XPCollection<TasarimGruplari> TasarimGruplari
        {
            get { return GetCollection<TasarimGruplari>(@"TasarimGruplari"); }
        }

        [XmlIgnore(), Association(@"Malzemeler.UretimOperasyonlari.MalzemeId"), NoForeignKey, VisibleInDetailView(false)]
        public XPCollection<UretimOperasyonlari> Uretimler
        {
            get { return GetCollection<UretimOperasyonlari>(@"Uretimler"); }
        }

        [XmlIgnore(), Association(@"Malzemeler.SevkEmriDetaylari.MalzemeId"), NoForeignKey, VisibleInDetailView(false)]
        public XPCollection<SevkEmriDetaylari> SevkEmriDetay
        {
            get { return GetCollection<SevkEmriDetaylari>(@"SevkEmriDetay"); }
        }

        [XmlIgnore(), Association(@"Malzemeler.UretimHurdalar.MalzemeId"), NoForeignKey, VisibleInDetailView(false)]
        public XPCollection<UretimHurdalari> UretimHurdalar
        {
            get { return GetCollection<UretimHurdalari>(@"UretimHurdalar"); }
        }

        [XmlIgnore(), Association(@"Malzemeler.MalzemeBirimleri.MalzemeId"), NoForeignKey]
        public XPCollection<MalzemeBirimleri> MalzemeBirimleri
        {
            get { return GetCollection<MalzemeBirimleri>(@"MalzemeBirimleri"); }
        }

        [XmlIgnore(), Association(@"Malzemeler.MalzemeBarkodlari.MalzemeId"), NoForeignKey]
        public XPCollection<MalzemeBarkodlari> MalzemeBarkodlari
        {
            get { return GetCollection<MalzemeBarkodlari>(@"MalzemeBarkodlari"); }
        }

        [XmlIgnore(), Association(@"Malzemeler.EtiketTanimlari.Malzeme"), NoForeignKey]
        public XPCollection<EtiketTanimlari> EtiketTanimlari
        {
            get { return GetCollection<EtiketTanimlari>(@"EtiketTanimlari"); }
        }

        [XmlIgnore(), Association(@"Malzemeler.SertifikaTanimlari.MalzemeId"), NoForeignKey]
        public XPCollection<SertifikaTanimlari> SertifikaTanimlari
        {
            get { return GetCollection<SertifikaTanimlari>(@"SertifikaTanimlari"); }
        }

        [XmlIgnore(), Association(@"TalepDetaylari.Malzeme.MalzemeId"), NoForeignKey, VisibleInDetailView(false)]
        public XPCollection<TalepDetaylari> TalepDetaylari
        {
            get { return GetCollection<TalepDetaylari>(@"TalepDetaylari"); }
        }

        [XmlIgnore(), Association(@"TalepKabulDetaylari.Malzeme.MalzemeId"), NoForeignKey, VisibleInDetailView(false)]
        public XPCollection<TalepKabulDetaylari> TalepKabulDetaylari
        {
            get { return GetCollection<TalepKabulDetaylari>(@"TalepKabulDetaylari"); }
        }

        [XmlIgnore(), Association(@"UretimMalzemeleri.Malzemeler.ReceteMalzeme"), NoForeignKey, VisibleInDetailView(false)]
        public XPCollection<UretimMalzemeleri> ReceteMalzeme
        {
            get { return GetCollection<UretimMalzemeleri>(@"ReceteMalzeme"); }
        }

        [XmlIgnore(), Association(@"UretimMalzemeleri.Malzemeler.Malzeme"), NoForeignKey, VisibleInDetailView(false)]
        public XPCollection<UretimMalzemeleri> UretimMalzeme
        {
            get { return GetCollection<UretimMalzemeleri>(@"UretimMalzeme"); }
        }

        [XmlIgnore(), Association(@"Malzemeler.MusteriMalzemeleri.MalzemeId"), NoForeignKey, VisibleInDetailView(false)]
        public XPCollection<MusteriMalzemeleri> MusteriMalzemeleri
        {
            get { return GetCollection<MusteriMalzemeleri>(@"MusteriMalzemeleri"); }
        }

        /*[XmlIgnore(), Association(@"IsEmriBilesenleri.Malzemeler_Bilesenler"), VisibleInListView(false)]
        public XPCollection<IsEmriBilesenleri> Bilesenler
        {
            get { return GetCollection<IsEmriBilesenleri>(@"Bilesenler"); }
        }

        [XmlIgnore(), Association(@"IsEmriBilesenleri.Malzemeler_UstBilesenler"), VisibleInListView(false)]
        public XPCollection<IsEmriBilesenleri> UstBilesenler
        {
            get { return GetCollection<IsEmriBilesenleri>(@"UstBilesenler"); }
        }*/
        /*[XmlIgnore(), Association(@"UretimMalzemeleri.MalzemeId"), VisibleInListView(false)]
        public XPCollection<UretimMalzemeleri> Uretimler
        {
            get { return GetCollection<UretimMalzemeleri>(@"Uretimler"); }
        }

        [XmlIgnore(), Association(@"UretimMalzemeleri.ReceteMalzeme"), VisibleInListView(false)]
        public XPCollection<UretimMalzemeleri> UretimBilesenleri
        {
            get { return GetCollection<UretimMalzemeleri>(@"UretimBilesenleri"); }
        }*/
        //[XmlIgnore()]
        //[Association(@"Malzemeler.MalzemeBirimleri"), VisibleInListView(false)]
        //public XPCollection<MalzemeBirimleri> MalzemeBirim
        //{
        //    get { return GetCollection<MalzemeBirimleri>(@"MalzemeBirim"); }
        //}

        #region Ortak Alanlar
        #region Olusturan
        [ModelDefault("AllowEdit", "False"), ReadOnly(true), XmlIgnore(), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int Olusturan { get; set; }

        private Kullanicilar _olusturankullanici;
        [XmlIgnore(), NonPersistent, XafDisplayName("Olusturan Kullanıcı"), ImmediatePostData,
        VisibleInListView(false), VisibleInLookupListView(false)]
        public Kullanicilar OlusturanKullanici
        {
            get
            {
                if (!IsLoading && !IsSaving)
                {
                    if (_olusturankullanici == null && this.Olusturan > 0)
                        this._olusturankullanici = this.Session.GetObjectByKey<Kullanicilar>(this.Olusturan);
                }
                return _olusturankullanici;
            }
        }
        #endregion

        [ModelDefault("AllowEdit", "False"), ModelDefault("DisplayFormat", "{0:dd.MM.yyyy HH:mm}"), ReadOnly(true), XmlIgnore(), VisibleInListView(false), VisibleInLookupListView(false)]
        public DateTime OlusturmaTarihi { get; set; }

        #region Guncelleyen
        [ModelDefault("AllowEdit", "False"), ReadOnly(true), XmlIgnore(), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int Guncelleyen { get; set; }

        private Kullanicilar _guncelleyenkullanici;
        [XmlIgnore(), NonPersistent, XafDisplayName("Guncelleyen Kullanıcı"), ImmediatePostData,
        VisibleInListView(false), VisibleInLookupListView(false)]
        public Kullanicilar GuncelleyenKullanici
        {
            get
            {
                if (!IsSaving && !IsLoading)
                {
                    if (_guncelleyenkullanici == null && this.Guncelleyen > 0)
                        this._guncelleyenkullanici = this.Session.GetObjectByKey<Kullanicilar>(this.Guncelleyen);
                }
                return _guncelleyenkullanici;
            }
        }
        #endregion

        [ModelDefault("AllowEdit", "False"), ModelDefault("DisplayFormat", "{0:dd.MM.yyyy HH:mm}"), ReadOnly(true), XmlIgnore(), VisibleInListView(false), VisibleInLookupListView(false)]
        public DateTime GuncellemeTarihi { get; set; }
        [Size(DbSize.ModulLenght), ModelDefault("AllowEdit", "False"), ReadOnly(true), XmlIgnore(), VisibleInListView(false), VisibleInLookupListView(false)]
        public string KaynakModul { get; set; }
        [ModelDefault("AllowEdit", "False"), ReadOnly(true), Description("Kaydın oluştuğu uygulama"), XmlIgnore(), VisibleInListView(false), VisibleInLookupListView(false)]
        public KaynakProgram KaynakProgram { get; set; }
        [Size(DbSize.CihazNoLenght), ModelDefault("AllowEdit", "False"), Browsable(false), ReadOnly(true), XmlIgnore(), VisibleInListView(false), VisibleInLookupListView(false)]
        public string CihazNo { get; set; }
        [ModelDefault("AllowEdit", "False"), ReadOnly(true), XmlIgnore(), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool Entegre { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public KayitDurumu Durum { get; set; }
        #endregion

        #region Butonlar
        [Action(Caption = "Guncelle", ImageName = "Action_Refresh", ToolTip = "Bilgileri guncelle..")]
        public void Entegrasyon()
        {
            ////try
            ////{
            ////    Mikrobar.Entegre.DataQuery query = new Mikrobar.Entegre.DataQuery(this.GetType());
            ////    object ret = query.Entegre();

            ////    //DevExpress.Xpo.DB.SelectedData data = this.Session.ExecuteSproc("sp_CariGuncelleERP");
            ////    if (WebWindow.CurrentRequestWindow != null)
            ////        WebWindow.CurrentRequestWindow.RegisterClientScript("tmm" + this.GetType().Name, "alert('İşlem tamamlandı. Sonuc:" + query.HataMesaji + "');");
            ////}
            ////catch (Exception exc)
            ////{
            ////    throw exc;
            ////}
        }
        #endregion

        #region Event

        protected override void OnSaving()
        {
            if (this.IsDeleted == false)
            {
                if (this.MalzemeId < 1)
                {
                    Object MaxMalzemeId = this.Session.Evaluate<Malzemeler>(CriteriaOperator.Parse("Max(MalzemeId)"), null);

                    if (MaxMalzemeId != null && MaxMalzemeId is Int32)
                    {
                        this.MalzemeId = (Int32)MaxMalzemeId + 1;
                    }
                }
            }
        }

        #endregion

        public Malzemeler() { }
        public Malzemeler(Session session) : base(session) { }
    }
}


/*
 web erp sql
 SELECT DISTINCT CAST(M.ITEM_ID AS INTEGER) AS "MalzemeId", M.ITEM_CODE AS "MalzemeKod", M.ITEM_NAME AS "MalzemeAd", M.ITEM_NAME2 AS "MalzemeAd2", B.UNIT_ID AS "BirimId", U.UNIT_CODE AS "Birim", B.CAT_CODE1_ID AS "HammaddeTakipId", C.CAT_CODE AS "HammaddeTakip" FROM INVD_BRANCH_ITEM B INNER JOIN INVD_ITEM M ON B.ITEM_ID = M.ITEM_ID LEFT OUTER JOIN INVD_UNIT U ON B.UNIT_ID = U.UNIT_ID LEFT OUTER JOIN GNLD_CATEGORY C ON B.CAT_CODE1_ID = C.CAT_CODE_ID WHERE B.CO_ID = #Firma# AND B.BRANCH_ID = #Isyeri# ORDER BY M.ITEM_CODE

 */

/*
 
--EXECUTE dbo.MalzemeBulErp 'ARMA2011', '', 497574

ALTER PROCEDURE [dbo].[MalzemeBulErp]
(
	@MALZEMEID INTEGER, -- '512000 FS 0001985'	
	@MALZEMEKOD NVARCHAR(30), -- '512000 FS 0001985'
	@FIRMAKOD NVARCHAR(30) = 'ARMA2011',
	@FIRMAID INTEGER = 0,
	@ISYERIKOD NVARCHAR(30) = '',
	@ISYERIID INTEGER = 0
)
AS BEGIN

DECLARE @TIRNAK CHAR(1)
DECLARE @SQLCOMMAND NVARCHAR(800)

IF LEN(@FIRMAKOD) < 1 BEGIN SET @FIRMAKOD = 'ARMA2011' END

SET @TIRNAK = ''''
SET @SQLCOMMAND = 'SELECT * FROM OPENQUERY(UYUMDB, ''select cast(s.rowid as int) as "MalzemeId"'
SET @SQLCOMMAND = @SQLCOMMAND + ', s.stok_kod as "MalzemeKod" '
SET @SQLCOMMAND = @SQLCOMMAND + ', s.stok_ad as "MalzemeAd" '
SET @SQLCOMMAND = @SQLCOMMAND + ', s.stok_ad2 as "MalzemeAd2"'
SET @SQLCOMMAND = @SQLCOMMAND + ', cast(b.rowid as int) as "BirimId" '
SET @SQLCOMMAND = @SQLCOMMAND + ', s.birim as "Birim"'
SET @SQLCOMMAND = @SQLCOMMAND + ', s.kdv_oran as "KdvOran"'
SET @SQLCOMMAND = @SQLCOMMAND + ', s.min_stok as "MinStok"'
SET @SQLCOMMAND = @SQLCOMMAND + ', s.max_stok as "MaxStok"'
SET @SQLCOMMAND = @SQLCOMMAND + ', s.fazla_sipyuzde as "FazlaSipMiktar" '
SET @SQLCOMMAND = @SQLCOMMAND + ', s.int_sira as "HammaddeTakip" '
SET @SQLCOMMAND = @SQLCOMMAND + ', s.kkontrol AS "KaliteKontrol" '
SET @SQLCOMMAND = @SQLCOMMAND + ', s.urun_tip AS "TipKod" '
SET @SQLCOMMAND = @SQLCOMMAND + ', case s.urun_tip when  ' + @TIRNAK  + @TIRNAK + 'Mamul' + @TIRNAK   + @TIRNAK + ' then 3 when ' + @TIRNAK   + @TIRNAK + 'Y.Mamul' + @TIRNAK   + @TIRNAK + ' then 2 when ' + @TIRNAK   + @TIRNAK + 'Hammadde' + @TIRNAK   + @TIRNAK + ' then 1 else 0 end as Tip '
SET @SQLCOMMAND = @SQLCOMMAND + ''
SET @SQLCOMMAND = @SQLCOMMAND + ''
SET @SQLCOMMAND = @SQLCOMMAND + ''
SET @SQLCOMMAND = @SQLCOMMAND + ''
SET @SQLCOMMAND = @SQLCOMMAND + ' from pub.stok_kart s, pub.birim b where s.firma_kod = ' + @TIRNAK + @TIRNAK + @FIRMAKOD + @TIRNAK + @TIRNAK + ' '
IF @MALZEMEID > 0 BEGIN
	SET @SQLCOMMAND = @SQLCOMMAND + ' and cast(s.rowid as int) = ' + CONVERT(NVARCHAR(30),@MALZEMEID)
END
IF LEN(@MALZEMEKOD) > 0 BEGIN
	SET @SQLCOMMAND = @SQLCOMMAND + ' and s.stok_kod = ' + @TIRNAK + @TIRNAK +  @MALZEMEKOD + @TIRNAK + @TIRNAK + ' '
END
SET @SQLCOMMAND = @SQLCOMMAND + ' and b.birim = s.birim and b.firma_kod = s.firma_kod ' + @TIRNAK + ')'
PRINT @SQLCOMMAND

EXEC(@SQLCOMMAND)

END


  
 */