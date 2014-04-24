using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using DevExpress.Xpo;
using System.Xml.Serialization;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    /// <summary>
    /// Is Emrinde kullanilan malzemeler (dmiktar)
    /// </summary>
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("IsEmriNo"), NavigationItem(false),
     ImageName("BO_Organization"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    [ReferansTablo("PRDT_WORDER_BOM_D", SistemTipi.WebErp, true)]
    [ReferansTablo("erp_recete", SistemTipi.Progress, true)]
    public class IsEmriBilesenleri : XPBaseObject
    {
        [Key(false), ModelDefault("DisplayFormat","d")]
        public int IsEmriBilesenId { get; set; }

        [ModelDefault("DisplayFormat", "d"),VisibleInListView(false),VisibleInDetailView(false),VisibleInLookupListView(false)]
        public int ReceteId { get; set; }
        //Bazi alanlar web erp de kullanilacak
        [ModelDefault("DisplayFormat", "d"), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int IsEmriId { get; set; }

        [Size(DbSize.NoLenght)]
        public string IsEmriNo { get; set; }

        [Description("Is Emri miktari")]
        [DbType(" DECIMAL(18,4) ")]
        public decimal IsEmriMiktar { get; set; }
        [NonPersistent]
        public decimal OkunanMiktar { get; set; }
        [NonPersistent]
        public decimal KalanMiktar { get; set; }

        [ModelDefault("DisplayFormat", "d"), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int SatirNo { get; set; }

        [ModelDefault("DisplayFormat", "d"), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int BirimId { get; set; }
        public string Birim { get; set; }

        /*[Association(@"IsEmriBilesenleri.Malzemeler_Bilesenler"), XmlIgnore()]
        public Malzemeler Malzeme { get; set; }*/
        [Persistent("Malzeme"),ModelDefault("DisplayFormat", "d"), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int MalzemeId { get; set; }
        [NonPersistent]
        public string MalzemeKod { get; set; }
        [NonPersistent]
        public string MalzemeAd { get; set; }
        
        /*[Association(@"IsEmriBilesenleri.Malzemeler_UstBilesenler"), XmlIgnore()]
        public Malzemeler UstMalzeme { get; set; }*/

        [Persistent("UstMalzeme"), ModelDefault("DisplayFormat", "d"), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int UstMalzemeId { get; set; }
        [NonPersistent]
        public string UstMalzemeKod { get; set; }
        [NonPersistent]
        public string UstMalzemeAd { get; set; }
        [ModelDefault("DisplayFormat", "d"), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int OperasyonId { get; set; }
        [ModelDefault("DisplayFormat", "d"),VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int OperasyonNo { get; set; }
        public string OperasyonKod { get; set; }
        
        [Description("Urun agacinda kullanilan miktar")]
        [DbType(" DECIMAL(18,5) ")]
        public decimal BirimMiktar { get; set; }

        //protected string fDMiktar;
        //[NonPersistent, VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        //public string DMiktar
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(fDMiktar))
        //            return this.BirimMiktar.ToString(Lang.CultureInfoEn);
        //        return fDMiktar;
        //    }
        //    set
        //    {
        //        if (!IsLoading && !IsSaving)
        //            fDMiktar = value;
        //    }
        //}

        [Description("Gereken net miktar")]
        [DbType(" DECIMAL(18,5) ")]
        public decimal NetMiktar { get; set; }


        [Size(DbSize.KodLenght), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string OzelKod { get; set; }
        [Size(DbSize.KodLenght), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string PartiKod { get; set; }
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public bool MiktarSekli { get; set; }

        [NonPersistent]
        public HammaddeTakip HammaddeTakip { get; set; }

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
                if (!IsLoading && !IsSaving)
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
            if (IsDeleted == false)
            {
                try
                {
                    #region Malzemeler
                    //if (this.Malzeme == null && this.MalzemeId > 0)
                    //{
                    //    this.Malzeme = Session.GetObjectByKey<Malzemeler>(this.MalzemeId);
                    //}
                    //if (this.UstMalzeme == null && this.UstMalzemeId > 0)
                    //{
                    //    this.UstMalzeme = Session.GetObjectByKey<Malzemeler>(this.UstMalzemeId);
                    //}
                    #endregion
                }
                catch 
                {
                    ;
                }
            }
            base.OnSaving();
        }

        public IsEmriBilesenleri() { }
        public IsEmriBilesenleri(Session session) : base(session) { }
    }
}

/*
 web erp sql
 select WORDER_BOM_D_ID AS "IsEmriBilesenId", ITEM_BOM_M_ID AS "ReceteId", WORDER_M_ID AS "IsEmriId", QTY AS "IsEmriMiktar", LINE_NO AS "SatirNo", UNIT_ID AS "BirimId", ITEM_ID AS "Malzeme", OPERATION_ID AS "OperasyonId", OPERATION_NO AS "OperasyonNo", QTY_BASE_BOM AS "BirimMiktar", QTY_NET AS "NetMiktar" from PRDT_WORDER_BOM_D WHERE CO_ID = #Firma#
 */

/*
 EXEC dbo.SP_UretimMalzemeKontrol 171, 381, 241, 1

ALTER PROCEDURE dbo.SP_UretimMalzemeKontrol
(
	@ISTASYONID		INT,
	@ISEMRIID		INT,
	@OPERASYONID	INT,
	@MIKTAR			DECIMAL(18,3)
)
/*
İş emri üretim girişinde hammadde kontrolü için.
Üretim parametresi Raf kontrolse sadece rafa bakılacak.
Önce çıkılan malzemelere bakılacak.
Sonra rafdakilere bakılacak.
 سْــــــــــــــــــــــمِ اﷲِارَّحْمَنِ ارَّحِيم           
AS BEGIN

DECLARE @RAFTUKETIM BIT, @MALZCIK BIT

SELECT TOP 1 @MALZCIK = MalzemeCikisZorunlu, @RAFTUKETIM = TuketimRafKontrol FROM dbo.UretimParametreleri WITH (NOLOCK)

SELECT     
	dbo.Ambalajlar.OID AS AmbalajId, 
	dbo.Ambalajlar.AmbalajTur, 
	dbo.Ambalajlar.IstasyonId, 
	dbo.Ambalajlar.IsEmriId, 
	dbo.Ambalajlar.IsEmriNo, 
	dbo.Ambalajlar.OperasyonId, 
	dbo.Ambalajlar.OperasyonNo, 
	dbo.Ambalajlar.Durum,
	dbo.Ambalajlar.Raf,
	dbo.Raflar.RafKod, 
	dbo.Ambalajlar.UstAmbalaj, 
	dbo.AmbalajDetaylari.MalzemeId, 
	dbo.AmbalajDetaylari.Miktar, 
	dbo.AmbalajDetaylari.Miktar2, 
	dbo.AmbalajDetaylari.Kalan, 
	dbo.AmbalajDetaylari.Kalan2, 
	dbo.AmbalajDetaylari.BirimId, 
	dbo.AmbalajDetaylari.Birim2Id, 
	dbo.AmbalajDetaylari.MalzemeKod, 
	dbo.AmbalajDetaylari.MalzemeAd, 
	dbo.AmbalajDetaylari.Birim, 
	dbo.AmbalajTurleri.AmbalajTip,
	dbo.AmbalajTurleri.Bolunebilirlik
FROM 
	dbo.Ambalajlar WITH (NOLOCK) INNER JOIN dbo.AmbalajDetaylari WITH (NOLOCK) ON dbo.Ambalajlar.OID = dbo.AmbalajDetaylari.Ambalaj INNER JOIN 
	dbo.AmbalajTurleri WITH (NOLOCK) ON dbo.Ambalajlar.AmbalajTur = dbo.AmbalajTurleri.OID LEFT OUTER JOIN dbo.Raflar WITH (NOLOCK) ON dbo.Ambalajlar.Raf = dbo.Raflar.OID

SON:
PRINT N'<<SON>>'
END
 */