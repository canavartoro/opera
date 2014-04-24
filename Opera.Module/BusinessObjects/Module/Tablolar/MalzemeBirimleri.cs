using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using System.ComponentModel;
using System.Xml.Serialization;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;


namespace Mikrobar.Module.BusinessObjects
{
    [MikrobarTablo(2, "MALZEME_BIRIM", "GNL", "STOK KARTI BIRIM SETI TANIMLARI TABLOSU")]
    [ModelDefault("Caption", "MalzemeBirimSetleri")]
    [DeferredDeletion(false), OptimisticLocking(false), DefaultClassOptions, XafDefaultProperty("Birim"),
     ModelDefault("DefaultListViewShowAutoFilterRow", "True"), NavigationItem(false), ImageName("BO_Role")]
    [ReferansTablo(TabloAdi = "INVD_ITEM_UNIT", QueryType = QueryType.Yoksa, SistemTipi = SistemTipi.WebErp, 
        SqlSorgu = @"SELECT U.ITEM_UNIT_ID AS ""MalzemeBirimId"", U.UNIT2_ID AS ""BirimId"", U.UNIT2_ID AS ""Birim2Id"", U.ITEM_ID AS ""Malzeme"", 1 AS ""Miktar"", CAST(U.RATE2/U.RATE AS DECIMAL(7,4)) AS ""Miktar2"",U.LINE_NO AS ""SiraNo"", U.WIDTH AS ""En"", U.HEIGHT AS ""Boy"", U.HEIGHT AS ""Yukekseklik"",U.VOLUME AS ""Hacim"", U.WEIGHT AS ""Agirlik"", U.DEPTH AS ""Derinlik"" FROM INVD_BRANCH_ITEM B INNER JOIN INVD_ITEM_UNIT U ON B.ITEM_ID = U.ITEM_ID WHERE B.CO_ID = #Firma# AND B.BRANCH_ID = #Isyeri# ",
        SqlWhere = @" AND U.UNIT2_ID = {0} AND U.ITEM_ID = {1} ")]
    [ReferansTablo(TabloAdi = @" pub.stok_birim t, pub.birim b ",
        SqlSorgu = @"select cast(pub.stok_birim.rowid as int) as ""MalzemeBirimId"", cast(pub.birim.rowid as int) as ""BirimId"", cast(pub.stok_kart.rowid as int) as ""Malzeme"", 
case when pub.stok_birim.oran2 = 0 then 1 else pub.stok_birim.oran2 end as ""Miktar"", case when pub.stok_birim.oran2 = 0 then 1 else pub.stok_birim.oran / pub.stok_birim.oran2 end as ""Miktar2"", 
pub.stok_birim.sira_no as ""SiraNo"", pub.stok_birim.en as ""En"", pub.stok_birim.boy as ""Boy"", pub.stok_birim.yukseklik as ""Yukekseklik"", pub.stok_birim.hacim as ""Hacim"", 
pub.stok_birim.agirlik as ""Agirlik"", 0 as ""Derinlik"" from pub.stok_kart inner join pub.stok_birim on pub.stok_kart.firma_kod = pub.stok_birim.firma_kod and pub.stok_kart.stok_kod = pub.stok_birim.stok_kod   	
left outer join pub.birim on pub.stok_birim.birim = pub.birim.birim and pub.stok_birim.firma_kod = pub.birim.firma_kod where pub.stok_kart.firma_kod = '#FirmaKod#' ",
        SqlWhere = @" and cast(pub.birim.rowid as int) = {0} and cast(pub.stok_kart.rowid as int) = {1} ",
        SistemTipi = SistemTipi.Progress, QueryType = QueryType.Yoksa)]
    public class MalzemeBirimleri : XPBaseObject
    {
        [ReferansAlan("cast(t.rowid as int)", SistemTipi=SistemTipi.Progress)]
        [Key(AutoGenerate = false), ModelDefault("DisplayFormat", "d")]
        public int MalzemeBirimId { get; set; }     

