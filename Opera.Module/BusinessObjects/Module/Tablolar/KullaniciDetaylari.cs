using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using System.Xml.Serialization;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [DeferredDeletion(false), OptimisticLocking(false), DefaultClassOptions]
    [NavigationItem(false), ImageName("BO_Role"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")] 
    public class KullaniciDetaylari : XPObject
    {
        #region Kullanici
        [PersistentAlias("Iif(Kullanici is null, 0, Kullanici.KullaniciId)"), Description("Mobil cihazlarda kullanmak icin."), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int KullaniciId
        {
            get
            {
                return (int)EvaluateAlias("KullaniciId");
            }
        }
        Kullanicilar fKullanici;
        [XmlIgnore(), Association(@"KullaniciDetaylari.KullaniciId"), Persistent("KullaniciId"), Indexed("Grup", Unique=true)]
        public Kullanicilar Kullanici
        {
            get { return fKullanici; }
            set { SetPropertyValue<Kullanicilar>("Kullanici", ref fKullanici, value); }
        }
        #endregion

        #region Grup
        [PersistentAlias("Iif(Grup is null, 0, Grup.Oid)"), Description("Mobil cihazlarda kullanmak icin."), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int GrupId
        {
            get
            {
                return (int)EvaluateAlias("GrupId");
            }
        }
        Gruplar fGrup;
        [XmlIgnore(), Association(@"KullaniciDetaylari.GrupId"), Persistent("GrupId")]
        public Gruplar Grup
        {
            get { return fGrup; }
            set { SetPropertyValue<Gruplar>("Grup", ref fGrup, value); }
        }
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
       

        public KullaniciDetaylari() { }
        public KullaniciDetaylari(Session session) : base(session) { }

    }
}
