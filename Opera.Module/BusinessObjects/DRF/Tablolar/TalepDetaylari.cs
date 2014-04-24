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
    XafDisplayName("Malzeme Talep Detaylari"), XafDefaultProperty("MalzemeKod"), ModelDefault("IsClonable", "True")]
    [OptimisticLocking(false), DeferredDeletion(false)]
    public class TalepDetaylari : XPObject
    {
        [PersistentAlias("Iif(Talep is null, 0, Talep.Oid)"), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int TalepId
        {
            get
            {
                return Convert.ToInt32(EvaluateAlias("TalepId"));
            }
            set
            {
                if (!IsLoading && !IsSaving)
                    SetPropertyValue<Talepler>("Talep", ref _talep, this.Session.GetObjectByKey<Talepler>(value));
            }
        }

        private Talepler _talep;
        [XmlIgnore(), Association("TalepDetaylari_Talepler"), Description("Talep master bilgisi")]
        public Talepler Talep
        {
            get { return _talep; }
            set { SetPropertyValue<Talepler>("Talep", ref _talep, value); }
        }

        #region Malzeme&Birim
        private Malzemeler _malzeme;
        [XmlIgnore(), Association(@"TalepDetaylari.Malzeme.MalzemeId"), NoForeignKey, Persistent("MalzemeId"), XafDisplayName("Malzeme Kodu"), ImmediatePostData, Indexed("Talep", Unique=true)]
        public Malzemeler Malzeme
        {
            get { return _malzeme; }
            set
            {
                SetPropertyValue<Malzemeler>("Malzeme", ref _malzeme, value);
                if (value != null && this.Oid < 1)
                {
                    SetPropertyValue("MalzemeBirim", ref _malzemeBirim,
                                     this.Session.FindObject<V_MalzemeBirimleri>(
                                         CriteriaOperator.Parse(" MalzemeId = ? AND BirimId = ? ", value.MalzemeId,
                                                                value.BirimId)));
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

        [DbType(" DECIMAL(18,4) "), NonPersistent, ModelDefault("EditMask", "c"), ModelDefault("AllowEdit", "false"), ReadOnly(true),
        VisibleInDetailView(false),VisibleInLookupListView(false)]
        public decimal OkunanMiktar { get; set; }

        [DbType(" DECIMAL(18,4) "), ModelDefault("EditMask", "c"), ModelDefault("AllowEdit", "false"), ReadOnly(true)]
        public decimal KabulMiktar { get; set; }

        protected decimal fSevkKalanMiktar = -1;
        [NonPersistent, ModelDefault("EditMask", "c"), ModelDefault("AllowEdit", "false"), ReadOnly(true)]
        public decimal SevkKalanMiktar 
        {
            get
            {
                if (fSevkKalanMiktar != -1)
                    return fSevkKalanMiktar;
                return TalepMiktar - SevkMiktar;
            }
            set
            {
                if (!IsLoading && !IsSaving)
                {
                    fSevkKalanMiktar = value;
                }
            }
        }

        protected decimal fKabulKalanMiktar = -1;
        [NonPersistent, ModelDefault("EditMask", "c"), ModelDefault("AllowEdit", "false"), ReadOnly(true)]
        public decimal KabulKalanMiktar
        {
            get
            {
                if (fKabulKalanMiktar != -1)
                    return fKabulKalanMiktar;
                return SevkMiktar - KabulMiktar;
            }
            set
            {
                if (!IsLoading && !IsSaving)
                {
                    fKabulKalanMiktar = value;
                }
            }
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
        public int TalepDetayId { get; set; }

        [ModelDefault("AllowEdit", "False"), ReadOnly(true)]
        public TalepDurumlari TalepDurumu { get; set; }

        [Association("TalepKabulDetaylari.TalepDetaylari.TalepDetay"), XmlIgnore()]
        public XPCollection<TalepKabulDetaylari> Kabuller
        {
            get { return GetCollection<TalepKabulDetaylari>("Kabuller"); }
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
            
        }

        

        public TalepDetaylari() { }
        public TalepDetaylari(Session session) : base(session) { }
    }
}
