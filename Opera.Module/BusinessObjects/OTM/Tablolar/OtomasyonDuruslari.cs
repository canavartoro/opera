using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using System.Xml.Serialization;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [OptimisticLocking(false), DeferredDeletion(false), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    public class OtomasyonDuruslari : XPObject
    {

        #region Istasyon Bilgisi

        protected Istasyonlar _Istasyon;

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int IstasyonId { get; set; }

        [Size(DbSize.KodLenght), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(true), Indexed(Unique = false)]
        public string IstasyonKod { get; set; }

        [XmlIgnore(), NonPersistent, XafDisplayName("Istasyon Bilgisi"),
        VisibleInListView(true), VisibleInLookupListView(false), DataSourceCriteria("IstasyonId = '@This.IstasyonId'")]
        public Istasyonlar Istasyon
        {
            get
            {
                if (_Istasyon == null && this.IstasyonId > 0)
                    this._Istasyon = this.Session.GetObjectByKey<Istasyonlar>(this.IstasyonId);
                return _Istasyon;
            }
            set
            {
                SetPropertyValue("Istasyon", ref _Istasyon, value);
            }
        }

        [NonPersistent, ModelDefault("AllowEdit", "False"), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        public string IstasyonAd
        {
            get
            {
                if (Istasyon != null)
                    return this.Istasyon.IstasyonAd;

                return "";
            }
        }

        #endregion

        public DateTime DurusBaslangic { get; set; }
        public DateTime DurusBitis { get; set; }
        public decimal DurusSuresi { get; set; }

        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [DevExpress.Xpo.Aggregated, Association("OtomasyonDuruslari-Duruslar", typeof(UretimDuruslari)), NoForeignKey, XmlIgnore()]
        public XPCollection<UretimDuruslari> UretimDurus
        {
            get { return GetCollection<UretimDuruslari>("UretimDurus"); }
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

        protected override void OnSaving()
        {
            #region IsNotDelete

            if (this.IsDeleted == false)
            {
                SistemKullanicilari currentUser = SecuritySystem.CurrentUser as SistemKullanicilari;

                if (this.Oid < 1)
                {
                    if (this.Olusturan < 1)
                        this.Olusturan = currentUser != null ? currentUser.KullaniciId : 0;

                    this.OlusturmaTarihi = DateTime.Now;
                    this.KaynakProgram = BusinessObjects.KaynakProgram.Sayim;
                    this.KaynakModul = GetType().Name;
                }
                else
                {
                    if (this.Guncelleyen < 1)
                        this.Guncelleyen = currentUser != null ? currentUser.KullaniciId : 0;

                    this.GuncellemeTarihi = DateTime.Now;
                }
            }

            #endregion
        }

        public OtomasyonDuruslari() { }
        public OtomasyonDuruslari(Session session) : base(session) { }
    }
}
