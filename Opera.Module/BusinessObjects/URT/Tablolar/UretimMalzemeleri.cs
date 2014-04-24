using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Xml.Serialization;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.ConditionalAppearance;
using Mikrobar;
using DevExpress.Data.Filtering;
using System.Diagnostics;
using DevExpress.ExpressApp.Model;
///dmiktar
namespace Mikrobar.Module.BusinessObjects
{
    [DebuggerDisplay("HammaddeTakip = {HammaddeTakip}, ReceteMalzemeKod = {ReceteMalzemeKod}, MalzemeKod = {MalzemeKod},  Birim = {Birim}")]
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("UretimId"), NavigationItem(false),
     ImageName("BO_Organization"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    [ReferansTablo("PUB.ham_detay", SistemTipi.Progress, false)]
    public class UretimMalzemeleri : XPObject
    {
        protected HammaddeTakip hammaddeTakip = HammaddeTakip.ZorunluDegil;
        //[ModelDefault("AllowEdit", "False")]
        [ImmediatePostData, XafDisplayName("Kullanim Sekli")]
        [Appearance("UretimMalzemeleri_HammaddeTakip", Enabled = false, Criteria = "Iif(Oid == -1, 0, 1) == 1", AppearanceItemType = "ViewItem", Context = "DetailView", TargetItems = "HammaddeTakip")]
        public HammaddeTakip HammaddeTakip
        {
            get { return hammaddeTakip; }
            set
            {                
                SetPropertyValue<HammaddeTakip>("HammaddeTakip", ref hammaddeTakip, value);
            }
        }

        #region Recete Malzeme
        [Description("Web serviste kullanmak icin.")]
        [PersistentAlias("Iif(ReceteMalzeme is null, 0, ReceteMalzeme.MalzemeId)")]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int ReceteMalzemeId
        {
            get 
            {
                return Convert.ToInt32(EvaluateAlias("ReceteMalzemeId")); 
            }
            set
            {
                SetPropertyValue<Malzemeler>("ReceteMalzeme", ref fReceteMalzeme, Session.GetObjectByKey<Malzemeler>(value));
            }
        }

        protected string fReceteMalzemeKod = string.Empty;
        [Size(DbSize.KodLenght), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string ReceteMalzemeKod 
        {
            get
            {
                if (string.IsNullOrEmpty(fReceteMalzemeKod) && fReceteMalzeme != null)
                    return fReceteMalzemeKod = fReceteMalzeme.MalzemeKod;
                return (!string.IsNullOrEmpty(fReceteMalzemeKod)) ? fReceteMalzemeKod : "";
            }
            set
            {
                SetPropertyValue<string>("ReceteMalzemeKod", ref fReceteMalzemeKod, value);
            }
        }

        Malzemeler fReceteMalzeme;
        [DataSourceProperty("UretimOperasyon.SourceMalzemeler"), ImmediatePostData]
        [XmlIgnore(), Association(@"UretimMalzemeleri.Malzemeler.ReceteMalzeme"), NoForeignKey, Persistent("ReceteMalzemeId")]
        [Appearance("UretimMalzemeleri_ReceteMalzeme", Enabled = false, Criteria = "Iif(Oid == -1, 0, 1) == 1", AppearanceItemType = "ViewItem", Context = "DetailView", TargetItems = "ReceteMalzeme")]
        public Malzemeler ReceteMalzeme
        {
            get { return fReceteMalzeme; }
            set
            {
                SetPropertyValue<Malzemeler>("ReceteMalzeme", ref fReceteMalzeme, value);
                if (!IsLoading && !IsSaving && fReceteMalzeme != null)
                {
                    this.ReceteMalzemeKod = fReceteMalzeme.MalzemeKod;                    
                    if (this.UretimOperasyon != null)
                    {
                        XPQuery<IsEmriBilesenleri> xbilesenler = new XPQuery<IsEmriBilesenleri>(Session);
                        IsEmriBilesenleri xbil = (from bm in xbilesenler
                                                  where bm.IsEmriId == UretimOperasyon.IsEmriId &&
                                                  bm.MalzemeId == fReceteMalzeme.MalzemeId
                                                  select bm).FirstOrDefault();
                        if (xbil != null)
                        {
                            this.BirimMiktar = xbil.BirimMiktar;
                            this.BirimId = xbil.BirimId;
                            this.Birim = xbil.Birim;
                        }
                    }
                }
            }
        }

        [PersistentAlias("Iif(ReceteMalzeme is null, '', ReceteMalzeme.MalzemeAd)")]
        public string ReceteMalzemeAd { get { return Convert.ToString(EvaluateAlias("ReceteMalzemeAd")); } }

        #endregion

        #region Malzeme

        [Description("Web serviste kullanmak icin.")]
        [PersistentAlias("Iif(Malzeme is null, 0, Malzeme.MalzemeId)")]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int MalzemeId
        {
            get { return Convert.ToInt32(EvaluateAlias("MalzemeId")); }
            set
            {
                SetPropertyValue<Malzemeler>("Malzeme", ref fMalzeme, Session.GetObjectByKey<Malzemeler>(value));
            }
        }

        protected string fMalzemeKod = string.Empty;
        [Size(DbSize.KodLenght), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string MalzemeKod
        {
            get
            {
                if (string.IsNullOrEmpty(fMalzemeKod) && fMalzeme != null)
                    return fMalzemeKod = fMalzeme.MalzemeKod;
                return (!string.IsNullOrEmpty(fMalzemeKod)) ? fMalzemeKod : "";
            }
            set
            {
                SetPropertyValue<string>("MalzemeKod", ref fMalzemeKod, value);
            }
        }

        Malzemeler fMalzeme;
        [ModelDefault("AllowEdit", "False")]
        //[ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxSearchEditButtonPropertyEditor")]
        [XmlIgnore(), Association(@"UretimMalzemeleri.Malzemeler.Malzeme"), NoForeignKey, Persistent("MalzemeId")]        
        public Malzemeler Malzeme
        {
            get { return fMalzeme; }
            set
            {
                SetPropertyValue<Malzemeler>("ReceteMalzeme", ref fMalzeme, value);
                if (!IsLoading && !IsSaving && fMalzeme != null)
                {
                    this.MalzemeKod = fMalzeme.MalzemeKod;
                    this.BirimId = fMalzeme.BirimId;
                    this.Birim = fMalzeme.Birim;
                }
            }
        }

        [PersistentAlias("Iif(Malzeme is null, '', Malzeme.MalzemeAd)")]
        public string MalzemeAd { get { return Convert.ToString(EvaluateAlias("MalzemeAd")); } set { } }
        #endregion

        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int BirimId { get; set; }

        [ReferansAlan("ham_detay", "birim")]
        [Size(DbSize.KodLenght), ModelDefault("AllowEdit", "False")]
        public string Birim { get; set; }

        [ReferansAlan("ham_detay", "recete_miktar")]
        [DbType(" DECIMAL(18,4) ")]
        [Appearance("Malzeme Hurdasi.", Enabled = false, Criteria = "HammaddeTakip != 'ZorunluDegil'", AppearanceItemType = "ViewItem", Context = "DetailView", TargetItems = "BirimMiktar")]
        public decimal BirimMiktar { get; set; }

        //protected string fDMiktar;
        //[NonPersistent, VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        //public string DMiktar 
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(fDMiktar))
        //            return this.BirimMiktar.ToString(Lang.CultureInfoEn);
        //        return fDMiktar;
        //    }
        //    set
        //    {
        //        if (!IsLoading && !IsSaving)
        //            fDMiktar = value;
        //    }
        //}

        [ReferansAlan("ham_detay", "kullanilan_miktar"), DbType(" DECIMAL(18,4) ")]
        public decimal KullanilanMiktar { get; set; }

        [ReferansAlan("ham_detay", "kullanilan_miktar"), DbType(" DECIMAL(18,4) ")]//, ModelDefault("AllowEdit", "False")] //test icin acildi
        public decimal Miktar { get; set; }

        [ReferansAlan("ham_detay", "fire_miktar")]
        [DbType(" DECIMAL(18,4) ")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public decimal FireMiktar { get; set; }

        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [ReferansAlan("ham_detay", "giris_sekli")]
        public GirisSekli GirisSekli { get; set; }

        [ReferansAlan(TabloAd = "ham_detay", KolonAd = "alter1")]
        [Description("Alternatif malzeme kullanildiysa")]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public bool Alternatif { get; set; }

        protected Ambalajlar fAmbalaj;
        [XmlIgnore(), Association(@"UretimMalzemeleri.AmbalajId"), Persistent("AmbalajId")]
        [ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxSearchEditButtonPropertyEditor")]
        public Ambalajlar Ambalaj 
        {
            get { return fAmbalaj; }
            set
            {
                SetPropertyValue<Ambalajlar>("Ambalaj", ref fAmbalaj, value);               
            }
        }

        [PersistentAlias("Iif(Ambalaj is null, '0', Ambalaj.Durum)")]
        public AmbalajDurumu AmbalajDurum
        {
            get { return (AmbalajDurumu)Convert.ToInt32(EvaluateAlias("AmbalajDurum")); }
        }

        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [PersistentAlias("Iif(Ambalaj is null, 0, Ambalaj.Oid)"), Description("Web serviste kullanmak icin.")]
        public int AmbalajId
        {
            get { return Convert.ToInt32(EvaluateAlias("AmbalajId")); }
            set
            {
                if (!IsLoading && !IsSaving)
                {
                    SetPropertyValue("Ambalaj", Session.GetObjectByKey<Ambalajlar>(value));
                }
            }
        }

        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public string PartiNo { get; set; }

        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [PersistentAlias("Iif(Ambalaj is null, '', Ambalaj.Barkod)"), Description("Web serviste kullanmak icin.")]
        public string AmbalajBarkod
        {
            get { return Convert.ToString(EvaluateAlias("AmbalajBarkod")); }
            set
            {
            }
        }

        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [PersistentAlias("Iif(Ambalaj is null, 0, Ambalaj.AmbalajDetaylari.Sum(Kalan))"), Description("Web serviste kullanmak icin.")]
        public decimal AmbalajMiktar
        {
            get { return Convert.ToDecimal(EvaluateAlias("AmbalajMiktar")); }
            set
            {
            }
        }

        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public string RenkNo { get; set; }

        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [XmlIgnore(), Association("UretimMalzemeleri_OperasyonMalzemeleri")/*, Indexed("Ambalaj;MalzemeId;ReceteMalzemeId;Durum", Unique = true)*/, Description("Uretim kayit bilgisi")]
        public UretimOperasyonlari UretimOperasyon { get; set; }

        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [PersistentAlias("Iif(UretimOperasyon is null, 0, UretimOperasyon.Oid)"), Description("Web serviste kullanmak icin.")]
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

        #region Butonlar
        
        //[Action(PredefinedCategory.Save, Caption = "Ambalaj Bul", TargetObjectsCriteria = "[Durum] = 0 And Oid= -1", ImageName = "BO_Product_Group", ToolTip = "Ambalaj bul", SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject)]
        //public void AmbalajBul()
        //{
        //    try
        //    {
        //        //if (this.Oid == -1)
        //        //{
        //        //if (UretimOrtak.UretimParametreleri.KasaTakibi)
        //        //{
        //        //    if (AmbalajTur.IsNull())
        //        //    {
        //        //        AmbalajTur = Session.GetObjectByKey<AmbalajTurleri>(UretimOrtak.UretimParametreleri.UretimYeniAmbalajTurId);

        //        //    }
        //        //}
        //        //}
        //    }
        //    catch (Exception exc)
        //    {
        //        throw exc;
        //    }
        //}
        //[Action(Caption = "Uretim Raporu", TargetObjectsCriteria = "1 = 1", ImageName = "Action_CloseAllWindows")]
        //public void UretimRapor()
        //{
        //    try
        //    {
        //        //this.Durum = KayitDurumu.Aktarilacak;


        //        //Frame.GetController<WebReportServiceController>().ShowPreview((IReportData)Object);

        //        //if (WebWindow.CurrentRequestWindow != null)
        //        //    WebWindow.CurrentRequestWindow.RegisterClientScript("js:yenile", "javascript:location.reload(true)");

        //    }
        //    catch (Exception exc)
        //    {

        //        throw exc;
        //    }
        //}
        #endregion

        protected override void OnSaving()
        {
            if (this.IsDeleted)
            {
                if (Ambalaj != null)
                {
                    Ambalaj.Durum = AmbalajDurumu.Bosta;
                    Ambalaj.DurumAciklama = AmbalajDurumlari.KASA_URETIMDE_KULLANILDI;
                    Ambalaj.Save();
                }
            }
            base.OnSaving();
        }

        protected override void OnDeleting()
        {
            if (Ambalaj != null)
            {
                if (Ambalaj.Durum == AmbalajDurumu.Kilitli)
                {
                    Ambalaj.Durum = AmbalajDurumu.Bosta;
                    Ambalaj.DurumAciklama = AmbalajDurumlari.KASA_URETIMDE_KULLANILDI;
                    Ambalaj.Save();
                }
            }

            base.OnDeleting();
        }

        public UretimMalzemeleri() { }
        public UretimMalzemeleri(Session session) : base(session) { }

    }
}
