using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Xml.Serialization;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using System.ComponentModel;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    /// <summary>
    /// INVD_COLOR_LANG renk dil bilgisi
    /// </summary>
    [ReferansTablo("INVD_COLOR",SistemTipi.WebErp, true)]
    [DeferredDeletion(false), OptimisticLocking(false), DefaultClassOptions, XafDefaultProperty("RenkKod"),
    NavigationItem(false), ImageName("BO_Role"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    public class Renkler : XPBaseObject
    {
        [Key(AutoGenerate = false)]
        public int RenkId { get; set; }
        [Size(DbSize.NoLenght)]
        public string RenkKod { get; set; }
        [Size(DbSize.AciklamaLenght)]
        public string Aciklama { get; set; }
        public bool Pasif { get; set; }

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

        [XmlIgnore(), Association(@"Renkler.SertifikaTanimlari.RenkId"), NoForeignKey, VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public XPCollection<SertifikaTanimlari> SertifikaTanimlari
        {
            get { return GetCollection<SertifikaTanimlari>(@"SertifikaTanimlari"); }
        }

        //[XmlIgnore(), Association(@"Renkler-IsEmirleri.RenkId"), VisibleInListView(false)]
        //public XPCollection<IsEmirleri> IsEmirleri
        //{
        //    get { return GetCollection<IsEmirleri>(@"IsEmirleri"); }
        //}

        //[XmlIgnore(), Association(@"Renkler-Ambalajlar.RenkId"), VisibleInListView(false)]
        //public XPCollection<Ambalajlar> Ambalajlar
        //{
        //    get { return GetCollection<Ambalajlar>(@"Ambalajlar"); }
        //}

        #region Butonlar
        [Action(Caption = "Guncelle", ImageName = "Action_Refresh", ToolTip = "Bilgileri guncelle..")]
        public void Entegrasyon()
        {
            ////try
            ////{
            ////    Mikrobar.Entegre.DataQuery query = new Mikrobar.Entegre.DataQuery(this.GetType());
            ////    object ret = query.Entegre();

            ////    //DevExpress.Xpo.DB.SelectedData data = this.Session.ExecuteSproc("sp_CariGuncelleERP");
            ////    if (WebWindow.CurrentRequestWindow != null)
            ////        WebWindow.CurrentRequestWindow.RegisterClientScript("tmm" + this.GetType().Name, "alert('İşlem tamamlandı. Sonuc:" + query.HataMesaji + "');");
            ////}
            ////catch (Exception exc)
            ////{
            ////    throw exc;
            ////}
        }
        #endregion

        protected override void OnSaving()
        {
            try
            {
                if (this.IsDeleted == false)
                {
                    SistemKullanicilari currentUser = SecuritySystem.CurrentUser as SistemKullanicilari;
                    if (string.IsNullOrEmpty(this.RenkKod)) throw new Exception("Renk kodu bos birakilamaz!");
                    if (this.RenkId < 1)
                    {
                        object xRenkId = Session.Evaluate<Renkler>(CriteriaOperator.Parse("Max(RenkId)"), null);
                        this.RenkId = Convert.ToInt32(xRenkId) + 1;
                        this.KaynakProgram = BusinessObjects.KaynakProgram.EtiketBasim;
                        this.KaynakModul = GetType().Name;
                        this.Olusturan = currentUser != null ? currentUser.KullaniciId : 0;
                        this.OlusturmaTarihi = DateTime.Now;
                    }
                    else
                    {                        
                        this.Guncelleyen = currentUser != null ? currentUser.KullaniciId : 0;
                        this.GuncellemeTarihi = DateTime.Now;
                    }
                }
                base.OnSaving();
            }
            catch (Exception exc)
            {
            }
        }

        public Renkler() { }
        public Renkler(Session session) : base(session) { }
    }
}
