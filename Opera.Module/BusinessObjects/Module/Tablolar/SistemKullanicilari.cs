using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Xml.Serialization;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;

using Mikrobar.Islemler;
using Mikrobar.Module.BusinessObjects;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp;


using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Web;
using DevExpress.Persistent.Base.Security;
using System.Security;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [DeferredDeletion(false), OptimisticLocking(false), DefaultClassOptions, XafDefaultProperty("UserName")]
    [NavigationItem(false), ImageName("BO_Role"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    public class SistemKullanicilari : DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser
    {
        [Description("Mobil cihazlarda kullanmak icin."), PersistentAlias("Iif(Kullanici is null, 0, Kullanici.KullaniciId)"),
        VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int KullaniciId 
        {
            get 
            {
                try
                {
                    return Convert.ToInt32(EvaluateAlias("KullaniciId"));
                }
                catch (ObjectDisposedException disp)
                {
                }
                return 0;                  
            } 
        }

        Kullanicilar fKullanici;
        [Indexed(Unique = false), XmlIgnore()]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [Association(@"SistemKullanicilari.Kullanicilar_KullaniciId"), Persistent("KullaniciId")]        
        public Kullanicilar Kullanici
        {
            get { return fKullanici; }
            set { SetPropertyValue<Kullanicilar>("Kullanici", ref fKullanici, value); }
        }

        [Indexed(Unique = false), XafDisplayName("Erp Kullanici No")]
        public int KullaniciId2 { get; set; }
        [Size(DbSize.KodLenght), XafDisplayName("Erp Kullanici Kod")]
        public string KullaniciKod2 { get; set; }

        [Size(DbSize.AdLenght), PersistentAlias("base.UserName")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public string KullaniciAd { get { return base.UserName; } }

        [Size(DbSize.AciklamaLenght)]
        public string Aciklama { get; set; }

        [Size(DbSize.KodLenght)]
        public string Departman { get; set; }

        string password;
        [XmlIgnore(), PasswordPropertyText(true), NonPersistent]
        public string Parola
        {
            get 
            {
                if (string.IsNullOrEmpty(password))
                    password = base.StoredPassword;
                return base.StoredPassword; 
            }
            set { password = value; SetPassword(value); OnChanged("StoredPassword"); }
        }

        #region Login Parametresi Icin Gereken Alanlar
        [Size(DbSize.KodLenght)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string SessionId { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int IsyeriId { get; set; }

        [Size(DbSize.KodLenght)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string IsyeriKod { get; set; }

        [Size(DbSize.AciklamaLenght)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string IsyeriAciklama { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int FirmaId { get; set; }

        [Size(DbSize.KodLenght)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string FirmaKod { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public DateTime SistemTarih { get; set; }

        [Size(DbSize.KodLenght)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string RafOnek { get; set; }

        #region web erp
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool IsQtyEnabledCycleCount { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool IsQtyEnabledShipping { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool IsQtyEnabledLocationTra { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool IsQtyEnabledPackage { get; set; }

        #endregion
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

        public bool Sayim { get; set; }

        protected SecimObj _acilisSayfa;
        [NonPersistent,VisibleInDetailView(true), VisibleInLookupListView(false), VisibleInListView(false)]
        [Size(DbSize.KodLenght), DataSourceProperty("Sayfalar"), XafDisplayName("Ilk Acilis Sayfasi"), XmlIgnore()]
        public SecimObj AcilisSayfa 
        {
            get 
            {
                if (!IsSaving && !IsLoading)
                {
                    if (this._acilisSayfa == null && !string.IsNullOrEmpty(this._acilisSayfasi))
                    {
                        this._acilisSayfa = this.Sayfalar.Where(x => x.Kod == this._acilisSayfasi).FirstOrDefault();
                    }
                }
                return _acilisSayfa;
            }
            set
            {
                SetPropertyValue("AcilisSayfasi", ref _acilisSayfa, value);
                if (value != null)
                {
                    this.AcilisSayfasi = value.Kod;
                    OnChanged("AcilisSayfasi");
                }
                else
                {
                    this.AcilisSayfasi = string.Empty;
                    OnChanged("AcilisSayfasi");
                }
            }
        }

        protected string _acilisSayfasi;
        [XmlIgnore(),VisibleInDetailView(false), VisibleInLookupListView(true), VisibleInListView(true)]
        public string AcilisSayfasi 
        {
            get { return _acilisSayfasi; }
            set
            {
                SetPropertyValue<string>("AcilisSayfasi", ref _acilisSayfasi, value);                                
            }
        }

        private List<SecimObj> sayfalar = null;
        [Browsable(false), NonPersistent, XmlIgnore(),
        VisibleInDetailView(true), VisibleInLookupListView(true), VisibleInListView(true)]
        public List<SecimObj> Sayfalar
        {
            get
            {
                try
                {
                    if (sayfalar == null)
                    {
                        sayfalar = new List<SecimObj>();
                        IModelApplicationNavigationItems navigationItems = WebApplication.Instance.Model as IModelApplicationNavigationItems;
                        if (navigationItems != null)
                        {
                            foreach (IModelNavigationItem item in navigationItems.NavigationItems.AllItems)
                            {
                                if (item.View != null)
                                {
                                    sayfalar.Add(new SecimObj() { Kod = item.View.Id, Ad = item.View.Caption });
                                }
                            }
                        }
                    }
                    return sayfalar;
                }
                catch //(Exception exc)
                {
                }
                return null;
            }
        }

        #region Convert


        public static explicit operator SistemKullanici(SistemKullanicilari kullanici)
        {
            if (kullanici == null)
                return null;
            SistemKullanici kl = new SistemKullanici(kullanici.Session);
            kl.Oid = kullanici.Oid;
            kl.UserName = kullanici.KullaniciKod2;
            kl.KullaniciKod = kullanici.KullaniciKod2;
            kl.Aciklama = kullanici.Aciklama;
            return kl;
        }

        //public static implicit operator SistemKullanicilari(Kullanicilar kullanici)
        //{
        //    if (kullanici == null)
        //        return null;
        //    SistemKullanicilari kull = new SistemKullanicilari(kullanici.Session);
        //    kull.Parola = kullanici.Parola;
        //    kull.CihazNo = kullanici.CihazNo;
        //    kull.KullaniciId = kullanici.KullaniciId;
        //    kull.UserName = kullanici.KullaniciKod;
        //    kull.KullaniciId2 = kullanici.KullaniciId2;
        //    kull.KullaniciKod2 = kullanici.KullaniciKod2;
        //    kull.FirmaId = kullanici.FirmaId;
        //    kull.FirmaKod = kullanici.FirmaKod;
        //    kull.IsyeriId = kullanici.IsyeriId;
        //    kull.IsyeriKod = kullanici.IsyeriKod;
        //    kull.SessionId = kullanici.SessionId;
        //    kull.SistemTarih = kullanici.SistemTarih;
        //    kull.RafOnek = kullanici.RafOnek;
        //    kull.IsQtyEnabledCycleCount = kullanici.IsQtyEnabledCycleCount;
        //    kull.IsQtyEnabledLocationTra = kullanici.IsQtyEnabledLocationTra;
        //    kull.IsQtyEnabledPackage = kullanici.IsQtyEnabledPackage;
        //    kull.IsQtyEnabledShipping = kullanici.IsQtyEnabledShipping;
        //    return kull;
        //}
        #endregion

        //public KullaniciDetay SunucuLogin()
        //{
        //    string strPassword = this.Parola;
        //    if (string.IsNullOrEmpty(strPassword)) strPassword = "";
        //    LoginIslemleri lg = new LoginIslemleri();
        //    LoginResult snc = lg.Islem(new LoginResult()
        //    {
        //        CihazNo = Environment.MachineName,
        //        CihazTipi = ClientType.Tarayici,
        //        KullaniciKodu = UserName,
        //        Parola = strPassword,
        //        Versiyon = Ortak.Versiyon
        //    });

        //    if (!snc.Results)
        //    {
        //        throw new Exception(snc.Mesage);
        //    }

        //    return snc.Detaylar;
        //}

        #region Securty
        //public void SetPassword(string password)
        //{
        //    base.StoredPassword = new PasswordCryptographer().GenerateSaltedPassword(password);
        //    OnChanged("StoredPassword");
        //}

        //public bool ComparePassword(string password)
        //{
        //    return SecurityUserBase.ComparePassword(base.StoredPassword, password);
        //} 
        #endregion

        protected override void OnSaving()
        {
            if (this.IsDeleted)
            {                
                SecurityUserWithRolesBase usrRol = this.Session.GetObjectByKey<SecurityUserWithRolesBase>(base.Oid);
                if (!object.ReferenceEquals(null, usrRol)) usrRol.Delete();
                SecurityUserBase usrBase = this.Session.GetObjectByKey<SecurityUserBase>(base.Oid);
                if (!object.ReferenceEquals(null, usrBase)) usrBase.Delete();
                SecurityUser usr = this.Session.GetObjectByKey<SecurityUser>(base.Oid);
                if (!object.ReferenceEquals(null, usr)) usr.Delete();
                if (this.Kullanici != null) this.Kullanici.Delete();
            }
            else
            {
                SistemKullanicilari currentUser = SecuritySystem.CurrentUser as SistemKullanicilari;
                if (this.Oid == new Guid("00000000-0000-0000-0000-000000000000"))
                {                    
                    //object xKullaniciId = Session.Evaluate<Kullanicilar>(CriteriaOperator.Parse("Max(KullaniciId)"), null);
                    //this.KullaniciId = Convert.ToInt32(xKullaniciId) + 1;
                    this.Olusturan = currentUser != null ? currentUser.KullaniciId : 0;
                    this.OlusturmaTarihi = DateTime.Now;
                    this.KaynakModul = GetType().Name;
                    this.KaynakProgram = KaynakProgram.DbEntegrasyon;
                    if (this.Kullanici == null)
                    {
                        Kullanicilar xkul = Session.FindObject<Kullanicilar>(CriteriaOperator.Parse(" KullaniciKod = ? ", UserName));
                        if (object.ReferenceEquals(xkul, null))
                            xkul = XpoHelper.CloneBaseObject(this, typeof(Kullanicilar), Session) as Kullanicilar;
                        if (xkul != null)
                        {
                            xkul.Parola = password;
                            xkul.KullaniciKod = UserName;
                            xkul.Save();
                            this.Kullanici = xkul;
                        }
                    }
                }
                else
                {                    
                    this.Guncelleyen = currentUser != null ? currentUser.KullaniciId : 0;
                    this.OlusturmaTarihi = DateTime.Now;
                }
            }
            base.OnSaving();
        }

        public SistemKullanicilari() : base(Session.DefaultSession) { }
        public SistemKullanicilari(Session session) : base(session) { }
    }
}
