using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Xml.Serialization;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.SystemModule;
using System.Collections;
using DevExpress.ExpressApp.Actions;

using Mikrobar.Module.BusinessObjects;
using Mikrobar.Islemler;
using Mikrobar;


using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("EmirAdi"),
     NavigationItem(false), ImageName("BO_Sale_Item_v92"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    public class SayimEmirleri : XPObject
    {
        [Size(DbSize.KodLenght)]
        public string EmirAdi { get; set; }
        [XmlIgnore()]
        public string Aciklama { get; set; }
        [ModelDefault("AllowEdit", "false"), ReadOnly(true), XmlIgnore()]
        public string DurumAciklama { get; set; }

        [Size(DbSize.NoLenght), Description("Belge erp ye aktarilirken verilecek belge no"), ImmediatePostData]
        public string BelgeNo { get; set; }

        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}"), ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxDatePropertyEditor")]
        public DateTime BelgeTarihi { get; set; }

        private int buttonEnabled = -1;
        [Browsable(false), NonPersistent, XmlIgnore(), ImmediatePostData]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int ButtonEnabled
        {
            get 
            {
                if (buttonEnabled == -1)
                {
                    buttonEnabled = 0;
                    SistemKullanicilari currentUser = SecuritySystem.CurrentUser as SistemKullanicilari;
                    if (currentUser != null)
                    {
                        for (int i = 0; i < currentUser.Roles.Count; i++)
                        {
                            if (currentUser.Roles[i].IsAdministrative)
                            {
                                buttonEnabled = 1;
                                break;
                            }
                            SecuritySystemTypePermissionObject obj = currentUser.Roles[i].FindTypePermissionObject<SayimEmirleri>();
                            if (obj != null)
                            {
                                if (obj.AllowWrite)
                                {
                                    buttonEnabled = 1;
                                    break;
                                }
                            }
                        }

                        //List<SecuritySystemRole> rols = currentUser.Roles.ToList();
                        //SecuritySystemTypePermissionObject per= rols[0].FindTypePermissionObject(this.GetType());
                        //var perm = new List<object>();//currentUser.GetPermissions().Where(x => x.Operation.Equals("Write")).ToList();
                        //foreach (var xp in perm)
                        //{
                        //    DevExpress.ExpressApp.Security.TypeOperationPermission txp = xp as DevExpress.ExpressApp.Security.TypeOperationPermission;
                        //    if (txp != null)
                        //    {
                        //        if (txp.ObjectType == GetType())
                        //        {
                        //            buttonEnabled = txp.Operation.Equals("Write") ? 1 : 0;
                        //            //if (txp.Operation.Equals("Read"))
                        //            //    _Read = true;
                        //            //if (txp.Operation.Equals("Write"))
                        //            //    _Write = true;
                        //            //if (txp.Operation.Equals("Create"))
                        //            //    _Create = true;
                        //            //if (txp.Operation.Equals("Delete"))
                        //            //    _Delete = true;
                        //            //if (txp.Operation.Equals("Navigate"))
                        //            //    _Navigate = true;   
                        //        }
                        //        System.Diagnostics.Trace.WriteLine(string.Format("{0} {1}", txp.ObjectType, txp.Operation));
                        //    }                            
                        //}                           
                    }
                    //TypeOperationPermissionData itemPermission = Session.FindObject<TypeOperationPermissionData>(CriteriaOperator.Parse(" TargetType = ?", GetType()));
                    //if (itemPermission != null)
                    //{
                    //    buttonEnabled = itemPermission.AllowWrite == true ? 1 : 0;
                    //}
                }
                return buttonEnabled; 
            }
            set
            {
                
            }
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

        [Association(@"Sayimlar.SayimE_SayimEmirleri")]
        public XPCollection<Sayimlar> Sayim
        {
            get { return GetCollection<Sayimlar>(@"Sayim"); }
        }

        BindingList<V_Raflar> BindingList_V_Raflar = null;
        [ImmediatePostData]
        public BindingList<V_Raflar> Raflar
        {
            get
            {
                if (BindingList_V_Raflar == null)
                {
                    BindingList_V_Raflar = new BindingList<V_Raflar>();
                    BindingList_V_Raflar.AllowNew = true;
                    BindingList_V_Raflar.AllowRemove = true;
                    BindingList_V_Raflar.AllowEdit = false;
                    BindingList_V_Raflar.Clear();
                }
                return BindingList_V_Raflar;
            }
        }

        BindingList<Kullanicilar> BindingList_Kullanicilar = null;
        [ImmediatePostData]
        public BindingList<Kullanicilar> Kullanicilar
        {
            get
            {
                if (BindingList_Kullanicilar == null)
                {
                    BindingList_Kullanicilar = new BindingList<Kullanicilar>();
                    BindingList_Kullanicilar.AllowNew = true;
                    BindingList_Kullanicilar.AllowRemove = true;
                    BindingList_Kullanicilar.AllowEdit = false;
                    BindingList_Kullanicilar.Clear();
                }
                return BindingList_Kullanicilar;
            }
        }

        [Action(PredefinedCategory.View, Caption = "Raflari Ac", ImageName = "BO_Sale_Item_v92")]
        public void RaflariAc()
        {
            try
            {
                WebApplication app = WebWindow.CurrentRequestWindow.Application;
                IObjectSpace objectSpace = app.CreateObjectSpace();
                string listViewId = app.FindListViewId(typeof(V_Raflar));
                ShowViewParameters svp = new ShowViewParameters();
                svp.CreatedView = app.CreateListView(listViewId, app.CreateCollectionSource(objectSpace, typeof(V_Raflar), listViewId), true);
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

        private void MyPopupWindowShowAction_ModelDefaultizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = WebWindow.CurrentRequestWindow.Application.CreateObjectSpace();
            string listViewId = WebWindow.CurrentRequestWindow.Application.FindListViewId(typeof(V_Raflar));
            e.View = WebWindow.CurrentRequestWindow.Application.CreateListView(
               listViewId,
               WebWindow.CurrentRequestWindow.Application.CreateCollectionSource(objectSpace, typeof(V_Raflar), listViewId),
               true);
            e.DialogController = WebWindow.CurrentRequestWindow.Application.CreateController<DialogController>();
        }

        void iptals_Executed(object sender, ActionBaseEventArgs e)
        {

        }

        void dialogController_ViewClosed(object sender, EventArgs e)
        {
            try
            {
                //SayimEmirleri emir = ((DetailView)WebWindow.CurrentRequestWindow.View).CurrentObject as SayimEmirleri;
                IList selectedObjects = ((ListView)sender).SelectedObjects;
                if (selectedObjects != null && selectedObjects.Count > 0)
                {
                    IEnumerable<V_Raflar> vraf = selectedObjects.Cast<V_Raflar>();
                    int[] rafIds = vraf.Select(x => x.RafId).ToArray();
                    int rowCount = this.Session.ExecuteNonQuery(string.Format("UPDATE \"Raflar\" SET \"Sayim\" = 1 WHERE \"RafId\" IN ({0})", string.Join(",", rafIds)));
                    //if (WebWindow.CurrentRequestWindow != null)
                    //    WebWindow.CurrentRequestWindow.RegisterClientScript("tmm" + this.GetType().Name, "alert('İşlem tamamlandı. Acilan raf sayisi:" + rowCount + "');");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Action(PredefinedCategory.View, Caption = "Kullanicilari Ata", ImageName = "BO_Sale_Item_v92", SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject)]
        public void KullanicilariAta()
        {
            try
            {
                WebApplication app = WebWindow.CurrentRequestWindow.Application;
                IObjectSpace objectSpace = app.CreateObjectSpace();
                string listViewId = app.FindListViewId(typeof(Kullanicilar));
                ShowViewParameters svp = new ShowViewParameters();
                svp.CreatedView = app.CreateListView(listViewId, app.CreateCollectionSource(objectSpace, typeof(Kullanicilar), listViewId), true);
                svp.TargetWindow = TargetWindow.NewModalWindow;
                svp.Context = TemplateContext.PopupWindow;
                svp.CreateAllControllers = true;
                DialogController dialogController = app.CreateController<DialogController>();
                dialogController.ViewClosed += new EventHandler(dialogController_ViewClosed2);
                svp.Controllers.Add(dialogController);
                WebWindow.CurrentRequestWindow.Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(WebWindow.CurrentRequestWindow, null));
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }

        void dialogController_ViewClosed2(object sender, EventArgs e)
        {
            try
            {
                IList selectedObjects = ((ListView)sender).SelectedObjects;
                if (selectedObjects != null && selectedObjects.Count > 0)
                {
                    IEnumerable<Kullanicilar> vraf = selectedObjects.Cast<Kullanicilar>();
                    int[] rafIds = vraf.Select(x => x.KullaniciId).ToArray();
                    int rowCount = this.Session.ExecuteNonQuery(string.Format("UPDATE \"Kullanicilar\" SET \"Sayim\" = 1 WHERE \"KullaniciId\" IN ({0})", string.Join(",", rafIds)));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Action(PredefinedCategory.Edit, Caption = "Sayım Aktar", ImageName = "BO_Sale_Item_v92",
            ConfirmationMessage = "Seçili sayım emri detayları ERP sistemine aktarılacaktır. Onaylıyor musunuz ?",
            TargetObjectsCriteria = "ButtonEnabled = 1")]
        public void SayimAktar(SayimAktarimParametreleri Params)
        {
            /*try
            {

                if (this.Durum == KayitDurumu.Kapali)
                    throw new Exception("Seçili sayım emri kapatılmıştır.");

                SayimParam Param = new SayimParam();

                if (Params.TumunuAktar == false)
                {
                    if (Params.Depo.IsNull())
                    {
                        throw new Exception("Tümünü Aktar Seçili Olmadığı Durumlarda Depo Kodu Zorunludur.");
                    }
                    else
                    {
                        Param.DepoKod = Params.Depo.DepoKod;
                    }
                }
                Param.Islem = SayimIslem.SayimKayitIslemleri;
                Param.KayitIslem = SayimKayitIslem.FiiliSayimDonemAktarim;
                Param.SayimEmriId = this.Oid;
                Param.Secim = new SecimIslemi();
                Param.Secim.Tarih1 = this.BelgeTarihi.Date;
                Param.Secim.Ad = this.BelgeNo;

                SayimResult Rslt = new SayimIslemler().Islem(Param);
                if (Rslt.Status)
                {
                    var count = Utility.Session.Evaluate<Sayimlar>
                        (
                            CriteriaOperator.Parse("Count()"),
                            CriteriaOperator.Parse("SayimEmri = ? AND Durum = ?", this.Oid, (Int32)KayitDurumu.Aktarilacak)
                        );

                    if ((Int32)count < 1)
                    {
                        this.DurumAciklama = "Sayım Emri Kapatıldı.";
                        this.Durum = KayitDurumu.Kapali;
                    }
                    else
                    {
                        this.DurumAciklama = "Depo Aktarımı Tamamlandı. Diğer Aktarımlar Beklenmekte.";
                    }

                    this.GuncellemeTarihi = DateTime.Now;
                    this.Save();

                    MessageBox(Rslt.IslemSonucu);
                }
                else
                    throw new Exception(Rslt.IslemSonucu);

            }
            catch (Exception exc)
            {
                MessageBox(exc.Message);
            }
            finally
            {
            }*/
        }

        private void MessageBox(string Message)
        {
            if (WebWindow.CurrentRequestWindow != null)
            {
                WebWindow.CurrentRequestWindow.RegisterClientScript("tmm" + this.GetType().Name, string.Format("alert('{0}');", Message));
                System.Threading.Thread.SpinWait(10000);
            }
        }

        #region Event

        protected override void OnSaving()
        {
            #region IsNotDelete

            if (this.IsDeleted == false)
            {
                object xSayimNo = null;
                if (string.IsNullOrEmpty(EmirAdi))
                {
                    xSayimNo = Session.Evaluate<SayimEmirleri>(CriteriaOperator.Parse("Max(Oid)"), null);
                    this.EmirAdi = string.Format("SY{0:00000000}", Convert.ToInt32(xSayimNo) + 1);
                }
                if (string.IsNullOrEmpty(this.BelgeNo))
                {
                    xSayimNo = Session.Evaluate<SayimEmirleri>(CriteriaOperator.Parse("Max(Oid)"), null);
                    this.BelgeNo = string.Format("S_{0:00000000}", Convert.ToInt32(xSayimNo) + 1);
                }
                if (this.BelgeTarihi <= DateTime.MinValue || this.BelgeTarihi >= DateTime.MaxValue)
                    this.BelgeTarihi = DateTime.Now.Date;
                SistemKullanicilari currentUser = SecuritySystem.CurrentUser as SistemKullanicilari;
                if (this.Oid < 1)
                {
                    if (this.Olusturan < 1)
                        this.Olusturan = currentUser != null ? currentUser.KullaniciId : 0;

                    this.OlusturmaTarihi = DateTime.Now;
                    this.KaynakProgram = KaynakProgram.Sayim;
                    this.KaynakModul = GetType().Name;
                    this.DurumAciklama = "Web Ekranından Sayım Emri Belgesi Oluşturuldu.";
                }
                else
                {
                    if (this.Guncelleyen < 1)
                        this.Guncelleyen = currentUser != null ? currentUser.KullaniciId : 0;

                    this.GuncellemeTarihi = DateTime.Now;
                    this.DurumAciklama = String.Format("Kayıt Üzerinde Yapılan Son İşlem: {0}", this.Durum.ToString());
                }
            }

            #endregion
        }
        protected override void OnDeleted()
        {
            if (this.IsDeleted)
            {
                if (Sayim.Count > 0)
                {
                    for (int i = 0; i < Sayim.Count; )
                    {
                        Sayim[i].Delete();
                    }
                }
            }

            base.OnDeleted();
        }

        #endregion

        public SayimEmirleri() { }
        public SayimEmirleri(Session session) : base(session) { }

    }

    [NonPersistent, DefaultClassOptions, ImageName("BO_Sale_Item_v92"),
    XafDisplayName("Aktarim İçin Gereken Bilgiler"), NavigationItem(false)]
    public class SayimAktarimParametreleri 
    {
        public SayimAktarimParametreleri() { }

        protected Depolar fDepo;
        [XafDisplayName("Depo Kodu Secin"),Index(0)]
        [ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxSearchEditButtonPropertyEditor")]
        public Depolar Depo
        {
            get { return fDepo; }
            set { fDepo = value; }
        }

        [Size(DbSize.AdLenght), Index(1)]
        public string DepoAciklama
        {
            get { return (!fDepo.IsNull()) ? fDepo.DepoAd : ""; }
        }

        [Index(2), XafDisplayName("Tüm Depoları Aktar")]
        public bool TumunuAktar { get; set; }
    }
}
