using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Xml.Serialization;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using System.Diagnostics;
using DevExpress.ExpressApp.Web;
using Mikrobar;
using DevExpress.ExpressApp.ConditionalAppearance;

using Mikrobar.Islemler;

using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;

namespace Mikrobar.Module.BusinessObjects
{
    //[ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    //[OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("IstasyonKod"), //LookupEditorMode(LookupEditorMode.AllItemsWithSearch),
    // ImageName("BO_Organization"), NavigationItem(false)]
    [ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    //[MapInheritance(MapInheritanceType.OwnTable)]
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions,
    XafDefaultProperty("IstasyonKod"), NavigationItem(false), ImageName("BO_Organization")]
    [DebuggerDisplay("IstasyonKod = {IstasyonKod}, IstasyonId = {IstasyonId}, FasonIstasyon = {FasonIstasyon}")]
    [ReferansTablo("PRDD_WSTATION", SistemTipi.WebErp, QueryType = QueryType.Yoksa,
        SqlSorgu = @"SELECT CAST(WSTATION_ID AS INTEGER) AS ""IstasyonId"", WSTATION_CODE AS ""IstasyonKod"", DESCRIPTION AS ""IstasyonAd"", ISOUTSIDE_WSTATION AS ""FasonIstasyon"", WCENTER_ID AS ""IsMerkeziId"", WSTATION_TYPE_ID AS ""IsIstasyonTipId"", ALLOW_MULTI_OPERATION AS ""CokluUretim"", ALLOW_POSTPONE AS ""ArayaIsSokma"", IS_NOT_START_DIFF_WS AS BASKA_ISTASYONDA_CALISAMAZ, SUPPLIER_ID AS ""CariId"", MTR_OUTPUT_WHOUSE_ID AS ""Depo"", PRD_INPUT_WHOUSE_ID AS ""Depo2"",SEMI_PRD_MTR_WHOUSE_ID AS ""Depo3""   FROM PRDD_WSTATION WHERE CO_ID = #Firma# AND BRANCH_ID = #Isyeri#")]
    [ReferansTablo("pub.is_istasyon", SistemTipi.Progress, QueryType = QueryType.Yoksa,
        SqlWhere = " and cast(i.rowid as int) = {0} ",
        SqlSorgu = "select cast(i.rowid as int) AS \"IstasyonId\",i.istasyon_kod as \"IstasyonKod\",i.istasyon_ad as \"IstasyonAd\",case when i.fason = 1 then 1 else 0 end as \"FasonIstasyon\",Cast(m.rowid as int) as \"IsMerkeziId\",i.ismer_kod as \"IsMerkeziKod\",m.ismer_ad as \"IsMerkeziAd\",cast(d1.rowid as int) as \"MalzemeCikisDepo\",d1.depo_kod as \"DepoKod\",	cast(d2.rowid as int) as \"UrunGirisDepo\",d2.depo_kod as \"DepoKod2\" from pub.is_istasyon i inner join pub.is_merkezi m on i.ismer_kod = m.ismer_kod and i.firma_kod = m.firma_kod left outer join pub.depo d1 on i.depo_kod = d1.depo_kod and i.firma_kod = d1.firma_kod left outer join pub.depo d2 on i.mdepo_kod = d2.depo_kod and i.firma_kod = d2.firma_kod where i.firma_kod = '#FirmaKod#' and i.aktif_pasif = 'Aktif' ")]
    [ReferansTablo("TBLISTASYON,TBLMRPMAKINE", SqlSorgu = @"SELECT * FROM Barset.V_Istasyonlar WHERE ( 1 = 1 ) ", SqlWhere = @" AND IstasyonId = {0} ", SistemTipi = SistemTipi.Netsis, QueryType = QueryType.Yoksa)]
    public class Istasyonlar : XPBaseObject
    {
        //rifat.gunes@inanu.edu.tr
        [ModelDefault("AllowEdit", "False")]
        [ReferansAlan("cast(rowid as int)", SistemTipi = SistemTipi.Progress, KeyField = true, KeyIndex = 0)]
        [Key(AutoGenerate = false), VisibleInLookupListView(true), ModelDefault("DisplayFormat", "d")]
        public int IstasyonId { get; set; }

