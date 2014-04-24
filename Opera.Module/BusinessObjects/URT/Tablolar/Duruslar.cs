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
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, 
    XafDefaultProperty("DurusKod"), NavigationItem(false),ImageName("BO_Organization")]
    [ReferansTablo(TabloAdi = "PRDD_BREAK_REASON", SistemTipi= SistemTipi.WebErp),
     ReferansTablo(TabloAdi = "erp_durustip", SistemTipi = SistemTipi.Progress)]
    [ReferansTablo("UAKARIZASABIT", SqlSorgu = "SELECT * FROM Barset.V_Duruslar WITH (NOLOCK) WHERE ( 1 = 1 ) ", SqlWhere = " AND DurusId = {0} ",SistemTipi=SistemTipi.Netsis,QueryType=QueryType.Yoksa)]
    public class Duruslar : XPBaseObject
    {
        [Key(AutoGenerate=false)]
        public int DurusId { get; set; }

        [Size(DbSize.KodLenght), Indexed, VisibleInLookupListView(true)]
        public string DurusKod { get; set; }
        [Size(DbSize.AdLenght), VisibleInLookupListView(true)]
        public string DurusAd { get; set; }
        [Size(DbSize.AciklamaLenght), VisibleInLookupListView(true)]
        public string DurusAciklama { get; set; }
        [VisibleInLookupListView(true)]
        public bool IsEmriBaglanti { get; set; }
        [Size(DbSize.KodLenght), VisibleInLookupListView(true)]
        public string DurusTip { get; set; }
        [VisibleInLookupListView(true)]
        public bool Planli { get; set; }

        [XmlIgnore(), Association(@"UretimDuruslari-Durus", typeof(UretimDuruslari)), NoForeignKey, VisibleInDetailView(false)]
        public XPCollection<UretimDuruslari> UretimDuruslari
        {
            get
            {
                return GetCollection<UretimDuruslari>("UretimDuruslari");
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
                if (!IsLoading && !IsSaving)
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
            
        }
        #endregion

        public Duruslar() { }
        public Duruslar(Session session) : base(session) { }
    }
}
