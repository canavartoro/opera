using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using Mikrobar;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    /// <summary>
    /// Sevk emirleri
    /// </summary>
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("SevkEmriDetayId"),
     NavigationItem(false), ImageName("BO_Role")]
    public class SevkEmriDetaylari : XPBaseObject
    {
        [Key]
        public int SevkEmriDetayId { get; set; }
        public int SevkEmriId { get; set; }
        public int SiparisId { get; set; }

        #region Malzeme Bilgisi

        [Indexed, VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false),
        PersistentAlias("Iif(Malzeme is null, 0, Malzeme.MalzemeId)")]
        public int MalzemeId
        {
            get { return Convert.ToInt32(EvaluateAlias("MalzemeId")); }
            set
            {
                SetPropertyValue<Malzemeler>("Malzeme", ref fMalzeme, Session.GetObjectByKey<Malzemeler>(value));
            }
        }

        [ModelDefault("AllowEdit", "false"), ReadOnly(true), Size(DbSize.KodLenght),
        VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public string MalzemeKod { get; set; }

        Malzemeler fMalzeme;
        [XmlIgnore(), Association(@"Malzemeler.SevkEmriDetaylari.MalzemeId"), NoForeignKey,
        Persistent("MalzemeId"), ModelDefault("AllowEdit", "False")]
        public Malzemeler Malzeme
        {
            get { return fMalzeme; }
            set
            {
                SetPropertyValue<Malzemeler>("Malzeme", ref fMalzeme, value);
                if (!IsLoading && !object.ReferenceEquals(this.fMalzeme, null))
                    this.MalzemeKod = this.fMalzeme.MalzemeKod;
            }
        }

        [VisibleInLookupListView(true), VisibleInDetailView(true), PersistentAlias("Iif(Malzeme is null, '', Malzeme.MalzemeAd)"), XmlIgnore()]
        public string MalzemeAd { get { return Convert.ToString(EvaluateAlias("MalzemeAd")); } }

        #endregion
        #region Birim
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int BirimId { get; set; }
        [Size(DbSize.KodLenght), VisibleInLookupListView(false), ModelDefault("AllowEdit", "False")]
        public string Birim { get; set; }
        #endregion               

        [Size(DbSize.KodLenght)]
        public string CariMalzemeKod { get; set; }
        [DbType(" DECIMAL(18,4) ")]
        public decimal Miktar { get; set; }
        [DbType(" DECIMAL(18,4) ")]
        public decimal SevkMiktar { get; set; }
        [DbType(" DECIMAL(18,4) ")]
        public decimal RedMiktar { get; set; }        
        public int SiraNo { get; set; }
        public int DepoId { get; set; }
        public int HizmetKartId { get; set; }

        [Size(DbSize.KodLenght)]
        public string OzelKod { get; set; }
        [Size(DbSize.KodLenght)]
        public string OzelKod1 { get; set; }

        [Size(DbSize.KodLenght)]
        public string DepoKod { get; set; }
        [Size(DbSize.KodLenght)]
        public string DepoKod1 { get; set; }

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

        SevkEmirleri fSevkemri;
        [XmlIgnore(), Association(@"SevkEmriDetaylari.SevkEmirleri")]
        public SevkEmirleri SevkEmirleri
        {
            get { return fSevkemri; }
            set
            {
                SetPropertyValue<SevkEmirleri>("SevkEmirleri", ref fSevkemri, value);
                if (!IsLoading && !object.ReferenceEquals(this.fSevkemri, null))
                    this.SevkEmriId = this.fSevkemri.SevkEmriId;
            }
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {

            }
        }

        public SevkEmriDetaylari() { }
        public SevkEmriDetaylari(Session session) : base(session) { }

    }
}
