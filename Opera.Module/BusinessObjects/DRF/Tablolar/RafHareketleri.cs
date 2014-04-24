using System;
using System.Collections.Generic;
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
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [ListViewFilter("Tum Belgeler", "")]
    [ListViewFilter("Acik Belgeler", "[Durum] = 0")]
    [ListViewFilter("Bugunku Belgeler", "[BelgeTarihi] >= LocalDateTimeToday()")]
    [ListViewFilter("Kapali Belgeler", "[Durum] = 4")]
    [ListViewFilter("Iptal Edilen Belgeler", "[Durum] = 2")]
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("IrsaliyeNo"), NavigationItem(false),
     ImageName("BO_Sale_Item_v92"), ModelDefault("DefaultListViewShowAutoFilterRow", "True"), ModelDefault("IsClonable", "True")]
    public class RafHareketleri : XPObject
    {
        public HareketTipi HareketTip { get; set; }

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

        #region Belge Bilgisi
        [Size(DbSize.NoLenght), SearchMemberOptions(SearchMemberMode.Include)]
        public string IrsaliyeNo { get; set; }
        [Size(DbSize.NoLenght), SearchMemberOptions(SearchMemberMode.Include)]
        public string FisNo { get; set; }
        [Size(DbSize.NoLenght), SearchMemberOptions(SearchMemberMode.Include)]
        public string BelgeNo { get; set; }

        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}")]//, ValueConverter(typeof(DateFieldConverter))]
        public DateTime BelgeTarihi { get; set; }
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}")]//, ValueConverter(typeof(DateFieldConverter))]
        public DateTime IrsaliyeTarihi { get; set; }
        #endregion

        [Description("Mal hazirlama emri id"), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int MalToplamaId { get; set; }

        [Description("Ithalat dosya id"), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int SiparisId { get; set; }
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
        public int MalTalepDetayId { get; set; }

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

        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int ReferansId { get; set; }
        [NonPersistent, Description("Web servise giden obje Oid tasimasi icin"),VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Oid2 { get; set; }

        [Association(@"RafHareketDetaylari.RafHareketleri_StokHareketleri")]
        public XPCollection<RafHareketDetaylari> RafHareketDetaylari
        {
            get { return GetCollection<RafHareketDetaylari>(@"RafHareketDetaylari"); }
           
        }

        [Association(@"AmbalajHareketleri.OID_RafHareketleri"), VisibleInListView(false)]
        public XPCollection<AmbalajHareketleri> AmbalajHareketleri
        {
            get { return GetCollection<AmbalajHareketleri>(@"AmbalajHareketleri"); }   
        }

        #region Butonlar
        [Action(Caption = "Belge Iptal", TargetObjectsCriteria = "[Durum] = 0", ImageName = "Action_CloseAllWindows", ConfirmationMessage = "Belge İptal Edilsin mi?", ToolTip = "Belgeyi İptal Etmek Için", SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject)]
        public void BelgeIptal()
        {
            try
            {
                if (this.Durum != KayitDurumu.Yeni)
                    throw new Exception("Belge kapalı iptal edilemez!");

                SistemKullanicilari currentUser = SecuritySystem.CurrentUser as SistemKullanicilari;
                if (currentUser == null)
                    throw new Exception("Bu işlem için yetkiniz yok!");

                #region Stok Hareket Detaylari

                for (int i = 0; i < this.RafHareketDetaylari.Count; i++)
                {
                    if (currentUser != null)
                    {
                        this.RafHareketDetaylari[i].Guncelleyen = currentUser.KullaniciId;
                        this.RafHareketDetaylari[i].GuncellemeTarihi = DateTime.Now;
                    }
                    this.RafHareketDetaylari[i].Durum = KayitDurumu.Iptal;
                    this.RafHareketDetaylari[i].Save();
                }

                #endregion

                #region Ambalaj Hareketleri

                for (int h = 0; h < this.AmbalajHareketleri.Count; h++)
                {
                    for (int d = 0; d < this.AmbalajHareketleri[h].AmbalajHareketDetaylari.Count; d++)
                    {
                        if (currentUser != null)
                        {
                            this.AmbalajHareketleri[h].AmbalajHareketDetaylari[d].Guncelleyen = currentUser.KullaniciId;
                            this.AmbalajHareketleri[h].AmbalajHareketDetaylari[d].GuncellemeTarihi = DateTime.Now;
                        }
                        this.AmbalajHareketleri[h].AmbalajHareketDetaylari[d].Durum = KayitDurumu.Iptal;
                        this.AmbalajHareketleri[h].AmbalajHareketDetaylari[d].Save();
                    }
                    if (currentUser != null)
                    {
                        this.AmbalajHareketleri[h].Guncelleyen = currentUser.KullaniciId;
                        this.AmbalajHareketleri[h].GuncellemeTarihi = DateTime.Now;
                    }
                    this.AmbalajHareketleri[h].Aciklama4 = "Belge iptal edildi.";
                    this.AmbalajHareketleri[h].Durum = KayitDurumu.Iptal;
                    this.AmbalajHareketleri[h].Save();
                }

                #endregion

                if (currentUser != null)
                {
                    this.Guncelleyen = currentUser.KullaniciId;
                    this.GuncellemeTarihi = DateTime.Now;
                }
                this.Aciklama4 = "Belge iptal edildi.";
                this.Durum = KayitDurumu.Iptal;

                this.Save();

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        #endregion       

        protected override void OnSaving()
        {            

        }

        public RafHareketleri() { }
        public RafHareketleri(Session session) : base(session) { }

    }
}
