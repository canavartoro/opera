using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Xml.Serialization;
using DevExpress.Persistent.Base;
using System.Diagnostics;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [DebuggerDisplay("MalzemeId = {MalzemeId}, MalzemeKod = {MalzemeKod}"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    [DeferredDeletion(false), OptimisticLocking(false), DefaultClassOptions, XafDefaultProperty("MalzemeKod"), NavigationItem(false), ImageName("BO_Role")]
    public class SertifikaTanimlari : XPObject
    {
        [XmlIgnore(), Persistent("RenkId"), Association(@"Renkler.SertifikaTanimlari.RenkId"), NoForeignKey]
        public Renkler Renk { get; set; }
        [NonPersistent, VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int MalzemeId { get; set; }
        [ModelDefault("AllowEdit", "false"), ReadOnly(true),Size(DbSize.KodLenght), VisibleInLookupListView(true)]
        public string MalzemeKod { get; set; }
        public bool Tse { get; set; }
        public bool Ce { get; set; }
        public bool Pct { get; set; }
        public bool Esc { get; set; }
        public bool Ww { get; set; }
        public bool Cb { get; set; }

        Malzemeler fMalzeme;
        [XmlIgnore(), Association(@"Malzemeler.SertifikaTanimlari.MalzemeId"), NoForeignKey, Persistent("MalzemeId")]
        public Malzemeler Malzeme
        {
            get { return fMalzeme; }
            set { SetPropertyValue<Malzemeler>("Malzeme", ref fMalzeme, value); }
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

        protected override void OnSaving()
        {
            
        }

        public SertifikaTanimlari() { }
        public SertifikaTanimlari(Session session) : base(session) { }
    }
}
