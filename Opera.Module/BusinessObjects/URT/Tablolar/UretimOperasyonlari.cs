using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Xml.Serialization;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Web;
using Mikrobar.Islemler;
using DevExpress.ExpressApp.Filtering;
using DevExpress.ExpressApp.Actions;
using System.Web.UI;

using Mikrobar;
using Mikrobar.Module;



namespace Mikrobar.Module.BusinessObjects
{
    public partial class UretimOperasyonlari
    {
        #region Override's
        protected override void OnDeleting()
        {

            #region AmbalajHareket
            if (this.AmbalajHareket.Count > 0)
            {
                for (int i = this.AmbalajHareket.Count; i > 0; i--)
                {
                    for (int l = this.AmbalajHareket[i - 1].AmbalajHareketDetaylari.Count; l > 0; l--)
                    {
                        this.AmbalajHareket[i - 1].AmbalajHareketDetaylari[l - 1].Delete();
                    }
                    this.AmbalajHareket[i - 1].Delete();
                }
            }
            #endregion
            #region Ambalaj
            if (!(this.Ambalaj == null))
            {
                this.Ambalaj.Durum = AmbalajDurumu.Kapali;
                this.Ambalaj.DurumAciklama = "Uretim Silindi!";
                this.Ambalaj.Save();
            }
            #endregion
            #region Ambalajlar
            if (this.Ambalajlar.Count > 0)
            {                
                List<Ambalajlar> ambList = this.Ambalajlar.ToList();
                for (int i = ambList.Count; i > 0; i--)
                {
                    ambList[i - 1].UretimOperasyon = null;
                    ambList[i - 1].Durum = AmbalajDurumu.Kapali;
                    ambList[i - 1].DurumAciklama = "Uretim Silindi!";
                    ambList[i - 1].Save();
                }
            }
            #endregion
            #region Iscilikler
            if (this.Iscilikler.Count > 0)
            {
                for (int i = this.Iscilikler.Count; i > 0; i--)
                {
                    if (i > 0)
                    {
                        this.Iscilikler[i - 1].Delete();
                    }
                }
            }
            #endregion
            #region Duruslar
            if (this.Duruslar.Count > 0)
            {
                for (int i = this.Duruslar.Count; i > 0; i--)
                {
                    if (i > 0)
                    {
                        this.Duruslar[i - 1].Delete();
                    }
                }
            }
            #endregion
            #region Hurdalar
            if (this.Hurdalar.Count > 0)
            {
                for (int i = this.Hurdalar.Count; i > 0; i--)
                {
                    if (i > 0)
                    {
                        this.Hurdalar[i - 1].Delete();
                    }
                }
            }
            #endregion
            #region Malzemeler
            if (this.Malzemeler.Count > 0)
            {
                
            }
            #endregion
            #region Aletler
            if (this.Aletler.Count > 0)
            {
                for (int i = this.Aletler.Count; i > 0; i--)
                {
                    if (i > 0)
                    {
                        this.Aletler[i - 1].Delete();
                    }
                }
            }
            #endregion


            base.OnDeleting();
        }
        protected override void OnSaving()
        {            
            if (!IsDeleted)
            {
            }
            else
            {
            }

            base.OnSaving();
        }
        public override void AfterConstruction()
        {
            //XafApplication
            //Frame.GetController<CloneObjectViewController>();
            base.AfterConstruction();

        }
        #endregion

