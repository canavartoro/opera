using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Xml.Serialization;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, 
    XafDefaultProperty("AletKod"), NavigationItem(false),
     ImageName("BO_Organization"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    public class UretimAletleri : XPObject
    {
        #region Uretim Bilgisi
        [XmlIgnore()]
        [Association("UretimAletleri-Aletler"), VisibleInDetailView(false)]
        public UretimOperasyonlari UretimOperasyon { get; set; }


        [PersistentAlias("Iif(UretimOperasyon is null, 0, UretimOperasyon.Oid)"), Description("Web serviste kullanmak icin.")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int UretimId
        {
            get { return Convert.ToInt32(EvaluateAlias("UretimId")); }
            set
            {
                if (!IsLoading && !IsSaving)
                {
                    SetPropertyValue("UretimOperasyon", Session.GetObjectByKey<UretimOperasyonlari>(value));
                }
            }
        }
        #endregion

        protected Aletler fAlet;
        [XmlIgnore(), Persistent("AletId"), Association("Aletler-Alet"), NoForeignKey, ImmediatePostData]
        //[ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxSearchEditButtonPropertyEditor")]
        public Aletler Alet 
        {
            get { return fAlet; }
            set
            {
                SetPropertyValue<Aletler>("Alet", ref fAlet, value);
                if (!IsLoading && !IsSaving && fAlet != null)
                {
                    this.AletKod = fAlet.AletKod;
                }
            }
        }

        [Size(DbSize.KodLenght),
       VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string AletKod { get; set; }

        [PersistentAlias("Iif(Alet is null, 0, Alet.AletId)")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int AletId
        {
            get { return Convert.ToInt32(EvaluateAlias("AletId")); }
            set
            {
                if (!IsLoading && !IsSaving)
                {
                    SetPropertyValue("Alet", Session.GetObjectByKey<Aletler>(value));
                }
            }
        }

        [PersistentAlias("Iif(Alet is null, '', Alet.AletAd)")]
        public string AletAciklama 
        { 
            get { return Convert.ToString(EvaluateAlias("AletAciklama")); } 
        }

        [Size(DbSize.AciklamaLenght), ModelDefault("RowCount", "2")]
        public string Aciklama { get; set; }

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

        public UretimAletleri() { }
        public UretimAletleri(Session session) : base(session) { }
    }
}
