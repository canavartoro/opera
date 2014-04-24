using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    /// <summary>
    /// Cari kartlari
    /// </summary>
    [DeferredDeletion(false),OptimisticLocking(false), DefaultClassOptions, XafDefaultProperty("CariKod")]
    [NavigationItem(false), ImageName("BO_Role"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    [ReferansTablo(TabloAdi = "FIND_ENTITY", SistemTipi=SistemTipi.WebErp)]
    [ReferansTablo(TabloAdi = "pub.cari_kart", SistemTipi = SistemTipi.Progress, QueryType=QueryType.Yoksa,
        SqlSorgu=@"select cast(RowId as int) as ""CariId"",cari_kod as ""CariKod"",cari_ad as ""CariAd"",kisa_ad as ""KisaAd"",bolge_kod as ""BolgeKod"",cgrup_kod3 as ""GrupKod"", adres1 as ""Adres1"",adres2 as ""Adres2"",adres3 as ""Adres3"",fat_adres1 as ""FaturaAdres1"",fat_adres2 as ""FaturaAdres2"",fat_adres3 as ""FaturaAdres3"",vergi_daire as ""VargiDaire"",vergi_no as ""VergiNo"" from Pub.cari_kart where firma_kod='#FirmaKod#' ")]
    public class Cariler : XPBaseObject
    {        
        [RuleUniqueValue("", DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "d")]
        [Key(AutoGenerate = false)]
        public int CariId { get; set; }

        [Size(DbSize.AdLenght), ModelDefault("ShowAutoFilterRow", "True"), VisibleInLookupListView(true),XafDisplayName("Cari Ad")]
        public string CariAd { get; set; }
        [Size(DbSize.KodLenght), ModelDefault("ShowAutoFilterRow", "True")/*, RuleRequiredField("Cari Kodu Boş Geçilemez!", DefaultContexts.Save)*/]
        public string CariKod { get; set; }
        [Size(DbSize.AdLenght)]
        public string KisaAd { get; set; }        
        [Size(DbSize.KodLenght)]
        public string BolgeKod { get; set; }
        [Size(DbSize.KodLenght)]
        public string GrupKod { get; set; }
        [Size(DbSize.AdresLenght)]
        public string Adres1 { get; set; }
        [Size(DbSize.AdresLenght)]
        public string Adres2 { get; set; }
        [Size(DbSize.AdresLenght)]
        public string Adres3 { get; set; }
        [Size(DbSize.AdresLenght)]
        public string FaturaAdres1 { get; set; }
        [Size(DbSize.AdresLenght)]
        public string FaturaAdres2 { get; set; }
        [Size(DbSize.AdresLenght)]
        public string FaturaAdres3 { get; set; }
        [Size(DbSize.AdresLenght)]
        public string VarigDaire { get; set; }
        [Size(DbSize.NoLenght)]
        public string VergiNo { get; set; }

        [DataObjectField(false)]
        [PersistentAlias("Iif(Len(CariKod) = 0, 0, StartsWith(CariKod, '120 00'), 1, StartsWith(CariKod, '120 50'), 2, 0)")]
        public CariTip CariTip { get { return (CariTip)Convert.ToInt32(EvaluateAlias("CariTip")); } }

        [XmlIgnore(), NoForeignKey, Association("Cariler-Operasyonlari"), VisibleInDetailView(false), VisibleInListView(false)]
        public XPCollection<UretimOperasyonlari> UretimOperasyonlari
        {
            get { return GetCollection<UretimOperasyonlari>("UretimOperasyonlari"); }
        }

        [XmlIgnore(), NoForeignKey, Association("Cariler-SevkEmirleri"), VisibleInDetailView(false), VisibleInListView(false)]
        public XPCollection<SevkEmirleri> SevkEmirleri
        {
            get { return GetCollection<SevkEmirleri>("SevkEmirleri"); }
        }

        [XmlIgnore(), NoForeignKey, Association("Cariler.TasarimGruplari.Cari"), VisibleInDetailView(false), VisibleInListView(false)]
        public XPCollection<TasarimGruplari> TasarimGruplari
        {
            get { return GetCollection<TasarimGruplari>("TasarimGruplari"); }
        }

        [XmlIgnore(), NoForeignKey, Association("Cariler.MusteriMalzemeleri.Cari"), VisibleInDetailView(true), VisibleInListView(false)]
        public XPCollection<MusteriMalzemeleri> MusteriMalzemeleri
        {
            get { return GetCollection<MusteriMalzemeleri>("MusteriMalzemeleri"); }
        }

        [XmlIgnore(), Association("TasarimGruplari.Cariler", typeof(TasarimGruplari)), NoForeignKey, VisibleInDetailView(false)]
        public XPCollection CariTasarimGruplari { get { return GetCollection("CariTasarimGruplari"); } }

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

        public Cariler() { }
        public Cariler(Session session) : base(session) { }

        protected override void OnSaving()
        {
            #region IsNotDelete
            if (this.IsDeleted == false)
            {
                if (this.CariId < 1)
                {
                    object xCariId = Session.Evaluate<Cariler>(CriteriaOperator.Parse("Max(CariId)"), null);
                    //int xCariId = Convert.ToInt32(this.Session.ExecuteScalar("SELECT MAX(CariId) + 1 FROM Cariler"));
                    this.CariId = Convert.ToInt32(xCariId) + 1;
                   
                    //XPBaseObject currentUser = DevExpress.ExpressApp.SecuritySystem.CurrentUser as XPBaseObject; 
                    //if (SecuritySystem.CurrentUser != null)
                    //{
                    //    User xUser = Session.GetObjectByKey(SecuritySystem.CurrentUser.GetType(), Session.GetKeyValue(SecuritySystem.CurrentUser)) as User;
                    //    if (xUser != null)
                    //        this.Olusturan = xUser.Oid;
                    //}
 

                    this.OlusturmaTarihi = DateTime.Now;

                }
                else
                {
                    this.GuncellemeTarihi = DateTime.Now;
                }
            }
            #endregion
            base.OnSaving();
        }        
        public static string Get_DisplayName(Cariler instance)
        {
            return String.Format("{0} - {1} - {2}", instance.CariId, instance.CariKod, instance.CariAd);
        }

    }
}