        #region Birim
        [ReferansAlan("cast(b.rowid as int)", SistemTipi = SistemTipi.Progress, KeyField=true,KeyIndex=1)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int BirimId { get; set; }

        [ReferansAlan("b.birim", SistemTipi = SistemTipi.Progress)]
        [Size(DbSize.KodLenght), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(true)]
        public string Birim { get; set; }

        protected Birimler _Birim;
        [NonPersistent, XmlIgnore(), VisibleInLookupListView(false)]
        [ImmediatePostData, XafDisplayName("Birim")]
        public Birimler MalzemeBirim
        {
            get
            {
                if (_Birim == null && this.BirimId > 0)
                    this._Birim = this.Session.FindObject<Birimler>(CriteriaOperator.Parse(" BirimId = ?", this.BirimId));
                return _Birim;
            }
            set
            {
                this.BirimId = value != null ? value.BirimId : 0;
                this.Birim = value != null ? value.Birim : string.Empty;
                SetPropertyValue("MalzemeBirim", ref _Birim, value);
            }
        }
        #endregion                        

        #region Malzeme Bilgisi
        //protected Malzemeler _malzeme;
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false), PersistentAlias("Iif(Malzeme is null, 0, Malzeme.MalzemeId)")]
        public int MalzemeId
        {
            get { return Convert.ToInt32(EvaluateAlias("MalzemeId")); }
        }

        [ReferansAlan("s.stok_kod", SistemTipi = SistemTipi.Progress)]
        [ModelDefault("AllowEdit", "false"), ReadOnly(true), Size(DbSize.KodLenght), VisibleInLookupListView(true)]
        public string MalzemeKod { get; set; }

        //[XmlIgnore(), NonPersistent, XafDisplayName("Malzeme Bilgisi"),
        //VisibleInListView(true), VisibleInLookupListView(false), DataSourceCriteria("MalzemeId = '@This.MalzemeId'")]
        //public Malzemeler Malzeme
        //{
        //    get
        //    {
        //        if (_malzeme == null && this.MalzemeId > 0)
        //            this._malzeme = this.Session.GetObjectByKey<Malzemeler>(this.MalzemeId);
        //        return _malzeme;
        //    }
        //    set
        //    {
        //        SetPropertyValue("Malzeme", ref _malzeme, value);
        //    }
        //}

        Malzemeler fMalzeme;
        [ReferansAlan("cast(s.rowid as int)", SistemTipi = SistemTipi.Progress, KeyField = true, KeyIndex = 0)]
        [XmlIgnore(), Association(@"Malzemeler.MalzemeBirimleri.MalzemeId"), NoForeignKey, Persistent("MalzemeId"), Indexed("BirimId")]
        public Malzemeler Malzeme
        {
            get { return fMalzeme; }
            set { SetPropertyValue<Malzemeler>("Malzeme", ref fMalzeme, value); }
        }
        #endregion

        //[Association(@"Malzemeler.MalzemeBirimleri"), XmlIgnore()]
        //public Malzemeler Malzeme { get; set; }

        //[Association(@"MalzemeBirimleri.Brm_Birimler"), XmlIgnore(), Persistent("BirimId")]
        //public Birimler Birimler { get; set; }


        [ReferansAlan("t.oran2", SistemTipi = SistemTipi.Progress)]
        [DbType(" DECIMAL(18,4) "), ModelDefault ("EditMask", "c")]
        public decimal Miktar { get; set; }

        [ReferansAlan("case when t.oran > 0 then t.oran/t.oran2 else 0 end", SistemTipi = SistemTipi.Progress)]
        [DbType(" DECIMAL(18,4) "), ModelDefault("EditMask", "c")]
        public decimal Miktar2 { get; set; }

        [ReferansAlan("t.sira_no", SistemTipi = SistemTipi.Progress)]
        public int SiraNo { get; set; }

        [ReferansAlan("t.en", SistemTipi = SistemTipi.Progress)]
        [DbType(" DECIMAL(18,4) "), ModelDefault("EditMask", "c")]
        public decimal En { get; set; }

        [ReferansAlan("t.boy", SistemTipi = SistemTipi.Progress)]
        [DbType(" DECIMAL(18,4) "), ModelDefault("EditMask", "c")]
        public decimal Boy { get; set; }

