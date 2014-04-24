using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using System.Xml.Serialization;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [DeferredDeletion(false), OptimisticLocking(false), DefaultClassOptions]
    [NavigationItem(false), ImageName("BO_Role"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    public class GrupDetaylari : XPObject
    {
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
        [XmlIgnore(), Association(@"Gruplar.GrupDetaylari"), Persistent("GrupId")]
        public Gruplar Grup
        {
            get { return fGrup; }
            set { SetPropertyValue<Gruplar>("Grup", ref fGrup, value); }
        } 
        #endregion

        #region Depo&Hareket
        [VisibleInListView(false), VisibleInDetailView(false)]
        public int DepoId { get; set; }
        [VisibleInListView(false), VisibleInDetailView(false)]
        public int HareketId { get; set; } 
        #endregion

        #region Menu
        [PersistentAlias("Iif(Menu is null, 0, Menu.Oid)"), Description("Mobil cihazlarda kullanmak icin."), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int MenuId 
        { 
            get 
            {
                return Convert.ToInt32(EvaluateAlias("MenuId"));
            }
            set
            {
                if (!IsLoading && !IsSaving)
                {
                    SetPropertyValue<Menuler>("Menu", ref fMenu, Session.GetObjectByKey<Menuler>(value));
                }
            }
        }
        Menuler fMenu;
        [XmlIgnore(), Association(@"Menuler.GrupDetaylari"), Persistent("MenuId")]
        public Menuler Menu
        {
            get { return fMenu; }
            set { SetPropertyValue<Menuler>("Menu", ref fMenu, value); }
        }
        [XmlIgnore(), PersistentAlias("Iif(Menu is null, '', Menu.Aciklama)")]
        public string MenuAciklama
        {
            get
            {
                return Convert.ToString(EvaluateAlias("MenuAciklama"));
            }
        }
        #endregion
        
        public bool Giris { get; set; }
        public bool Yazma { get; set; }
        public bool Guncelleme { get; set; }
        public bool Silme { get; set; }

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

        protected override void OnSaving()
        {
            if (!this.IsDeleted)
            {
                SistemKullanicilari currentUser = SecuritySystem.CurrentUser as SistemKullanicilari;
                if (this.Oid < 1)
                {
                    if (this.Menu == null) throw new Exception("Menu zorunludur!");
                    if (this.Grup == null) throw new Exception("Grup zorunludur!");
                    this.Olusturan = currentUser != null ? currentUser.KullaniciId : 0;
                    this.OlusturmaTarihi = DateTime.Now;
                    this.KaynakModul = GetType().Name;
                    this.KaynakProgram = BusinessObjects.KaynakProgram.DbEntegrasyon;
                }
                else
                {
                    this.Guncelleyen = currentUser != null ? currentUser.KullaniciId : 0;
                    this.OlusturmaTarihi = DateTime.Now;
                }
            }
            base.OnSaving();
        }

        public GrupDetaylari() { }
        public GrupDetaylari(Session session) : base(session) { }
    }
}