        [ReferansAlan("istasyon_kod", SistemTipi = SistemTipi.Progress)]
        [Size(DbSize.KodLenght), VisibleInLookupListView(true)]
        public string IstasyonKod { get; set; }

        [ReferansAlan("istasyon_ad", SistemTipi = SistemTipi.Progress)]
        [Size(DbSize.AdLenght), VisibleInLookupListView(true)]
        public string IstasyonAd { get; set; }

        [Size(DbSize.AciklamaLenght), VisibleInLookupListView(true), XmlIgnore()]
        public string Aciklama { get; set; }

        [XmlIgnore(), VisibleInLookupListView(false)]
        [ReferansAlan("case when fason = 1 then 1 else 0 end", SistemTipi = SistemTipi.Progress)]
        public bool FasonIstasyon { get; set; }

        [VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(false), ModelDefault("DisplayFormat", "d")]
        public int IsMerkeziId { get; set; }

        [VisibleInLookupListView(true)]
        public string IsMerkeziKod { get; set; }

        [VisibleInLookupListView(true)]
        public string IsMerkeziAd { get; set; }

        #region Istasyon Depolari
        //MalzemeCikisDepo
        [NonPersistent, VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(false)]
        public int MalzemeCikisDepoId
        {
            get
            {
                if (MalzemeCikisDepo != null) return MalzemeCikisDepo.DepoId;
                else return 0;
            }
            set
            {
                SetPropertyValue<Depolar>("MalzemeCikisDepo", Session.GetObjectByKey<Depolar>(value));
                OnChanged("MalzemeCikisDepo");
            }
        }
        [NonPersistent]
        public string MalzemeCikisDepoKod { get; set; }
        [XmlIgnore(), Persistent("MalzemeCikisDepo"), Association(@"Istasyonlar.Depo_MalzemeCikisDepo"), VisibleInLookupListView(true)]
        public Depolar MalzemeCikisDepo { get; set; }
        //UrunGirisDepo
        [NonPersistent, VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(false)]
        public int UrunGirisDepoId
        {
            get
            {
                if (UrunGirisDepo != null) return UrunGirisDepo.DepoId;
                else return 0;
            }
            set
            {
                SetPropertyValue<Depolar>("UrunGirisDepo", Session.GetObjectByKey<Depolar>(value));
                OnChanged("UrunGirisDepo");
            }
        }
        [NonPersistent]
        public string UrunGirisDepoKod { get; set; }
        [XmlIgnore(), Persistent("UrunGirisDepo"), Association(@"Istasyonlar.Depo_UrunGirisDepo"), VisibleInLookupListView(true)]
        public Depolar UrunGirisDepo { get; set; }
        //YariMamulCikisDepo
        [NonPersistent, VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(false)]
        public int YariMamulCikisDepoId
        {
            get
            {
                if (YariMamulCikisDepo != null) return YariMamulCikisDepo.DepoId;
                else return 0;
            }
            set
            {
                SetPropertyValue<Depolar>("YariMamulCikisDepo", Session.GetObjectByKey<Depolar>(value));
                OnChanged("YariMamulCikisDepo");
            }
        }
        [NonPersistent]
        public string YariMamulCikisDepoKod { get; set; }
        [XmlIgnore(), Persistent("YariMamulCikisDepo"), Association(@"Istasyonlar.Depo_YariMamulCikisDepo"), VisibleInLookupListView(true),
        Description("Yari mamul tuketim deposu web erpde ymamul deposu ayri")]
        public Depolar YariMamulCikisDepo { get; set; }
        //YariMamulGirisDepo
        [NonPersistent, VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(false)]
        public int YariMamulGirisDepoId
        {
            get
            {
                if (YariMamulGirisDepo != null) return YariMamulGirisDepo.DepoId;
                else return 0;
            }
            set
            {
                SetPropertyValue<Depolar>("YariMamulGirisDepo", Session.GetObjectByKey<Depolar>(value));
                OnChanged("YariMamulGirisDepo");
            }
        }
        [NonPersistent]
        public string YariMamulGirisDepoKod { get; set; }
        [XmlIgnore(), Persistent("YariMamulGirisDepo"), Association(@"Istasyonlar.Depo_YariMamulGirisDepo"), VisibleInLookupListView(true),
        Description("Yari mamul tuketim deposu web erpde ymamul deposu ayri")]
        public Depolar YariMamulGirisDepo { get; set; }
        #endregion<<Depolar>>

