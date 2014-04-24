using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Xml.Serialization;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
/*//             using سْــــــــــــــــــــــمِ اﷲِارَّحْمَنِ ارَّحِيم             */
namespace Mikrobar.Module.BusinessObjects
{
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("Oid"), NavigationItem(false),
     ImageName("BO_Organization"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    public class UretimParametreleri : XPObject
    {

        #region Ambalaj Tur "Yeni"
        [PersistentAlias("Iif(UretimYeniAmbalajTur is null, 0, UretimYeniAmbalajTur.Oid)"), Description("Mobil cihazlarda kullanmak icin."), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int UretimYeniAmbalajTurId { get { return Convert.ToInt32(EvaluateAlias("UretimYeniAmbalajTurId")); } }
        //[ModelDefault("AllowEdit", "False")]
        [XmlIgnore(), Association("UretimParametreleri.AmbalajTur_AmbalajTurleri"), Persistent("UretimYeniAmbalajTurId"), NoForeignKey]
        public AmbalajTurleri UretimYeniAmbalajTur { get; set; }
        #endregion
        #region Ambalaj Tur "Hurda"
        [PersistentAlias("Iif(HurdaAmbalajTur is null, 0, HurdaAmbalajTur.Oid)"), Description("Mobil cihazlarda kullanmak icin."), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int HurdaAmbalajTurId { get { return Convert.ToInt32(EvaluateAlias("HurdaAmbalajTurId")); } }
        //[ModelDefault("AllowEdit", "False")]
        [XmlIgnore(), Association("UretimParametreleri.HurdaAmbalajTur_AmbalajTurleri"), Persistent("HurdaAmbalajTurId"), NoForeignKey]
        public AmbalajTurleri HurdaAmbalajTur { get; set; }
        #endregion
        #region Ambalaj Tur "MalzemeCikis"
        [PersistentAlias("Iif(MalzemeCikisAmbalajTur is null, 0, MalzemeCikisAmbalajTur.Oid)"), Description("Mobil cihazlarda kullanmak icin."), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int MalzemeCikisAmbalajTurId { get { return Convert.ToInt32(EvaluateAlias("MalzemeCikisAmbalajTurId")); } }
        //[ModelDefault("AllowEdit", "False")]
        [XmlIgnore(), Association("UretimParametreleri.MalzemeCikisAmbalajTur_AmbalajTurleri"), Persistent("MalzemeCikisAmbalajTurId"), NoForeignKey]
        public AmbalajTurleri MalzemeCikisAmbalajTur { get; set; }
        #endregion
        #region Ambalaj Tur "Seri"
        [PersistentAlias("Iif(UretimSeriAmbalajTur is null, 0, UretimSeriAmbalajTur.Oid)"), Description("Mobil cihazlarda kullanmak icin."), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int UretimSeriAmbalajTurId { get { return Convert.ToInt32(EvaluateAlias("UretimYeniAmbalajTurId")); } }
        //[ModelDefault("AllowEdit", "False")]
        [XmlIgnore(), Association("UretimParametreleri.UretimSeriAmbalajTur_AmbalajTurleri"), Persistent("UretimSeriAmbalajTurId"), NoForeignKey]
        public AmbalajTurleri UretimSeriAmbalajTur { get; set; }
        #endregion


        public bool TuketimKontrol { get; set; }
        public bool IscilikKaydiZorunlu { get; set; }
        public bool VardiyaZorunlu { get; set; }
        [DXDescription("GEcerli vardiyayi otomatik bulmak icin.")]
        public bool VardiyaBul { get; set; }
        public string HaftasonuVardiya { get; set; }
        [DXDescription("Manuel durus kaydi kapali")]
        public bool DurusKaydiYok { get; set; }
        [DXDescription("Uretilen miktari erp sisteminden sorgulamak icin")]
        public bool UretilenSorgulaERP { get; set; }

        [Size(DbSize.AciklamaLenght)]
        public string Aciklama { get; set; }

        [Description("Malzeme cikisi kontrol edilecek!")]
        public bool MalzemeCikisZorunlu { get; set; }

        [Description("Kasa takibi yapilip yapilmayacagi! kasa takibi yoksa ambalajla ilgili islemler olmayacak!")]
        public bool KasaTakibi { get; set; }
        [Description("Her iş emri icin ayri kasa olusturulacak!")]
        public bool KasaIsEmriTakibi { get; set; }

        [Description("Is emri bilesenleri istasyonun rafindan tuketilir!")]
        public bool TuketimRafKontrol { get; set; }

        [Description("Istasyonda operasyonun yapilip yapilmadiginin kontrolu.")]
        public bool IstasyonOperasyonKontrol { get; set; }

        [Description("Uretim baslangic/bitis toleransi!")]
        public int SureTolerans { get; set; }

        [Description("Kullanilan kasa okutulmadiginda depodan fifo ya gore kasa bulur")]
        public bool OtoKasaBul { get; set; }

        [Description("Hurda kaydinda ambalaj zorunlu mu?"), DefaultValue(false)]
        public bool HurdaKasaZorunlu { get; set; }

        [Description("Operasyon suresini kontrol eder sifir sureli operasyonu kapatmaz!"), DefaultValue(false)]
        public bool SureKontrol { get; set; }

        [Description("Operasyon miktarini kontrol eder sifir miktarli operasyonu kapatmaz!"), DefaultValue(false)]
        public bool MiktarKontrol { get; set; }

        [Description("Otomasyon varmi!"), DefaultValue(false)]
        public bool UretimVeriToplama { get; set; }

        //[Description("Uretim isciliklerinin takibi operasyonelmi istasyon bazindami olacak?")]
        //public UretimIscilikDurumu IscilikTakibi { get; set; }

        public string EtiketTasarim { get; set; }

        public string EtiketYazici { get; set; }

        [Persistent("IsEmriTipId"), XmlIgnore(), Association("UretimParametreleri.IsEmriTipleri_IsEmriTip"), NoForeignKey]
        public IsEmriTipleri IsEmriTip { get; set; }

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

        public UretimParametreleri() { }
        public UretimParametreleri(Session session) : base(session) { }
    }
}
