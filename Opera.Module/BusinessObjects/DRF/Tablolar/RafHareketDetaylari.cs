using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Reflection;
using System.Xml.Serialization;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("MalzemeKod"),
     NavigationItem(false), ImageName("BO_Sale_Item_v92")]
    public class RafHareketDetaylari : XPObject
    {

        RafHareketleri fRafHareket;
        [Association(@"RafHareketDetaylari.RafHareketleri_StokHareketleri"), XmlIgnore()]
        public RafHareketleri RafHareket
        {
            get { return fRafHareket; }
            set
            {
                SetPropertyValue<RafHareketleri>("RafHareket", ref fRafHareket, value);
            }
        }

        [NonPersistent(), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int RafHareketId { get; set; }

        #region Malzeme Bilgisi
        protected Malzemeler _malzeme;
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int MalzemeId { get; set; }
        [Size(DbSize.KodLenght), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(true)]
        public string MalzemeKod { get; set; }
        [XmlIgnore(), NonPersistent, XafDisplayName("Malzeme Bilgisi"),
        VisibleInListView(true), VisibleInLookupListView(false), DataSourceCriteria("MalzemeId = '@This.MalzemeId'")]
        public Malzemeler Malzeme
        {
            get
            {
                if (_malzeme == null && this.MalzemeId > 0)
                    this._malzeme = this.Session.GetObjectByKey<Malzemeler>(this.MalzemeId);
                return _malzeme;
            }
            set
            {
                SetPropertyValue("Malzeme", ref _malzeme, value);
            }
        }
        #endregion

        #region Birim&Miktar
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int BirimId { get; set; }
        [Size(DbSize.KodLenght), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(true)]
        public string Birim { get; set; }

        protected V_MalzemeBirimleri _malzemeBirim;
        [NonPersistent, XmlIgnore(), VisibleInLookupListView(false)]
        [DataSourceCriteria("MalzemeId = '@This.MalzemeId'"), ImmediatePostData, XafDisplayName("Birim")]
        public V_MalzemeBirimleri MalzemeBirim
        {
            get
            {
                if (_malzemeBirim == null && this.BirimId > 0 && this.MalzemeId > 0)
                    this._malzemeBirim = this.Session.FindObject<V_MalzemeBirimleri>(CriteriaOperator.Parse(" MalzemeId = ? AND BirimId = ?", this.MalzemeId, this.BirimId));
                return _malzemeBirim;
            }
            set
            {
                this.BirimId = value != null ? value.BirimId : 0;
                this.Birim = value != null ? value.Birim : string.Empty;
                SetPropertyValue("MalzemeBirim", ref _malzemeBirim, value);
            }
        }

        [DbType(" DECIMAL(18,4) ")]
        public decimal Miktar { get; set; }

        [ModelDefault("AllowEdit", "false"), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Birim2Id { get; set; }
        [ModelDefault("AllowEdit", "false"), Size(DbSize.KodLenght), VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Birim2 { get; set; }
        [ModelDefault("AllowEdit", "false"), DbType(" DECIMAL(18,4) "), VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public decimal Miktar2 { get; set; }
        #endregion

        [Size(DbSize.KodLenght)]
        public string PartiKod { get; set; }

        #region Depo&Raf

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int DepoId { get; set; }

        [Size(DbSize.KodLenght), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(true)]
        public string DepoKod { get; set; }

        protected Depolar _depo;
        [XmlIgnore(), NonPersistent, XafDisplayName("Depo Bilgisi"),
        VisibleInLookupListView(false)]
        public Depolar Depo
        {
            get
            {
                if (_depo == null && this.MalzemeId > 0)
                    this._depo = this.Session.GetObjectByKey<Depolar>(this.DepoId);
                return _depo;
            }
            set
            {
                SetPropertyValue("Depo", ref _depo, value);
            }
        }


        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int HedefDepoId { get; set; }
        [Size(DbSize.KodLenght), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string HedefDepoKod { get; set; }
        protected Depolar _hedefDepo;
        [XmlIgnore(), NonPersistent, XafDisplayName("Hedef Depo Bilgisi"),
        VisibleInLookupListView(false)]
        public Depolar HedefDepo
        {
            get
            {
                if (_hedefDepo == null && this.HedefDepoId > 0)
                    this._hedefDepo = this.Session.GetObjectByKey<Depolar>(this.HedefDepoId);
                return _hedefDepo;
            }
            set
            {
                SetPropertyValue("HedefDepo", ref _hedefDepo, value);
            }
        }


        Raflar fHedefRaf;
        [Association(@"_RafHareketDetaylari.HedefRaf_Raflar"), XmlIgnore(),
        DataSourceCriteria("Depo = '@This.HedefDepoId'")]
        public Raflar HedefRaf
        {
            get { return fHedefRaf; }
            set { SetPropertyValue<Raflar>("HedefRaf", ref fHedefRaf, value); }
        }

        Raflar fKaynakRaf;
        [Association(@"_RafHareketDetaylari.KaynakRaf_Raflar"), XmlIgnore(),
        DataSourceCriteria("Depo = '@This.DepoId'")]
        public Raflar KaynakRaf
        {
            get { return fKaynakRaf; }
            set { SetPropertyValue<Raflar>("KaynakRaf", ref fKaynakRaf, value); }
        }
        #endregion

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int SiparisId { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int SiparisDetayId { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int SevkEmriId { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int SevkEmriDetayId { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int ToplamaEmirId { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int ToplamaEmirDetayId { get; set; }



        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int MalTalepId { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int MalTalepDetayId { get; set; }

        #region Uretim&Fason
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int IsEmriId { get; set; }
        [Size(DbSize.KodLenght), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string IsEmriNo { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int OperasyonId { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int OperasyonNo { get; set; }
        [Size(DbSize.KodLenght), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string OperasyonKod { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int IsEmriDetayId { get; set; }
        [Description("Fason'a mal gönderimi yada Fason dönüşte işemri tamamlandı."),
        VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool Tamamlandi { get; set; }
        #endregion

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int SatirTipi { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Diger1 { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Diger2 { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Diger3 { get; set; }


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
           
        }


        public RafHareketDetaylari() { }
        public RafHareketDetaylari(Session session) : base(session) { }

    }
}
