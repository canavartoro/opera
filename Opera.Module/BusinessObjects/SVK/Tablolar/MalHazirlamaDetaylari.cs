using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Xml.Serialization;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [ReferansTablo(TabloAdi = "INVT_ITEM_PICKING_D", SistemTipi=SistemTipi.WebErp)]
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("OId"),
     NavigationItem(false), ImageName("BO_Role")]
    public class MalHazirlamaDetaylari : XPBaseObject
    {
        [NonPersistent()]
        public int EmirId { get; set; }

        [Persistent("OID"), Key(AutoGenerate = false)]
        public int OId { get; set; }

        MalHazirlama fMalHazirlamaEmri;
        //protected int OId;
        [XmlIgnore(), Association(@"MalHazirlamaDetaylari.MalHazirlamaEmri_MalHazirlama")]
        public MalHazirlama MalHazirlamaEmri
        {
            get { return fMalHazirlamaEmri; }
            set { SetPropertyValue<MalHazirlama>("MalHazirlamaEmri", ref fMalHazirlamaEmri, value); }
        }

        [Size(DbSize.KodLenght)]
        public string MalzemeKod { get; set; }
        [NonPersistent]
        public string MalzemeAd { get; set; }
        public int MalzemeId { get; set; }
        public int BirimId { get; set; }
        public int Birim2Id { get; set; }
        [DbType(" DECIMAL(18,4) ")]
        public decimal Miktar { get; set; }
        [DbType(" DECIMAL(18,4) ")]
        public decimal Miktar2 { get; set; }
        [DbType(" DECIMAL(18,4) ")]
        public decimal SevkMiktar { get; set; }
        [DbType(" DECIMAL(18,4) ")]
        public decimal OkunanMiktar { get; set; }
        [NonPersistent]
        public bool IsReal { get; set; }
        [NonPersistent]
        public decimal OkunanMiktarPrm { get; set; }
        [NonPersistent]
        public decimal OkunanBirimId { get; set; }
        [NonPersistent]
        public string OkunanBirim { get; set; }
        [NonPersistent]
        public decimal PackageTraDId { get; set; }
        [NonPersistent]
        public decimal PackageTraMId { get; set; }               
  

        public int SiparisId { get; set; }
        public int SiparisDetayId { get; set; }
        public int SevkEmriId { get; set; }
        public int SevkEmriDetayId { get; set; }
        public int IsEmriId { get; set; }
        public int IsEmriDetayId { get; set; }
        public int SatirTipi { get; set; }

        [NonPersistent]
        public int RafId { get; set; }
        [XmlIgnore(), Association(@"MalHazirlamaDetaylari.Raflar_RafId")]
        public Raflar Raf { get; set; }
        [NonPersistent]
        public string RafKod { get; set; }

        [NonPersistent]
        public int KaynakDId { get; set; }
        
        public int Diger1 { get; set; }
        public int Diger2 { get; set; }
        public int Diger3 { get; set; }

        public int CariId { get; set; }

        [Size(DbSize.KodLenght)]
        public string Birim { get; set; }

        [Size(DbSize.KodLenght)]
        public string SevkEmriNo { get; set; }

        [Size(DbSize.AdLenght)]
        public string Ek1 { get; set; }
        [Size(DbSize.AdLenght)]
        public string Ek2 { get; set; }
        [Size(DbSize.AdLenght)]
        public string Ek3 { get; set; }

        [Size(DbSize.KodLenght)]
        public string CariKod { get; set; }

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
        
        public MalHazirlamaDetaylari() { }
        public MalHazirlamaDetaylari(Session session) : base(session) { }
    }
}
