using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.Xml.Serialization;
using System.Diagnostics;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [OptimisticLocking(false), DeferredDeletion(false)]
    [ReferansTablo("pub.cari_stokkodu", SistemTipi.Progress, SqlSorgu = " select cast(m.rowid as int) as \"MusteriMalzemeId\", cast(c.rowid as int) as \"Cari\", cast(s.rowid as int) as \"Malzeme\", s.stok_kod as \"MalzemeKod\", m.mstok_kod as \"MusteriKod\", m.stok_ad as \"MusteriAd\" from pub.cari_stokkodu m, pub.cari_kart c, pub.stok_kart s where m.firma_kod = '#FirmaKod#' and m.firma_kod = c.firma_kod and m.firma_kod = s.firma_kod and m.cari_kod = c.cari_kod and m.stok_kod = s.stok_kod",QueryType=QueryType.Yoksa)]
    [DebuggerDisplay("MusteriMalzemeId = {MusteriMalzemeId}, MalzemeKod = {MalzemeKod}, MusteriKod = {MusteriKod}")]
    [ModelDefault("DefaultListViewShowAutoFilterRow", "True"), DefaultClassOptions, XafDefaultProperty("MalzemeKod"), NavigationItem(false), ImageName("BO_Role")]
    public class MusteriMalzemeleri : XPBaseObject
    {
        [ModelDefault("DisplayFormat", "d"), Key(AutoGenerate = false)]
        public int MusteriMalzemeId { get; set; }

        #region Malzeme Bilgisi
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false), PersistentAlias("Iif(Malzeme is null, 0, Malzeme.MalzemeId)")]
        public int MalzemeId { get { return Convert.ToInt32(EvaluateAlias("MalzemeId")); } }

        [ModelDefault("AllowEdit", "false"), ReadOnly(true), Size(DbSize.KodLenght)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string MalzemeKod { get; set; }

        Malzemeler fMalzeme;
        [Indexed("Cari", Unique = false)]
        [XmlIgnore(), Association(@"Malzemeler.MusteriMalzemeleri.MalzemeId"), NoForeignKey, Persistent("MalzemeId")]
        public Malzemeler Malzeme
        {
            get { return fMalzeme; }
            set { SetPropertyValue<Malzemeler>("Malzeme", ref fMalzeme, value); }
        }

        [PersistentAlias("Iif(Malzeme is null, '', Malzeme.MalzemeAd)"), XmlIgnore()]
        public string MalzemeAd { get { return Convert.ToString(EvaluateAlias("MalzemeAd")); } }

        [Size(DbSize.KodLenght)]
        public string MusteriKod { get; set; }

        [Size(DbSize.KodLenght)]
        public string MusteriAd { get; set; }
        #endregion

        #region Cariler

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false), PersistentAlias("Iif(Cari is null, 0, Cari.CariId)")]
        public int CariId { get { return Convert.ToInt32(EvaluateAlias("Cari")); } }

        protected Cariler _cari;
        [XmlIgnore(), XafDisplayName("Cari Kodu"), ImmediatePostData,
        VisibleInListView(false), VisibleInLookupListView(false)]
        [Association(@"Cariler.MusteriMalzemeleri.Cari"), Persistent("CariId"), NoForeignKey]
        //[ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxSearchEditButtonPropertyEditor")]
        public Cariler Cari
        {
            get
            {
                return _cari;
            }
            set
            {
                SetPropertyValue("Cari", ref _cari, value);
            }
        }

        [PersistentAlias("Iif(Cari is null, '', Cari.CariAd)"), XmlIgnore()]
        [VisibleInDetailView(false),VisibleInListView(false),VisibleInLookupListView(false)]
        public string CariAd { get { return Convert.ToString(EvaluateAlias("CariAd")); } }

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

        public MusteriMalzemeleri() { }
        public MusteriMalzemeleri(Session session) : base(session) { }
    }
}

/*
 select top 5 cast(m.rowid as int) as "MusteriMalzemeId", cast(c.rowid as int) as "CariId", cast(s.rowid as int) as "Malzeme", m.mstok_kod as "MusteriKod", m.stok_ad as "MusteriAd" 
from pub.cari_stokkodu m, pub.cari_kart c, pub.stok_kart s where m.firma_kod = 'ARMA2011' and m.firma_kod = c.firma_kod and m.firma_kod = s.firma_kod and m.cari_kod = c.cari_kod and m.stok_kod = s.stok_kod
 */