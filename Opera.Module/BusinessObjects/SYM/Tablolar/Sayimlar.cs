using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Xml.Serialization;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using System.Diagnostics;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{

    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions,
    DebuggerDisplay("Oid = {Oid}"), XafDefaultProperty("ReferansNo")]
    [ImageName("BO_Sale_Item_v92"), ModelDefault("DefaultListViewShowAutoFilterRow", "True"), NavigationItem(false)]
    public class Sayimlar : XPObject
    {
        #region Tablo Alanları

        SayimEmirleri fSayimEmri;
        [XmlIgnore()]
        [Association(@"Sayimlar.SayimE_SayimEmirleri")]
        public SayimEmirleri SayimEmri
        {
            get { return fSayimEmri; }
            set { SetPropertyValue<SayimEmirleri>("SayimE", ref fSayimEmri, value); }
        }

        [Association(@"SayimDetaylari.SayimD_Sayimlar"), XmlIgnore()]
        public XPCollection<SayimDetaylari> SayimDetay
        {
            get { return GetCollection<SayimDetaylari>(@"SayimDetay"); }
        }

        [DbType(" DECIMAL(18,0) "), ModelDefault("DisplayFormat", "{0:0}")]
        public decimal ReferansNo { get; set; }

        #region Depo&Raf
        [ModelDefault("AllowEdit", "False"), PersistentAlias("Iif(Depo is null, 0, Depo.DepoId)"), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int DepoId
        {
            get
            {
                return Convert.ToInt32(EvaluateAlias("DepoId"));
            }
            set
            {
                SetPropertyValue("Depo", ref _depo, Session.GetObjectByKey<Depolar>(value));
                OnChanged("Depo");
            }
        }

        protected Depolar _depo;
        [XmlIgnore(), XafDisplayName("Depo Kodu"), ImmediatePostData, Association(@"Sayimlar.Depo.DepoId"), Persistent("DepoId"), NoForeignKey]
        public Depolar Depo
        {
            get
            {
                return _depo;
            }
            set
            {
                SetPropertyValue("Depo", ref _depo, value);
            }
        }

        [VisibleInLookupListView(true), VisibleInDetailView(true), PersistentAlias("Iif(Depo is null, '', Depo.DepoAd)")]
        public string DepoAd { get { return Convert.ToString(EvaluateAlias("DepoAd")); } }

        [ModelDefault("AllowEdit", "False"), PersistentAlias("Iif(Raf is null, 0, Raf.RafId)"), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int RafId
        {
            get
            {
                return Convert.ToInt32(EvaluateAlias("RafId"));
            }
            set
            {
                if (!IsLoading && !IsSaving)
                {
                    SetPropertyValue("Raf", ref _raf, Session.GetObjectByKey<Raflar>(value));
                }
            }
        }

        protected Raflar _raf;
        [XmlIgnore(), XafDisplayName("Raf Kodu"), ImmediatePostData, Association(@"Sayimlar.Raf.RafId"), Persistent("RafId"), NoForeignKey]
        public Raflar Raf
        {
            get
            {
                return _raf;
            }
            set
            {
                SetPropertyValue("Raf", ref _raf, value);
            }
        }

        [VisibleInLookupListView(true), VisibleInDetailView(true), PersistentAlias("Iif(Raf is null, '', Raf.Aciklama)")]
        public string RafAciklama { get { return Convert.ToString(EvaluateAlias("RafAciklama")); } }
        #endregion

        public SayimIslemTip IslemTip { get; set; }
        [ModelDefault("AllowEdit", "false"), ReadOnly(true), XmlIgnore()]
        public string DurumAciklama { get; set; }

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

        [Size(DbSize.AciklamaLenght), XmlIgnore()]
        public string Aciklama { get; set; }
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

        [NonPersistent]
        private KayitDurumu _Durum;
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public KayitDurumu Durum
        {
            get { return _Durum; }
            set
            {
                this._Durum = value;

                if (this.Durum == KayitDurumu.Iptal)
                {
                    this.DurumAciklama = SayimDurumlari.IptalSayimBelgesi;

                    if (SayimDetay.IsLoaded && SayimDetay.Count > 0)
                    {
                        for (int i = 0; i < SayimDetay.Count; i++)
                        {
                            SayimDetay[i].Durum = KayitDurumu.Iptal;
                            SayimDetay[i].DurumAciklama = SayimDurumlari.TopluIptalDetayi;
                            SayimDetay[i].Save();
                        }
                    }
                }
                else if (this.Durum == KayitDurumu.Yeni)
                {
                    this.DurumAciklama = SayimDurumlari.YeniSayimBelgesi;

                    if (SayimDetay.IsLoaded && SayimDetay.Count > 0)
                    {
                        for (int i = 0; i < SayimDetay.Count; i++)
                        {
                            SayimDetay[i].Durum = KayitDurumu.Yeni;
                            SayimDetay[i].DurumAciklama = SayimDurumlari.YeniSayimDetayi;
                            SayimDetay[i].Save();
                        }
                    }
                }
                else if (this.Durum == KayitDurumu.Tamamlandi)
                {
                    this.DurumAciklama = SayimDurumlari.TamamlananSayimBelgesi;

                    if (SayimDetay.IsLoaded && SayimDetay.Count > 0)
                    {
                        for (int i = 0; i < SayimDetay.Count; i++)
                        {
                            SayimDetay[i].Durum = KayitDurumu.Tamamlandi;
                            SayimDetay[i].DurumAciklama = SayimDurumlari.TamamlananSayimDetayi;
                            SayimDetay[i].Save();
                        }
                    }
                }
            }
        }
        #endregion

        #region Button

        [Action(PredefinedCategory.View, Caption = "Sayım Fark Rapor", ImageName = "BO_PivotChart", ToolTip = "Sayım Fark Rapor")]
        public void SayimFarkRaror()
        {
            try
            {
                SistemKullanicilari currentUser = SecuritySystem.CurrentUser as SistemKullanicilari;
                if (currentUser == null)
                    throw new Exception("Bu işlem için yetkiniz yok!");

                if (WebWindow.CurrentRequestWindow != null)
                    WebWindow.CurrentRequestWindow.RegisterClientScript("FarkRapor" + this.GetType().Name, "window.open ('" + @"Apps/SayimFark.aspx', 'Sayım Fark Rapor','status=1,toolbar=1,scrollbars=1,width=800,height=600');");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public Sayimlar() { }
        public Sayimlar(Session session) : base(session) { }

        #region Event

        protected override void OnSaving()
        {
            if (this.IsDeleted == false)
            {
                SistemKullanicilari currentUser = SecuritySystem.CurrentUser as SistemKullanicilari;

                if (this.Oid < 1)
                {
                    if (this.SayimEmri == null)
                        throw new Exception("Yeni Bir Belge İçin Sayım Emri Girilmesi Zorunludur.");

                    if (this.Olusturan < 1)
                        this.Olusturan = currentUser != null ? currentUser.KullaniciId : 0;

                    this.OlusturmaTarihi = DateTime.Now;
                    this.KaynakProgram = KaynakProgram.Sayim;
                    this.KaynakModul = GetType().Name;
                }
                else
                {
                    if (this.Guncelleyen < 1)
                        this.Guncelleyen = currentUser != null ? currentUser.KullaniciId : 0;

                    this.GuncellemeTarihi = DateTime.Now;
                }
            }
        }
        protected override void OnDeleted()
        {
            if (this.IsDeleted)
            {
                if (SayimDetay.Count > 0)
                {
                    for (int i = 0; i < SayimDetay.Count; )
                    {
                        SayimDetay[i].Delete();
                    }
                }
            }

            base.OnDeleted();
        }

        #endregion
    }
}