        #region Butonlar
        [Action(Caption = "Uretim Iptal", TargetObjectsCriteria = "[Durum] = 0", ImageName = "Action_CloseAllWindows", ConfirmationMessage = "Üretim İptal Edilsin mi?", ToolTip = "Üretim İptal Etmek Için", SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject)]
        public void UretimIptal()
        {
            try
            {
                if (this.Durum != KayitDurumu.Yeni)
                    throw new Exception("Üretim kapalı iptal edilemez!");

                Mikrobar.Module.BusinessObjects.SistemKullanicilari currentUser = DevExpress.ExpressApp.SecuritySystem.CurrentUser as Mikrobar.Module.BusinessObjects.SistemKullanicilari;

                #region AmbalajHareket
                if (this.AmbalajHareket.Count > 0)
                {
                    if (this.AmbalajHareket[0].AmbalajHareketDetaylari != null && this.AmbalajHareket[0].AmbalajHareketDetaylari.Count > 0)
                    {
                        for (int i = 0; i < this.AmbalajHareket[0].AmbalajHareketDetaylari.Count; i++)
                        {
                            this.AmbalajHareket[0].AmbalajHareketDetaylari[0].Guncelleyen = currentUser != null ? currentUser.KullaniciId : this.Guncelleyen;
                            this.AmbalajHareket[0].AmbalajHareketDetaylari[0].GuncellemeTarihi = DateTime.Now;
                            this.AmbalajHareket[0].AmbalajHareketDetaylari[0].KaynakAmbalaj.Durum = AmbalajDurumu.Bekliyor;
                            this.AmbalajHareket[0].AmbalajHareketDetaylari[0].KaynakAmbalaj.Guncelleyen = currentUser != null ? currentUser.KullaniciId : this.Guncelleyen;
                            this.AmbalajHareket[0].AmbalajHareketDetaylari[0].KaynakAmbalaj.GuncellemeTarihi = DateTime.Now;
                            this.AmbalajHareket[0].AmbalajHareketDetaylari[0].KaynakAmbalaj.DurumAciklama = AmbalajDurumlari.KASA_URETIMDE_BOSTA;
                            this.AmbalajHareket[0].AmbalajHareketDetaylari[0].KaynakAmbalaj.Save();
                            this.AmbalajHareket[0].AmbalajHareketDetaylari[0].Durum = KayitDurumu.Iptal;
                            this.AmbalajHareket[0].AmbalajHareketDetaylari[0].Save();
                        }
                        this.AmbalajHareket[0].Guncelleyen = currentUser != null ? currentUser.KullaniciId : this.Guncelleyen;
                        this.AmbalajHareket[0].GuncellemeTarihi = DateTime.Now;
                        this.AmbalajHareket[0].Durum = KayitDurumu.Iptal;
                        this.AmbalajHareket[0].Aciklama1 = "Üretim iptal edildi.";
                        this.AmbalajHareket[0].Save();
                    }
                }
                #endregion

                #region Silmiyoruz kaydi iptal ediyoruz
                if (this.Hurdalar.Count > 0)
                {
                    for (int i = 0; i < this.Hurdalar.Count; i++)
                    {
                        this.Hurdalar[i].Guncelleyen = currentUser != null ? currentUser.KullaniciId : this.Guncelleyen;
                        this.Hurdalar[i].GuncellemeTarihi = DateTime.Now;
                        this.Hurdalar[i].Durum = KayitDurumu.Iptal;
                        this.Hurdalar[i].Save();
                    }
                }

                if (this.Duruslar.Count > 0)
                {
                    for (int i = 0; i < this.Duruslar.Count; i++)
                    {
                        this.Duruslar[i].Guncelleyen = currentUser != null ? currentUser.KullaniciId : this.Guncelleyen;
                        this.Duruslar[i].GuncellemeTarihi = DateTime.Now;
                        this.Duruslar[i].Durum = KayitDurumu.Iptal;
                        this.Duruslar[i].Save();
                    }
                }

                if (this.Iscilikler.Count > 0)
                {
                    for (int i = 0; i < this.Iscilikler.Count; i++)
                    {
                        this.Iscilikler[i].Guncelleyen = currentUser != null ? currentUser.KullaniciId : this.Guncelleyen;
                        this.Iscilikler[i].GuncellemeTarihi = DateTime.Now;
                        this.Iscilikler[i].Durum = KayitDurumu.Iptal;
                        this.Iscilikler[i].Save();
                    }
                }

                if (this.Aletler.Count > 0)
                {
                    for (int i = 0; i < this.Aletler.Count; i++)
                    {
                        this.Aletler[i].Guncelleyen = currentUser != null ? currentUser.KullaniciId : this.Guncelleyen;
                        this.Aletler[i].GuncellemeTarihi = DateTime.Now;
                        this.Aletler[i].Durum = KayitDurumu.Iptal;
                        this.Aletler[i].Save();
                    }
                }

                if (this.Malzemeler.Count > 0)
                {
                    for (int i = 0; i < this.Malzemeler.Count; i++)
                    {
                        this.Malzemeler[i].Guncelleyen = currentUser != null ? currentUser.KullaniciId : this.Guncelleyen;
                        this.Malzemeler[i].GuncellemeTarihi = DateTime.Now;
                        this.Malzemeler[i].Durum = KayitDurumu.Iptal;
                        this.Malzemeler[i].Save();

                        if (this.Malzemeler[i].Ambalaj != null)
                        {
                            this.Malzemeler[i].Ambalaj.Guncelleyen = currentUser != null ? currentUser.KullaniciId : this.Guncelleyen;
                            this.Malzemeler[i].Ambalaj.GuncellemeTarihi = DateTime.Now;
                            this.Malzemeler[i].Ambalaj.Durum = AmbalajDurumu.Bosta;
                            this.Malzemeler[i].Ambalaj.DurumAciklama = AmbalajDurumlari.KASA_URETIMDE_KULLANILDI;
                            this.Malzemeler[i].Ambalaj.Save();
                        }
                    }
                }
                #endregion

                if (this.Uretim != null)
                {
                    this.Uretim.Durum = KayitDurumu.Iptal;
                    this.Uretim.Save();
                }

                this.Durum = KayitDurumu.Iptal;

                this.Save();

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }

        [Action(Caption = "Bilesenleri Guncelle", AutoCommit = false, ImageName = "Action_Refresh", ConfirmationMessage = "Is Emri Bilesenleri Guncellenecek Kabul Ediyor musunuz?", ToolTip = "Is Emri Bilesenlerini Guncellenecek Için")]
        public void BilesenGuncelle()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.IsEmriNo))
                {
                    ////#region Bilesenleri Sil
                    ////XPQuery<IsEmriBilesenleri> xbilesenler = new XPQuery<IsEmriBilesenleri>(Utility.Session);
                    ////List<IsEmriBilesenleri> qeryBilesen = (from x in xbilesenler
                    ////                                       where x.IsEmriNo == this.IsEmriNo
                    ////                                       select x).ToList();
                    ////foreach (IsEmriBilesenleri xbil in qeryBilesen)
                    ////{
                    ////    xbil.Delete();
                    ////}
                    ////#endregion
                    ////#region Guncelle
                    ////SecimIslemi sc = new SecimIslemi();
                    ////sc.Kullanici = Utility.Kullanici;
                    ////if (this.IsEmri != null)
                    ////    sc.Kod = this.IsEmri.IsEmriNo;
                    ////else
                    ////    sc.Kod = this.IsEmriNo;

                    ////this.SourceBilesenler = Utility.Entegre.IsEmriReceteleri(sc);
                    ////#endregion
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        [Action(Caption = "Harekete Git", AutoCommit = false, TargetObjectsCriteria = "AmbalajHareket[].Count > 0", ImageName = "ModelEditor_GoToObject", ToolTip = "Stok hareket kaydina git")]
        public void HareketeGit()
        {
            try
            {
                WebApplication application = WebApplication.Instance;
                IObjectSpace objectSpace = application.CreateObjectSpace();
                if (this.AmbalajHareket.Count > 0)
                {
                    View detailView = application.CreateDetailView(objectSpace, objectSpace.GetObject(this.AmbalajHareket[0]));
                    ShowViewParameters showViewParameters = new ShowViewParameters(detailView);
                    showViewParameters.TargetWindow = TargetWindow.Default;
                    ShowViewSource viewSource = new ShowViewSource(WebWindow.CurrentRequestWindow, null);
                    application.ShowViewStrategy.ShowView(showViewParameters, viewSource);
                }
            }
            catch (Exception) { }
        }
        
        //[Action(PredefinedCategory.Save, Caption = "Ambalaj Bul", TargetObjectsCriteria = "[Durum] = 0 And Oid= -1", ImageName = "BO_Product_Group", ToolTip = "Ambalaj bul", SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject)]
        //public void AmbalajBul()
        //{
        //    try
        //    {
        //        //if (this.Oid == -1)
        //        //{
        //        //if (UretimOrtak.UretimParametreleri.KasaTakibi)
        //        //{
        //        //    if (AmbalajTur.IsNull())
        //        //    {
        //        //        AmbalajTur = Session.GetObjectByKey<AmbalajTurleri>(UretimOrtak.UretimParametreleri.UretimYeniAmbalajTurId);

        //        //    }
        //        //}
        //        //}
        //    }
        //    catch (Exception exc)
        //    {
        //        throw exc;
        //    }
        //}
        //[Action(Caption = "Uretim Raporu", TargetObjectsCriteria = "1 = 1", ImageName = "Action_CloseAllWindows")]
        //public void UretimRapor()
        //{
        //    try
        //    {
        //        //this.Durum = KayitDurumu.Aktarilacak;


        //        //Frame.GetController<WebReportServiceController>().ShowPreview((IReportData)Object);

        //        //if (WebWindow.CurrentRequestWindow != null)
        //        //    WebWindow.CurrentRequestWindow.RegisterClientScript("js:yenile", "javascript:location.reload(true)");

        //    }
        //    catch (Exception exc)
        //    {

        //        throw exc;
        //    }
        //}
        #endregion

        #region Yerel metodlar

        #endregion

        public UretimOperasyonlari() { }
        public UretimOperasyonlari(Session session) : base(session) { }

    }
}
