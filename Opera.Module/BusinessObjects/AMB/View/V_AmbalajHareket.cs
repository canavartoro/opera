using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{

    [Persistent("V_AmbalajHareket"), ModelDefault("DefaultListViewShowAutoFilterRow", "True"),
    ImageName("BO_Sale_Item_v92"), XafDefaultProperty("Barkod"), XafDisplayName("Ambalaj Hareket Bilgisi")]
    public class V_AmbalajHareket : XPLiteObject
    {
        public V_AmbalajHareket() { }
        public V_AmbalajHareket(Session session) : base(session) { }

        public string Barkod { get; set; }
        public string Birim { get; set; }
        public string Birim2 { get; set; }
        public string MalzemeAd { get; set; }
        public string MalzemeKod { get; set; }
        public string CikisDepo { get; set; }
        public string CikisRaf { get; set; }
        [DbType(" DECIMAL(18,4) ")]
        public decimal Miktar { get; set; }
        [DbType(" DECIMAL(18,4) ")]
        public decimal Miktar2 { get; set; }
        public string GirisDepo { get; set; }
        public string GirisRaf { get; set; }
        public string KaynakModul { get; set; }
        public KayitDurumu Durum { get; set; }
        public HareketTipi HareketTip { get; set; }
        public string PartiNo { get; set; }
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}")]
        public DateTime OlusturmaTarihi { get; set; }
        protected int hareketId = 0;
        public int HareketId 
        {
            get { return hareketId; }
            set
            {
                SetPropertyValue<int>("HareketId", ref hareketId, value);                
            }
        }
        [Key]
        public int OID { get; set; }
        public string KullaniciAd { get; set; }
        public string KullaniciKod { get; set; }

        #region Harekete Git
        [Action(Caption = "Harekete Git", AutoCommit = false, TargetObjectsCriteria = "[HareketId] > 0", ImageName = "ModelEditor_GoToObject", ToolTip = "Hareket kaydina git", SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject)]
        public void HareketeGit()
        {
            try
            {
                WebApplication application = WebApplication.Instance;
                IObjectSpace objectSpace = application.CreateObjectSpace();
                AmbalajHareketleri obj = objectSpace.GetObjectByKey<AmbalajHareketleri>(hareketId);
                if (obj != null)
                {
                    View detailView = application.CreateDetailView(objectSpace, obj);
                    ShowViewParameters showViewParameters = new ShowViewParameters(detailView);
                    showViewParameters.TargetWindow = TargetWindow.Default;
                    ShowViewSource viewSource = new ShowViewSource(WebWindow.CurrentRequestWindow, null);
                    application.ShowViewStrategy.ShowView(showViewParameters, viewSource);
                }
            }
            catch (Exception) { }
        }

        [Action(Caption = "Ambalaja Git", AutoCommit = false, TargetObjectsCriteria = "Len(Barkod) > 0", ImageName = "ModelEditor_GoToObject", ToolTip = "Ambalaj kaydina git")]
        public void AmbalajaGit()
        {
            try
            {
                WebApplication application = WebApplication.Instance;
                IObjectSpace objectSpace = application.CreateObjectSpace();
                Ambalajlar obj = objectSpace.FindObject<Ambalajlar>(new BinaryOperator("Barkod", this.Barkod, BinaryOperatorType.Equal));
                if (obj != null)
                {
                    View detailView = application.CreateDetailView(objectSpace, obj);
                    ShowViewParameters showViewParameters = new ShowViewParameters(detailView);
                    showViewParameters.TargetWindow = TargetWindow.Default;
                    ShowViewSource viewSource = new ShowViewSource(WebWindow.CurrentRequestWindow, null);
                    application.ShowViewStrategy.ShowView(showViewParameters, viewSource);
                }
            }
            catch (Exception) { }
        } 
        #endregion
    }
}
