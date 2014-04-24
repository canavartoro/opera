using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Reflection;
using System.Xml.Serialization;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Filtering;
using DevExpress.ExpressApp;
using Mikrobar;
using DevExpress.ExpressApp.Web.SystemModule;
using DevExpress.ExpressApp.Web;
using System.Web;
using System.IO;
using System.Data;
using System.Web.UI;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [ListViewFilter("Tum Belgeler", "")]
    [ListViewFilter("Acik Belgeler", "[Durum] = 0")]
    [ListViewFilter("Bugunku Belgeler", "[BelgeTarihi] >= LocalDateTimeToday()")]
    [ListViewFilter("Kapali Belgeler", "[Durum] = 4")]
    [ListViewFilter("Iptal Edilen Belgeler", "[Durum] = 2")]
    [DebuggerDisplay("FisNo = {FisNo}, Oid = {Oid}, Durum = {Durum}")]
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("IrsaliyeNo"), NavigationItem(false),
     ImageName("BO_Sale_Item_v92"), ModelDefault("DefaultListViewShowAutoFilterRow", "True"), ModelDefault("IsClonable", "True")]
    public class StokHareketleri : XPObject
    {
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int HareketId { get; set; }

        [PersistentAlias("Oid"), VisibleInDetailView(false), ModelDefault("DisplayFormat", "d")]
        public int FisId { get { return Convert.ToInt32(EvaluateAlias("FisId")); } }

        public HareketTipi HareketTip { get; set; }

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

        [PersistentAlias("Iif(Cari is null, '', Cari.CariAd)")]
        public string CariAd 
        { 
            get 
            {
                try
                {
                    return Convert.ToString(EvaluateAlias("CariAd"));
                }
                catch (ObjectDisposedException disp)
                {
                }
                return ""; 
            } 
        }
        #endregion

        #region Belge Bilgisi
        [Size(DbSize.NoLenght), Indexed, SearchMemberOptions(SearchMemberMode.Include), Index(2)]
        public string IrsaliyeNo { get; set; }
        [Size(DbSize.NoLenght), SearchMemberOptions(SearchMemberMode.Include)]
        public string FisNo { get; set; }
        [Size(DbSize.NoLenght), Indexed, SearchMemberOptions(SearchMemberMode.Include)]
        public string BelgeNo { get; set; }

        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}")]
        public DateTime BelgeTarihi { get; set; }
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}")]
        public DateTime IrsaliyeTarihi { get; set; }
        #endregion

        [Description("Mal hazirlama emri id"), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int MalToplamaId { get; set; }

        [Description("Ithalat dosya id"), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int SiparisId { get; set; }
        [Size(DbSize.NoLenght), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string SiparisNo { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int SevkEmriId { get; set; }
        [Size(DbSize.NoLenght), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string SevkEmriNo { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int IsEmriId { get; set; }
        [Size(DbSize.NoLenght), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string IsEmriNo { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int UretimId { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int MalTalepId { get; set; }
        [Size(DbSize.KodLenght), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string MalTalepNo { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public TalepDurumlari TalepDurumu { get; set; }

        #region Aciklamalar
        [Size(DbSize.AciklamaLenght)]
        public string Aciklama1 { get; set; }
        [Size(DbSize.AciklamaLenght)]
        public string Aciklama2 { get; set; }
        [Size(DbSize.AciklamaLenght)]
        public string Aciklama3 { get; set; }
        [Size(DbSize.AciklamaLenght)]
        public string Aciklama4 { get; set; }
        #endregion

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

        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int ReferansId { get; set; }
        [NonPersistent, Description("Web servise giden obje Oid tasimasi icin"), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Oid2 { get; set; }

        [Association(@"StokHareketDetaylari.StokHareket_StokHareketleri")]
        public XPCollection<StokHareketDetaylari> StokHareketDetaylari
        {
            get { return GetCollection<StokHareketDetaylari>(@"StokHareketDetaylari"); }

        }

        [Association(@"AmbalajHareketleri.OID_StokHareketleri"), VisibleInListView(false)]
        public XPCollection<AmbalajHareketleri> AmbalajHareketleri
        {
            get { return GetCollection<AmbalajHareketleri>(@"AmbalajHareketleri"); }
        }

        [Association(@"TalepKabulleri.StokHareketleri.StokHareket"), NoForeignKey,
        VisibleInListView(false), VisibleInDetailView(false)]
        public XPCollection<TalepKabulleri> TalepKabulleri
        {
            get { return GetCollection<TalepKabulleri>(@"TalepKabulleri"); }
        }

        [Association(@"TalepKabulleri.StokHareketleri.KabulStokHareket"), NoForeignKey,
        VisibleInListView(false), VisibleInDetailView(false)]
        public XPCollection<TalepKabulleri> TalepKabulleri2
        {
            get { return GetCollection<TalepKabulleri>(@"TalepKabulleri2"); }
        }

        #region Butonlar
        [Action(Caption = "Belge Iptal", TargetObjectsCriteria = "[Durum] = 0", ImageName = "Action_CloseAllWindows", ConfirmationMessage = "Belge İptal Edilsin mi?", ToolTip = "Belgeyi İptal Etmek Için", SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject)]
        public void BelgeIptal()
        {
           
        }

        [Action(PredefinedCategory.Save, Caption = "Ceki Listesi", TargetObjectsCriteria = "[HareketTip] = 'Cikis'", ImageName = "Navigation_Item_Report", ToolTip = "Çeki listesi", SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject)]
        public void CekiListesi()
        {
            try
            {
                SistemKullanicilari currentUser = SecuritySystem.CurrentUser as SistemKullanicilari;
                if (currentUser == null)
                    throw new Exception("Bu işlem için yetkiniz yok!");

                if (this.AmbalajHareketleri.Count < 1) throw new Exception("Ambalaj bilgisi yok! Bu belgede hiç okutma yapilmamis!");
                else
                {
                    int belge = this.AmbalajHareketleri[0].Oid;

                    if (WebWindow.CurrentRequestWindow != null)
                        WebWindow.CurrentRequestWindow.RegisterClientScript("tmm" + this.GetType().Name, "window.open ('" + @"Apps/mobil/ceki.aspx?belge=" + belge + "', 'Çeki Listesi','status=1,toolbar=1,width=850,height=650');");


                    //System.IO.FileStream fs1 = null;
                    //fs1 = System.IO.File.Open(copyFile, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    //byte[] buffer = new byte[fs1.Length];
                    //fs1.Read(buffer, 0, (int)fs1.Length);
                    //fs1.Close();




                    //Page page = WebWindow.CurrentRequestPage;
                    //HttpContext.Current.Response.Clear();
                    //HttpContext.Current.Response.ClearHeaders();
                    //HttpContext.Current.Response.ClearContent();
                    //HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + copInfo.Name);
                    //HttpContext.Current.Response.AddHeader("Content-Type", "application/Excel");
                    //HttpContext.Current.Response.ContentType = "application/vnd.xls";
                    //HttpContext.Current.Response.AddHeader("Content-Length", copInfo.Length.ToString());
                    //HttpContext.Current.Response.TransmitFile(copInfo.FullName);
                    //HttpContext.Current.Response.Flush();
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();

                    //HttpContext.Current.Response.Clear();
                    //HttpContext.Current.Response.AddHeader("Content-Disposition", "Attachment; FileName = " + HttpContext.Current.Server.UrlEncode(copInfo.Name));
                    //HttpContext.Current.Response.Charset = "";
                    //HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //HttpContext.Current.Response.ContentType = "application/vnd.xls";
                    //HttpContext.Current.Response.End();


                    //WebWindow.CurrentRequestPage.Response.Clear();
                    //WebWindow.CurrentRequestPage.Response.AddHeader("Content-Disposition", "attachment; filename=" +
                    //HttpContext.Current.Server.UrlEncode(copInfo.Name));
                    //WebWindow.CurrentRequestPage.Response.AddHeader("Content-Length", copInfo.Length.ToString());
                    ////HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                    //WebWindow.CurrentRequestPage.Response.AppendHeader("Content-Type", "application/vnd.ms-excel");
                    ////Response.ContentType = "application/octet-stream";
                    //WebWindow.CurrentRequestPage.Response.WriteFile(copInfo.FullName);
                    //WebWindow.CurrentRequestPage.Response.End();



                    //HttpContext.Current.Response.Clear();
                    //HttpContext.Current.Response.ClearHeaders();
                    //HttpContext.Current.Response.Buffer = false;
                    //HttpContext.Current.Response.AppendHeader("Content-Type", "application/force-download");//application/vnd.ms-excel
                    ////HttpContext.Current.Response.AppendHeader("Content-Transfer-Encoding", "binary");
                    //HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + HttpContext.Current.Server.UrlEncode(copyFile));                        
                    ////HttpContext.Current.Response.End();
                }
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }

        void webExportController_ModelDefaultExport(object sender, CustomExportEventArgs e)
        {

        }

        #endregion

        protected override void OnDeleting()
        {
        }

        protected override void OnSaving()
        {
            

        }

        #region Fields
        //public class Fields
        //{
        //    //col.Add(Talepler.Fields.KullaniciId == 0);
        //    private Fields() { }
        //    [XmlIgnore()]
        //    public static OperandProperty TalepNo
        //    {
        //        get { return new OperandProperty("TalepNo"); }
        //    }
        //    [XmlIgnore()]
        //    public static OperandProperty TalepEdilenDepo
        //    {
        //        get { return new OperandProperty("StokHareketDetaylari[DepoId=?]"); }
        //    }
        //}
        #endregion
        
        public StokHareketleri() { }
        public StokHareketleri(Session session) : base(session) { }

    }
}
