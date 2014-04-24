using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.DC;
using System.Xml.Serialization;
using Mikrobar.Islemler;


using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;


namespace Mikrobar.Module.BusinessObjects
{

    [DeferredDeletion(false), OptimisticLocking(false), DefaultClassOptions, XafDefaultProperty("GrupAd"),
    NavigationItem(false), ImageName("BO_Role"), ModelDefault("DefaultListViewShowAutoFilterRow", "True"), ModelDefault("IsClonable", "True")]
    public class TasarimGruplari : XPBaseObject
    {
        [Key(true)]
        public int GrupId { get; set; }
        public string GrupAd { get; set; }
        public bool KutuBarkod { get; set; }

        #region Kutu Bilgileri
        protected string _kutuTasarim;

        [VisibleInDetailView(false), VisibleInLookupListView(true), VisibleInListView(true)]
        public string KutuTasarim
        {
            get { return _kutuTasarim; }
            set
            {
                SetPropertyValue("KutuTasarim", ref _kutuTasarim, value);
                if (KutuTasarimi == null)
                {
                    KutuTasarimi = new SecimObj() { Ad = value };
                    OnChanged("KutuTasarimi");
                }
            }
        }

        protected SecimObj _kutuTasarimi;
        [DataSourceProperty("Tasarimlar"), NonPersistent,
        VisibleInDetailView(true), VisibleInLookupListView(false), VisibleInListView(false)]
        public SecimObj KutuTasarimi
        {
            get { return _kutuTasarimi; }
            set
            {
                SetPropertyValue("KutuTasarimi", ref _kutuTasarimi, value);
                if (value != null)
                {
                    this.KutuTasarim = value.Ad;
                    OnChanged("KutuTasarim");
                }
                else
                {
                    this.KutuTasarim = string.Empty;
                    OnChanged("");
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

        protected int _kutuKopyaSayisi = 1;
        public int KutuKopyaSayisi
        {
            get { return _kutuKopyaSayisi; }
            set
            {
                if (value < 1) value = 1;
                SetPropertyValue("KutuKopyaSayisi", ref _kutuKopyaSayisi, value);
            }
        }

        public AmbalajTurleri KutuAmbalajTur { get; set; }

        [VisibleInDetailView(false), VisibleInLookupListView(true), VisibleInListView(true)]
        public string KutuYazici
        {
            get
            {
                if (_kutuYazicisi != null)
                    return _kutuYazicisi.YaziciAd;
                else
                    return null;
            }
            set
            {
                if (value != null)
                {
                    if (_kutuYazicisi == null)
                        _kutuYazicisi = new Yazicilar();
                    _kutuYazicisi.YaziciAd = value.ToString();
                    OnChanged("KutuYazicisi");
                }
            }

        }

        protected Yazicilar _kutuYazicisi;
        [XafDisplayName("Yazıcı")]
        [DataSourceProperty("Yazicilar"), ImmediatePostData, NonPersistent,
        VisibleInDetailView(true), VisibleInLookupListView(false), VisibleInListView(false)]
        public Yazicilar KutuYazicisi
        {
            get { return _kutuYazicisi; }
            set
            {
                SetPropertyValue("KutuYazicisi", ref _kutuYazicisi, value);
                if (value != null)
                {
                    this.KutuYazici = value.YaziciAd;
                    OnChanged("KutuYazici");
                }
                else
                {
                    this.KutuYazici = string.Empty;
                    OnChanged("KutuYazici");
                }
            }
        }
        #endregion

        #region Koli Bilgileri
        protected string _koliTasarim;
        [VisibleInDetailView(false), VisibleInLookupListView(true), VisibleInListView(true)]
        public string KoliTasarim
        {
            get { return _koliTasarim; }
            set
            {
                SetPropertyValue("KoliTasarim", ref _koliTasarim, value);
                if (KoliTasarimi == null)
                {
                    KoliTasarimi = new SecimObj() { Ad = value };
                    OnChanged("KoliTasarimi");
                }
            }
        }

        protected SecimObj _koliTasarimi;
        [DataSourceProperty("Tasarimlar"), NonPersistent,
        VisibleInDetailView(true), VisibleInLookupListView(false), VisibleInListView(false)]
        public SecimObj KoliTasarimi
        {
            get { return _koliTasarimi; }
            set
            {
                SetPropertyValue("KoliTasarimi", ref _koliTasarimi, value);
                if (value != null)
                {
                    this.KoliTasarim = value.Ad;
                    OnChanged("KoliTasarim");
                }
                else
                {
                    this.KoliTasarim = string.Empty;
                    OnChanged("");
                }
            }
        }

        protected int _koliKopyaSayisi = 1;
        public int KoliKopyaSayisi
        {
            get { return _koliKopyaSayisi; }
            set
            {
                if (value < 1) value = 1;
                SetPropertyValue("KoliKopyaSayisi", ref _koliKopyaSayisi, value);
            }
        }

        public AmbalajTurleri KoliAmbalajTur { get; set; }

        [VisibleInDetailView(false), VisibleInLookupListView(true), VisibleInListView(true)]
        public string KoliYazici
        {
            get
            {
                if (_koliYazicisi != null)
                    return _koliYazicisi.YaziciAd;
                else
                    return null;
            }
            set
            {
                if (value != null)
                {
                    if (_koliYazicisi == null)
                        _koliYazicisi = new Yazicilar();
                    _koliYazicisi.YaziciAd = value.ToString();
                    OnChanged("KoliYazicisi");
                }
            }
        }

        protected Yazicilar _koliYazicisi;

        [XafDisplayName("Yazıcı 2")]
        [DataSourceProperty("Yazicilar"), ImmediatePostData, NonPersistent,
        VisibleInDetailView(true), VisibleInLookupListView(false), VisibleInListView(false)]
        public Yazicilar KoliYazicisi
        {
            get { return _koliYazicisi; }
            set
            {
                SetPropertyValue("KoliYazicisi", ref _koliYazicisi, value);
                if (value != null)
                {
                    this.KoliYazici = value.YaziciAd;
                    OnChanged("KoliYazici");
                }
                else
                {
                    this.KoliYazici = string.Empty;
                    OnChanged("KoliYazici");
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
                    if (_yazicilar == null || _yazicilar.Count<1)
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

        //private List<string> _yazicilar2 = null;
        //[Browsable(true), NonPersistent, XmlIgnore(),
        //VisibleInDetailView(true), VisibleInLookupListView(true), VisibleInListView(true)]
        //public List<string> Yazicilar2
        //{
        //    get
        //    {
        //        try
        //        {
        //            if (_yazicilar2 == null)
        //            {
        //                _yazicilar2 = new List<string>();

        //                YaziciParam param = new YaziciParam();
        //                param.Islem = YaziciIslemleri.YaziciList;
        //                YazdirmaIslemleri yazicilar = new YazdirmaIslemleri();
        //                YaziciResult res = yazicilar.Islem(param);
        //                if (res != null)
        //                {
        //                    foreach (string s in res.YaziciList)
        //                    {
        //                        _yazicilar2.Add((new Yazicilar(this.Session) { YaziciAd = s }).YaziciAd);

        //                    }
        //                }
        //            }
        //            return _yazicilar2;
        //        }
        //        catch (Exception exc)
        //        {
        //        }
        //        return null;
        //    }
        //}

        #region İlişkiler

        #region Cariler

        [PersistentAlias("Iif(Cari is null, 0, Cari.CariId)")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int CariId
        {
            get { return Convert.ToInt32(EvaluateAlias("CariId")); }
            set
            {
                if (!IsLoading && !IsSaving)
                {
                    SetPropertyValue("Cari", ref _cari, Session.GetObjectByKey<Cariler>(value));
                }
            }
        }

        protected Cariler _cari;
        [XmlIgnore(), XafDisplayName("Cari Kodu"), ImmediatePostData,
        VisibleInListView(false), VisibleInLookupListView(false)]
        [Association(@"Cariler.TasarimGruplari.Cari"), Persistent("CariId"), NoForeignKey]
        //[ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxSearchEditButtonPropertyEditor")]
        public Cariler Cari
        {
            get
            {
                return _cari;
            }
            set
            {
                SetPropertyValue("Cari", ref _cari, value);
            }
        }

        [PersistentAlias("Iif(Cari is null, '', Cari.CariAd)"), XmlIgnore()]
        public string CariAd { get { return Convert.ToString(EvaluateAlias("CariAd")); } }

        [PersistentAlias("Cariler[].Count")]
        public int CariSayisi { get { return Convert.ToInt32(EvaluateAlias("CariSayisi")); } }

        [XmlIgnore(), Association("TasarimGruplari.Cariler", typeof(Cariler)), NoForeignKey]
        public XPCollection Cariler { get { return GetCollection("Cariler"); } }
        #endregion

        #region Malzemeler
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [PersistentAlias("Iif(Malzeme is null, 0, Malzeme.MalzemeId)"), XmlIgnore()]
        public int MalzemeId 
        {
            get { return Convert.ToInt32(EvaluateAlias("MalzemeId")); } 
            set 
            {
                if (!IsLoading && !IsSaving)
                {
                    SetPropertyValue<Malzemeler>("Malzeme", ref _malzeme, Session.GetObjectByKey<Malzemeler>(value));
                }
            }
        }

        private Malzemeler _malzeme;
        [XmlIgnore(), Association(@"TasarimGruplari.Malzeme.MalzemeId"), NoForeignKey,
        Persistent("MalzemeId"), XafDisplayName("Malzeme Kodu"), ImmediatePostData]
        //[ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxSearchEditButtonPropertyEditor")]
        public Malzemeler Malzeme
        {
            get {
                //if(_malzeme != null)
                    return _malzeme;
               // return null;
            }
            set { 
                //if(value != null)
                SetPropertyValue<Malzemeler>("Malzeme", ref _malzeme, value);
               
            }
        }

        [PersistentAlias("Iif(Malzeme is null, '', Malzeme.MalzemeAd)"), XmlIgnore()]
        public string MalzemeAd { get { return Convert.ToString(EvaluateAlias("MalzemeAd")); } }

        protected string _malzemeKod2;
        [NonPersistent, XafDisplayName("Musteri Kod"), ImmediatePostData, ReadOnly(true),
        VisibleInListView(false), VisibleInLookupListView(false), ModelDefault("AllowEdit", "False")]
        public string MalzemeKod2
        {
            get
            {
                return _malzemeKod2;
            }
            //set
            //{
            //    SetPropertyValue("MalzemeKod2", ref _malzemeKod2, value);
            //}
        }

        #endregion

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

        public TasarimGruplari() { }
        public TasarimGruplari(Session session) : base(session) { }


        protected override void OnSaving()
        {
           
        }

        protected override void OnDeleting()
        {
        }
    }

    [NonPersistent, DefaultProperty("Ad")]
    public class SecimObj
    {
        public string Kod { get; set; }
        public string Ad { get; set; }
    }
}

