using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Xml.Serialization;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using System.Collections;

using Mikrobar.Islemler;

using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    //[Appearance(AppearanceItemType = "Action", TargetItems = "Save; SaveAndClose; SaveAndNew", Enabled = false, Criteria = "TalepTur != 'Malzemeler'", Context = "Any")]    
    [ModelDefault("DefaultListViewAllowEdit", "False")]
    [ModelDefault("DefaultListViewShowAutoFilterRow", "True"), ModelDefault("IsClonable", "True")]
    [NavigationItem(false), ImageName("BO_Sale_Item_v92"),
    XafDisplayName("Malzeme Talepleri"), XafDefaultProperty("TalepNo")]
    [DefaultClassOptions, OptimisticLocking(false), DeferredDeletion(false)]
    public class Talepler : XPObject // UPDATE dbo.Talepler SET Durum = 5, Aciklama3 = N'Talep süresi doldu kapatıldı.', GuncellemeTarihi = GETDATE() WHERE CONVERT(DATE, GecerlilikSuresi) < CONVERT(DATE, GETDATE())
    {                
        [Size(DbSize.KodLenght), ReadOnly(true), ModelDefault("AllowEdit", "false")]
        public string TalepNo { get; set; }
        protected TalepTurleri _talepTur;
        [ImmediatePostData]
        [Appearance("Talepturudegistirilemez.", Enabled = false, Criteria = "Oid > 0", AppearanceItemType = "ViewItem", Context = "DetailView", TargetItems = "TalepTur")]
        public TalepTurleri TalepTur
        {
            get { return _talepTur; }
            set
            {
                SetPropertyValue("TalepTur", ref _talepTur, value);
            }
        }

        #region Depolar
        //Talep eden depo
        private Depolar _alepDepo;
        [XmlIgnore(), Association(@"Talepler.TalepDepo.TalepDepoId"), NoForeignKey, Persistent("TalepDepoId"), XafDisplayName("Talep Depo"), ImmediatePostData]
        public Depolar TalepDepo
        {
            get { return _alepDepo; }
            set { SetPropertyValue<Depolar>("TalepDepo", ref _alepDepo, value); }
        }
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false), PersistentAlias("Iif(TalepDepo is null, 0, TalepDepo.DepoId)")]
        public int TalepDepoId 
        { 
            get 
            {
                return Convert.ToInt32(EvaluateAlias("TalepDepoId"));
            }
            set 
            { 
                SetPropertyValue<Depolar>("TalepDepo", ref _alepDepo, Session.GetObjectByKey<Depolar>(value));
                OnChanged("TalepDepo");
            }
        }
        [Size(DbSize.KodLenght), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public string TalepDepoKod { get; set; }
        [VisibleInDetailView(false), VisibleInLookupListView(false), PersistentAlias("Iif(TalepDepo is null, '', TalepDepo.DepoAd)")]
        public string TalepDepoAd
        {
            get
            {
                return Convert.ToString(EvaluateAlias("TalepDepoAd"));
            }
            set
            { 
            }
        }

        //Talep edilen depo
        private Depolar _talepEdilenDepo;
        [XmlIgnore(), Association(@"Talepler.TalepEdilenDepo.TalepEdilenDepoId"), NoForeignKey, Persistent("TalepEdilenDepoId"), XafDisplayName("Talep Edilen Depo"), ImmediatePostData]
        public Depolar TalepEdilenDepo
        {
            get { return _talepEdilenDepo; }
            set { SetPropertyValue<Depolar>("TalepEdilenDepo", ref _talepEdilenDepo, value); }
        }
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false), PersistentAlias("Iif(TalepEdilenDepo is null, 0, TalepEdilenDepo.DepoId)")]
        public int TalepEdilenDepoId
        {
            get
            {
                return Convert.ToInt32(EvaluateAlias("TalepEdilenDepoId"));
            }
            set
            {
                SetPropertyValue<Depolar>("TalepEdilenDepo", ref _talepEdilenDepo, Session.GetObjectByKey<Depolar>(value));
                OnChanged("TalepEdilenDepo");
            }
        }
        [Size(DbSize.KodLenght), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public string TalepEdilenDepoKod { get; set; }
        [VisibleInDetailView(false), VisibleInLookupListView(false), PersistentAlias("Iif(TalepEdilenDepo is null, '', TalepEdilenDepo.DepoAd)")]
        public string TalepEdilenDepoAd
        {
            get
            {
                return Convert.ToString(EvaluateAlias("TalepEdilenDepoAd"));
            }
            set { }
        }
        #endregion

        private Kullanicilar _kullanici;
        [XmlIgnore(), Association(@"Talepler.Kullanici.KullaniciId"), NoForeignKey, Persistent("KullaniciId"), XafDisplayName("Gorevli Kullanici"), ImmediatePostData]
        public Kullanicilar Kullanici
        {
            get { return _kullanici; }
            set { SetPropertyValue<Kullanicilar>("Kullanici", ref _kullanici, value); }
        }
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false), PersistentAlias("Iif(Kullanici is null, 0, Kullanici.KullaniciId)")]
        public int KullaniciId { get { return Convert.ToInt32(EvaluateAlias("KullaniciId")); } }
        //[ValueConverter(typeof(TalepSureConverter))]
        public TalepSureleri Oncelik { get; set; }
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}"), ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxDatePropertyEditor")]
        public DateTime GecerlilikSuresi { get; set; }
        [DXDescription("Mal kabul/onay islemi gerektirir.")]
        public bool TeslimTesellum { get; set; }
        [DXDescription("Tam okutulmadan gonderim kontrolu.")]
        public bool BakiyeKontrolu { get; set; }
        [DXDescription("Konsinye cikis hareketi.")]
        public bool KonsinyeCikis { get; set; } // viko konsinye calismasi

        [ModelDefault("DisplayFormat", "{0:0.#}")]
        public decimal ReferansId { get; set; }
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}")]
        public DateTime TalepTarihi { get; set; }

        [ModelDefault("AllowEdit", "False"), ReadOnly(true)]
        public TalepDurumlari TalepDurumu { get; set; }

        [Size(SizeAttribute.Unlimited)]
        //[ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.HtmlPropertyEditor.Web.ASPxHtmlPropertyEditor")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public string Aciklama { get; set; }
        [Size(DbSize.Limitsiz), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Aciklama2 { get; set; }
        [Size(DbSize.AciklamaLenght), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Aciklama3 { get; set; }

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

        [ModelDefault("AllowEdit", "False"), ModelDefault("DisplayFormat", "{0:dd.MM.yyyy HH:mm}"), ReadOnly(true), VisibleInListView(false), VisibleInLookupListView(false)]
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

        [ModelDefault("AllowEdit", "False"), ReadOnly(true), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false), NonPersistent]
        public string OlusturanKullaniciKod
        {
            get
            {
                if (!IsLoading && !IsSaving)
                {
                    if (this.OlusturanKullanici != null)
                        return this.OlusturanKullanici.KullaniciAd;
                }
                return "";
            }
            set { }
        }

        #endregion

        [Association("TalepDetaylari_Talepler"), XmlIgnore()]
        public XPCollection<TalepDetaylari> Malzemeler
        {
            get { return GetCollection<TalepDetaylari>("Malzemeler"); }
        }

        [Association("TalepKabulleri.Talepler.Talep"), XmlIgnore()]
        public XPCollection<TalepKabulleri> Kabuller
        {
            get { return GetCollection<TalepKabulleri>("Kabuller"); }
        }

        [Action(PredefinedCategory.Save/*Windows*/, Caption = "Yazdir", TargetObjectsCriteria = " 1 = 1 ", ImageName = "BO_Sale_Item_v92")]
        public void Yazdir()
        {
            try
            {



                //if (WebWindow.CurrentRequestWindow != null)
                //    WebWindow.CurrentRequestWindow.RegisterClientScript("prnt" + this.GetType().Name, sbScript.ToString());


                //WebApplication app = WebWindow.CurrentRequestWindow.Application;
                //IObjectSpace objectSpace = app. CreateObjectSpace();
                //string listViewId = app.FindListViewId(typeof(IsEmriBilesenleri));
                //ShowViewParameters svp = new ShowViewParameters();
                //svp.CreatedView = app.CreateListView(listViewId, app.CreateCollectionSource(objectSpace, typeof(IsEmriBilesenleri), listViewId), true);

                ////CollectionSourceBase CollectionSource = new CollectionSource(ObjectSpace, ClassInfo.ClassType);
                //View v = app.CreateListView(app.FindListViewId(typeof(IsEmriBilesenleri)), new CollectionSource(objectSpace, typeof(IsEmriBilesenleri)), false);

                //svp.TargetWindow = TargetWindow.NewModalWindow;
                //svp.Context = TemplateContext.PopupWindow;
                //svp.CreateAllControllers = true;
                //DialogController dialogController = app.CreateController<DialogController>();
                //dialogController.ViewClosed += new EventHandler(dialogController_ViewClosed);
                //svp.Controllers.Add(dialogController);
                //WebWindow.CurrentRequestWindow.Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(WebWindow.CurrentRequestWindow, null));
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        
        [Action(PredefinedCategory.Save/*Windows*/, Caption = "İş Emri Seç", TargetObjectsCriteria = "TalepTur = 'IsEmriMalzemeleri'", ImageName = "BO_Sale_Item_v92")]
        public void IsEmriSec()
        {
            try
            {
                WebApplication app = WebWindow.CurrentRequestWindow.Application;
                IObjectSpace objectSpace = app.CreateObjectSpace();
                string listViewId = app.FindListViewId(typeof(IsEmriBilesenleri));
                ShowViewParameters svp = new ShowViewParameters();
                svp.CreatedView = app.CreateListView(listViewId, app.CreateCollectionSource(objectSpace, typeof(IsEmriBilesenleri), listViewId), true);

                //CollectionSourceBase CollectionSource = new CollectionSource(ObjectSpace, ClassInfo.ClassType);
                View v = app.CreateListView(app.FindListViewId(typeof(IsEmriBilesenleri)), new CollectionSource(objectSpace, typeof(IsEmriBilesenleri)), false);

                svp.TargetWindow = TargetWindow.NewModalWindow;
                svp.Context = TemplateContext.PopupWindow;
                svp.CreateAllControllers = true;
                DialogController dialogController = app.CreateController<DialogController>();
                dialogController.ViewClosed += new EventHandler(dialogController_ViewClosed);
                svp.Controllers.Add(dialogController);
                WebWindow.CurrentRequestWindow.Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(WebWindow.CurrentRequestWindow, null));
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }

        void dialogController_ViewClosed(object sender, EventArgs e)
        {
            //SayimEmirleri emir = ((DetailView)WebWindow.CurrentRequestWindow.View).CurrentObject as SayimEmirleri;
            IList selectedObjects = ((ListView)sender).SelectedObjects;
            if (selectedObjects != null && selectedObjects.Count > 0)
            {
                //IEnumerable<V_IsEmirleri> vraf = selectedObjects.Cast<V_IsEmirleri>();
                //int[] rafIds = vraf.Select(x => x.RafId).ToArray();
                //int rowCount = this.Session.ExecuteNonQuery(string.Format("UPDATE Raflar SET Sayim = 1 WHERE RafId IN ({0})", string.Join(",", rafIds)));
            }
        }

        #region Calismalar<xaf>
        //BindingList<V_IsEmirleri> BindingList_IsEmirleri = null;
        //[ImmediatePostData]
        //[Appearance("isemrilist", Visibility = ViewItemVisibility.Hide, Criteria = "TalepTur != 'IsEmriMalzemeleri'", AppearanceItemType = "ViewItem", Context = "DetailView", TargetItems = "IsEmirleri")]
        //public BindingList<V_IsEmirleri> IsEmirleri
        //{
        //    get {
        //        if (BindingList_IsEmirleri == null)
        //        {
        //            BindingList_IsEmirleri = new BindingList<V_IsEmirleri>();
        //            BindingList_IsEmirleri.AllowNew = true;
        //            BindingList_IsEmirleri.AllowRemove = true;
        //            BindingList_IsEmirleri.AllowEdit = false;
        //            BindingList_IsEmirleri.Clear();
        //        }
        //        if (BindingList_IsEmirleri != null)
        //        {
        //            BindingList_IsEmriBilesenleri = new BindingList<IsEmriBilesenleri>();
        //            int[] ids = BindingList_IsEmirleri.Select(x => x.IsEmriId).ToArray();
        //            XPCollection<IsEmriBilesenleri> xbilesen = new XPCollection<IsEmriBilesenleri>(this.Session, new InOperator("IsEmriId", ids), null);
        //            foreach (IsEmriBilesenleri ibil in xbilesen)
        //                BindingList_IsEmriBilesenleri.Add(ibil);
        //        }
        //        return BindingList_IsEmirleri;
        //    }
        //}

        //BindingList<IsEmriBilesenleri> BindingList_IsEmriBilesenleri = null;
        //[ImmediatePostData]
        //[Appearance("bilesenler", Visibility = ViewItemVisibility.Hide, Criteria = "TalepTur != 'IsEmriMalzemeleri'", AppearanceItemType = "ViewItem", Context = "DetailView", TargetItems = "Bilesenler")]
        //public BindingList<IsEmriBilesenleri> Bilesenler
        //{
        //    get
        //    {
        //        if (BindingList_IsEmriBilesenleri == null)
        //        {
        //            BindingList_IsEmriBilesenleri = new BindingList<IsEmriBilesenleri>();
        //            BindingList_IsEmriBilesenleri.AllowNew = true;
        //            BindingList_IsEmriBilesenleri.AllowRemove = true;
        //            BindingList_IsEmriBilesenleri.AllowEdit = false;
        //            BindingList_IsEmriBilesenleri.Clear();
        //        }
        //        return BindingList_IsEmriBilesenleri;
        //    }
        //}

        /*[DataSourceCriteria("Region = '@This.Region'")]

ICity City { get; set; }

}*/

        //[Action(Caption = "Is Emrinden Olustur", TargetObjectsCriteria = "[TalepTur] = 1", ImageName = "BO_Sale_Item_v92")]
        //public void IsEmrindenTalepOlustur()
        //{
        //    try
        //    {
        //        if (BindingList_IsEmriBilesenleri.Count > 0)
        //        {
        //            foreach (IsEmriBilesenleri ibilesen in BindingList_IsEmriBilesenleri)
        //            {
        //                TalepDetaylari xdetay = this.Malzemeler.Where(x => x.MalzemeId == ibilesen.MalzemeId).FirstOrDefault();
        //                if (xdetay != null)
        //                {
        //                    xdetay.TalepMiktar += ibilesen.IsEmriMiktar * ibilesen.BirimMiktar;
        //                }
        //                else
        //                {
        //                    TalepDetaylari tdetay = new TalepDetaylari(this.Session);
        //                    tdetay.MalzemeId = ibilesen.MalzemeId;
        //                    tdetay.MalzemeKod = ibilesen.MalzemeKod;
        //                    tdetay.TalepMiktar = ibilesen.IsEmriMiktar * ibilesen.BirimMiktar;
        //                    tdetay.BirimId = ibilesen.BirimId;
        //                    tdetay.Birim = ibilesen.Birim;
        //                    this.Malzemeler.Add(tdetay);
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception exc)
        //    {

        //        throw exc;
        //    }
        //}
        #endregion


        protected override void OnSaving()
        {
            
        }

        protected override void OnDeleting()
        {
            int okunanCount = (int)this.Session.Evaluate<StokHareketleri>(CriteriaOperator.Parse("Count()"),
                    new GroupOperator(GroupOperatorType.And, new BinaryOperator("MalTalepId", this.Oid), new BinaryOperator("Durum", KayitDurumu.Iptal)));
            if (okunanCount > 0)
                throw new Exception("Bu talep için okutma yapilmıştır silinemez!");

            #region Detaylari Sil
            if (this.Malzemeler.Count > 0)
            {
                for (int i = 0; this.Malzemeler.Count > 0; i++)
                {
                    this.Malzemeler[0].Delete();
                }
            }
            #endregion
            base.OnDeleting();
        }

        #region Fields
        public class Fields
        {
            //col.Add(Talepler.Fields.KullaniciId == 0);
            private Fields() { }
            [XmlIgnore()]
            public static OperandProperty TalepNo
            {
                get { return new OperandProperty("TalepNo"); }
            }
            [XmlIgnore()]
            public static OperandProperty TalepEdilenDepo
            {
                get { return new OperandProperty("TalepEdilenDepo"); }
            }
            [XmlIgnore()]
            public static OperandProperty Kullanici
            {
                get { return new OperandProperty("Kullanici"); }
            }
            [XmlIgnore()]
            public static OperandProperty TalepSuresi
            {
                get { return new OperandProperty("TalepSuresi"); }
            }
        } 
        #endregion

        public Talepler() { }
        public Talepler(Session session) : base(session) { }
    }
}
