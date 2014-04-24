using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using Mikrobar;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    /// <summary>
    /// Uretim de olusan duruslar
    /// </summary>
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, 
    XafDefaultProperty("DurusNedeniId"), NavigationItem(false),
    ImageName("BO_Organization"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    [ReferansTablo("pub.erp_durus", SistemTipi.Progress, false)]
    public class UretimDuruslari : XPObject
    {
        #region Durus
        
        protected Duruslar _durus;
        [PersistentAlias("Iif(Durus is null, 0, Durus.DurusId)")]
        [ModelDefault("AllowEdit", "false"), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int DurusNedeniId 
        {
            get { return Convert.ToInt32(EvaluateAlias("DurusNedeniId")); }
            set
            {
                if (!IsLoading && !IsSaving)
                {
                    SetPropertyValue("Durus", ref _durus, Session.GetObjectByKey<Duruslar>(value));
                    //if (object.ReferenceEquals(this._durus, null))
                    //    throw new MikrobarException(Lang.Mesaj(GenelMesajlar.ERR_6002, value), 6002); // hata mesaji duzeltilecek
                    //else
                    //    this.DurusKod = this._durus.DurusKod;
                }
            }
        }

        [ModelDefault("AllowEdit", "false"), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false), 
        Size(DbSize.KodLenght)]
        public string DurusKod { get; set; }

        [XmlIgnore(), XafDisplayName("Durus Kodu"), ImmediatePostData,
        VisibleInListView(false), VisibleInLookupListView(false)]
        [Persistent(@"DurusNedeniId"), Indexed]
        [DataSourceCriteria("IsEmriBaglanti = True")]
        [Association(@"UretimDuruslari-Durus", typeof(Duruslar)), NoForeignKey]
        //[ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxSearchEditButtonPropertyEditor")]
        public Duruslar Durus
        {
            get
            {
                return _durus;
            }
            set
            {
                SetPropertyValue("Durus", ref _durus, value);
            }
        }

        [VisibleInLookupListView(true), VisibleInDetailView(false), PersistentAlias("Iif(Durus is null, '', Durus.DurusAd)")]
        public string DurusAd { get { return Convert.ToString(EvaluateAlias("DurusAd")); } }

        [VisibleInLookupListView(true), VisibleInDetailView(false), PersistentAlias("Iif(Durus is null, '', Durus.DurusTip)")]
        public string DurusTip { get { return Convert.ToString(EvaluateAlias("DurusTip")); } }

        [VisibleInLookupListView(true), VisibleInDetailView(false), PersistentAlias("Iif(Durus is null, 'False', Durus.IsEmriBaglanti)")]
        public bool IsEmriBaglanti { get { return Convert.ToBoolean(EvaluateAlias("IsEmriBaglanti")); } }
        #endregion

        #region Istasyon<<İş emrinden bağımsız duruşlar için>>

        protected Istasyonlar _Istasyon;
        [NonPersistent]
        [ModelDefault("AllowEdit", "false"), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int IstasyonId { get; set; }

        [ModelDefault("AllowEdit", "false"), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false),
        Size(DbSize.KodLenght)]
        public string IstasyonKod { get; set; }

        [XmlIgnore(), XafDisplayName("Istasyon Kodu"), ImmediatePostData,
        VisibleInListView(false), VisibleInLookupListView(false)]
        [Persistent(@"IstasyonId"), Indexed]
        [Association(@"UretimDuruslari-Istasyon", typeof(Istasyonlar)), NoForeignKey]
        public Istasyonlar Istasyon
        {
            get
            {
                return _Istasyon;
            }
            set
            {
                SetPropertyValue("Istasyon", ref _Istasyon, value);
            }
        }

        [VisibleInLookupListView(true), VisibleInDetailView(false), PersistentAlias("Iif(Istasyon.IstasyonAd is null, '', Istasyon.IstasyonAd)")]
        public string IstasyonAd { get { return Convert.ToString(EvaluateAlias("IstasyonAd")); } }
        #endregion

        protected DateTime fBaslangicTarihi = DateTime.Now;
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy HH:mm}")]
        [ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxDateTimePropertyEditor")]
        public DateTime BaslangicTarihi 
        {
            get { return fBaslangicTarihi; }
            set { SetPropertyValue<DateTime>("BaslangicTarihi", ref fBaslangicTarihi, value); }
        }

        protected DateTime fBitisTarihi = DateTime.Now;
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy HH:mm}")]
        [ ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxDateTimePropertyEditor")]
        public DateTime BitisTarihi
        {
            get { return fBitisTarihi; }
            set { SetPropertyValue<DateTime>("BitisTarihi", ref fBitisTarihi, value); }
        }

        [DbType(" DECIMAL(18,4) "), ModelDefault("AllowEdit", "false")]
        public decimal DurusSuresi { get; set; }

        [Size(DbSize.AciklamaLenght)]
        public string Aciklama { get; set; }

        [ModelDefault("AllowEdit", "false")]
        public int SiraNo { get; set; }

        [XafDisplayName("Kayıt Id (ERP)"), ModelDefault("AllowEdit", "False"),
        VisibleInListView(false), VisibleInLookupListView(false)]
        public int ReferansId { get; set; } 

        protected OtomasyonDuruslari _OtomasyonDurus;
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [DevExpress.Xpo.Aggregated, Association("OtomasyonDuruslari-Duruslar", typeof(OtomasyonDuruslari)), NoForeignKey, XmlIgnore()]
        public OtomasyonDuruslari OtomasyonDurus
        {
            get { return _OtomasyonDurus; }
            set
            {
                SetPropertyValue("OtomasyonDurus", ref _OtomasyonDurus, value);
            }
        }

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

        [Association("UretimDuruslari-Duruslar")]
        public UretimOperasyonlari UretimOperasyon { get; set; }

        protected override void OnSaving()
        {  
        }

        public UretimDuruslari() { }
        public UretimDuruslari(Session session) : base(session) { }



    }
}