        [VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(false), ModelDefault("DisplayFormat", "d")]
        public int IsIstasyonTipId { get; set; }

        [VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(false), ModelDefault("DisplayFormat", "d")]
        public int CariId { get; set; }

        [Size(DbSize.KodLenght)]
        public string CariKod { get; set; }

        [NonPersistent]
        public string CariAd { get; set; }

        [XmlIgnore(), ModelDefault("DisplayFormat", "{0:0.00}")]
        [Description("Otomasyon için duruş süresi"), VisibleInLookupListView(false)]
        public decimal DurusSuresi { get; set; }

        [XmlIgnore(), VisibleInLookupListView(false)]
        [Description("Uretim isciliklerinin takibi operasyonelmi istasyon bazindami olacak?")]
        public UretimIscilikDurumu IscilikTakibi { get; set; }

        [XmlIgnore(), VisibleInLookupListView(false)]
        [Description("Maliyet katsayinin kaynagini belirtir?")]
        //[Appearance("Istasyonlar_MaliyetKatsayi", Enabled = false, Criteria = " MaliyetKatsayiSor == false", AppearanceItemType = "ViewItem", Context = "DetailView", TargetItems = "MaliyetKatsayi")]
        public MaliyetKatsayiBilgisi MaliyetKatsayi { get; set; }

        #region UretimParametresi
        [DXDescription("Uretim sonrasi uretim miktari kadar seri olusturulsun mu?")]
        public bool SeriOlustur { get; set; }

        [DXDescription("Malzeme kullanılsın mı?")]
        public bool MalzemeKullanimi { get; set; }

        [DXDescription("Uretim sonrasi olusan etiketler yazdirilsin mi?")]
        public bool Yazdir { get; set; }

        [DXDescription("Coklu uretim acik olan istasyonlarda tum uretimler tek seferde kapatilsin mi?")]
        public bool TopluKapat { get; set; }

        [DXDescription("Uretim veri toplama varmi?")]
        public bool Otomasyon { get; set; }

        [DXDescription("Uretim katsayi girilsin mi?")]
        public bool UretimKatsayiSor { get; set; }

        [DXDescription("Maliyet katsayi bilgisi girilsin mi?")]
        public bool MaliyetKatsayiSor { get; set; }

        [DXDescription("Surec kalite kontrol var mi?")]
        public bool SurecKaliteKontrolVar { get; set; }

        [DXDescription("Uretim kydinda cari bilgisi girilmesi icin")]
        public bool CariBazindaUretim { get; set; }

        [XmlIgnore(), DXDescription("Hurda kaydinda etiket olsturulsun mu?")]
        public bool HurdayiEtiketle { get; set; }

        [DXDescription("URetim miktarina varsayilan deger atamak icin.")]
        public decimal VarsayilanUretimMiktari { get; set; }

        [XmlIgnore(), DXDescription("Kullanilan malzemelerden ayni oranda tuketim icin")]
        public bool EsitTuketim { get; set; }

        [DXDescription("Ek alanlar kullanimi icin")]
        public bool EkAlanlar { get; set; }

        [DXDescription("Ek alanlar kullanimi icin")]
        public bool EkAciklamalar { get; set; }

