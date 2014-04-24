using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Xml.Serialization;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using System.ComponentModel;
using System.Diagnostics;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Model;


namespace Mikrobar.Module.BusinessObjects
{
    [ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    [OptimisticLocking(false), DeferredDeletion(false), ReferansTablo("INVD_BWH_LOCATION", SistemTipi = SistemTipi.WebErp),
    DefaultClassOptions, XafDefaultProperty("RafKod"), NavigationItem(false), ImageName("BO_Sale_Item_v92")]
    [DebuggerDisplay("RafKod = {RafKod}, RafId = {RafId}, Depo = {Depo}")]
    [ReferansTablo("pub.raf r left outer join pub.depo d on r.depo_kod = d.depo_kod and r.firma_kod = d.firma_kod where r.firma_kod = '#FirmaKod#' ",SistemTipi=SistemTipi.Progress,QueryType=QueryType.Yoksa)]
    public class Raflar : XPBaseObject
    {
        [NonPersistent, VisibleInListView(false), VisibleInLookupListView(false)]
        public int DepoId { get; set; }

        [Persistent("DepoId"), VisibleInLookupListView(true),
        /*Indexed("RafKod;HiyerArsi;IsyeriId", Name = "x_dep", Unique = true),*/ 
        XmlIgnore(), Association(@"Depolar.Raflar_RafId"), NoForeignKey]
        [ReferansAlan("cast(d.rowid as int)",SistemTipi=SistemTipi.Progress)]
        public Depolar Depo { get; set; }

        [ReferansAlan("cast(r.rowid as int)", SistemTipi = SistemTipi.Progress)]
        [Key(AutoGenerate = false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int RafId { get; set; }

        [ReferansAlan("r.raf_kod", KeyField = true, KeyIndex = 0, SistemTipi = SistemTipi.Progress)]
        [Size(DbSize.KodLenght), VisibleInListView(false), VisibleInLookupListView(true)]
        public string RafKod { get; set; }

        [ReferansAlan("r.raf_ad", SistemTipi = SistemTipi.Progress)]
        [Size(DbSize.AciklamaLenght), VisibleInListView(false), VisibleInLookupListView(true)]
        public string Aciklama { get; set; }

        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int HiyerArsi { get; set; }

        [ReferansAlan("r.siralama", SistemTipi = SistemTipi.Progress)]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int Seviye { get; set; }

        [VisibleInListView(false), VisibleInLookupListView(false)]
        public bool Rapor { get; set; }

        [ReferansAlan("r.raf_kod", SistemTipi = SistemTipi.Progress)]
        [Size(DbSize.KodLenght), VisibleInLookupListView(true)]
        public string Barkod { get; set; }

        public bool Sayim { get; set; }

        [VisibleInListView(false), VisibleInLookupListView(false)]
        public bool EksiStok { get; set; }

        [VisibleInListView(false), VisibleInLookupListView(false)]
        public bool Sevkiyat { get; set; }

        [VisibleInListView(false), VisibleInLookupListView(false)]
        public bool IstasyonMu { get; set; }

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

        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int IsyeriId { get; set; }

        [Size(DbSize.KodLenght)]
        public string IsyeriKod { get; set; }

        [XmlIgnore(), Association(@"Sayimlar.Raf.RafId"), VisibleInDetailView(false), NoForeignKey]
        public XPCollection<Sayimlar> Sayimlar
        {
            get { return GetCollection<Sayimlar>(@"Sayimlar"); }
        }

        [XmlIgnore(), VisibleInDetailView(false), VisibleInListView(false), Association(@"MalHazirlamaDetaylari.Raflar_RafId")]
        public XPCollection<MalHazirlamaDetaylari> MalHazirlamaDetaylari
        {
            get { return GetCollection<MalHazirlamaDetaylari>(@"MalHazirlamaDetaylari"); }
        }

        [XmlIgnore(), VisibleInDetailView(false), VisibleInListView(false), Association(@"SevkiyatParametreleri.HedefRaf_Raf")]
        public XPCollection<SevkiyatParametreleri> SevkiyatParametreleri
        {
            get { return GetCollection<SevkiyatParametreleri>(@"SevkiyatParametreleri"); }
        }

        [XmlIgnore(), VisibleInDetailView(false), VisibleInListView(false), Association(@"Ambalajlar.RafIds_Raflar")]
        public XPCollection<Ambalajlar> Ambalajlar
        {
            get { return GetCollection<Ambalajlar>(@"Ambalajlar"); }
        }

        [XmlIgnore(), VisibleInDetailView(false), VisibleInListView(false), Association(@"_StokHareketDetaylari.HedefRaf_Raflar")]
        public XPCollection<StokHareketDetaylari> StokHareketDetaylariH
        {
            get { return GetCollection<StokHareketDetaylari>(@"StokHareketDetaylariH"); }
        }

        [XmlIgnore(), VisibleInDetailView(false), VisibleInListView(false), Association(@"_StokHareketDetaylari.KaynakRaf_Raflar")]
        public XPCollection<StokHareketDetaylari> StokHareketDetaylariK
        {
            get { return GetCollection<StokHareketDetaylari>(@"StokHareketDetaylariK"); }
        }

        [XmlIgnore(), VisibleInDetailView(false), VisibleInListView(false), Association(@"_RafHareketDetaylari.HedefRaf_Raflar")]
        public XPCollection<RafHareketDetaylari> RafHareketDetaylariH
        {
            get { return GetCollection<RafHareketDetaylari>(@"RafHareketDetaylariH"); }
        }

        [XmlIgnore(), VisibleInDetailView(false), VisibleInListView(false), Association(@"_RafHareketDetaylari.KaynakRaf_Raflar")]
        public XPCollection<RafHareketDetaylari> RafHareketDetaylariK
        {
            get { return GetCollection<RafHareketDetaylari>(@"RafHareketDetaylariK"); }
        }

        [XmlIgnore(), VisibleInDetailView(false), VisibleInListView(false), Association(@"AmbalajHareketDetaylari.HedefRaf_Raflar")]
        public XPCollection<AmbalajHareketDetaylari> AmbalajHareketH
        {
            get { return GetCollection<AmbalajHareketDetaylari>(@"AmbalajHareketH"); }
        }

        [XmlIgnore(), VisibleInDetailView(false), VisibleInListView(false), Association(@"AmbalajHareketDetaylari.KaynakRaf_Raflar")]
        public XPCollection<AmbalajHareketDetaylari> AmbalajHareketK
        {
            get { return GetCollection<AmbalajHareketDetaylari>(@"AmbalajHareketK"); }
        }

        [XmlIgnore(), VisibleInDetailView(false), VisibleInListView(false), Association(@"DepoStoklari.RafId_Raflar"), NoForeignKey]
        public XPCollection<DepoStoklari> DepoStoklari
        {
            get { return GetCollection<DepoStoklari>(@"DepoStoklari"); }
        }

        [XmlIgnore(), VisibleInDetailView(false), VisibleInListView(false), Association(@"RafStoklari.RafId_Raflar"), NoForeignKey]
        public XPCollection<RafStoklari> RafStoklari
        {
            get { return GetCollection<RafStoklari>(@"RafStoklari"); }
        }

        [XmlIgnore(), VisibleInDetailView(false), VisibleInListView(false), Association(@"Depolar.Depo_GirisRafId")]
        public XPCollection<Depolar> Depolar1
        {
            get { return GetCollection<Depolar>(@"Depolar1"); }
        }

        [XmlIgnore(), VisibleInDetailView(false), VisibleInListView(false), Association(@"Depolar.Depo_CikisRafId")]
        public XPCollection<Depolar> Depolar2
        {
            get { return GetCollection<Depolar>(@"Depolar2"); }
        }

        [XmlIgnore(), VisibleInDetailView(false), VisibleInListView(false), Association(@"Raflar.Aletler_AletId"), NoForeignKey]
        public XPCollection<Aletler> Aletler
        {
            get { return GetCollection<Aletler>(@"Aletler"); }
        }
        #region Butonlar
        [Action(Caption = "Guncelle", ImageName = "Action_Refresh", ToolTip = "Bilgileri guncelle..")]
        public void Entegrasyon()
        {
        }
        #endregion


        public Raflar() { }
        public Raflar(Session session) : base(session) { }

    }

    /*Uyum progress raflari iceri alirken kullanılan temp tablo*/    

    /*
     CREATE TABLE [dbo].[Temp_Raflar](
        [DepoId] [int] NULL,
        [RafId] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
        [RafKod] [nvarchar](64) NULL,
        [Aciklama] [nvarchar](128) NULL,
        [HiyerArsi] [int] NULL,
        [Seviye] [int] NULL,
        [Rapor] [bit] NULL,
        [Barkod] [nvarchar](64) NULL,
        [Sayim] [bit] NULL,
        [EksiStok] [bit] NULL,
        [Sevkiyat] [bit] NULL,
        [IstasyonMu] [bit] NULL,
        [Entegre] [bit] NULL,
        [Olusturan] [int] NULL,
        [OlusturmaTarihi] [datetime] NULL,
        [Guncelleyen] [int] NULL,
        [GuncellemeTarihi] [datetime] NULL,
        [KaynakModul] [nvarchar](128) NULL,
        [CihazNo] [nvarchar](24) NULL,
        [OptimisticLockField] [int] NULL,
     CONSTRAINT [PK_Temp_Raflar] PRIMARY KEY CLUSTERED 
    (
        [RafId] ASC
    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
    ) ON [PRIMARY]
     */
}

/*WEB ERP RAFLARI OLUSTURMAK ICIN SORGU*/
/***************************************/
/*
 SET NOCOUNT ON
IF OBJECT_ID('tempdb..#Raflar') IS NOT NULL
BEGIN
	DROP TABLE [#Raflar]
	DROP TABLE [#Raflar2]
	--DROP TABLE [#Raflar3]
	DROP TABLE [#Depolar]
	DROP TABLE [#Istasyonlar]
END
CREATE TABLE [#Raflar](
	--[OID] [int] IDENTITY(1,1) PRIMARY KEY,
	[DepoId] [int] NULL,
	[RafId] [int] NOT NULL,
	[RafKod] [nvarchar](64) NULL,
	[Aciklama] [nvarchar](128) NULL,
	[HiyerArsi] [int] NULL,
	[Seviye] [int] NULL,
	[Rapor] [bit] NULL,
	[Barkod] [nvarchar](64) NULL,
	[Sayim] [bit] NULL,
	[EksiStok] [bit] NULL,
	[Sevkiyat] [bit] NULL,
	[IstasyonMu] [bit] NULL,
	[Entegre] [bit] NULL,
	[Olusturan] [int] NULL,
	[OlusturmaTarihi] [datetime] NULL,
	[Guncelleyen] [int] NULL,
	[GuncellemeTarihi] [datetime] NULL,
	[KaynakModul] [nvarchar](128) NULL,
	[CihazNo] [nvarchar](24) NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL
) 
DECLARE @IDS INT = 108000
DECLARE @DepoId INT, @RafId INT, @RafKod NVARCHAR(30), @Aciklama NVARCHAR(128), @HiyerArsi INT, @Rapor BIT, @Barkod NVARCHAR(30), @EksiStok BIT, @Sevkiyat BIT, @IstasyonMu BIT
DECLARE @WHOUSE_ID INT, @WHOUSE_CODE NVARCHAR(30), @DESCRIPTION NVARCHAR(128), @ISNEGATIVE BIT, @LOCATION_CODE NVARCHAR(30)
DECLARE @WSTATION_ID INT, @WSTATION_CODE NVARCHAR(30), @BWH_LOCATION_ID INT, @LOCATION_HALL_NO NVARCHAR(30), @LOCATION_SECTION_NO NVARCHAR(30), @LOCATION_LAYER_NO NVARCHAR(30), @LOCATION_SEQUENCE_NO NVARCHAR(30)
DECLARE @LOCATION_HALL_NO_ID INT, @LOCATION_SECTION_NO_ID INT, @LOCATION_LAYER_NO_ID INT, @LOCATION_SEQUENCE_NO_ID INT
SELECT *, CAST(0 AS BIT) AS DURUM INTO #Depolar FROM Depolar ORDER BY WHOUSE_ID
WHILE (SELECT COUNT(*) FROM [#Depolar] WHERE DURUM = 0) > 0 
BEGIN
	SELECT TOP (1) @WHOUSE_ID = CONVERT(INT,WHOUSE_ID), @WHOUSE_CODE = WHOUSE_CODE, @DESCRIPTION = [DESCRIPTION], @ISNEGATIVE = CONVERT(BIT,ISNEGATIVE) FROM [#Depolar] WHERE DURUM = 0
	INSERT INTO [#Raflar] (DepoId, RafId, RafKod, Aciklama, HiyerArsi, Seviye, Rapor, Barkod, Sayim, EksiStok, Sevkiyat, IstasyonMu) VALUES
	(@WHOUSE_ID, @WHOUSE_ID, @WHOUSE_CODE, @DESCRIPTION, -1, 0, 0, @WHOUSE_CODE, 0, @ISNEGATIVE, 1, 0)
	UPDATE [#Depolar] SET DURUM = 1 WHERE WHOUSE_ID = @WHOUSE_ID
END

SELECT *, CAST(0 AS BIT) AS DURUM INTO [#Istasyonlar] FROM Istasyonlar WHERE ISOUTSIDE_WSTATION = 0 ORDER BY WSTATION_ID
WHILE (SELECT COUNT(*) FROM [#Istasyonlar] WHERE DURUM = 0) > 0
BEGIN
	SELECT TOP (1) @WSTATION_ID = WSTATION_ID, @WSTATION_CODE = WSTATION_CODE, @DESCRIPTION = [DESCRIPTION], @WHOUSE_ID = MTR_OUTPUT_WHOUSE_ID FROM [#Istasyonlar] WHERE DURUM = 0
	INSERT INTO [#Raflar] (DepoId, RafId, RafKod, Aciklama, HiyerArsi, Seviye, Rapor, Barkod, Sayim, EksiStok, Sevkiyat, IstasyonMu) VALUES
	(@WHOUSE_ID, @WSTATION_ID, @WSTATION_CODE, @DESCRIPTION, @WHOUSE_ID, 1, 0, @WSTATION_CODE, 0, @ISNEGATIVE, 1, 1)
	UPDATE [#Istasyonlar] SET DURUM = 1 WHERE WSTATION_ID = @WSTATION_ID
END


SELECT *, CAST(0 AS BIT) AS DURUM INTO [#Raflar2] FROM [dbo].[Raflar]

WHILE (SELECT COUNT(*) FROM [#Raflar2] WHERE DURUM = 0) > 0
BEGIN
	SELECT TOP (1) @WHOUSE_ID = CONVERT(INT, WHOUSE_ID), @BWH_LOCATION_ID = CONVERT(INT, BWH_LOCATION_ID), @LOCATION_CODE = LOCATION_CODE, @LOCATION_HALL_NO = LOCATION_HALL_NO, @LOCATION_SECTION_NO = LOCATION_SECTION_NO, @LOCATION_LAYER_NO = LOCATION_LAYER_NO, @LOCATION_SEQUENCE_NO = LOCATION_SEQUENCE_NO FROM [#Raflar2] WHERE DURUM = 0
	
	SELECT @LOCATION_HALL_NO_ID = RafId FROM [#Raflar] WHERE DepoId = @WHOUSE_ID AND RafKod = @LOCATION_HALL_NO	
	IF @LOCATION_HALL_NO_ID < 1 
	BEGIN			
		SELECT @LOCATION_HALL_NO_ID = @IDS, @IDS = @IDS + 1
		INSERT INTO [#Raflar] (DepoId, RafId, RafKod, Aciklama, HiyerArsi, Seviye, Rapor, Barkod, Sayim, EksiStok, Sevkiyat, IstasyonMu) VALUES
		(@WHOUSE_ID, @LOCATION_HALL_NO_ID, @LOCATION_HALL_NO, @LOCATION_HALL_NO, @WHOUSE_ID, 1, 1, @LOCATION_HALL_NO, 0, 0, 0, 0)
	END
	
	SELECT @LOCATION_SECTION_NO_ID = RafId FROM [#Raflar] WHERE DepoId = @WHOUSE_ID AND RafKod = @LOCATION_SECTION_NO AND HiyerArsi = @LOCATION_HALL_NO_ID	
	IF @LOCATION_SECTION_NO_ID < 1 
	BEGIN		  
		SELECT @LOCATION_SECTION_NO_ID = @IDS, @IDS = @IDS + 1
		INSERT INTO [#Raflar] (DepoId, RafId, RafKod, Aciklama, HiyerArsi, Seviye, Rapor, Barkod, Sayim, EksiStok, Sevkiyat, IstasyonMu) VALUES
		(@WHOUSE_ID, @LOCATION_SECTION_NO_ID, @LOCATION_SECTION_NO, @LOCATION_SECTION_NO, @LOCATION_HALL_NO_ID, 2, 1, @LOCATION_SECTION_NO, 0, 0, 0, 0)
	END
	
	SELECT @LOCATION_LAYER_NO_ID = RafId FROM [#Raflar] WHERE DepoId = @WHOUSE_ID AND RafKod = @LOCATION_LAYER_NO AND HiyerArsi = @LOCATION_SECTION_NO_ID	
	IF @LOCATION_LAYER_NO_ID < 1 
	BEGIN
		SELECT @LOCATION_LAYER_NO_ID = @IDS, @IDS = @IDS + 1	
		PRINT N'INSERT ' + @LOCATION_HALL_NO + '-' + @LOCATION_SECTION_NO + '-' + @LOCATION_LAYER_NO + ',' + CONVERT(NVARCHAR,@IDS)		
		INSERT INTO [#Raflar] (DepoId, RafId, RafKod, Aciklama, HiyerArsi, Seviye, Rapor, Barkod, Sayim, EksiStok, Sevkiyat, IstasyonMu) VALUES
		(@WHOUSE_ID, @LOCATION_LAYER_NO_ID, @LOCATION_LAYER_NO, @LOCATION_LAYER_NO, @LOCATION_SECTION_NO_ID, 3, 1, @LOCATION_LAYER_NO, 0, 0, 0, 0)
	END
	
	SELECT @LOCATION_SEQUENCE_NO_ID = RafId FROM [#Raflar] WHERE DepoId = @WHOUSE_ID AND RafKod = @LOCATION_SEQUENCE_NO AND HiyerArsi = @LOCATION_LAYER_NO_ID	
	IF @LOCATION_SEQUENCE_NO_ID < 1 
	BEGIN
		SELECT @LOCATION_SEQUENCE_NO_ID = @IDS, @IDS = @IDS + 1	
		IF EXISTS ( SELECT * FROM [#Raflar] WHERE RafId = @IDS ) BEGIN
			PRINT N'VAR! ' + CONVERT(NVARCHAR,@IDS)
		END	
		PRINT N'INSERT ' + @LOCATION_HALL_NO + '-' + @LOCATION_SECTION_NO + '-' + @LOCATION_LAYER_NO + '-' + @LOCATION_SEQUENCE_NO + ',' + CONVERT(NVARCHAR,@IDS)		
		INSERT INTO [#Raflar] (DepoId, RafId, RafKod, Aciklama, HiyerArsi, Seviye, Rapor, Barkod, Sayim, EksiStok, Sevkiyat, IstasyonMu) VALUES
		(@WHOUSE_ID, @LOCATION_SEQUENCE_NO_ID, @LOCATION_SEQUENCE_NO, @LOCATION_SEQUENCE_NO, @LOCATION_LAYER_NO_ID, 4, 1, @LOCATION_SEQUENCE_NO, 0, 0, 0, 0)
	END
	
	
	IF EXISTS ( SELECT * FROM [#Raflar] WHERE RafId = @BWH_LOCATION_ID ) BEGIN
		PRINT N'VAR! ' + CONVERT(NVARCHAR,@BWH_LOCATION_ID)
	END	
	INSERT INTO [#Raflar] (DepoId, RafId, RafKod, Aciklama, HiyerArsi, Seviye, Rapor, Barkod, Sayim, EksiStok, Sevkiyat, IstasyonMu) VALUES
	(@WHOUSE_ID, @BWH_LOCATION_ID, @LOCATION_CODE, @LOCATION_CODE, @LOCATION_LAYER_NO_ID, 5, 0, @LOCATION_CODE, 0, 0, 0, 0)
	
	UPDATE TOP (1) [#Raflar2] SET DURUM = 1 WHERE CONVERT(INT, BWH_LOCATION_ID) = @BWH_LOCATION_ID
	SELECT @LOCATION_LAYER_NO_ID = 0, @LOCATION_SECTION_NO_ID = 0, @LOCATION_HALL_NO_ID = 0, @LOCATION_SEQUENCE_NO_ID = 0
END


--INSERT INTO Mikrobar.dbo.Raflar
SELECT * FROM [#Raflar] ORDER BY DepoId, RafId

PRINT N'İŞLEM TAMAMLANDI.'
RETURN





 */