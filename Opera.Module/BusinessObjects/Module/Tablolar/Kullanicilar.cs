
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
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Core;
using System.Web;
using System.Web.UI;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [DeferredDeletion(false), OptimisticLocking(false), DefaultClassOptions, XafDefaultProperty("KullaniciKod")]
    [NavigationItem(false), ImageName("BO_Role"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    [ReferansTablo(TabloAdi = "USERS", SistemTipi = SistemTipi.WebErp, QueryType = QueryType.Yoksa)]
    [ReferansTablo(TabloAdi = "pub.uyum_user", SistemTipi = SistemTipi.Progress, QueryType = QueryType.Yoksa, 
        SqlSorgu = "select cast(rowid as int) as \"KullaniciId\", cast(rowid as int) as \"KullaniciId2\", user_kod as \"KullaniciKod\",  user_kod as \"KullaniciKod2\", user_ad as \"KullaniciAd\", ugrup_kod as \"Aciklama\", varsayilan_firma as \"FirmaKod\", 0 AS \"Durum\"  from pub.uyum_user where 1 = 1 ",
        SqlWhere = "  and user_kod = '{0}' ")]
    public class Kullanicilar : XPBaseObject
    {
        [Key(AutoGenerate = true)]
        [ReferansAlan("US_ID", SistemTipi.WebErp)]
        public int KullaniciId { get; set; }

        [Indexed(Unique = false), ReferansAlan("rowid", SistemTipi.Progress)]
        [ReferansAlan("US_ID", SistemTipi.WebErp)]
        public int KullaniciId2 { get; set; }

        [Indexed(Unique = true), Size(DbSize.KodLenght)]
        [ReferansAlan("US_USERNAME", SistemTipi.WebErp, KeyField = true, KeyIndex = 0)]
        [ReferansAlan("user_kod", SistemTipi.Progress, KeyField = true, KeyIndex = 0)]
        [ReferansAlan("KullaniciKod", SistemTipi.Diger, KeyField = true)]
        public string KullaniciKod { get; set; }

        [ReferansAlan("US_USERNAME", SistemTipi.WebErp)]
        [Size(DbSize.KodLenght), VisibleInLookupListView(true), ReferansAlan("user_kod", SistemTipi.Progress)]
        public string KullaniciKod2 { get; set; }

        [ReferansAlan("US_NAME", SistemTipi.WebErp)]
        [Size(DbSize.AdLenght), VisibleInLookupListView(true), ReferansAlan("user_ad", SistemTipi.Progress)]
        public string KullaniciAd { get; set; }

        [Size(DbSize.AciklamaLenght), VisibleInLookupListView(true)]
        public string Aciklama { get; set; }

        [Size(DbSize.KodLenght), ReferansAlan("ugrup_kod", SistemTipi.Progress)]
        public string Departman { get; set; }

        protected string parola = "";
        [Size(DbSize.PassLenght), PasswordPropertyText(true)]
        public string Parola
        {
            get { return string.IsNullOrEmpty(parola) ? "" : parola; }
            set { SetPropertyValue("Parola", ref parola, value); }
        }

        [VisibleInLookupListView(true)]
        public bool Sayim { get; set; }

        [XmlIgnore(), Association(@"KullaniciDetaylari.KullaniciId")]
        public XPCollection<KullaniciDetaylari> KullaniciDetaylari
        {
            get { return GetCollection<KullaniciDetaylari>(@"KullaniciDetaylari"); }
        }

        [XmlIgnore(), Association(@"SistemKullanicilari.Kullanicilar_KullaniciId")]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public XPCollection<SistemKullanicilari> SistemKullanicilari
        {
            get { return GetCollection<SistemKullanicilari>(@"SistemKullanicilari"); }
        }

        [XmlIgnore(), Association(@"Talepler.Kullanici.KullaniciId")]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public XPCollection<Talepler> KullaniciTalepleri
        {
            get { return GetCollection<Talepler>(@"KullaniciTalepleri"); }
        }


        [XmlIgnore(), Association(@"TalepKabulleri.Kullanici.KullaniciId")]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public XPCollection<TalepKabulleri> KullaniciTalepKabulleri
        {
            get { return GetCollection<TalepKabulleri>(@"KullaniciTalepKabulleri"); }
        }

        #region Login Parametresi Icin Gereken Alanlar
        [Size(DbSize.KodLenght)]
        public string SessionId { get; set; }
        public int IsyeriId { get; set; }
        [Size(DbSize.KodLenght)]
        public string IsyeriKod { get; set; }
        [Size(DbSize.AciklamaLenght)]
        public string IsyeriAciklama { get; set; }
        public int FirmaId { get; set; }
        [Size(DbSize.KodLenght)]
        public string FirmaKod { get; set; }

        public DateTime SistemTarih { get; set; }

        [Size(DbSize.KodLenght)]
        public string RafOnek { get; set; }

        #region web erp

        public bool IsQtyEnabledCycleCount { get; set; }

        public bool IsQtyEnabledShipping { get; set; }

        public bool IsQtyEnabledLocationTra { get; set; }

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

        #region Convert
        //public static explicit operator MikWeb.KullaniciDetay(Kullanicilar kullanici)
        //{                
        //    if (kullanici == null)
        //        return null;
        //    MikWeb.KullaniciDetay detay = new MikWeb.KullaniciDetay();
        //    detay.Parola = kullanici.Parola;
        //    detay.CihazNo = kullanici.CihazNo;
        //    detay.KullaniciId = kullanici.KullaniciId;
        //    detay.KullaniciKod = kullanici.KullaniciKod;
        //    detay.KullaniciId2 = kullanici.KullaniciId2;
        //    detay.KullaniciKod2 = kullanici.KullaniciKod2;
        //    detay.FirmaId = kullanici.FirmaId;
        //    detay.FirmaKod = kullanici.FirmaKod;
        //    detay.IsyeriId = kullanici.IsyeriId;
        //    detay.IsyeriKod = kullanici.IsyeriKod;
        //    detay.SessionId = kullanici.SessionId;
        //    detay.SistemTarih = kullanici.SistemTarih;
        //    detay.RafOnek = kullanici.RafOnek;
        //    detay.IsQtyEnabledCycleCount = kullanici.IsQtyEnabledCycleCount;
        //    detay.IsQtyEnabledLocationTra = kullanici.IsQtyEnabledLocationTra;
        //    detay.IsQtyEnabledPackage = kullanici.IsQtyEnabledPackage;
        //    detay.IsQtyEnabledShipping = kullanici.IsQtyEnabledShipping;
        //    return detay;
        //}

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

        #region Butonlar
        [Action(Caption = "Guncelle", ImageName = "Action_Refresh", ToolTip = "Bilgileri guncelle..")]
        public void Entegrasyon()
        {
           
        }
        //[Action(Caption = "Kullanicilari Guncelle", TargetObjectsCriteria = "", ImageName = "Action_Refresh")]
        //public void Entegrasyon()
        //{
        //    //Page executingPage = null;
        //    //DevExpress.ExpressApp.Web.WebWindowTemplateHttpHandler executingTemplate = null;
        //    try
        //    {
        //        //if (HttpContext.Current != null)
        //        //    executingTemplate = HttpContext.Current.Handler as DevExpress.ExpressApp.Web.WebWindowTemplateHttpHandler;
        //        //if (executingTemplate != null)
        //        //    executingPage = executingTemplate.ActualHandler as Page;

        //        //if (executingPage != null)
        //        //{
        //        //    string script = "<script type=\"text/javascript\" language=\"javascript\"> " +
        //        //    " alert('aaa'); </script>";
        //        //    executingPage.ClientScript.RegisterClientScriptBlock(executingPage.GetType(), "test", script);
        //        //    //if (!executingPage.ClientScript.IsClientScriptBlockRegistered(executingPage.GetType(), script))                        
        //        //        //executingPage.ClientScript.RegisterClientScriptInclude(executingPage.GetType(), script, script);
        //        //}

        //        //Frame.TemplateChanged += new EventHandler(Frame_TemplateChanged);
        //        //CompositeView parentView = ((NestedFrame)Frame).DetailViewItem.View;
        //        DevExpress.Xpo.DB.SelectedData data = this.Session.ExecuteSproc("sp_KullaniciGuncelleERP");
        //    }
        //    catch (Exception exc)
        //    {

        //        throw exc;
        //    }
        //}
        #endregion

        #region Override's
        protected override void OnDeleted()
        {
            if (this.IsDeleted)
            {
                if (KullaniciDetaylari.Count > 0)
                {
                    for (int i = 0; i < KullaniciDetaylari.Count; )
                    {
                        KullaniciDetaylari[i].Delete();
                    }
                }
            }
            base.OnDeleted();
        }
        protected override void OnSaving()
        {
            try
            {
                if (this.IsDeleted)
                {
                    if (KullaniciDetaylari.Count > 0)
                    {
                        for (int i = 0; i < KullaniciDetaylari.Count; )
                        {
                            KullaniciDetaylari[i].Delete();
                        }
                    }
                }
                else
                {
                    SistemKullanicilari currentUser = SecuritySystem.CurrentUser as SistemKullanicilari;
                    if (this.KullaniciId < 1)
                    {
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
            }
            catch (Exception)
            {
            }
        }
        public override void AfterConstruction()
        {
            //XafApplication
            //Frame.GetController<CloneObjectViewController>();
            base.AfterConstruction();
        }
        #endregion

        #region NoneBrowsable

        [Browsable(false)]
        public List<V_KullaniciYetkileri> Yetkiler(CihazTip cihazTip)
        {
            List<V_KullaniciYetkileri> resp = new List<V_KullaniciYetkileri>();
            try
            {
                XPQuery<GrupDetaylari> xgrupDetay = new XPQuery<GrupDetaylari>(this.Session);
                int[] grpDets = KullaniciDetaylari.Where(x => x.Durum == KayitDurumu.Yeni).Select(x => x.GrupId).ToArray();
                var xgrupDetaylari = (from x in xgrupDetay
                                      where grpDets.Contains(x.GrupId) && x.Menu.CihazTip == cihazTip &&
                                      (x.Menu.Durum == KayitDurumu.Yeni || x.Menu.Durum == KayitDurumu.Tamamlandi)
                                      orderby x.Menu.Aciklama
                                      select new V_KullaniciYetkileri()
                                      {
                                          KullaniciKod = this.KullaniciKod,
                                          KullaniciAd = this.KullaniciAd,
                                          KullaniciSoyad = this.Aciklama,
                                          Departman = this.Departman,
                                          GrupKod = x.Grup.GrupKod,
                                          GrupAciklama = x.Grup.GrupAciklama,
                                          MenuKod = x.Menu.MenuKod,
                                          MenuAd = x.Menu.Aciklama,
                                          UstMenuId = x.Menu.UstMenuId == 0 ? -1 : x.Menu.UstMenuId,
                                          Ekran = x.Menu.Ekran,
                                          DllModul = x.Menu.DllModul,
                                          Giris = x.Giris,
                                          Yazma = x.Yazma,
                                          Guncelleme = x.Guncelleme,
                                          Silme = x.Silme,
                                          KullaniciDetaylariId = x.Oid,
                                          GrupId = x.GrupId,
                                          GrupDetaylariId = x.Oid,
                                          MenuId = x.MenuId,
                                          HareketId = x.HareketId,
                                          RafId = x.DepoId,
                                          CihazTip = x.Menu.CihazTip,
                                          Durum = true,
                                          KullaniciId = this.KullaniciId,
                                          RefId = this.KullaniciId2
                                      }).ToList();
                return xgrupDetaylari;
            }
            catch (Exception exc)
            {
                ////SistemLog.Instance.HataLog(0, 315, GetType().Name, exc.Message, exc.StackTrace);
            }
            return resp;
        }
        #endregion

        public Kullanicilar() : base(Session.DefaultSession) { }
        public Kullanicilar(Session session) : base(session) { }

    }
}
