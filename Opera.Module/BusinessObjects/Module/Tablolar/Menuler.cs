using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.Xml;
using Mikrobar;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [ModelDefault("DefaultListViewShowAutoFilterRow", "True"), ModelDefault("IsClonable", "True")]
    [DeferredDeletion(false), OptimisticLocking(false), DefaultClassOptions, XafDefaultProperty("MenuKod"), NavigationItem(false), ImageName("BO_Role")]
    public class Menuler : XPObject
    {
        protected MikrobarMenuItem fMenu;

        [XafDisplayName("Menu Tanim"), ImmediatePostData, Index(0)]
        [DataSourceProperty("MenuTanimlari"), NonPersistent]
        public MikrobarMenuItem Menu
        {
            get
            {
                if (fMenu.IsNull() && !string.IsNullOrEmpty(this.MenuKod) && this.Oid != -1)
                {
                    fMenu = this.MenuTanimlari.Where(x => x.MenuKod == this.MenuKod).FirstOrDefault();
                }
                return fMenu;
            }
            set
            {
                SetPropertyValue<MikrobarMenuItem>("Menu", ref fMenu, value);
                if (!IsLoading && !IsSaving)
                {
                    if (!fMenu.IsNull())
                    {
                        this.MenuKod = fMenu.MenuKod;
                        this.Aciklama = fMenu.Aciklama;
                        this.Ekran = fMenu.Ekran;
                        this.DllModul = fMenu.DllModul;
                        this.CihazTip = fMenu.CihazTip;
                    }
                }
            }
        }

        [ModelDefault("AllowEdit", "False"), Index(2), Size(DbSize.KodLenght), VisibleInLookupListView(true), NonCloneable()]
        public string MenuKod { get; set; }

        [Size(DbSize.AciklamaLenght), Index(3), VisibleInLookupListView(true)]
        [Appearance("UstMenu_Aciklama", Enabled = false, Criteria = "Iif(Menu is null, 0, 1) == 1", AppearanceItemType = "ViewItem", Context = "DetailView", TargetItems = "Aciklama")]
        public string Aciklama { get; set; }

        #region Ust Menu
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int UstMenuId { get; set; }

        private Menuler _ustMenu;
        [XmlIgnore(), NonPersistent, XafDisplayName("Ust Menu"), Index(1),
        VisibleInListView(false), VisibleInLookupListView(true),
        DataSourceCriteria(" Oid !=  '@This.Oid' ")]
        public Menuler UstMenu
        {
            get
            {
                if (_ustMenu == null && this.UstMenuId > 0)
                    this._ustMenu = this.Session.GetObjectByKey<Menuler>(this.UstMenuId);
                return _ustMenu;
            }
            set
            {
                SetPropertyValue("UstMenu", ref _ustMenu, value);
                if (value == null) this.UstMenuId = -1;
                OnChanged("UstMenuId");
            }
        }
        #endregion

        [Size(DbSize.ModulLenght), ModelDefault("AllowEdit", "False"), VisibleInDetailView(false), VisibleInLookupListView(true)]
        public string Ekran { get; set; }

        [Size(DbSize.ModulLenght), ModelDefault("AllowEdit", "False"), VisibleInDetailView(false), VisibleInLookupListView(true)]
        public string DllModul { get; set; }

        [XafDisplayName("Cihaz Tipi"), VisibleInLookupListView(true)]
        [Appearance("UstMenu_CihazTip", Enabled = false, Criteria = "Iif(Menu is null, 0, 1) == 1", AppearanceItemType = "ViewItem", Context = "DetailView", TargetItems = "CihazTip")]
        public CihazTip CihazTip { get; set; }


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

        [XmlIgnore(), Association(@"Menuler.GrupDetaylari")]
        public XPCollection<GrupDetaylari> GrupDetaylari
        {
            get { return GetCollection<GrupDetaylari>(@"GrupDetaylari"); }
        }

        protected List<MikrobarMenuItem> menuTanimlari;
        [Browsable(false), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public List<MikrobarMenuItem> MenuTanimlari
        {
            get
            {
                if (menuTanimlari == null)
                {
                    ////menuTanimlari = new List<MikrobarMenuItem>();
                    ////XmlDocument doc = new XmlDocument();
                    ////doc.Load(Utility.AppsPath + "menu.xml");
                    ////XmlNodeList moduleList = doc.SelectNodes("//module");
                    ////foreach (XmlNode node in moduleList)
                    ////{
                    ////    XmlElement moduleElement = node as XmlElement;
                    ////    if (moduleElement != null)
                    ////    {
                    ////        if (moduleElement.HasAttributes)
                    ////        {
                    ////            System.Diagnostics.Trace.WriteLine(moduleElement.Attributes["name"].InnerText);
                    ////            XmlNodeList _nodes = moduleElement.ChildNodes;
                    ////            {
                    ////                foreach (XmlNode n in _nodes)
                    ////                {
                    ////                    menuTanimlari.Add(new MikrobarMenuItem(n["code"].InnerText, n["text"].InnerText, n["runnable"].InnerText, n["assembly"].InnerText, (CihazTip)Enum.Parse(typeof(CihazTip), moduleElement.Attributes["devicetype"].InnerText), Session));
                    ////                }
                    ////            }
                    ////        }
                    ////    }
                    ////}
                    ////menuTanimlari.TrimExcess();
                }
                return menuTanimlari;
            }
        }

        protected override void OnDeleting()
        {
            base.OnDeleting();
            if (this.GrupDetaylari.Count > 0)
            {
                for (int i = this.GrupDetaylari.Count; i > 0; i--)
                {
                    this.GrupDetaylari[i - 1].Delete();
                }
            }
        }

        protected override void OnSaving()
        {
            if (!this.IsDeleted)
            {
                SistemKullanicilari currentUser = SecuritySystem.CurrentUser as SistemKullanicilari;

                if (this._ustMenu != null)
                    this.UstMenuId = this._ustMenu.Oid;
                else
                    this.UstMenuId = -1;

                if (string.IsNullOrEmpty(this.MenuKod) && !string.IsNullOrEmpty(this.Ekran))
                    throw new MikrobarException("Menu taniminda Menu Kodu zorunludur!", 214, "Menuler");
                if (string.IsNullOrEmpty(this.MenuKod))
                {
                    object xMenuKod = Session.Evaluate<Menuler>
                        (CriteriaOperator.Parse("Count()"), CriteriaOperator.Parse(" CihazTip = ? and UstMenuId = -1 ", this.CihazTip));
                    int mnuKod = Convert.ToInt32(xMenuKod) + 1;
                    if (CihazTip == BusinessObjects.CihazTip.Bilgisayar)
                        this.MenuKod = string.Format("PC{0:000}", mnuKod);
                    else if (CihazTip == BusinessObjects.CihazTip.CepTelefonu)
                        this.MenuKod = string.Format("MOB{0:000}", mnuKod);
                    else if (CihazTip == BusinessObjects.CihazTip.Elterminali)
                        this.MenuKod = string.Format("ELT{0:000}", mnuKod);
                    else if (CihazTip == BusinessObjects.CihazTip.Tablet)
                        this.MenuKod = string.Format("TAB{0:000}", mnuKod);
                }


                if (this.Oid > 0)
                {
                    if (this.Guncelleyen < 1)
                        this.Guncelleyen = currentUser != null ? currentUser.KullaniciId : 0;

                    this.GuncellemeTarihi = DateTime.Now;
                }
                else
                {
                    if (Olusturan < 1)
                        this.Olusturan = currentUser != null ? currentUser.KullaniciId : 0;

                    this.OlusturmaTarihi = DateTime.Now;
                }

            }
            base.OnSaving();
        }

        public Menuler() : base(Session.DefaultSession) { }
        public Menuler(Session session) : base(session) { }
    }

    [NonPersistent, XafDefaultProperty("FullName"), NavigationItem(false)]
    public class MikrobarMenuItem : XPLiteObject
    {
        public MikrobarMenuItem() : base(Session.DefaultSession) { }
        public MikrobarMenuItem(Session session) : base(session) { }

        public MikrobarMenuItem(string mnKod, string mnAciklama, string mnEkran, string mnModul, CihazTip mnCihaz, Session session)
            : base(session)
        {
            this.MenuKod = mnKod;
            this.Aciklama = mnAciklama;
            this.Ekran = mnEkran;
            this.DllModul = mnModul;
            this.CihazTip = mnCihaz;
        }

        [VisibleInListView(true), VisibleInLookupListView(true)]
        public string MenuKod { get; set; }

        [VisibleInListView(true), VisibleInLookupListView(true)]
        public string Aciklama { get; set; }

        [VisibleInListView(true), VisibleInLookupListView(true)]
        public string Ekran { get; set; }

        [VisibleInListView(true), VisibleInLookupListView(true)]
        public string DllModul { get; set; }

        [VisibleInListView(true), VisibleInLookupListView(true)]
        public CihazTip CihazTip { get; set; }

        public string FullName { get { return string.Format("[{0}] {1} ({2})", MenuKod, Aciklama, CihazTip); } }
    }
}
