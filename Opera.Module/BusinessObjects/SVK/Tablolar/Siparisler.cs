using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("SiparisId"),
     NavigationItem(false), ImageName("BO_Role"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    public class Siparisler:XPObject
    {
        [Indexed(Unique = true), ModelDefault("DisplayFormat", "d")]
        public int SiparisId { get; set; }
        public int CariId { get; set; }
        public int DepoId { get; set; }
        [Size(DbSize.NoLenght)]
        public string SiparisNo { get; set; }
        [Size(DbSize.KodLenght)]
        public string OzelKod1 { get; set; }
        [Size(DbSize.KodLenght)]
        public string OzelKod2 { get; set; }
        [Size(DbSize.AciklamaLenght)]
        public string Aciklama { get; set; }
        [Size(DbSize.AciklamaLenght)]
        public string SiparisAciklama { get; set; }
        [Size(DbSize.AdresLenght)]
        public string FaturaAdres1 { get; set; }
        [Size(DbSize.AdresLenght)]
        public string FaturaAdres2 { get; set; }
        [Size(DbSize.AdresLenght)]
        public string FaturaAdres3 { get; set; }
        public DateTime SiparisTarihi { get; set; }
        public DateTime TeslimTarihi { get; set; }
        public DateTime SevkTarihi { get; set; }
        public bool AlisSatis { get; set; }

        [XmlIgnore(), Association(@"SiparisDetaylari.Siparisler_SiparisId"), NoForeignKey]
        public XPCollection<SiparisDetaylari> SiparisDetaylari
        {
            get { return GetCollection<SiparisDetaylari>(@"SiparisDetaylari"); }
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

        public Siparisler() { }
        public Siparisler(Session session) : base(session) { }

    }
}
