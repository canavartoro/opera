using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Xml.Serialization;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    public class SatinAlmaParametreleri : XPObject
    {
        #region Ambalaj Tur "Yeni"
        //[PersistentAlias("Iif(YeniAmbalajTur is null, 0, YeniAmbalajTur.Oid)"), Description("Mobil cihazlarda kullanmak icin."), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //public int YeniAmbalajTurId { get { return Convert.ToInt32(EvaluateAlias("YeniAmbalajTurId")); } }
        //[XmlIgnore(), Association("SatinAlmaParametreleri.AmbalajTur_AmbalajTurleri"), Persistent("YeniAmbalajTurId"), NoForeignKey]
        //public AmbalajTurleri YeniAmbalajTur { get; set; }

        public int YeniAmbalajTur { get; set; }
        #endregion
        
        public int HareketTanimId { get; set; }
        public int Raf { get; set; }
        public bool YeniBelge { get; set; }
        public bool OkunanGonder { get; set; }
        public bool OnayKontrol { get; set; }
        public bool OkunanKontrol { get; set; }
        public bool YeniSeriOlustur { get; set; }
        public bool TopluYazdir { get; set; }
        [XafDisplayName("Fason Malzeme Gonderim Miktar Kontrolu")]
        public bool MalzemeCikisKontrol { get; set; }
        [XafDisplayName("Satın Alma Toplu Yazdır")]
        public bool SatinAlmaTopluYazdir { get; set; }
        [XafDisplayName("İthalat Toplu Yazdır")]
        public bool IthalatTopluYazdir { get; set; }
         
        public LogSeviye LogKaydi { get; set; }


        [PersistentAlias("Iif(KonsinyeHareketi is null, 0, KonsinyeHareketi.Oid)"), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int KonsinyeHareketId { get { return Convert.ToInt32(EvaluateAlias("KonsinyeHareketId")); } }
        [XmlIgnore(), Association("SatinAlmaParametreleri.HareketTanimlari_KonsinyeHareketi"), Persistent("KonsinyeHareketId"), NoForeignKey]
        public HareketTanimlari KonsinyeHareketi { get; set; }

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

        public SatinAlmaParametreleri() { }
        public SatinAlmaParametreleri(Session session) : base(session) { }
    }
}
