using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Xml.Serialization;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [DeferredDeletion(false), OptimisticLocking(false)]
    public class T_Etiket : XPBaseObject
    {
        [Key(AutoGenerate = false)]
        public decimal OID { get; set; }
        public string IsEmri { get; set; }

        public int MalzemeId { get; set; }
        public string StokKod { get; set; }
        public string StokAd { get; set; }
        public string Barkod { get; set; }

        public string OperasyonKod { get; set; }
        public string OperasyonAd { get; set; }

        public string IstasyonKod { get; set; }
        public string IstasyonAd { get; set; }

        public int BirimId { get; set; }
        public string Birim { get; set; }

        public DateTime Tarih { get; set; }
        public string UstAmbalaj { get; set; }
        [DbType(" DECIMAL(18,4) ")]
        public decimal Miktar { get; set; }
        [DbType(" DECIMAL(18,4) ")]
        public decimal Kalan { get; set; }
        public string Tip { get; set; }
        public int SiraNo { get; set; }
        public AmbalajDurumu AmbalajDurum { get; set; }
        public MalzemeTip MalzemeTip { get; set; }
        public string Aciklama { get; set; }
        public string Saat { get; set; }

        public string PartiNo { get; set; }
        public string RenkNo { get; set; }

        public string BelgeNo { get; set; }


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

        public T_Etiket() { }
        public T_Etiket(Session session) : base(session) { }
    }
}

/*LinkSQL
 
--EXECUTE dbo.T_EtiketSorgula N'28982325'

ALTER PROCEDURE [dbo].[T_EtiketSorgula]
(
	@BARKOD NVARCHAR(40)
)
AS BEGIN

DECLARE @COMMAND NVARCHAR(1400), @TIRNAK CHAR(1) --, @BARKOD NVARCHAR(40) SET @BARKOD = '28982325'
SET @TIRNAK = ''''
SET @COMMAND = ''
SET @COMMAND = @COMMAND + 'SELECT * FROM OpenQuery(UYUMDB, ''SELECT top 1 EDT.erp_detayno2 AS "OID" '
SET @COMMAND = @COMMAND + ', EDT.isemri_no as "IsEmri"'
SET @COMMAND = @COMMAND + ', CAST(BRM.rowid as int) as "MalzemeId"'
SET @COMMAND = @COMMAND + ', EDT.stok_kod as "StokKod"'
SET @COMMAND = @COMMAND + ', EDT.stok_ad as "StokAd"'
SET @COMMAND = @COMMAND + ', cast(EDT.erp_detayno2 as varchar(30)) as "Barkod"'
SET @COMMAND = @COMMAND + ', EDT.operasyon_kod as "OperasyonKod"'
SET @COMMAND = @COMMAND + ', EDT.operasyon_ad as "OperasyonAd"'
SET @COMMAND = @COMMAND + ', EDT.istasyon_kod2 as "IstasyonKod"'
SET @COMMAND = @COMMAND + ', EDT.istasyon_ad as "IstasyonAd"'
SET @COMMAND = @COMMAND + ', CAST(BRM.rowid as int) as "BirimId"'
SET @COMMAND = @COMMAND + ', EDT.dbirim as "Birim"'
SET @COMMAND = @COMMAND + ', EDT.bas_tarih as "Tarih"' --bas_tarih
SET @COMMAND = @COMMAND + ', '''''''' as "UstAmbalaj"'
SET @COMMAND = @COMMAND + ', EDT.burut_miktar as "Miktar"'
SET @COMMAND = @COMMAND + ', EDT.dmiktar as "Kalan"'
SET @COMMAND = @COMMAND + ', EDT.urun_tip as "Tip"'
SET @COMMAND = @COMMAND + ', EDT.sira_no as "SiraNo"'
SET @COMMAND = @COMMAND + ', 0 as "AmbalajDurum"'
SET @COMMAND = @COMMAND + ', 0 as "MalzemeTip"'
SET @COMMAND = @COMMAND + ', EDT.create_user as "Aciklama"'
SET @COMMAND = @COMMAND + ', EDT.bas_zaman2 as "Saat"'
SET @COMMAND = @COMMAND + ', EDT.parti_kod as "PartiNo"'
SET @COMMAND = @COMMAND + ', EDT.renk_no as "RenkNo"'
SET @COMMAND = @COMMAND + ', EDT.vardiya_kod as "BelgeNo"'
SET @COMMAND = @COMMAND + ', 0 as "Olusturan"'
SET @COMMAND = @COMMAND + ', EDT.create_date AS "OlusturmaTarihi"'
SET @COMMAND = @COMMAND + ', 0 as "Guncelleyen"'
SET @COMMAND = @COMMAND + ', EDT.create_date AS "GuncellemeTarihi"'
SET @COMMAND = @COMMAND + ', '''''''' AS "KaynakModul"'
SET @COMMAND = @COMMAND + ', '''''''' AS "CihazNo"'
--SET @COMMAND = @COMMAND + ', ''''False'''' AS "Entegre"'
SET @COMMAND = @COMMAND + ' from pub.erp_detay2 EDT INNER JOIN pub.stok_kart SKART ON EDT.stok_kod = SKART.stok_kod INNER JOIN pub.birim BRM ON EDT.firma_kod = BRM.firma_kod where EDT.firma_kod = ''''ARMA2011'''' '
SET @COMMAND = @COMMAND + ''
SET @COMMAND = @COMMAND + ''
SET @COMMAND = @COMMAND + ''
SET @COMMAND = @COMMAND + ' and erp_detayno2 = ' + @BARKOD + ''') '

            
PRINT @COMMAND
EXEC(@COMMAND)
 
END     
 

 


*/