        [ReferansAlan("t.yukseklik", SistemTipi = SistemTipi.Progress)]
        [DbType(" DECIMAL(18,4) "), ModelDefault("EditMask", "c")]
        public decimal Yukseklik { get; set; }

        [ReferansAlan("t.hacim", SistemTipi = SistemTipi.Progress)]
        [DbType(" DECIMAL(18,4) "), ModelDefault("EditMask", "c")]
        public decimal Hacim { get; set; }

        [ReferansAlan("t.agirlik", SistemTipi = SistemTipi.Progress)]
        [DbType(" DECIMAL(18,4) "), ModelDefault("EditMask", "c")]
        public decimal Agirlik { get; set; }

        [DbType(" DECIMAL(18,4) "), ModelDefault("EditMask", "c")]
        public decimal Derinlik { get; set; }

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

        protected override void OnSaving()
        {
            #region IsNotDelete
            if (this.IsDeleted == false)
            {
                if (object.ReferenceEquals(null, this.Malzeme)) throw new Exception("Malzeme bilgisi bos birakilamaz!");
                this.MalzemeKod = this.Malzeme.MalzemeKod;
                SistemKullanicilari currentUser = SecuritySystem.CurrentUser as SistemKullanicilari;
                if (this.MalzemeBirimId < 1)
                {
                    object xMalzemeBirimId = Session.Evaluate<MalzemeBirimleri>(CriteriaOperator.Parse("Max(MalzemeBirimId)"), null);
                    this.MalzemeBirimId = Convert.ToInt32(xMalzemeBirimId) + 1;
                    this.Olusturan = currentUser != null ? currentUser.KullaniciId : 0;
                    this.OlusturmaTarihi = DateTime.Now;
                    this.KaynakProgram = BusinessObjects.KaynakProgram.EtiketBasim;
                    this.KaynakModul = GetType().Name;
                }
                else
                {
                    this.Guncelleyen = currentUser != null ? currentUser.KullaniciId : 0;
                    this.GuncellemeTarihi = DateTime.Now;
                }
            }
            #endregion
            base.OnSaving();
        }

        public MalzemeBirimleri() { }
        public MalzemeBirimleri(Session session) : base(session) { }

