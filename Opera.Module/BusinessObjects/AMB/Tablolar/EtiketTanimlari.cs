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
    [DebuggerDisplay("MalzemeKod = {MalzemeKod}"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    [DeferredDeletion(false), OptimisticLocking(false), DefaultClassOptions, XafDefaultProperty("MalzemeKod"), NavigationItem(false), ImageName("BO_Role")]
    public class EtiketTanimlari : XPObject
    {
        Malzemeler fMalzeme;
        [XmlIgnore(), Association(@"Malzemeler.EtiketTanimlari.Malzeme"), NoForeignKey, Persistent("MalzemeId")]
        public Malzemeler Malzeme
        {
            get { return fMalzeme; }
            set { SetPropertyValue<Malzemeler>("Malzeme", ref fMalzeme, value); }
        }
        [ModelDefault("AllowEdit", "false"), ReadOnly(true), Size(DbSize.KodLenght), VisibleInLookupListView(true)]
        public string MalzemeKod { get; set; }
        [ModelDefault("AllowEdit", "false"), ReadOnly(true), Size(DbSize.AdLenght), VisibleInLookupListView(true)]
        public string MalzemeAd { get; set; }
        [Size(DbSize.AdLenght)]
        public string UrunAdi { get; set; }
        [Size(DbSize.AdLenght)]
        public string MarkaAdi { get; set; }
        public string Amper { get; set; }
        [Size(DbSize.IkiYuzElli)]
        public string Resim { get; set; }
        [Size(DbSize.AdLenght)]
        public string MadeIn { get; set; }
        [Size(DbSize.IkiYuzElli), VisibleInLookupListView(false), VisibleInListView(false)]
        public string Adres { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string Turkce { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string Rusca { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string Ingilizce { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string Hollandaca { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string Romence { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string Arapca { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string Ibranice { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string Slovence { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string Ispanyolca { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string Almanca { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string Esyonya { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string Letonya { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string Litvanya { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string Ukrayna { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string Italyanca { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string Hirvatca { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string Isvecce { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string Lethce { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string Fransizca { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string Portekizce { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string Cekce { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string Polonya { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string TurkceRenk { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string RuscaRenk { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string IngilizceRenk { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string HollandacaRenk { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string RomenceRenk { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string ArapcaRenk { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string IbraniceRenk { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string SlovenceRenk { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string IspanyolcaRenk { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string AlmancaRenk { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string EstonyaRenk { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string LetonyaRenk { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string LitvanyaRenk { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string UkraynaRenk { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string ItalyancaRenk { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string HirvatcaRenk { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string IsvetceRenk { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string LethceRenk { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string FransizcaRenk { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string PortekizceRenk { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string CekceRenk { get; set; }
        [VisibleInLookupListView(false), VisibleInListView(false)]
        public string PolonyaRenk { get; set; }

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

        public EtiketTanimlari() { }
        public EtiketTanimlari(Session session) : base(session) { }
    }
}