/*
 SORGU
INSERT INTO Mikrobar.dbo.T_Etiket (OID, IsEmri, StokKod, StokAd, Barkod, Miktar, Kalan, Tarih, Saat, 
OperasyonKod, OperasyonAd, IstasyonKod, IstasyonAd, UstAmbalaj, Aciklama, AmbalajDurum, SiraNo)
SELECT TOP 1000 ID AS OID,
      ISEMRI AS IsEmri,
      STOKKODU AS StokKod,
      STOKADI AS StokAd,
      HIDROMAX.Barset.PadLeft(ID,'0', 12) AS Barkod,
      MIKTAR AS Miktar,
      KALAN AS Kalan,
      CONVERT(DATETIME, TARIH, 103) AS Tarih,
      SAAT AS Saat,
      OPERASYONKOD AS OperasyonKod,
      OPERASYONAD AS OperasyonAd,
      TEZGAHKOD AS IstasyonKod,
      TEZGAHAD AS IstasyonAd,
      ATAKASA AS UstAmbalaj,
      ACIKLAMA AS Aciklama,      
      DURUM AS AmbalajDurum,      
      E_SIRA AS SiraNo
  FROM HIDROMAX.dbo.ETIKET WITH (NOLOCK) WHERE DURUM <> 'C' ORDER BY ID DESC
 */

/*
CREATE TABLE [dbo].[ETIKET](
	[ID] [bigint] NOT NULL,
	[ISEMRI] [varchar](12) NOT NULL,
	[STOKKODU] [varchar](12) NULL,
	[STOKADI] [varchar](50) NULL,
	[MIKTAR] [float] NULL,
	[KALAN] [float] NULL,
	[TARIH] [varchar](10) NULL,
	[SAAT] [varchar](5) NULL,
	[INDEX_OPERASYON] [varchar](255) NULL,
	[OPERASYONKOD] [varchar](12) NULL,
	[OPERASYONAD] [varchar](50) NULL,
	[TEZGAHKOD] [varchar](12) NULL,
	[TEZGAHAD] [varchar](50) NULL,
	[ATAKASA] [varchar](50) NULL,
	[ACIKLAMA] [nvarchar](50) NULL,
	[F_GIDEN] [float] NULL,
	[F_GELEN] [float] NULL,
	[G_DEPO] [varchar](50) NULL,
	[C_DEPO] [varchar](50) NULL,
	[DURUM] [varchar](1) NULL,
	[U_ID] [int] NULL,
	[E_SIRA] [int] NULL,
	[TIP] [char](1) NULL,
	[KONTROL] [int] NOT NULL,
 CONSTRAINT [PK_ETIKET] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ETIKET] ADD  CONSTRAINT [DF_ETIKET_MIKTAR]  DEFAULT ((0)) FOR [MIKTAR]
GO

ALTER TABLE [dbo].[ETIKET] ADD  CONSTRAINT [DF_ETIKET_KALAN]  DEFAULT ((0)) FOR [KALAN]
GO

ALTER TABLE [dbo].[ETIKET] ADD  CONSTRAINT [DF_ETIKET_ATAKASA]  DEFAULT ((0)) FOR [ATAKASA]
GO

ALTER TABLE [dbo].[ETIKET] ADD  CONSTRAINT [DF_ETIKET_F_GIDEN]  DEFAULT ((0)) FOR [F_GIDEN]
GO

ALTER TABLE [dbo].[ETIKET] ADD  CONSTRAINT [DF_ETIKET_F_GELEN]  DEFAULT ((0)) FOR [F_GELEN]
GO

ALTER TABLE [dbo].[ETIKET] ADD  CONSTRAINT [DF_ETIKET_DURUM]  DEFAULT ((0)) FOR [DURUM]
GO

ALTER TABLE [dbo].[ETIKET] ADD  CONSTRAINT [DF_ETIKET_U_ID]  DEFAULT ((0)) FOR [U_ID]
GO

ALTER TABLE [dbo].[ETIKET] ADD  CONSTRAINT [DF_ETIKET_BORU]  DEFAULT ('H') FOR [TIP]
GO
 */