        public decimal BirimDonusum(int hbirimId, decimal miktar)
        {
            // this gr  = kaynak birim
            //kg = hedef
           // MalzemeBirimleri hedefbirim = ObjectQuery.GetObjectsByQuery<MalzemeBirimleri>(hbirimId, this.MalzemeId);
            MalzemeBirimleri hedefbirim = this.Session.FindObject<MalzemeBirimleri>(CriteriaOperator.Parse("MalzemeId = ? AND BirimId = ?", this.MalzemeId, hbirimId));
            if (hedefbirim == null)
                throw new Exception("Birim donusumunde hata hedef birim bulunamadi! " + hbirimId);
            
            return (miktar / this.Miktar2) * hedefbirim.Miktar2;

            /*if (this.Miktar == this.Miktar2)
            {
                return (miktar / hedefbirim.Miktar2) * this.Miktar2;
            }
            else
            {
                return (Miktar / this.Miktar2) * hedefbirim.Miktar2;
            }*/
            /*
         (ÇevrilmekİstenenMiktar/HedefBirimMiktar2si)* kaynakBirimMiktar2si
=hedefbirimCinsindenMiktar


100 metreyi   10  kg  ma  cevirek .
X =  (100/kgMiktar2) * metreninMiktar2 

         */
        }       

    }

    #region PROGRESS LINKSERVER SQL PROCEDURE

 /*
   
--EXEC T_MalzemeBirimleri '497574'

ALTER PROCEDURE [dbo].[T_MalzemeBirimleri]
(
	@MALZEMEID INTEGER,
	@FIRMAKOD NVARCHAR(30) = 'ARMA2011'
)
AS BEGIN

DECLARE @TIRNAK CHAR(1)
DECLARE @SQLCOMMAND NVARCHAR(1500)

IF LEN(@FIRMAKOD) < 1 BEGIN SET @FIRMAKOD = 'ARMA2011' END

SET @TIRNAK = ''''
SET @SQLCOMMAND = ''
SET @SQLCOMMAND = @SQLCOMMAND + ' DECLARE @XT dbo.T_MalzemeBirimleri INSERT INTO  @XT '
SET @SQLCOMMAND = @SQLCOMMAND + ' SELECT * FROM OPENQUERY(UYUMDB, ''SELECT CAST(pub.stok_birim.rowid AS INT) AS "MalzemeBirimId" '
SET @SQLCOMMAND = @SQLCOMMAND + ', CAST(pub.birim.rowid AS INT) AS "BirimId" '
SET @SQLCOMMAND = @SQLCOMMAND + ', 0 AS "Birim2Id" '
SET @SQLCOMMAND = @SQLCOMMAND + ', CAST(pub.stok_kart.rowid AS INT) AS "MalzemeId" '
SET @SQLCOMMAND = @SQLCOMMAND + ', pub.stok_birim.oran2 AS "Miktar" '
SET @SQLCOMMAND = @SQLCOMMAND + ', CASE WHEN pub.stok_birim.oran2 > 0 THEN pub.stok_birim.oran/pub.stok_birim.oran2 ELSE pub.stok_birim.oran END AS "Miktar2" '
SET @SQLCOMMAND = @SQLCOMMAND + ', pub.stok_birim.sira_no AS "SiraNo" '
SET @SQLCOMMAND = @SQLCOMMAND + ', pub.stok_birim.en AS "En" '
SET @SQLCOMMAND = @SQLCOMMAND + ', pub.stok_birim.boy AS "Boy" '
SET @SQLCOMMAND = @SQLCOMMAND + ', pub.stok_birim.yukseklik AS "Yukekseklik" '
SET @SQLCOMMAND = @SQLCOMMAND + ', pub.stok_birim.hacim AS "Hacim" '
SET @SQLCOMMAND = @SQLCOMMAND + ', pub.stok_birim.agirlik AS "Agirlik" '
SET @SQLCOMMAND = @SQLCOMMAND + ', 0 as "Derinlik" '
SET @SQLCOMMAND = @SQLCOMMAND + ' FROM pub.stok_kart '

SET @SQLCOMMAND = @SQLCOMMAND + ' INNER JOIN pub.stok_birim '
SET @SQLCOMMAND = @SQLCOMMAND + ' ON pub.stok_kart.firma_kod = pub.stok_birim.firma_kod AND pub.stok_kart.stok_kod = pub.stok_birim.stok_kod '
SET @SQLCOMMAND = @SQLCOMMAND + ' LEFT OUTER JOIN pub.birim '
SET @SQLCOMMAND = @SQLCOMMAND + ' ON pub.stok_birim.birim = pub.birim.birim AND pub.stok_birim.firma_kod = pub.birim.firma_kod '

SET @SQLCOMMAND = @SQLCOMMAND + ' WHERE pub.stok_kart.firma_kod = ' + @TIRNAK + @TIRNAK + @FIRMAKOD + @TIRNAK + @TIRNAK + ' '


IF @MALZEMEID > 0 BEGIN
	SET @SQLCOMMAND = @SQLCOMMAND + ' AND CAST(pub.stok_kart.rowid AS INT) = ' + CONVERT(NVARCHAR(30),@MALZEMEID)
END

SET @SQLCOMMAND = @SQLCOMMAND + ' ORDER BY CAST(pub.stok_birim.rowid AS INT) ' + @TIRNAK + ') '
SET @SQLCOMMAND = @SQLCOMMAND + ' '
SET @SQLCOMMAND = @SQLCOMMAND + ' SELECT MalzemeBirimId, BirimId, Birim2Id, MalzemeId, Miktar, Miktar2, SiraNo, En, Boy, Yukseklik, Hacim, Agirlik, Derinlik FROM @XT '

PRINT @SQLCOMMAND

EXEC(@SQLCOMMAND)

END



*/






 
/*
CREATE TYPE T_MalzemeBirimleri AS TABLE
(
	MalzemeBirimId INTEGER,
	BirimId INTEGER,
	Birim2Id INTEGER,
	MalzemeId INTEGER,
	Miktar DECIMAL(8,4),
	Miktar2 DECIMAL(8,4),
	SiraNo INTEGER,
	En DECIMAL(8,4),
	Boy DECIMAL(8,4),
	Yukseklik DECIMAL(8,4),
	Hacim DECIMAL(8,4),
	Agirlik DECIMAL(8,4),
	Derinlik DECIMAL(8,4)
)
*/
    #endregion

    #region ORACLE LINKSERVER SQL PROCEDURE
    /*

    ALTER PROCEDURE [dbo].[MalzemeBirimleriBul]
(		
	@MALZEMEID INTEGER
)

AS BEGIN
DECLARE @TIRNAK CHAR(1)
DECLARE @SQLCOMMAND NVARCHAR(1300)

SET @TIRNAK = ''''
SET @SQLCOMMAND = ''
SET @SQLCOMMAND = @SQLCOMMAND + ' DECLARE @XT dbo.T_MalzemeBirimleri INSERT INTO  @XT '
SET @SQLCOMMAND = @SQLCOMMAND + ' SELECT * FROM OPENQUERY(UYUMDB, ''SELECT MB.ITEM_UNIT_ID AS "MalzemeBirimId" '
SET @SQLCOMMAND = @SQLCOMMAND + ', MB.UNIT_ID AS "BirimId" '
SET @SQLCOMMAND = @SQLCOMMAND + ', MB.UNIT2_ID AS "Birim2Id" '
SET @SQLCOMMAND = @SQLCOMMAND + ', MB.ITEM_ID AS "MalzemeId" '
SET @SQLCOMMAND = @SQLCOMMAND + ', MB.RATE AS "Miktar" '
SET @SQLCOMMAND = @SQLCOMMAND + ', MB.RATE2 AS "Miktar2" '
SET @SQLCOMMAND = @SQLCOMMAND + ', MB.LINE_NO AS "SiraNo" '
SET @SQLCOMMAND = @SQLCOMMAND + ', MB.WIDTH AS "En" '
SET @SQLCOMMAND = @SQLCOMMAND + ', MB.HEIGHT AS "Boy" '
SET @SQLCOMMAND = @SQLCOMMAND + ', MB.HEIGHT AS "Yukseklik" '
SET @SQLCOMMAND = @SQLCOMMAND + ', MB.VOLUME AS "Hacim" '
SET @SQLCOMMAND = @SQLCOMMAND + ', MB.WEIGHT AS "Agirlik" '
SET @SQLCOMMAND = @SQLCOMMAND + ', MB.DEPTH AS "Derinlik" '
SET @SQLCOMMAND = @SQLCOMMAND + ' FROM INVD_ITEM_UNIT MB '
SET @SQLCOMMAND = @SQLCOMMAND + ' WHERE '

IF @MALZEMEID > 0 BEGIN
	SET @SQLCOMMAND = @SQLCOMMAND + ' MB.ITEM_ID = ' + CONVERT(NVARCHAR(30),@MALZEMEID)
END

SET @SQLCOMMAND = @SQLCOMMAND + ' ORDER BY MB.ITEM_UNIT_ID ' + @TIRNAK + ') '
SET @SQLCOMMAND = @SQLCOMMAND + ' '
SET @SQLCOMMAND = @SQLCOMMAND + ' SELECT MalzemeBirimId, BirimId, Birim2Id, MalzemeId, Miktar, Miktar2, SiraNo, En, Boy, Yukseklik, Hacim, Agirlik, Derinlik FROM @XT '

PRINT @SQLCOMMAND

EXEC(@SQLCOMMAND)

END





/*
CREATE TYPE T_MalzemeBirimleri AS TABLE
(
	MalzemeBirimId INTEGER,
	BirimId INTEGER,
	Birim2Id INTEGER,
	MalzemeId INTEGER,
	Miktar DECIMAL(8,4),
	Miktar2 DECIMAL(8,4),
	SiraNo INTEGER,
	En DECIMAL(8,4),
	Boy DECIMAL(8,4),
	Yukseklik DECIMAL(8,4),
	Hacim DECIMAL(8,4),
	Agirlik DECIMAL(8,4),
	Derinlik DECIMAL(8,4)
)
*/
    #endregion

}
