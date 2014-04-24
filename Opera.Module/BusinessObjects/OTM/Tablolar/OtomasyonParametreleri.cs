using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using System.Xml.Serialization;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [DeferredDeletion(false), OptimisticLocking(false), DefaultClassOptions,
    NavigationItem(false), ImageName("BO_Role")]
    public class OtomasyonParametreleri : XPObject
    {
        public bool DatabaseKayit { get; set; }
        public bool UretimKaydi { get; set; }
        [Description("Dakika türünden üretim kaydı süresini belirtmektedir.")]
        public int UretimKayitSuresi { get; set; }
        public LogSeviye LogKaydi { get; set; }
        [Description("Dakika türünden log kaydı süresini belirtmektedir.")]
        public int LogKayitSuresi { get; set; }

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

        #region Event

        protected override void OnSaving()
        {
            #region IsNotDelete

            if (this.IsDeleted == false)
            {
                SistemKullanicilari currentUser = SecuritySystem.CurrentUser as SistemKullanicilari;
                
                if (this.Oid < 1)
                {
                    if (this.Olusturan < 1)
                        this.Olusturan = currentUser != null ? currentUser.KullaniciId : 0;

                    this.OlusturmaTarihi = DateTime.Now;
                    this.KaynakProgram = BusinessObjects.KaynakProgram.Sayim;
                    this.KaynakModul = GetType().Name;
                }
                else
                {
                    if (this.Guncelleyen < 1)
                        this.Guncelleyen = currentUser != null ? currentUser.KullaniciId : 0;

                    this.GuncellemeTarihi = DateTime.Now;
                }
            }

            #endregion
        }

        #endregion

        public OtomasyonParametreleri() { }
        public OtomasyonParametreleri(Session session) : base(session) { }
    }
}