/*
 CREATE TABLE [dbo].[URUN_SERI](
	[ID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[SERINO] [varchar](50) NOT NULL,
	[STOKKOD] [varchar](50) NULL,
	[STOKAD] [varchar](50) NULL,
	[ISEMRI] [varchar](50) NULL,
	[TARIH] [datetime] NULL,
	[URETIMID] [varchar](50) NULL,
	[DEPO] [varchar](50) NULL,
	[BELGE_NO] [varchar](50) NULL,
	[MASTER] [varchar](30) NULL,
	[DETAY] [varchar](30) NULL,
	[KAYNAK] [varchar](50) NULL,
	[PAKET] [varchar](50) NULL,
	[BARKOD] [varchar](50) NULL,
	[RENK] [nvarchar](30) NULL,
	[PARTI] [nvarchar](30) NULL,
	[FLAG] [char](1) NULL,
	[AKTARIM] [int] NULL,
 CONSTRAINT [PK_URUN_SERI] PRIMARY KEY CLUSTERED 
(
	[SERINO] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[URUN_SERI] ADD  CONSTRAINT [DF_URUN_SERI_TARIH]  DEFAULT (getdate()) FOR [TARIH]
GO

ALTER TABLE [dbo].[URUN_SERI] ADD  CONSTRAINT [DF_URUN_SERI_DEPO]  DEFAULT ('Z-PAKET') FOR [DEPO]
GO

ALTER TABLE [dbo].[URUN_SERI] ADD  CONSTRAINT [DF_URUN_SERI_MASTER]  DEFAULT ('') FOR [MASTER]
GO

ALTER TABLE [dbo].[URUN_SERI] ADD  CONSTRAINT [DF_URUN_SERI_DETAY]  DEFAULT ('') FOR [DETAY]
GO

ALTER TABLE [dbo].[URUN_SERI] ADD  CONSTRAINT [DF_URUN_SERI_RENK]  DEFAULT ('') FOR [RENK]
GO

ALTER TABLE [dbo].[URUN_SERI] ADD  CONSTRAINT [DF_URUN_SERI_PARTI]  DEFAULT ('') FOR [PARTI]
GO

ALTER TABLE [dbo].[URUN_SERI] ADD  CONSTRAINT [DF_URUN_SERI_FLAG]  DEFAULT ('H') FOR [FLAG]
GO

ALTER TABLE [dbo].[URUN_SERI] ADD  CONSTRAINT [DF_URUN_SERI_AKTARIM]  DEFAULT ((0)) FOR [AKTARIM]
GO
 */

/*
 CREATE TABLE [dbo].[URUN_DETAY](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SERINO] [varchar](50) NULL,
	[STOKAD] [varchar](50) NULL,
	[STOKKOD] [varchar](50) NULL,
	[KASA] [varchar](50) NULL,
	[MIKTAR] [int] NULL,
	[TARIH] [datetime] NULL,
	[URETIMID] [varchar](50) NULL,
 CONSTRAINT [PK_URUN_DETAY] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[URUN_DETAY] ADD  CONSTRAINT [DF_URUN_DETAY_MIKTAR]  DEFAULT (1) FOR [MIKTAR]
GO

ALTER TABLE [dbo].[URUN_DETAY] ADD  CONSTRAINT [DF_URUN_DETAY_TARIH]  DEFAULT (getdate()) FOR [TARIH]
GO
 */




/*
 * PROGRESS SQL SORGU
 * 
 SELECT TOP 1 
EDT.erp_detayno2 AS "OID", 
EDT.isemri_no as "IsEmri", 
SKART.stok_kod as "MalzemeId",
EDT.stok_kod as "StokKod", 
EDT.stok_ad as "StokAd", 
cast(EDT.erp_detayno2 as varchar(30)) as "Barkod", 
EDT.operasyon_kod as "OperasyonKod", 
EDT.operasyon_ad as "OperasyonAd", 
EDT.istasyon_kod2 as "IstasyonKod", 
EDT.istasyon_ad as "IstasyonAd", 
CAST(BRM.rowid as int) as "BirimId",
EDT.dbirim as "Birim", 
EDT.bas_tarih as "Tarih", 
'' as "UstAmbalaj", 
EDT.burut_miktar as "Miktar", 
EDT.dmiktar as "Kalan", 
EDT.urun_tip as "Tip", 
EDT.sira_no as "SiraNo", 
0 as "AmbalajDurum", 
0 as "MalzemeTip", 
EDT.create_user as "Aciklama", 
EDT.bas_zaman2 as "Saat", 
EDT.parti_kod as "PartiNo", 
EDT.renk_no as "RenkNo", 
EDT.vardiya_kod as "BelgeNo", 
0 as "Olusturan", 
EDT.create_date AS "OlusturmaTarihi", 
0 as "Guncelleyen", 
EDT.create_date AS "GuncellemeTarihi", 
'' AS "KaynakModul", 
'' AS "CihazNo" 
FROM pub.erp_detay2 EDT
INNER JOIN pub.stok_kart SKART
ON EDT.stok_kod = SKART.stok_kod
INNER JOIN pub.birim BRM
ON EDT.firma_kod = BRM.firma_kod
WHERE EDT.firma_kod = 'ARMA2011' AND erp_detayno2 = 28982325
 */