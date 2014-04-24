using System;
using System.ComponentModel;
using System.Xml.Serialization;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [OptimisticLocking(false), DeferredDeletion(false)]
    public class SurecEmirleri : XPObject
    {
        public string EmirNo { get; set; }
        public DateTime EmirTarihi { get; set; }
        public int IsemriId { get; set; }
        public string IsemriNo { get; set; }
        public decimal KontrolMiktari { get; set; }
        public decimal NumuneMiktar { get; set; }
        public decimal KalanNumuneMiktar { get; set; }
        public bool OlcumlerTamam { get; set; }
        public int KontrolEdenId { get; set; }

        public KaliteDurumu KaliteDurumu { get; set; }

        #region ERP KAYIT
        public int OnayDurumu { get; set; }
        [XafDisplayName("ERP Referans Id"), ModelDefault("AllowEdit", "False"),
        VisibleInListView(false), VisibleInLookupListView(false)]
        public int ReferansId { get; set; }
        [XafDisplayName("ERP Referans EmirNo"), ModelDefault("AllowEdit", "False"),
         VisibleInListView(false), VisibleInLookupListView(false)]
        public string ReferansEmirNo { get; set; }

        #endregion

        [VisibleInListView(false)]
        public KontrolSebebi KontrolSebebi { get; set; }


        [VisibleInListView(false)]
        public string Aciklama { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public int CariId { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public bool IlkNumune { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public decimal NemOrani { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public int StokId { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public int PartiId { get; set; }

        [VisibleInListView(false)]
        public int OperasyonId { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public int VardiyaId { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public int KaynakDetayId { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public int KaynakMasterId { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public decimal OrtamSicakligi { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public int BirimId { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public bool UretimYonetimi2 { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public int IsMerkeziId { get; set; }

        [VisibleInListView(false)]
        public int IstasyonId { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public string OrjinalDosyaAdi { get; set; }

        [DevExpress.Xpo.Aggregated, Association("OlcumDegeri_Emir.Olcumler", typeof(OlcumDegerleri)), XafDisplayName("Kontrol Noktaları")]
        public XPCollection<OlcumDegerleri> Olcumler
        {
            get { return GetCollection<OlcumDegerleri>("Olcumler"); }
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

        #region Methods

        protected override void OnSaving()
        {
        }

        #endregion
        public SurecEmirleri() { }
        public SurecEmirleri(Session session) : base(session) { }
    }
}
