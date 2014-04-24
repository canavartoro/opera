using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Web;
using Mikrobar;
using DevExpress.ExpressApp.Model;
using Microsoft.Win32.TaskScheduler;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace Mikrobar.Module.BusinessObjects
{
    /// <summary>
    /// Data entegrasyonlari icin
    /// </summary>
    [DeferredDeletion(false), OptimisticLocking(false), DefaultClassOptions, XafDefaultProperty("Oid"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    [NavigationItem(false), ImageName("BO_Role")]
    public class Entegrasyonlar : XPObject
    {
        [Indexed, Size(DbSize.KodLenght)]
        public string TabloAdi { get; set; }

        [Size(DbSize.AciklamaLenght), Indexed]
        public string ObjeAdi { get; set; }

        [Size(SizeAttribute.Unlimited)]
        public string Sorgu { get; set; }

        [Size(DbSize.DetayliMesaj)]
        public string Kosul { get; set; }

        [NonPersistent, Browsable(false)]
        public string FirmaKosul
        {
            get
            {
                if (!IsLoading && !IsSaving)
                {
                    if (!string.IsNullOrEmpty(Kosul))
                    {
                        return Kosul;
                    }
                }
                return Kosul;
            }
        }

        [Size(DbSize.SaatLenght)]
        public string BaslangicSaat { get; set; }

        public int Gun { get; set; }
        public int Saat { get; set; }
        public int Dakika { get; set; }
        public int Saniye { get; set; }

        public string SonCalismaDurumu { get; set; }
        public DateTime SonCalismaZamani { get; set; }

        public EntegrasyonTip ProgramTipi { get; set; }
        public QueryType EntegrasyonTipi { get; set; }
        public bool BulkInsert { get; set; }

        #region Butonlar
        [Action(PredefinedCategory.RecordEdit, Caption = "Çalıştır", ImageName = "Action_Refresh", ToolTip = "Bilgileri guncelle..")]
        public void Entegrasyon()
        {
            try
            {
                ////Type t = Type.GetType(string.Format("Mikrobar.Module.BusinessObjects.{0}", ObjeAdi));
                ////if (object.ReferenceEquals(t, null))
                ////{
                ////    //t = AppDomain.CurrentDomain.GetAssemblies()
                ////    //            .Where(a => a.FullName == "MyFramework")
                ////    //            .SelectMany(a => a.GetTypes())
                ////    //            .Where(x => x.Name == "Car")
                ////    //            .FirstOrDefault();
                ////    System.Reflection.Assembly[] ass = AppDomain.CurrentDomain.GetAssemblies();
                ////    foreach (System.Reflection.Assembly assembly in ass)
                ////    {
                ////        if (assembly.FullName.StartsWith("Mikrobar.Module"))
                ////        {
                ////            t = assembly.GetType(string.Format("Mikrobar.Module.BusinessObjects.{0}", ObjeAdi), false);
                ////            if (t != null) break;
                ////        }
                ////    }
                ////}
                ////Mikrobar.Entegre.DataQuery query = new Mikrobar.Entegre.DataQuery(t);
                ////object ret = query.Entegre();

                //////DevExpress.Xpo.DB.SelectedData data = this.Session.ExecuteSproc("sp_CariGuncelleERP");
                ////if (WebWindow.CurrentRequestWindow != null)
                ////    WebWindow.CurrentRequestWindow.RegisterClientScript("tmm" + this.GetType().Name, "alert('İşlem tamamlandı. Sonuc:" + query.HataMesaji + "');");
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        [Action(PredefinedCategory.RecordEdit, Caption = "Görev Oluştur", ImageName = "Action_Refresh", ToolTip = "Zamanlanmış görev oluştur.")]
        public void GorevOlustur()
        {
            try
            {
                ////using (TaskService ts = new TaskService())
                ////{
                ////    TaskDefinition td = ts.NewTask();
                ////    td.RegistrationInfo.Description = string.Format("Barset {0} Entegrasyon", this.ObjeAdi);
                ////    Trigger trig = new TimeTrigger();
                ////    trig.Repetition.Interval = new TimeSpan(this.Gun, this.Saat, this.Dakika, 0);
                ////    td.Triggers.Add(trig);

                ////    td.Actions.Add(new ExecAction(Utility.AppsPath + "\\Entegrasyon\\Entegrasyon.exe", "-" + this.TabloAdi, null));

                ////    /*WebConfige eklenmeli..
                ////     * <system.web>
                ////     *      <identity impersonate="true"  userName="BilgisayaraGirisKullaniciAdi" password="Sifre"  />
                ////     * </system.web>
                ////     */
                ////    ts.RootFolder.RegisterTaskDefinition(this.TabloAdi, td);
                ////    //ts.RootFolder.RegisterTaskDefinition(this.TabloAdi, td, TaskCreation.CreateOrUpdate, "SYSTEM", null, TaskLogonType.ServiceAccount, null); 

                ////    if (WebWindow.CurrentRequestWindow != null)
                ////        WebWindow.CurrentRequestWindow.RegisterClientScript("tmm" + this.GetType().Name, "alert('İşlem tamamlandı.');");

                ////}

            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        [Action(PredefinedCategory.RecordEdit, Caption = "Görev İptal", ImageName = "Action_Refresh", ToolTip = "Zamanlanmış görev varsa iptal et.")] 
        public void GorevIptal()
        {
            try
            {
                using (TaskService ts = new TaskService())
                {
                    Task task = ts.FindTask(this.TabloAdi, true);
                    if (task != null)
                    {
                        ts.RootFolder.DeleteTask(this.TabloAdi);
                        if (WebWindow.CurrentRequestWindow != null)
                            WebWindow.CurrentRequestWindow.RegisterClientScript("tmm" + this.GetType().Name, "alert('Görev silindi.');");
                    }
                    else
                    {
                        if (WebWindow.CurrentRequestWindow != null)
                            WebWindow.CurrentRequestWindow.RegisterClientScript("tmm" + this.GetType().Name, "alert('İşlem tamamlandı.');");
                    }
                }

            }
            catch (Exception exc)
            {
                throw exc;
            }
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

       

        public Entegrasyonlar() { }
        public Entegrasyonlar(Session session) : base(session) { }
    }
}


/*


CREATE VIEW [dbo].[V_Entegrasyonlar]
AS
SELECT  
	OID AS EntegrasyonId,   
	TabloAdi, 
	SonCalismaDurumu, 
	SonCalismaZamani, 
	GuncellemeTarihi, 
	dbo.f_GorevZamanlama(OID) AS [Zamanlama],
	DATEDIFF(MINUTE, SonCalismaZamani, GETDATE()) AS [GecenSure],
	Sorgu,
	ObjeAdi,
	ProgramTipi
FROM dbo.Entegrasyonlar WITH (NOLOCK)
WHERE 1 = 1
AND Durum = 0 
AND DATEDIFF(MINUTE, SonCalismaZamani, GETDATE()) >= dbo.f_GorevZamanlama(OID)
GO



 */