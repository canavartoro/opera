using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Xml.Serialization;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("AletKod"),
    NavigationItem(false), ImageName("BO_Organization"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    [ReferansTablo("pub.platform_master", SistemTipi.Progress, false)]
    public class Aletler : XPBaseObject
    {
        [ReferansAlan("platform_master", "platform_masterno"), Description("Sistemdeki master no")]
        [Key(AutoGenerate = false)]
        public int AletId { get; set; }

        [Size(DbSize.KodLenght)]
        public string GrupKod { get; set; }

        public int TipId { get; set; }

        [Size(DbSize.KodLenght)]
        public string TipKod { get; set; }

        [ReferansAlan("platform_master", "platform_kod")]
        [Indexed, Size(DbSize.KodLenght)]
        public string AletKod { get; set; }

        [Size(DbSize.AdLenght)]
        [ReferansAlan("platform_master", "platform_ad")]
        public string AletAd { get; set; }

        [Size(DbSize.AciklamaLenght)]
        [ReferansAlan("platform_master", "aciklama")]
        public string Aciklama { get; set; }

        [Size(DbSize.KodLenght)]
        public string DemirBasKod { get; set; }
        
        [XmlIgnore(), Association("Depolar.Aletler_DepoId"), Persistent("DepoId"), NoForeignKey]
        public Depolar Depo { get; set; }
        
        [XmlIgnore(), Association("Raflar.Aletler_AletId"), Persistent("RafId")]
        public Raflar Raf { get; set; }
        
        [NonPersistent]
        public string DepoKod { get; set; }
        [NonPersistent]
        public string RafKod { get; set; }

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

        [XmlIgnore(), Association("Aletler-Alet"), NoForeignKey, VisibleInListView(false), VisibleInDetailView(false)]
        public XPCollection<UretimAletleri> Uretimler
        {
            get { return GetCollection<UretimAletleri>("Uretimler"); }
        }

        #region Butonlar
        [Action(Caption = "Guncelle", ImageName = "Action_Refresh", ToolTip = "Bilgileri guncelle..")]
        public void Entegrasyon()
        {
            
        }
        #endregion

        public Aletler() { }
        public Aletler(Session session) : base(session) { }
    }
}
