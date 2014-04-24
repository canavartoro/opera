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
    /// <summary>
    /// Istasyonda calisan personellerin kayitlari icin
    /// </summary>
    //[Indices("IstasyonId", "PersonelId")]
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("Oid"), NavigationItem(false),
     ImageName("BO_Organization"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    public class IstasyonIscilikleri : XPObject
    {       
        #region Istasyon
        [Indexed("Durum", Unique = false), PersistentAlias("Iif(Istasyon is null, 0, Istasyon.IstasyonId)"),
        VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int IstasyonId
        {
            get
            {
                try
                {
                    if (!IsLoading)
                        return Convert.ToInt32(EvaluateAlias("IstasyonId"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return 0;
            }
            set
            {
                if (!IsLoading && !IsSaving)
                {
                    SetPropertyValue("Istasyon", ref _istasyon, Session.GetObjectByKey<Istasyonlar>(value));
                    if (object.ReferenceEquals(this._istasyon, null))
                        throw new MikrobarException(Lang.Mesaj(GenelMesajlar.ERR_6002, value), 6002);
                    else
                        this.IstasyonKod = this._istasyon.IstasyonKod;
                }
            }
        }

        private Istasyonlar _istasyon;
        [Indexed("Personel", "VardiyaId", Unique = true)]
        [XmlIgnore(), XafDisplayName("Istasyon Kodu"), ImmediatePostData,
        VisibleInListView(false), VisibleInLookupListView(false), Association(@"IstasyonIscilikleri.Istasyonlar.Istasyon"), Persistent("IstasyonId"), NoForeignKey]
        [ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxSearchEditButtonPropertyEditor")]
        //[Appearance("UretimOperasyonlari_Istasyon", Enabled = false, Criteria = "Iif(Oid == -1, 0, 1) == 1", AppearanceItemType = "ViewItem", Context = "DetailView", TargetItems = "Istasyon")]
        public Istasyonlar Istasyon
        {
            get
            {
                return _istasyon;
            }
            set
            {
                SetPropertyValue("Istasyon", ref _istasyon, value);
                if (!IsLoading && !object.ReferenceEquals(_istasyon, null))
                    this.IstasyonKod = this._istasyon.IstasyonKod;
            }
        }

        [Size(DbSize.KodLenght),VisibleInListView(true), VisibleInLookupListView(true),VisibleInDetailView(false)]
        public string IstasyonKod { get; set; }

        [VisibleInLookupListView(true), VisibleInDetailView(true), PersistentAlias("Iif(Istasyon is null, '', Istasyon.IstasyonAd)")]
        public string IstasyonAd
        {
            get
            {
                try
                {
                    if (!IsLoading)
                        return Convert.ToString(EvaluateAlias("IstasyonAd"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return "";
            }
        }
        #endregion

        #region Personel Bilgisi
        [PersistentAlias("Iif(Personel is null, 0, Personel.PersonelId)"), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int PersonelId 
        {
            get
            {
                return (int)EvaluateAlias("PersonelId");
            }
            set
            {
                if (!IsLoading && !IsSaving)
                {
                    SetPropertyValue<Personeller>("Personel", ref fPersonel, Session.GetObjectByKey<Personeller>(value));
                }
            }

        }

        [Size(DbSize.KodLenght), VisibleInListView(false), VisibleInLookupListView(true), VisibleInDetailView(false)]
        public string PersonelKod { get; set; }

        Personeller fPersonel;
        [VisibleInListView(true), VisibleInLookupListView(false), VisibleInDetailView(true)]
        [XmlIgnore(), Association(@"Personeller.IstasyonIscilikleri.PersonelId"), NoForeignKey, Persistent("PersonelId")]
        public Personeller Personel
        {
            get { return fPersonel; }
            set { SetPropertyValue<Personeller>("Personel", ref fPersonel, value); }
        }

        [PersistentAlias("Iif(Personel is null, '', Personel.PersonelIsim)")]
        public string PersonelIsim { get { return Convert.ToString(EvaluateAlias("PersonelIsim")); } }
        #endregion

        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}"), ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxDateTimePropertyEditor")]
        public DateTime BaslangicTarihi { get; set; }

        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}"), ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxDateTimePropertyEditor")]
        public DateTime BitisTarihi { get; set; }
        
        public int VardiyaId { get; set; }
        [Size(DbSize.KodLenght)]
        public string VardiyaKod { get; set; }

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

        public IstasyonIscilikleri() { }
        public IstasyonIscilikleri(Session session) : base(session) { }
    }
}
