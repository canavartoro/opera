using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Reflection;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using System.Diagnostics;
using DevExpress.Data.Filtering;
using Mikrobar;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp;
using System.Web;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [DebuggerDisplay("Prefix = {Prefix}, Oid = {Oid}, AmbalajTip = {AmbalajTip}")]
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions]
     [ImageName("BO_Sale_Item_v92"), ModelDefault("DefaultListViewShowAutoFilterRow", "True"),ModelDefault("IsClonable", "True"),
    NavigationItem(false), XafDefaultProperty("AmbalajTip")]
    public class AmbalajTurleri : XPObject
    {
        [Indexed(Unique = false), Size(DbSize.AciklamaLenght)]
        public string Prefix { get; set; }

        [Size(DbSize.KodLenght), Indexed]
        public string AmbalajTip { get; set; }

        public int Sayac { get; set; }

        public MalzemeTip MalzemeTip { get; set; }        

        public bool Sevkiyat { get; set; }

        public bool Dara { get; set; }

        public bool Bolunebilirlik { get; set; }

        [Description("Seri no sayac kontrolu icin.")]
        public bool SeriNoKontrol { get; set; }

        [Description("Kasa istasyonlar arasinda otomatik transfer yapilmasi icin")]
        public bool OtomatikTransfer { get; set; }

        [Description("Kasanin tamami tek seferde uretimden cikar")]
        public bool ParcaliUretim { get; set; }

        [Description("Bi operasyondan bi defa gecebilir")]
        public bool OperasyonKontrol { get; set; }

        [Description("Ambalaj miktari ekisye dusebilir")]
        public bool EksiStok { get; set; }

        public int SeriUzunluk { get; set; }
       
        #region AltAmbalajlar

        [XmlIgnore(), Association("AmbalajTurleri.AmbalajTurleri.AmbalajTur"), VisibleInDetailView(false), VisibleInLookupListView(false), VisibleInListView(false)]
        public XPCollection<AmbalajTurleri> AmbalajTur
        {
            get { return GetCollection<AmbalajTurleri>("AmbalajTur"); }
        }

        [XmlIgnore(), VisibleInListView(false), DataSourceCriteria("AmbalajTip<>'@This.AmbalajTip'"), Association("AmbalajTurleri.AmbalajTurleri.AmbalajTur"), VisibleInLookupListView(false)]
        public XPCollection<AmbalajTurleri> AltAmbalajlar
        {
            get { return GetCollection<AmbalajTurleri>("AltAmbalajlar"); }
        }

        #endregion
        [Size(DbSize.Mesaj)]
        public string SeriFormat { get; set; }

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

        [XmlIgnore(),Association("Ambalajlar.AmbalajTur_AmbalajTurleri"), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public XPCollection<Ambalajlar> Ambalajlar
        {
            get { return GetCollection<Ambalajlar>("Ambalajlar"); }
        }

        [XmlIgnore(), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false), Association("SevkiyatParametreleri.AmbalajTur_SevkiyatParametreleri")]
        public XPCollection<SevkiyatParametreleri> SevkiyatParametreleri
        {
            get { return GetCollection<SevkiyatParametreleri>("SevkiyatParametreleri"); }
        }

        [XmlIgnore(), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false), Association("UretimParametreleri.AmbalajTur_AmbalajTurleri"), NoForeignKey]
        public XPCollection<UretimParametreleri> UretimParametreleri
        {
            get { return GetCollection<UretimParametreleri>("UretimParametreleri"); }
        }

        [XmlIgnore(), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false), Association("UretimParametreleri.HurdaAmbalajTur_AmbalajTurleri"), NoForeignKey]
        public XPCollection<UretimParametreleri> UretimParametreleri2
        {
            get { return GetCollection<UretimParametreleri>("UretimParametreleri2"); }
        }

        [XmlIgnore(), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false), Association("UretimParametreleri.MalzemeCikisAmbalajTur_AmbalajTurleri"), NoForeignKey]
        public XPCollection<UretimParametreleri> UretimParametreleri3
        {
            get { return GetCollection<UretimParametreleri>("UretimParametreleri3"); }
        }

        [XmlIgnore(), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false), Association("UretimParametreleri.UretimSeriAmbalajTur_AmbalajTurleri"), NoForeignKey]
        public XPCollection<UretimParametreleri> UretimParametreleri4
        {
            get { return GetCollection<UretimParametreleri>("UretimParametreleri4"); }
        }

        [XmlIgnore(), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false), Association("Istasyonlar.AmbalajTurleri.AmbalajTur"), NoForeignKey]
        public XPCollection<Istasyonlar> Istasyonlar
        {
            get { return GetCollection<Istasyonlar>("Istasyonlar"); }
        }

        /// <summary>
        /// Etiketleme ekranı için ürün barkodu yazdırmada kullanılmaktadır.
        /// </summary>
        /// <returns></returns>
        public string UrunSeriNoOlustur()
        {
            StringBuilder sb = new StringBuilder();

            using (UnitOfWork wrk = new UnitOfWork())
            {
                AmbalajTurleri xTuru = wrk.GetObjectByKey<AmbalajTurleri>(this.Oid, true);
                xTuru.Sayac++;
                xTuru.Save();

                xTuru.SeriUzunluk = 10;

                int xUzunluk = xTuru.SeriUzunluk;

                if (!string.IsNullOrEmpty(xTuru.Prefix) && xTuru.Prefix.Length > 0)
                    sb.Append(xTuru.Prefix);
                sb.Append(xTuru.Sayac.ToString().PadLeft(xUzunluk, '0'));
                wrk.CommitChanges();
            }
            return sb.ToString();
        }

        public AmbalajTurleri() { }
        public AmbalajTurleri(Session session) : base(session) { }        

    }
}
