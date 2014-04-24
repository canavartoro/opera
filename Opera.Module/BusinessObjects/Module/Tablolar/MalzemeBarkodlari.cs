using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Xml.Serialization;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Web;
using System.Diagnostics;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [DeferredDeletion(false), OptimisticLocking(false), DefaultClassOptions, XafDefaultProperty("MalzemeBarkodId"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    [NavigationItem(false), ImageName("BO_Role"), DebuggerDisplay("MalzemeId = {MalzemeId}, MalzemeKod = {MalzemeKod}, BirimId = {BirimId}, Birim = {Birim}")]
    [ReferansTablo(TabloAdi = "INVD_ITEM_BARCODE", SistemTipi = SistemTipi.WebErp),
     ReferansTablo("pub.stok_barkod",SistemTipi.Progress, false)]
    public class MalzemeBarkodlari : XPBaseObject
    {
        [ModelDefault("DisplayFormat", "d"), Key(AutoGenerate = false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int MalzemeBarkodId { get; set; }

        #region Malzeme Bilgisi
        //protected Malzemeler _malzeme;
        [Indexed, VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false), PersistentAlias("Iif(Malzeme is null, 0, Malzeme.MalzemeId)")]
        public int MalzemeId 
        {
            get { return Convert.ToInt32(EvaluateAlias("MalzemeId")); }
        }

        [ModelDefault("AllowEdit", "false"), ReadOnly(true), Size(DbSize.KodLenght), VisibleInLookupListView(true)]
        public string MalzemeKod { get; set; }
        //[XmlIgnore(), NonPersistent, XafDisplayName("Malzeme Bilgisi"),
        //VisibleInListView(false), VisibleInLookupListView(false)]
        //public Malzemeler Malzeme
        //{
        //    get
        //    {
        //        if (_malzeme == null && this.MalzemeId > 0)
        //            this._malzeme = this.Session.GetObjectByKey<Malzemeler>(this.MalzemeId);
        //        return _malzeme;
        //    }
        //    set
        //    {
        //        SetPropertyValue("Malzeme", ref _malzeme, value);
        //    }
        //}
        Malzemeler fMalzeme;
        [XmlIgnore(), Association(@"Malzemeler.MalzemeBarkodlari.MalzemeId"), NoForeignKey, Persistent("MalzemeId")]
        public Malzemeler Malzeme
        {
            get { return fMalzeme; }
            set { SetPropertyValue<Malzemeler>("Malzeme", ref fMalzeme, value); }
        }
        #endregion

        [Size(DbSize.BarKodLenght)]
        public string Barkod { get; set; }

        [Persistent("BirimId"), Association("MalzemeBarkodlari-Birimler")]
        public Birimler Birim { get; set; }

        [ModelDefault("AllowEdit", "false"), NonPersistent, VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int BirimId { get; set; }
        [DbType(" DECIMAL(18,4) ")]
        public decimal Miktar { get; set; }
        public int SiraNo { get; set; }

        [Size(DbSize.AciklamaLenght)]
        public string Aciklama { get; set; }

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

        #region Butonlar
        [Action(Caption = "Guncelle", ImageName = "Action_Refresh", ToolTip = "Bilgileri guncelle..")]
        public void Entegrasyon()
        {
            //try
            //{
            //    Mikrobar.Entegre.DataQuery query = new Mikrobar.Entegre.DataQuery(this.GetType());
            //    object ret = query.Entegre();

            //    //DevExpress.Xpo.DB.SelectedData data = this.Session.ExecuteSproc("sp_CariGuncelleERP");
            //    if (WebWindow.CurrentRequestWindow != null)
            //        WebWindow.CurrentRequestWindow.RegisterClientScript("tmm" + this.GetType().Name, "alert('İşlem tamamlandı. Sonuc:" + query.HataMesaji + "');");
            //}
            //catch (Exception exc)
            //{
            //    throw exc;
            //}
        }
        #endregion

        protected override void OnSaving()
        {
            if (this.IsDeleted == false)
            {
                SistemKullanicilari currentUser = SecuritySystem.CurrentUser as SistemKullanicilari;
                if (object.ReferenceEquals(null, this.Malzeme)) throw new Exception("Malzeme bilgisi bos birakilamaz!");
                if (object.ReferenceEquals(null, this.Birim)) throw new Exception("Malzeme birimi zorunludur!");
                this.MalzemeKod = this.Malzeme.MalzemeKod;
                if (this.MalzemeBarkodId < 1)
                {
                    object countSira = Session.Evaluate<MalzemeBarkodlari>(CriteriaOperator.Parse("Count()"), CriteriaOperator.Parse(" Malzeme = ? ", this.Malzeme));
                    if (object.ReferenceEquals(countSira, null) == false)
                        this.SiraNo = Convert.ToInt32(countSira) + 1;
                    object xMalzemeBirimId = Session.Evaluate<MalzemeBarkodlari>(CriteriaOperator.Parse("Max(MalzemeBarkodId)"), null);
                    this.MalzemeBarkodId = Convert.ToInt32(xMalzemeBirimId) + 1;
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

        public MalzemeBarkodlari() { }
        public MalzemeBarkodlari(Session session) : base(session) { }

    }
}