        [XmlIgnore(), VisibleInLookupListView(false)]
        public bool CokluUretim { get; set; }

        [XmlIgnore(), VisibleInLookupListView(false)]
        public bool ArayaIsSokma { get; set; }

        [DXDescription("Kontrol kriter bilgilerinin gösterilmesi icin")]
        public bool KontrolKriterleri { get; set; }

        [DXDescription("Toplu Uretim Girisi")]
        public bool TopluUretimGirisi { get; set; }

        [DXDescription("Üretim miktarını ağırlık merkezinden çekmek için")]
        public bool AgirlikMerkeziKullan { get; set; }


        #region Yazicilar
        [VisibleInDetailView(false), VisibleInLookupListView(true), VisibleInListView(true)]
        public string Yazici { get; set; }

        [VisibleInDetailView(false), VisibleInLookupListView(true), VisibleInListView(true)]
        public string Yazici2 { get; set; }

        protected Yazicilar _kutuYazicisi;
        [XafDisplayName("Yazıcı")]
        [DataSourceProperty("Yazicilar"), ImmediatePostData, NonPersistent, XmlIgnore(),
        VisibleInDetailView(true), VisibleInLookupListView(false), VisibleInListView(false)]
        public Yazicilar KutuYazicisi
        {
            get
            {
                try
                {
                    if (!IsLoading && !IsSaving)
                    {
                        try
                        {
                            if (_kutuYazicisi == null)
                            {
                                _kutuYazicisi = new Yazicilar(this.Session) { YaziciAd = this.Yazici };
                            }
                        }
                        catch (ObjectDisposedException disp) { }
                        catch (Exception) { }
                    }
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return _kutuYazicisi;
            }
            set
            {
                SetPropertyValue("KutuYazicisi", ref _kutuYazicisi, value);
                if (value != null)
                {
                    this.Yazici = value.YaziciAd;
                    OnChanged("Yazici");
                }
                else
                {
                    this.Yazici = string.Empty;
                    OnChanged("Yazici");
                }
            }
        }

        protected Yazicilar _koliYazicisi;
        [XafDisplayName("Yazıcı 2")]
        [DataSourceProperty("Yazicilar"), ImmediatePostData, NonPersistent, XmlIgnore(),
        VisibleInDetailView(true), VisibleInLookupListView(false), VisibleInListView(false)]
        public Yazicilar KoliYazicisi
        {
            get
            {
                try
                {
                    if (!IsLoading && !IsSaving)
                    {
                        try
                        {
                            if (_koliYazicisi == null)
                            {
                                _koliYazicisi = new Yazicilar(this.Session) { YaziciAd = this.Yazici2 };
                            }
                        }
                        catch (ObjectDisposedException disp) { }
                        catch (Exception) { }
                    }
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return _koliYazicisi;
            }
            set
            {
                SetPropertyValue("KoliYazicisi", ref _koliYazicisi, value);
                if (value != null)
                {
                    this.Yazici2 = value.YaziciAd;
                    OnChanged("Yazici2");
                }
                else
                {
                    this.Yazici2 = string.Empty;
                    OnChanged("Yazici2");
                }
            }
        }

        private List<Yazicilar> _yazicilar = null;
        [Browsable(false), NonPersistent, XmlIgnore(),
        VisibleInDetailView(true), VisibleInLookupListView(true), VisibleInListView(true)]
        public List<Yazicilar> Yazicilar
        {
            get
            {
                /*try
                {
                    if (_yazicilar == null || _yazicilar.Count < 1)
                    {
                        _yazicilar = new List<Yazicilar>();

                        YaziciParam param = new YaziciParam();
                        param.Islem = YaziciIslemleri.YaziciList;
                        YazdirmaIslemleri yazicilar = new YazdirmaIslemleri();
                        YaziciResult res = yazicilar.Islem(param);
                        if (res != null)
                        {
                            foreach (string s in res.YaziciList)
                            {
                                _yazicilar.Add(new Yazicilar(this.Session) { YaziciAd = s });

                            }
                        }
                    }
                    return _yazicilar;
                }
                catch (Exception exc)
                {
                }*/
                return null;
            }
        }

        #endregion

        #region Tasarim

        protected string _etiketTasarim;
        [VisibleInDetailView(false), VisibleInLookupListView(true), VisibleInListView(true)]
        public string EtiketTasarim
        {
            get { return _etiketTasarim; }
            set
            {
                SetPropertyValue("EtiketTasarim", ref _etiketTasarim, value);
                if (EtiketTasarimi == null)
                {
                    EtiketTasarimi = new SecimObj() { Ad = value };
                    OnChanged("KutuTasarimi");
                }
            }
        }

        protected SecimObj _etiketTasarimi;
        [DataSourceProperty("Tasarimlar"), NonPersistent, XmlIgnore(),
        VisibleInDetailView(true), VisibleInLookupListView(false), VisibleInListView(false)]
        public SecimObj EtiketTasarimi
        {
            get { return _etiketTasarimi; }
            set
            {
                SetPropertyValue("EtiketTasarimi", ref _etiketTasarimi, value);
                if (value != null)
                {
                    this.EtiketTasarim = value.Ad;
                    OnChanged("EtiketTasarim");
                }
                else
                {
                    this.EtiketTasarim = string.Empty;
                    OnChanged("EtiketTasarim");
                }
            }
        }

        private List<SecimObj> tasarimlar = null;
        [Browsable(false), NonPersistent, XmlIgnore()]
        public List<SecimObj> Tasarimlar
        {
            get
            {
                try
                {
                    if (tasarimlar == null)
                    {
                        tasarimlar = new List<SecimObj>();
                        XPQuery<AllDesing> _tasarimlar = new XPQuery<AllDesing>(this.Session);
                        var a = (from t in _tasarimlar orderby t.name select new { Kod = t.ViewName, Ad = t.name }).ToList();
                        foreach (var b in a)
                            tasarimlar.Add(new SecimObj() { Kod = b.Kod, Ad = b.Ad });
                    }
                    return tasarimlar;
                }
                catch //(Exception exc)
                {
                }
                return null;
            }
        }

        #endregion

        [XmlIgnore(), Association(@"Istasyonlar.AmbalajTurleri.AmbalajTur"), NoForeignKey]
        public AmbalajTurleri AmbalajTur { get; set; }

        [VisibleInLookupListView(true), VisibleInDetailView(false),
        PersistentAlias("Iif(AmbalajTur is null, '', AmbalajTur.AmbalajTip)")]
        public string AmbalajTip
        {
            get
            {
                try
                {
                    if (!IsLoading && !IsSaving)
                        return Convert.ToString(EvaluateAlias("AmbalajTip"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }

                return "";
            }
            set
            {
            }
        }
        #endregion

        //params

        [XmlIgnore(), Association(@"Istasyon.Istasyonlar.IstasyonId"), VisibleInDetailView(false), NoForeignKey]
        public XPCollection<UretimOperasyonlari> UretimOperasyonlari
        {
            get { return GetCollection<UretimOperasyonlari>(@"UretimOperasyonlari"); }
        }

        [XmlIgnore(), Association(@"IstasyonIscilikleri.Istasyonlar.Istasyon"), VisibleInDetailView(false), NoForeignKey]
        public XPCollection<IstasyonIscilikleri> IstasyonIscilikleri
        {
            get { return GetCollection<IstasyonIscilikleri>(@"IstasyonIscilikleri"); }
        }

        [XmlIgnore(), Association(@"UretimDuruslari-Istasyon", typeof(UretimDuruslari)), NoForeignKey, VisibleInDetailView(false)]
        public XPCollection<UretimDuruslari> UretimDuruslari
        {
            get
            {
                return GetCollection<UretimDuruslari>("UretimDuruslari");
            }
        }

        [XmlIgnore(), Association("IstasyonlarSabitDuruslar", typeof(SabitDuruslar)), NoForeignKey, VisibleInDetailView(false)]
        public XPCollection SabitDuruslar { get { return GetCollection("SabitDuruslar"); } }

        [XmlIgnore(), Association(@"CihazDetaylari.Istasyonlar.IstasyonId"), VisibleInDetailView(false), NoForeignKey]
        public XPCollection<CihazDetaylari> CihazDetaylari
        {
            get { return GetCollection<CihazDetaylari>(@"CihazDetaylari"); }
        }

        /*[XmlIgnore(), Association(@"OperasyonDetaylari.Istasyonlar_IstasyonId")]
        public XPCollection<OperasyonDetaylari> Detaylar
        {
            get { return GetCollection<OperasyonDetaylari>(@"Detaylar"); }
        }*/

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

        #region Metodlar
        [XmlIgnore(), Browsable(false)]
        public IsMerkezi IsMerkezi
        {
            get
            {
                return new IsMerkezi(this.Session)
                {
                    IsMerkeziId = this.IsMerkeziId,
                    IsMerkeziKod = this.IsMerkeziKod,
                    Aciklama = this.IsMerkeziAd
                };
            }
        }
        #endregion

        #region Butonlar
        [Action(Caption = "Guncelle", ImageName = "Action_Refresh", ToolTip = "Bilgileri guncelle..")]
        public void Entegrasyon()
        {
            ////try
            ////{
            ////    Mikrobar.Entegre.DataQuery query = new Mikrobar.Entegre.DataQuery(this.GetType());
            ////    object ret = query.Entegre();

            ////    //DevExpress.Xpo.DB.SelectedData data = this.Session.ExecuteSproc("sp_CariGuncelleERP");
            ////    if (WebWindow.CurrentRequestWindow != null)
            ////        WebWindow.CurrentRequestWindow.RegisterClientScript("tmm" + this.GetType().Name, "alert('İşlem tamamlandı. Sonuc:" + query.HataMesaji + "');");
            ////}
            ////catch (Exception exc)
            ////{
            ////    throw exc;
            ////}
        }
        #endregion

        #region NoneBrowsable
        [Browsable(false)]
        public string SonUretimBilgisi()
        {
            string strSonUretim = string.Empty;
            XPQuery<UretimOperasyonlari> _sonOp = new XPQuery<UretimOperasyonlari>(this.Session);
            var query = (from o in _sonOp
                         where o.Durum == KayitDurumu.Aktarildi && o.IstasyonId == this.IstasyonId
                         orderby o.Oid descending
                         select new { o.BitisTarihi }).Take(1);
            if (query != null && query.Count() > 0)
            {
                var date = query.Select(x => x.BitisTarihi).FirstOrDefault();
                strSonUretim = string.Format("S. Üretim:{0}, {1} dk", date.ToString("HH:mm"), DateTime.Now.Subtract(date).Minutes);
            }
            return strSonUretim;
        }

        [Browsable(false)]
        public List<V_UretimOperasyonlari> AcikUretimler()
        {
            List<V_UretimOperasyonlari> resp = new List<V_UretimOperasyonlari>();
            try
            {
                XPQuery<UretimMalzemeleri> xUretimMalzemeleri = new XPQuery<UretimMalzemeleri>(this.Session);
                XPQuery<UretimDuruslari> xUretimDuruslari = new XPQuery<UretimDuruslari>(this.Session);
                XPQuery<V_Ambalajlar> xAmbalajlar = new XPQuery<V_Ambalajlar>(this.Session);
                List<UretimOperasyonlari> xUretimlerTmp = new XPQuery<UretimOperasyonlari>(this.Session)
                    .Where(x => x.IstasyonId == IstasyonId && x.Durum == KayitDurumu.Yeni)
                    .OrderByDescending(x => x.BaslangicTarihi)
                    .ToList();
                resp = (from x in xUretimlerTmp
                        select new V_UretimOperasyonlari()
                        {
                            UretimId = x.Oid,
                            IsEmriId = x.IsEmriId,
                            MalzemeId = x.MalzemeId,
                            MalzemeKod = x.MalzemeKod,
                            MalzemeAd = x.MalzemeAd,
                            BirimId = x.BirimId,
                            Birim = x.Birim,
                            Miktar = x.Miktar,
                            NetMiktar = x.NetMiktar,
                            FireMiktari = x.FireMiktari,
                            PlanlananMiktar = x.PlanlananMiktar,
                            UretilenMiktar = x.UretilenMiktar,
                            IsEmriDetayId = x.IsEmriDetayId,
                            IsEmriNo = x.IsEmriNo,
                            IstasyonId = this.IstasyonId,
                            IstasyonKod = x.IstasyonKod,
                            OperasyonId = x.OperasyonId,
                            OperasyonNo = x.OperasyonNo,
                            OperasyonKod = x.OperasyonKod,
                            OperasyonAd = x.OperasyonAd,
                            BaslangicTarihi = x.BaslangicTarihi,
                            BitisTarihi = x.BitisTarihi,
                            UretimSuresi = x.UretimSuresi,
                            NetUretimSuresi = x.NetUretimSuresi,
                            KatSayi1 = x.KatSayi1,
                            KatSayi2 = x.KatSayi2,
                            Aciklama = x.Aciklama,
                            Aciklama2 = x.Aciklama2,
                            UretimDurum = x.Durum,
                            VardiyaId = x.VardiyaId,
                            VardiyaKod = x.VardiyaKod,
                            VardiyaAciklama = x.VardiyaAciklama,
                            CariId = x.CariId,
                            CariKod = x.CariKod,
                            CariAd = x.CariAd,
                            AmbalajId = x.AmbalajId,
                            Barkod = x.Barkod,
                            AmbalajMiktar = x.AmbalajMiktar,
                            Ambalaj = (from amb in xAmbalajlar
                                       where amb.AmbalajId == x.AmbalajId &&
                                       (amb.Durum == AmbalajDurumu.Bosta || amb.Durum == AmbalajDurumu.Isleniyor || amb.Durum == AmbalajDurumu.Bekliyor)
                                       select amb).FirstOrDefault(),
                            UretimDuruslari = (from d in xUretimDuruslari
                                               where d.UretimOperasyon == x
                                               orderby d.BaslangicTarihi
                                               select new V_UretimDuruslari()
                                               {
                                                   Aciklama = d.Aciklama,
                                                   BaslangicTarihi = d.BaslangicTarihi,
                                                   BitisTarihi = d.BitisTarihi,
                                                   Durum = (int)d.Durum,
                                                   DurusAd = d.Durus.DurusAd,
                                                   DurusKod = d.DurusKod,
                                                   DurusSuresi = (int)d.DurusSuresi,
                                                   DurusTanimId = d.DurusNedeniId,
                                                   DurusTip = d.Durus.DurusTip,
                                                   IsEmriBaglanti = d.Durus.IsEmriBaglanti,
                                                   IstasyonAd = d.Istasyon.IstasyonAd,
                                                   IstasyonId = d.Istasyon.IstasyonId,
                                                   IstasyonKod = d.Istasyon.IstasyonKod,
                                                   UretimDurusId = d.Oid,
                                                   UretimId = x.Oid
                                               }).ToList(),
                            UretimMalzemeleri = (from m in xUretimMalzemeleri
                                                 where m.Durum == KayitDurumu.Yeni &&
                                                 m.UretimId == x.Oid
                                                 orderby m.MalzemeId
                                                 select m).ToList()
                        }).ToList();
            }
            catch (Exception exc)
            {
                //UretimLog.Instance.HataLog(0, 315, GetType().Name, exc.Message, exc.StackTrace);
            }
            return resp;
        }

        [Browsable(false)]
        public List<V_UretimDuruslari> AcikDuruslar(int uretimId)
        {
            List<V_UretimDuruslari> xduruslar = new List<V_UretimDuruslari>();
            try
            {
                XPQuery<UretimDuruslari> xUretimDuruslari = new XPQuery<UretimDuruslari>(this.Session);
                xduruslar = (from d in xUretimDuruslari
                             where
                             (d.UretimOperasyon.Oid == uretimId || uretimId == 0) &&
                             d.Istasyon == this && d.Durum != KayitDurumu.Iptal
                             orderby d.BaslangicTarihi descending
                             select new V_UretimDuruslari()
                             {
                                 Aciklama = d.Aciklama,
                                 BaslangicTarihi = d.BaslangicTarihi,
                                 BitisTarihi = d.BitisTarihi,
                                 Durum = (int)d.Durum,
                                 DurusAd = d.Durus.DurusAd,
                                 DurusKod = d.DurusKod,
                                 DurusSuresi = (int)d.DurusSuresi,
                                 DurusTanimId = d.DurusNedeniId,
                                 DurusTip = d.Durus.DurusTip,
                                 IsEmriBaglanti = d.Durus.IsEmriBaglanti,
                                 IstasyonAd = d.Istasyon.IstasyonAd,
                                 IstasyonId = d.Istasyon.IstasyonId,
                                 IstasyonKod = d.Istasyon.IstasyonKod,
                                 UretimDurusId = d.Oid,
                                 UretimId = uretimId
                             }).ToList();
            }
            catch (Exception exc)
            {
                //UretimLog.Instance.HataLog(0, 350, GetType().Name, exc.Message, exc.StackTrace);
            }
            return xduruslar;
        }
        #endregion

        protected override void OnSaving()
        {
            if (this.IsDeleted == false)
            {
                if (this.MalzemeCikisDepo == null && this.MalzemeCikisDepoId > 0)
                {
                    this.MalzemeCikisDepo = this.Session.GetObjectByKey<Depolar>(this.MalzemeCikisDepoId);
                }
                if (this.UrunGirisDepo == null && this.UrunGirisDepoId > 0)
                {
                    this.UrunGirisDepo = this.Session.GetObjectByKey<Depolar>(this.UrunGirisDepoId);
                }
                if (this.YariMamulCikisDepo == null && this.YariMamulCikisDepoId > 0)
                {
                    this.YariMamulCikisDepo = this.Session.GetObjectByKey<Depolar>(this.YariMamulCikisDepoId);
                }
                if (this.YariMamulGirisDepo == null && this.YariMamulGirisDepoId > 0)
                {
                    this.YariMamulGirisDepo = this.Session.GetObjectByKey<Depolar>(this.YariMamulGirisDepoId);
                }
                if (this.IstasyonId < 1)
                {
                    object xIstasyonId = Session.Evaluate<Istasyonlar>(CriteriaOperator.Parse("Max(IstasyonId)"), null);
                    this.IstasyonId = Convert.ToInt32(xIstasyonId) + 1;

                    this.OlusturmaTarihi = DateTime.Now;

                }
                else
                {
                    this.GuncellemeTarihi = DateTime.Now;
                }
            }
            base.OnSaving();
        }

        public Istasyonlar() { }
        public Istasyonlar(Session session) : base(session) { }
    }

    /*istasyonlarin raflara tanitilmasi icin sorgu*/

    /*SELECT 
DepoId,
IstasyonId AS RafId,
IstasyonKod AS RafKod,
Istasyonlar.IsMerkeziKod AS Aciklama,
'' AS HiyerArsi,
0 AS Seviye,
0 AS Rapor,
IstasyonKod AS Barkod,
0 AS Sayim,
0 AS EksiStok,
0 AS Sevkiyat,
1 AS IstasyonMu,
Olusturan,
OlusturmaTarihi,
Guncelleyen,
GuncellemeTarihi,
KaynakModul,
CihazNo,
OptimisticLockField,
GCRecord
FROM dbo.Istasyonlar (NOLOCK) WHERE IstasyonId IN 
( SELECT RafId FROM dbo.Raflar (NOLOCK) )*/
}
