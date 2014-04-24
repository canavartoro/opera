using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, NavigationItem(false),
     ImageName("BO_Organization"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    [XafDisplayName("Uretim Iscilik Kaydi")]
    [ReferansTablo("pub.erp_iscilik", SistemTipi.Progress, false)]
    public class UretimIscilikleri : XPObject
    {
        #region Personel
        [PersistentAlias("Iif(Personel is null, 0, Personel.PersonelId)"),
        VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int PersonelId
        {
            get
            {
                return Convert.ToInt32(EvaluateAlias("PersonelId"));
            }
            set
            {
                if (!IsLoading && !IsSaving)
                {
                    SetPropertyValue<Personeller>("Personel", ref fPersonel, Session.GetObjectByKey<Personeller>(value));
                }
            }

        }

        [Size(DbSize.KodLenght),
        VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string PersonelKod { get; set; }

        Personeller fPersonel;
        [Association("UretimIscilikleri.Personeller.Personel"),
        NoForeignKey, Indexed, XmlIgnore(), Index(0)]
        //[ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxSearchEditButtonPropertyEditor")]
        public Personeller Personel
        {
            get { return fPersonel; }
            set { SetPropertyValue<Personeller>("Personel", ref fPersonel, value); }
        }

        [PersistentAlias("Iif(Personel is null, '', Personel.PersonelIsim)"), Index(1)]
        public string PersonelIsim { get { return Convert.ToString(EvaluateAlias("PersonelIsim")); } }
        #endregion

        #region Zamanlar
        [Index(2)]
        [ModelDefault("EditMask", "t")]
        [XafDisplayName("Baslangic")]
        [ModelDefault("DisplayFormat", "{0:HH:mm}")]
        public DateTime BaslangicTarihi { get; set; }

        [Index(3)]
        [ModelDefault("EditMask", "t")]
        [XafDisplayName("Bitis")]
        [ModelDefault("DisplayFormat", "{0:HH:mm}")]
        public DateTime BitisTarihi { get; set; }

        #endregion

        [Size(DbSize.AciklamaLenght), ModelDefault("RowCount", "2")]
        public string Aciklama { get; set; }

        #region Uretim Bilgisi
        [PersistentAlias("Iif(UretimOperasyon is null, 0, UretimOperasyon.Oid)"),
        VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int UretimOperasyonId
        {
            get { return Convert.ToInt32(EvaluateAlias("UretimOperasyonId")); }
            set
            {
                if (!IsLoading && !IsSaving)
                {
                    SetPropertyValue<UretimOperasyonlari>("UretimOperasyon", ref fUretimOperasyon, Session.GetObjectByKey<UretimOperasyonlari>(value));
                }
            }
        }

        protected UretimOperasyonlari fUretimOperasyon;
        [Association("UretimIscilikleri.UretimOperasyonlari.UretimOperasyon")]
        public UretimOperasyonlari UretimOperasyon
        {
            get { return fUretimOperasyon; }
            set { SetPropertyValue<UretimOperasyonlari>("UretimOperasyon", ref fUretimOperasyon, value); }
        }
        #endregion

        #region Ortak Alanlar
        #region Olusturan
        [ModelDefault("AllowEdit", "False"), ReadOnly(true), XmlIgnore(), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int Olusturan { get; set; }

        private Kullanicilar _olusturankullanici;
        [XmlIgnore(), NonPersistent, XafDisplayName("Olusturan Kullan�c�"), ImmediatePostData,
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
        [XmlIgnore(), NonPersistent, XafDisplayName("Guncelleyen Kullan�c�"), ImmediatePostData,
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
        [ModelDefault("AllowEdit", "False"), ReadOnly(true), Description("Kayd�n olu�tu�u uygulama"), XmlIgnore(), VisibleInListView(false), VisibleInLookupListView(false)]
        public KaynakProgram KaynakProgram { get; set; }
        [Size(DbSize.CihazNoLenght), ModelDefault("AllowEdit", "False"), Browsable(false), ReadOnly(true), XmlIgnore(), VisibleInListView(false), VisibleInLookupListView(false)]
        public string CihazNo { get; set; }
        [ModelDefault("AllowEdit", "False"), ReadOnly(true), XmlIgnore(), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool Entegre { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public KayitDurumu Durum { get; set; }
        #endregion

        public UretimIscilikleri()
        {
        }
        public UretimIscilikleri(Session session)
            : base(session)
        {
        }
    }
}
