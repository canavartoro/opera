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
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("Birim"),
    ReferansTablo("pub.birim", SistemTipi.Progress, false), NavigationItem(false), ImageName("BO_Role")]
    [ReferansTablo(TabloAdi = "INVD_UNIT", SistemTipi = SistemTipi.WebErp), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    public class Birimler : XPBaseObject
    {
        [Key(AutoGenerate=false)]
        public int BirimId { get; set; }
        [Size(DbSize.KodLenght),Indexed]
        public string Birim { get; set; }
        [Size(DbSize.AciklamaLenght)]
        public string Aciklama { get; set; }
        public bool AnaBirim { get; set; }

        [DefaultValue(4)]
        public int Hassasiyet { get; set; }

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

        [Association(@"MalzemeBarkodlari-Birimler"), XmlIgnore(), VisibleInListView(false)]
        public XPCollection<MalzemeBarkodlari> Barkodlar
        {
            get { return GetCollection<MalzemeBarkodlari>(@"Barkodlar"); }
        }

        #region Butonlar
        [Action(Caption = "Guncelle", ImageName = "Action_Refresh", ToolTip = "Bilgileri guncelle..")]
        public void Entegrasyon()
        {
            
        }
        #endregion

        //[XmlIgnore()]
        //[Association(@"MalzemeBirimleri.Brm_Birimler"), VisibleInListView(false)]
        //public XPCollection<MalzemeBirimleri> MalzemeBirimleri
        //{
        //    get { return GetCollection<MalzemeBirimleri>(@"MalzemeBirimleri"); }
        //}

        public Birimler() { }
        public Birimler(Session session) : base(session) { }

    }
}
