using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using System.Xml.Serialization;

using Mikrobar;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [DeferredDeletion(false), OptimisticLocking(false), DefaultClassOptions, XafDefaultProperty("ErpTur"),
    NavigationItem(false), ImageName("BO_Role")]
    public class SistemParametreleri : XPObject
    {
        public SistemTipi ErpTur { get; set; }
        [Description("Tanimlarin direk Erp sisteminden sorgulanmasi, stok kart cari kart vb."), DefaultValue(false)]
        public bool Isonline { get; set; }
        [Description("Olusturulan kayitlarin direk sisteme aktarilmasi, stok hareket uretim irsaliye vb."), DefaultValue(false)]
        public bool Entegrasyon { get; set; }
        [Description("ERP Sisteminden Online Çekilen Kayıtların Mikrobar Sistemine Aktarılması."), DefaultValue(false)]
        public bool VeriEntegrasyon { get; set; }
        [Description("Sisteme login olunduktan sonra Erp sistemindende login kontrolu."), DefaultValue(false)]
        public bool GirisKontrol { get; set; }
        [Description("Sisteme login olmadan Erp sistemindende login kontrolu."), DefaultValue(false)]
        public bool SunucuGirisKontrol { get; set; }
        [Description("Kayıtlarda Raf kontrolu yapılcakmı."), DefaultValue(true)]
        public bool RafKontrol { get; set; }
        [Size(DbSize.KodLenght)]
        public string EntegrasyonKullaniciKod { get; set; }
        [Size(DbSize.PassLenght)]
        public string EntegrasyonKullaniciParola { get; set; }
        public int FirmaId { get; set; }
        public int IsyeriId { get; set; }

        public bool PartiTakibi { get; set; }

        [Size(DbSize.KodLenght)]
        public string FirmaKod { get; set; }
        [Size(DbSize.KodLenght)]
        public string IsYeriKod { get; set; }
        [Size(DbSize.AciklamaLenght)]
        public string Aciklama { get; set; }
        [Size(DbSize.KodLenght)]
        public string DbVersiyonu { get; set; }
        [Size(DbSize.KodLenght)]
        public string PcVersiyonu { get; set; }
        [Size(DbSize.KodLenght)]
        public string MobilVersiyon { get; set; }
        [Size(DbSize.KodLenght)]
        public string ApkVersiyon { get; set; }
        [Size(DbSize.KodLenght)]
        public string WebServis { get; set; }
        public LogSeviye LogKaydi { get; set; }

        

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

        public SistemParametreleri() { }
        public SistemParametreleri(Session session) : base(session) { }

    }
}
