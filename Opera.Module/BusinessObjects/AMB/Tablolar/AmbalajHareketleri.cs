using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Xml.Serialization;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using System.Diagnostics;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Filtering;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [ListViewFilter("Tum Belgeler", "")]
    [ListViewFilter("Acik Belgeler", "[Durum] = 0")]
    [ListViewFilter("Bugunku Belgeler", "[BelgeTarihi] >= LocalDateTimeToday()")]
    [ListViewFilter("Kapali Belgeler", "[Durum] = 4")]
    [ListViewFilter("Iptal Edilen Belgeler", "[Durum] = 2")]
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions,
    DebuggerDisplay("Oid = {Oid}, IrsaliyeNo = {IrsaliyeNo}, ReferansId = {ReferansId}")]
     [ImageName("BO_Sale_Item_v92"), ModelDefault("DefaultListViewShowAutoFilterRow", "True"), ModelDefault("IsClonable", "True"),
    NavigationItem(false), XafDefaultProperty("BelgeNo")]
    public class AmbalajHareketleri : XPObject
    {
        [PersistentAlias("Oid"), VisibleInDetailView(false), ModelDefault("DisplayFormat", "d")]
        public int FisId { get { return Convert.ToInt32(EvaluateAlias("FisId")); } }

        public HareketTipi HareketTip { get; set; }

        [Size(DbSize.NoLenght)]
        public string IrsaliyeNo { get; set; }
        
        [Size(DbSize.NoLenght)]        
        public string FisNo { get; set; }
        
        [Size(DbSize.NoLenght)]        
        public string BelgeNo { get; set; }

        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}")]//, ValueConverter(typeof(DateFieldConverter))]
        public DateTime BelgeTarihi { get; set; }
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}")]//, ValueConverter(typeof(DateFieldConverter))]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public DateTime BelgeTarihi1 { get; set; }
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}")]//, ValueConverter(typeof(DateFieldConverter))]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public DateTime BelgeTarihi2 { get; set; }

        [DbType(" DECIMAL(18, 5) "), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public decimal Diger1 { get; set; }
        [DbType(" DECIMAL(18, 5) "), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public decimal Diger2 { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Ek1 { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Ek2 { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int HareketId { get; set; }

        #region Cari Kod
        protected Cariler _cari;
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int CariId { get; set; }
        [Size(DbSize.NoLenght), VisibleInDetailView(false), SearchMemberOptions(SearchMemberMode.Include)]
        public string CariKod { get; set; }
        [XmlIgnore(), NonPersistent, XafDisplayName("Cari Bilgisi"),
        VisibleInListView(false), VisibleInLookupListView(false), DataSourceCriteria("CariId = '@This.CariId'")]
        public Cariler Cari
        {
            get
            {
                if (_cari == null && this.CariId > 0)
                    this._cari = this.Session.GetObjectByKey<Cariler>(this.CariId);
                return _cari;
            }
            set
            {
                SetPropertyValue("Cari", ref _cari, value);
            }
        }
        #endregion

        [Persistent("IsEmriId"), Association("AmbalajHareketleri-IsEmirleri.IsEmri"), XmlIgnore(), NoForeignKey,
        VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public IsEmirleri IsEmri { get; set; }

        #region Sevkiyat Alanlari
        [Description("Mal hazirlama emri id"), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int MalToplamaId { get; set; }

        [Description("Ithalat dosya id"), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int SiparisId { get; set; }
        [Size(DbSize.NoLenght), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string SiparisNo { get; set; } 
        #endregion
    
        [Size(DbSize.AciklamaLenght)]
        public string Aciklama1 { get; set; }
        [Size(DbSize.AciklamaLenght)]
        public string Aciklama2 { get; set; }
        [Size(DbSize.AciklamaLenght)]
        public string Aciklama3 { get; set; }
        [Size(DbSize.AciklamaLenght)]
        public string Aciklama4 { get; set; }

        #region Ortak Alanlar
        #region Olusturan
        [ModelDefault("AllowEdit", "False"), ReadOnly(true), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
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
        [Size(DbSize.ModulLenght), ModelDefault("AllowEdit", "False"), ReadOnly(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string KaynakModul { get; set; }
        [ModelDefault("AllowEdit", "False"), ReadOnly(true), Description("Kaydın oluştuğu uygulama"), VisibleInListView(false), VisibleInLookupListView(false)]
        public KaynakProgram KaynakProgram { get; set; }
        [Size(DbSize.CihazNoLenght), ModelDefault("AllowEdit", "False"), Browsable(false), ReadOnly(true), XmlIgnore(), VisibleInListView(false), VisibleInLookupListView(false)]
        public string CihazNo { get; set; }
        [ModelDefault("AllowEdit", "False"), ReadOnly(true), XmlIgnore(), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool Entegre { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public KayitDurumu Durum { get; set; }
        #endregion

        public int ReferansId { get; set; }

        #region Kaynak Ambalaj Hareket
        private AmbalajHareketleri ambalajHareket;
        [XmlIgnore(), Association("AmbalajHareketleri-KaynakAmbHareket"), NoForeignKey,
        VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public AmbalajHareketleri KaynakAmbHareket
        {
            get { return ambalajHareket; }
            set
            {
                SetPropertyValue<AmbalajHareketleri>("KaynakAmbHareket", ref ambalajHareket, value);
            }
        }

        //[NonPersistent, XmlIgnore(), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //public AmbalajHareketleri KaynakAmbHareketId { get; set; }

        [XmlIgnore(), VisibleInListView(false), Association("AmbalajHareketleri-KaynakAmbHareket"), NoForeignKey,
        VisibleInDetailView(false), VisibleInLookupListView(false)]
        public XPCollection<AmbalajHareketleri> AltAmbHareketler
        {
            get { return GetCollection<AmbalajHareketleri>("AltAmbHareketler"); }
        } 
        #endregion

        [Association("AmbalajHareket-UretimOperasyon"), XmlIgnore(), 
        VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public UretimOperasyonlari UretimOperasyon { get; set; }
       
        [Association(@"AmbalajHareketleri.OID_StokHareketleri"), XmlIgnore()]
        public StokHareketleri StokHareket { get; set; }

        [Association(@"AmbalajHareketleri.OID_RafHareketleri"), XmlIgnore()]
        public RafHareketleri RafHareket { get; set; }

        [Association(@"AmbalajHareketDetaylari.AmbalajHareket_AmbalajHareketleri"), XmlIgnore()]
        public XPCollection<AmbalajHareketDetaylari> AmbalajHareketDetaylari
        {
            get { return GetCollection<AmbalajHareketDetaylari>(@"AmbalajHareketDetaylari"); }
        }

        [Action(Caption = "Uretime Git", AutoCommit = false, TargetObjectsCriteria = "Not IsNull(UretimOperasyon)", ImageName = "ModelEditor_GoToObject", ToolTip = "Uretim kaydina git")]
        public void UretimeGit()
        {
            try
            {
                WebApplication application = WebApplication.Instance;
                IObjectSpace objectSpace = application.CreateObjectSpace();
                if (this.UretimOperasyon != null)
                {
                    View detailView = application.CreateDetailView(objectSpace, objectSpace.GetObject(this.UretimOperasyon));
                    ShowViewParameters showViewParameters = new ShowViewParameters(detailView);
                    showViewParameters.TargetWindow = TargetWindow.Default;
                    ShowViewSource viewSource = new ShowViewSource(WebWindow.CurrentRequestWindow, null);
                    application.ShowViewStrategy.ShowView(showViewParameters, viewSource);
                }
            }
            catch (Exception) { }
        }

        public AmbalajHareketleri() {}
        public AmbalajHareketleri(Session session) : base(session) { }

        #region events

        protected override void OnSaving()
        {
        }

        protected override void OnDeleting()
        {
        }        

        #endregion        
    }
}
