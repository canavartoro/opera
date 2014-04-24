using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("Oid"),
    NavigationItem(false), ImageName("BO_Sale_Item_v92")]
    public class TransferParametreleri : XPObject
    {
        public int HareketTanimId { get; set; }
        public int TranserDepoId { get; set; }
        public bool YeniBelge { get; set; }
        public string RafOnek { get; set; }
        public bool RafHareket { get; set; }
        public bool KaliteOnayKontrolu { get; set; }
        public bool HareketKontrol { get; set; }
        public SiralamaTip SiralamaTipi { get; set; }
        public bool DepoRafKontrol { get; set; }
        public LogSeviye LogKaydi { get; set; }
        public bool HareketSec { get; set; }
        public string HareketKodu { get; set; }
        public bool DepoStokKontrolu { get; set; }
        
        #region Mal Talep Parametreleri
        public int TalepGecerlilikSuresi { get; set; }
        [PersistentAlias("Iif(TalepHareketTuru is null, 0, TalepHareketTuru.HareketTurId)"), Description("Mobil cihazlarda kullanmak icin."), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int TalepHareketTurId 
        { 
            get
            {
                return Convert.ToInt32(EvaluateAlias("TalepHareketTurId"));
            } 
            set { } 
        }

        [PersistentAlias("Iif(TalepHareketTuru is null, 0, TalepHareketTuru.HareketTuru)"), Description("Mobil cihazlarda kullanmak icin."), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string TalepHareketTurKod 
        {
            get 
            {
                return Convert.ToString(EvaluateAlias("TalepHareketTurKod"));
            } 
            set { } 
        }
        
        [XmlIgnore(), Association("TransferParametreleri.HareketTurleri_TalepHareketTuru"), Persistent("TalepHareketTurId"), NoForeignKey]
        public HareketTurleri TalepHareketTuru { get; set; }

        public SecimParametreTip TalepTeslimTesellum { get; set; }

        public SecimParametreTip TalepBakiyeKontrolu { get; set; }

        public SecimParametreTip TalepKonsinyeCikis { get; set; }

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

        public TransferParametreleri() { }
        public TransferParametreleri(Session session) : base(session) { }

    }
}
