using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("Oid"), NavigationItem(false),
     ImageName("BO_Organization"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    [ReferansTablo("pub.hurda_detay", SistemTipi.Progress, false)]
    public class UretimHurdalari : XPObject
    {
        private HurdaTipi hurdaTipi;
        [ImmediatePostData]
        public HurdaTipi HurdaTipi
        {
            get { return hurdaTipi; }
            set 
            { 
                SetPropertyValue("HurdaTipi", ref hurdaTipi, value);
                if (!IsLoading && !IsSaving)
                {
                    if (hurdaTipi == BusinessObjects.HurdaTipi.UrunHurdasi && UretimOperasyon != null)
                    {
                        this.Malzeme = this.UretimOperasyon.Malzeme;
                        sourceMalzemeler = null;
                    }
                }
            }
        }

        [Browsable(false), PersistentAlias("HurdaTipi")]
        public int HurdaTip
        {
            get { return hurdaTipi.GetHashCode(); }
        }

        #region Hurda Nedeni
        //[PersistentAlias("Iif(HurdaNedeni is null, 0, HurdaNedeni.HurdaId)")]
        [NonPersistent]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int HurdaNedeniId
        {
            get;
            set;
            //get
            //{
            //    try
            //    {
            //        return Convert.ToInt32(EvaluateAlias("HurdaNedeniId"));
            //    }
            //    catch (Exception exc)
            //    {
            //        System.Diagnostics.Trace.WriteLine("<!-->");
            //        System.Diagnostics.Trace.WriteLine(exc.Message);
            //        System.Diagnostics.Trace.WriteLine("<!-->");
            //        System.Diagnostics.Trace.WriteLine(exc.StackTrace);
            //        return 0;
            //    }
            //}
            //set
            //{
            //    if (!IsLoading && IsSaving)
            //    {
            //        SetPropertyValue<Hurdalar>("HurdaNedeni", ref fHurda, Session.GetObjectByKey<Hurdalar>(value));
            //        OnChanged("HurdaNedeni");
            //    }
            //}
        }

        [Size(DbSize.KodLenght), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string HurdaKod { get; set; }

        Hurdalar fHurda;
        //[DataSourceCriteria("BilesenHurda = '@This.BilesenHurda'")]
        [XmlIgnore(), Association(@"UretimHurdalar.Hurdalar_HurdaId"), NoForeignKey, Persistent("HurdaNedeniId")]
        [ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxSearchEditButtonPropertyEditor")]
        public Hurdalar HurdaNedeni
        {
            get { return fHurda; }
            set { SetPropertyValue<Hurdalar>("HurdaNedeni", ref fHurda, value); }
        }

        [PersistentAlias("Iif(HurdaNedeni is null, '', HurdaNedeni.HurdaAd)")]
        public string HurdaNedenAciklama { get { return Convert.ToString(EvaluateAlias("HurdaNedenAciklama")); } }
        #endregion

        [XmlIgnore(), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int ReceteMasterId { get; set; }
        [XmlIgnore(), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int ReceteDetayId { get; set; }

        #region Malzeme
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false), PersistentAlias("Iif(Malzeme is null, 0, Malzeme.MalzemeId)")]
        public int MalzemeId
        {
            get { return Convert.ToInt32(EvaluateAlias("MalzemeId")); }
            set
            {
                if (!IsLoading && !IsSaving)
                {
                    SetPropertyValue<Malzemeler>("Malzeme", ref fMalzeme, Session.GetObjectByKey<Malzemeler>(value));
                    OnChanged("Malzeme");
                }
            }
        }

        [Size(DbSize.KodLenght), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string MalzemeKod { get; set; }

        Malzemeler fMalzeme;
        [DataSourceProperty("SourceMalzemeler")]
        [Appearance("Malzeme Hurdasi.", Enabled = false, Criteria = "HurdaTipi != 'MalzemeHurdasi'", AppearanceItemType = "ViewItem", Context = "DetailView", TargetItems = "Malzeme")]
        [XmlIgnore(), Association(@"Malzemeler.UretimHurdalar.MalzemeId"), NoForeignKey, Persistent("MalzemeId")]
        public Malzemeler Malzeme
        {
            get { return fMalzeme; }
            set 
            { 
                SetPropertyValue<Malzemeler>("Malzeme", ref fMalzeme, value);
                if (!IsLoading && !IsSaving)
                {
                    if (fMalzeme != null)
                    {
                        this.MalzemeKod = fMalzeme.MalzemeKod;
                        this.BirimId = fMalzeme.BirimId;
                        this.Birim = fMalzeme.Birim;
                    }
                }
            }
        }

        [PersistentAlias("Iif(Malzeme is null, '', Malzeme.MalzemeAd)")]
        public string MalzemeAd { get { return Convert.ToString(EvaluateAlias("MalzemeAd")); } }
        #endregion        

        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int BirimId { get; set; }

        [Size(DbSize.NoLenght), ModelDefault("AllowEdit", "False")]
        public string Birim { get; set; }

        #region Depo
        private int depoId;
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false), PersistentAlias("Iif(Depo is null, 0, Depo.DepoId)")]
        public int DepoId
        {
            get { return Convert.ToInt32(EvaluateAlias("DepoId")); }
            set
            {
                if (!IsLoading && !IsSaving)
                {
                    SetPropertyValue<int>("DepoId", ref depoId, value);
                    SetPropertyValue<Depolar>("Depo", ref fDepo, Session.GetObjectByKey<Depolar>(depoId));
                    OnChanged("DepoId");
                    OnChanged("Depo");
                }
            }
        }
        [Size(DbSize.KodLenght), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string DepoKod { get; set; }
        Depolar fDepo;
        [XmlIgnore(), Association(@"Depolar.UretimHurdalar.DepoId"), NoForeignKey, Persistent("DepoId")]
        [ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxSearchEditButtonPropertyEditor")]
        public Depolar Depo
        {
            get { return fDepo; }
            set { SetPropertyValue<Depolar>("Depo", ref fDepo, value); }
        }
        [VisibleInDetailView(false), VisibleInLookupListView(false), PersistentAlias("Iif(Depo is null, '', Depo.DepoAd)")]
        public string DepoAd { get { return Convert.ToString(EvaluateAlias("DepoAd")); } }
        #endregion

        [XmlIgnore(), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int SatirNo { get; set; }

        [DbType(" DECIMAL(18,4) ")]
        public decimal Miktar { get; set; }

        #region Ambalaj&Brkod
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string PartiNo { get; set; }

        [Appearance("Malzeme Hurdasi Ambalaj.", Enabled = false, Criteria = "HurdaTipi != 'MalzemeHurdasi'", AppearanceItemType = "ViewItem", Context = "DetailView", TargetItems = "KaynakAmbalaj")]
        [XmlIgnore(), Association(@"UretimHurdalar.KaynakAmbalajId"), Persistent("KaynakAmbalajId")]
        [ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxSearchEditButtonPropertyEditor")]
        public Ambalajlar KaynakAmbalaj { get; set; }

        [PersistentAlias("Iif(KaynakAmbalaj is null, '0', KaynakAmbalaj.Durum)")]
        public AmbalajDurumu KaynakAmbalajDurum
        {
            get { return (AmbalajDurumu)Convert.ToInt32(EvaluateAlias("KaynakAmbalajDurum")); }
        }

        protected Ambalajlar fAmbalaj;
        [XmlIgnore(), Association(@"UretimHurdalar.AmbalajId"), Persistent("AmbalajId")]
        [ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxSearchEditButtonPropertyEditor")]
        public Ambalajlar Ambalaj 
        {
            get { return fAmbalaj; }
            set { SetPropertyValue<Ambalajlar>("Ambalaj", ref fAmbalaj, value); }
        }

        [PersistentAlias("Iif(Ambalaj is null, '0', Ambalaj.Durum)")]
        public AmbalajDurumu AmbalajDurum
        {
            get { return (AmbalajDurumu)Convert.ToInt32(EvaluateAlias("AmbalajDurum")); }
        }

        [PersistentAlias("Iif(Ambalaj is null, 0, Ambalaj.Oid)"), 
        VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int AmbalajId 
        {
            get { return Convert.ToInt32(EvaluateAlias("AmbalajId")); }
            set
            {
                if (!IsLoading && !IsSaving)
                {
                    SetPropertyValue<Ambalajlar>("Ambalaj", ref fAmbalaj, Session.GetObjectByKey<Ambalajlar>(value));
                }
            }
        } 
        #endregion

        #region Aciklama
        [Size(DbSize.AciklamaLenght), ModelDefault("RowCount", "2"), XafDisplayName("Aciklama")]
        public string Aciklama1 { get; set; }

        [Size(DbSize.AciklamaLenght), XmlIgnore(), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string Aciklama2 { get; set; }

        [Size(DbSize.AciklamaLenght), XmlIgnore(), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string Aciklama3 { get; set; }

        [Size(DbSize.AciklamaLenght), XmlIgnore(), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string Aciklama4 { get; set; } 
        #endregion

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

        protected UretimOperasyonlari fUretimOperasyon;
        [XmlIgnore(), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Association("UretimOperasyon-Hurdalar")]
        public UretimOperasyonlari UretimOperasyon
        {
            get { return fUretimOperasyon; }
            set { SetPropertyValue<UretimOperasyonlari>("UretimOperasyon", ref fUretimOperasyon, value); }
        }

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

        private List<Malzemeler> sourceMalzemeler = null;
        [Browsable(false),NonPersistent]
        private List<Malzemeler> SourceMalzemeler
        {
            get
            {
                if (hurdaTipi == BusinessObjects.HurdaTipi.MalzemeHurdasi && UretimOperasyon != null && sourceMalzemeler == null)
                {
                    XPQuery<IsEmriBilesenleri> xbilesenler = new XPQuery<IsEmriBilesenleri>(Session);
                    XPQuery<Malzemeler> xmalzemeler = new XPQuery<Malzemeler>(Session);

                    var mlzids = (from bm in xbilesenler
                                  where bm.IsEmriId == UretimOperasyon.IsEmriId
                                  select bm.MalzemeId).ToArray();
                    sourceMalzemeler = (from m in xmalzemeler
                               where mlzids.Contains(m.MalzemeId)
                               select m).ToList();
                }
                return sourceMalzemeler;
            }
        }

        

        public UretimHurdalari() { }
        public UretimHurdalari(Session session) : base(session) { }

    }
}
