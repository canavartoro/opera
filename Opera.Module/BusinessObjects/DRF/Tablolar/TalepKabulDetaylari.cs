using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Xml.Serialization;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [ImageName("BO_Sale_Item_v92"),
    XafDisplayName("Malzeme Talep Kabul Detaylari"), XafDefaultProperty("MalzemeKod"), ModelDefault("IsClonable", "True")]
    [OptimisticLocking(false), DeferredDeletion(false)]
    public class TalepKabulDetaylari : XPObject
    {

        [ModelDefault("AllowEdit", "False"), PersistentAlias("Iif(TalepKabul is null, 0, TalepKabul.Oid)"), 
        VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int TalepKabulId
        {
            get { return Convert.ToInt32(EvaluateAlias("TalepKabulId")); }
            set
            {
                if (!IsLoading && !IsSaving)
                {
                    SetPropertyValue<TalepKabulleri>("TalepKabul", ref _talepkabul, Session.GetObjectByKey<TalepKabulleri>(value));
                }
            }
        }

        private TalepKabulleri _talepkabul;
        [XmlIgnore(), Association("TalepKabulDetaylari_TalepKabul"), Description("Talep master bilgisi")]
        public TalepKabulleri TalepKabul
        {
            get { return _talepkabul; }
            set { SetPropertyValue<TalepKabulleri>("TalepKabul", ref _talepkabul, value); }
        }        

        #region Talep Detay
        [PersistentAlias("Iif(TalepDetay is null, 0, TalepDetay.Oid)"), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int TalepDetayId
        {
            get
            {
                return Convert.ToInt32(EvaluateAlias("TalepDetayId"));
            }
            set
            {
                if (!IsLoading && !IsSaving)
                    SetPropertyValue<TalepDetaylari>("TalepDetay", ref _talepDetay, this.Session.GetObjectByKey<TalepDetaylari>(value));
            }
        }

        private TalepDetaylari _talepDetay;
        [XmlIgnore(), Association("TalepKabulDetaylari.TalepDetaylari.TalepDetay"),
        Persistent("TalepDetayId"), Description("Talep detay bilgisi")]
        public TalepDetaylari TalepDetay
        {
            get { return _talepDetay; }
            set { SetPropertyValue<TalepDetaylari>("TalepDetay", ref _talepDetay, value); }
        }
        #endregion

        #region Malzeme&Birim
        private Malzemeler _malzeme;
        [XmlIgnore(), Association(@"TalepKabulDetaylari.Malzeme.MalzemeId"), NoForeignKey, Persistent("MalzemeId"), XafDisplayName("Malzeme Kodu"), ImmediatePostData, Indexed("TalepKabul", Unique = false)]
        public Malzemeler Malzeme
        {
            get { return _malzeme; }
            set
            {
                SetPropertyValue<Malzemeler>("Malzeme", ref _malzeme, value);
                if (!IsLoading && !IsSaving)
                {
                    if (value != null && this.Oid < 1)
                    {
                        SetPropertyValue("MalzemeBirim", ref _malzemeBirim,
                                         this.Session.FindObject<V_MalzemeBirimleri>(
                                             CriteriaOperator.Parse(" MalzemeId = ? AND BirimId = ? ", value.MalzemeId,
                                                                    value.BirimId)));
                    }
                }
            }
        }
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false), PersistentAlias("Iif(Malzeme is null, 0, Malzeme.MalzemeId)")]
        public int MalzemeId
        {
            get
            {
                return Convert.ToInt32(EvaluateAlias("MalzemeId"));
            }
            set
            {
                SetPropertyValue<Malzemeler>("Malzeme", ref _malzeme, this.Session.GetObjectByKey<Malzemeler>(value));
                OnChanged("Malzeme");
            }
        }
        [Size(DbSize.KodLenght), ReadOnly(true), ModelDefault("AllowEdit", "false"), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public string MalzemeKod { get; set; }
        [VisibleInLookupListView(false), PersistentAlias("Iif(Malzeme is null, '', Malzeme.MalzemeAd)")]
        public string MalzemeAd
        {
            get
            {
                return Convert.ToString(EvaluateAlias("MalzemeAd"));
            }
            set { }
        }
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int BirimId { get; set; }
        [Size(DbSize.KodLenght), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public string Birim { get; set; }         

        private V_MalzemeBirimleri _malzemeBirim;
        [NonPersistent, DataSourceCriteria("MalzemeId = '@This.Malzeme.MalzemeId'"), ImmediatePostData, XafDisplayName("Birim"), XmlIgnore()]
        public V_MalzemeBirimleri MalzemeBirim
        {
            get 
            {
                if (this._malzemeBirim == null && this.BirimId > 0)
                    this._malzemeBirim = this.Session.FindObject<V_MalzemeBirimleri>(CriteriaOperator.Parse(" MalzemeId = ? AND BirimId = ? ", this.MalzemeId, this.BirimId));
                return _malzemeBirim; 
            }
            set
            {                
                SetPropertyValue("MalzemeBirim", ref _malzemeBirim, value);
                if (_malzemeBirim != null)
                {
                    this.Birim = this._malzemeBirim.Birim;
                    this.BirimId = this._malzemeBirim.BirimId;
                    OnChanged("Birim");
                    OnChanged("BirimId");
                }
            }
        }
        #endregion

        #region Miktar Alanlari
        [DbType(" DECIMAL(18,4) "), ModelDefault("EditMask", "c")]
        public decimal TalepMiktar { get; set; }

        [DbType(" DECIMAL(18,4) "), ModelDefault("EditMask", "c")]
        public decimal SevkMiktar { get; set; }

        [DbType(" DECIMAL(18,4) "), ModelDefault("EditMask", "c"), NonPersistent, ModelDefault("AllowEdit", "false"), ReadOnly(true)]
        public decimal OkunanMiktar { get; set; }

        [DbType(" DECIMAL(18,4) "), ModelDefault("EditMask", "c"), ModelDefault("AllowEdit", "false"), ReadOnly(true)]
        public decimal KabulMiktar { get; set; }

        [NonPersistent, ModelDefault("EditMask", "c"), ModelDefault("AllowEdit", "false"), ReadOnly(true)]
        public decimal KalanMiktar 
        {
            get
            {
                return TalepMiktar - SevkMiktar;
            }
            set
            { }
        }

        [NonPersistent, ModelDefault("EditMask", "c"), ModelDefault("AllowEdit", "false"), ReadOnly(true)]
        public decimal KabulKalanMiktar
        {
            get
            {
                return OkunanMiktar - KabulMiktar;
            }
            set
            { }
        }

        [NonPersistent, ModelDefault("EditMask", "p"), ModelDefault("DisplayFormat", "{0} %"), ModelDefault("AllowEdit", "false"), ReadOnly(true)]
        public decimal Tamamlanan 
        {
            get
            {
                if (TalepMiktar > 0)
                    return (OkunanMiktar * 100) / TalepMiktar;
                else
                    return 0;
            }
            set { }
        }

        [XmlIgnore(), NonPersistent, ModelDefault("EditMask", "c"), ModelDefault("AllowEdit", "false"), ReadOnly(true)]
        public decimal KaynakStokMiktar { get; set; }

        [XmlIgnore(), NonPersistent, ModelDefault("EditMask", "c"), ModelDefault("AllowEdit", "false"), ReadOnly(true)]
        public decimal HedefStokMiktar { get; set; } 
        #endregion

        public string SevkDepoKod { get; set; }
        public int SevkDepoId { get; set; }

        public TalepSureleri Oncelik { get; set; }
        public TalepMiktarTip MiktarTip { get; set; }

        [ModelDefault("AllowEdit", "False"), ReadOnly(true)]
        public TalepDurumlari TalepDurumu { get; set; }


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

        

        public TalepKabulDetaylari() { }
        public TalepKabulDetaylari(Session session) : base(session) { }
    }
}
