using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Xml.Serialization;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using System.Diagnostics;
using MainDemo.Module;
using DevExpress.ExpressApp.Web;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using Mikrobar;
using DevExpress.ExpressApp.Model;


namespace Mikrobar.Module.BusinessObjects
{
    /*
     --SELECT v.WHOUSE_ID, v.WHOUSE_CODE, v.DESCRIPTION, v.ISNEGATIVE, v.ENTITY_ID FROM INVD_BRANCH_WHOUSE i INNER JOIN INVD_WHOUSE v ON i.WHOUSE_ID = v.WHOUSE_ID WHERE i.CO_ID = 191

SELECT i.ISSCRAPT_WHOUSE, i.CONTROL_LOCATION FROM INVD_BRANCH_WHOUSE i INNER JOIN INVD_WHOUSE v ON i.WHOUSE_ID = v.WHOUSE_ID WHERE i.CO_ID = 191
    */

    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("DepoKod"),
     NavigationItem(false), ImageName("BO_Sale_Item_v92"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    [DebuggerDisplay("DepoKod = {DepoKod}, DepoId = {DepoId}, Hurda = {Hurda}")]
    [ReferansTablo(TabloAdi = "pub.depo", SistemTipi = SistemTipi.Progress),
     ReferansTablo("INVD_BRANCH_WHOUSE,INVD_WHOUSE", SistemTipi.WebErp, true)]
    [ReferansTablo("TBLSTOKDP", SqlSorgu = @"SELECT * FROM Barset.V_Depolar ", SqlWhere = @" AND DepoId = {0} ", SistemTipi = SistemTipi.Netsis, QueryType = QueryType.Yoksa)]
    public class Depolar : XPBaseObject
    {
        [Key(AutoGenerate = false)]
        public int DepoId { get; set; }
        [Size(DbSize.KodLenght), Index(2)]
        public string DepoKod { get; set; }
        [Size(DbSize.AdLenght), VisibleInLookupListView(true)]
        public string DepoAd { get; set; }
        [Size(DbSize.AciklamaLenght), VisibleInLookupListView(true)]
        public string Aciklama { get; set; }
        [Size(DbSize.BarKodLenght), VisibleInLookupListView(true)]
        public string Barkod { get; set; }
        [VisibleInLookupListView(true)]
        public bool Hurda { get; set; }
        [VisibleInLookupListView(true)]
        public bool Fason { get; set; }
        [VisibleInLookupListView(true)]
        public bool EksiStok { get; set; }
        public bool HaricDepo { get; set; }
        [VisibleInLookupListView(true)]
        public bool Sayim { get; set; }
        [VisibleInLookupListView(true)]
        public bool Kalite { get; set; }

        [VisibleInLookupListView(true), Description("Konsinye deposu.")]
        public bool Konsinye { get; set; }
        //[NonPersistent, Description("Secilen depo varsayilan hurda deposudur.")]
        //public bool VarsayilanHurdaDeposu { get; set; }
        [NonPersistent]
        public int GirisRafId { get; set; }
        [XmlIgnore(), Persistent("GirisRafId"), Association(@"Depolar.Depo_GirisRafId")]
        public Raflar GirisRaf { get; set; }
        [NonPersistent]
        public int CikisRafId { get; set; }
        [XmlIgnore(), Persistent("CikisRafId"), Association(@"Depolar.Depo_CikisRafId")]
        public Raflar CikisRaf { get; set; }

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

        [XmlIgnore(), Association(@"Sayimlar.Depo.DepoId"), VisibleInDetailView(false), NoForeignKey]
        public XPCollection<Sayimlar> Sayimlar
        {
            get { return GetCollection<Sayimlar>(@"Sayimlar"); }
        }

        [XmlIgnore(), VisibleInListView(false), Association(@"Depolar.UretimHurdalar.DepoId"), NoForeignKey, VisibleInDetailView(false)]
        public XPCollection<UretimHurdalari> UretimHurdalar
        {
            get { return GetCollection<UretimHurdalari>(@"UretimHurdalar"); }
        }

        [XmlIgnore(), VisibleInListView(false), Association(@"Ambalajlar.Depo_DepoId"), VisibleInDetailView(false)]
        public XPCollection<Ambalajlar> Ambalajlar
        {
            get { return GetCollection<Ambalajlar>(@"Ambalajlar"); }
        }

        [XmlIgnore(), VisibleInListView(false), Association(@"AmbalajHareketDetaylari.HedefDepo_Depolar"), VisibleInDetailView(false)]
        public XPCollection<AmbalajHareketDetaylari> AmbalajHareketH
        {
            get { return GetCollection<AmbalajHareketDetaylari>(@"AmbalajHareketH"); }
        }

        [XmlIgnore(), VisibleInListView(false), Association(@"AmbalajHareketDetaylari.KaynakDepo_Depolar"), VisibleInDetailView(false)]
        public XPCollection<AmbalajHareketDetaylari> AmbalajHareketK
        {
            get { return GetCollection<AmbalajHareketDetaylari>(@"AmbalajHareketK"); }
        }

        [XmlIgnore(), VisibleInListView(false), Association(@"DepoStoklari.DepoId_Depolar"), VisibleInDetailView(false), NoForeignKey]
        public XPCollection<DepoStoklari> DepoStoklari
        {
            get { return GetCollection<DepoStoklari>(@"DepoStoklari"); }
        }

        [XmlIgnore(), VisibleInListView(false), Association(@"RafStoklari.DepoId_Depolar"), VisibleInDetailView(false), NoForeignKey]
        public XPCollection<RafStoklari> RafStoklari
        {
            get { return GetCollection<RafStoklari>(@"RafStoklari"); }
        }

        #region Istasyon Baglantisi

        [XmlIgnore(), VisibleInListView(false), Association(@"Istasyonlar.Depo_MalzemeCikisDepo"), VisibleInDetailView(false)]
        public XPCollection<Istasyonlar> IstasyonMalzemeCikisDepo
        {
            get { return GetCollection<Istasyonlar>(@"IstasyonMalzemeCikisDepo"); }
        }

        [XmlIgnore(), VisibleInListView(false), Association(@"Istasyonlar.Depo_UrunGirisDepo"), VisibleInDetailView(false)]
        public XPCollection<Istasyonlar> IstasyonUrunGirisDepo
        {
            get { return GetCollection<Istasyonlar>(@"IstasyonUrunGirisDepo"); }
        }

        [XmlIgnore(), VisibleInListView(false), Association(@"Istasyonlar.Depo_YariMamulCikisDepo"), VisibleInDetailView(false)]
        public XPCollection<Istasyonlar> IstasyonYariMamulCikisDepo
        {
            get { return GetCollection<Istasyonlar>(@"IstasyonYariMamulCikisDepo"); }
        }

        [XmlIgnore(), VisibleInListView(false), Association(@"Istasyonlar.Depo_YariMamulGirisDepo"), VisibleInDetailView(false)]
        public XPCollection<Istasyonlar> IstasyonYariMamulGirisDepo
        {
            get { return GetCollection<Istasyonlar>(@"IstasyonYariMamulGirisDepo"); }
        }

        #endregion

        [XmlIgnore(), Association(@"Depolar.Raflar_RafId"), NoForeignKey]
        public XPCollection<Raflar> Raflar
        {
            get { return GetCollection<Raflar>(@"Raflar"); }
        }

        [XmlIgnore(), VisibleInListView(false), Association(@"Depolar.Aletler_DepoId"), VisibleInDetailView(false), NoForeignKey]
        public XPCollection<Aletler> Aletler
        {
            get { return GetCollection<Aletler>(@"Aletler"); }
        }

        [XmlIgnore(), VisibleInListView(false), Association(@"Depolar.SevkiyatParametreleri_DepoId"), NoForeignKey, VisibleInDetailView(false)]
        public XPCollection<SevkiyatParametreleri> SevkiyatParametreleri
        {
            get { return GetCollection<SevkiyatParametreleri>(@"SevkiyatParametreleri"); }
        }

        #region Mal Talep
        [XmlIgnore(), VisibleInListView(false), Association(@"Talepler.TalepDepo.TalepDepoId"), NoForeignKey, VisibleInDetailView(false)]
        public XPCollection<Talepler> DepoTalepleri
        {
            get { return GetCollection<Talepler>(@"DepoTalepleri"); }
        }

        [XmlIgnore(), VisibleInListView(false), Association(@"Talepler.TalepEdilenDepo.TalepEdilenDepoId"), NoForeignKey, VisibleInDetailView(false)]
        public XPCollection<Talepler> DepoTalepleri2
        {
            get { return GetCollection<Talepler>(@"DepoTalepleri2"); }
        }


        [XmlIgnore(), VisibleInListView(false), Association(@"TalepKabulleri.TalepDepo.TalepDepoId"), NoForeignKey, VisibleInDetailView(false)]
        public XPCollection<TalepKabulleri> DepoTalepKabulleri
        {
            get { return GetCollection<TalepKabulleri>(@"DepoTalepKabulleri"); }
        }

        [XmlIgnore(), VisibleInListView(false), Association(@"TalepKabulleri.TalepEdilenDepo.TalepEdilenDepoId"), NoForeignKey, VisibleInDetailView(false)]
        public XPCollection<TalepKabulleri> DepoTalepKabulleri2
        {
            get { return GetCollection<TalepKabulleri>(@"DepoTalepKabulleri2"); }
        }
        #endregion

        protected override void OnSaving()
        {
           
        }

        #region Butonlar
        [Action(Caption = "Guncelle", ImageName = "Action_Refresh", ToolTip = "Bilgileri guncelle..")]
        public void Entegrasyon()
        {
        }
        #endregion

        public Depolar() { }
        public Depolar(Session session) : base(session) { }

    }

    /*depolarin raflara tanitildigi sorgu*/

    /*INSERT INTO dbo.Raflar
SELECT 
	DepoId,
	DepoId AS RafId,
	DepoKod AS RafKod,
	Aciklama AS Aciklama,
	'' AS HiyerArsi,
	0 AS Seviye,
	0 AS Rapor,
	DepoKod AS Barkod,
	Sayim AS Sayim,
	EksiStok AS EksiStok,
	0 AS Sevkiyat,
	0 AS IstasyonMu,
	Olusturan,
	OlusturmaTarihi,
	Guncelleyen,
	GuncellemeTarihi,
	KaynakModul,
	CihazNo,
	OptimisticLockField,
	GCRecord
FROM dbo.Depolar (NOLOCK) WHERE DepoId NOT IN 
( SELECT RafId FROM dbo.Raflar (NOLOCK) )*/
}
