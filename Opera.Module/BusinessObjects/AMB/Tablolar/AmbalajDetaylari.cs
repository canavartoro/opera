
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Xml.Serialization;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using System.Diagnostics;
using DevExpress.ExpressApp.DC;
using Mikrobar;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [DebuggerDisplay("Ambalaj = {Ambalaj.Barkod}, MalzemeId = {MalzemeId}, MalzemeKod = {MalzemeKod},  Oid = {Oid}")]
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("MalzemeKod")]
    [ImageName("BO_Sale_Item_v92"), ModelDefault("DefaultListViewShowAutoFilterRow", "True"),
   NavigationItem(false)]
    public class AmbalajDetaylari : XPObject
    {
        [NonPersistent, Description("Mobil cihazlarda kullanmak icin."), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int AmbalajId { get; set; }

        Ambalajlar fAmbalaj;
        [XmlIgnore(), Association(@"AmbalajDetaylari.Ambalaj_Ambalajlar")]
        public Ambalajlar Ambalaj
        {
            get { return fAmbalaj; }
            set { SetPropertyValue<Ambalajlar>("Ambalaj", ref fAmbalaj, value); }
        }

        #region Malzeme Bilgisi
        protected Malzemeler _malzeme;
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false), Indexed("Ambalaj")]
        public int MalzemeId { get; set; }
        [Size(DbSize.KodLenght), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(true)]
        public string MalzemeKod { get; set; }
        [XmlIgnore(), NonPersistent, XafDisplayName("Malzeme Bilgisi"),
        VisibleInListView(true), VisibleInLookupListView(false)]
        public Malzemeler Malzeme
        {
            get
            {
                if (_malzeme == null && this.MalzemeId > 0)
                    this._malzeme = this.Session.GetObjectByKey<Malzemeler>(this.MalzemeId);
                return _malzeme;
            }
            set
            {
                if (value != null)
                {
                    MalzemeId = value.MalzemeId;
                    MalzemeKod = value.MalzemeKod;
                }
                else
                {
                    MalzemeId = 0;
                    MalzemeKod = "";
                }
                SetPropertyValue("Malzeme", ref _malzeme, value);
            }
        }

        [VisibleInLookupListView(true), VisibleInDetailView(false), PersistentAlias("Iif(Malzeme.MalzemeAd is null, '', Malzeme.MalzemeAd)")]
        public string MalzemeAd { get { return Convert.ToString(EvaluateAlias("MalzemeAd")); } }
        #endregion

        #region Birim&Miktar
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int BirimId { get; set; }
        [Size(DbSize.KodLenght), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(true)]
        public string Birim { get; set; }

        protected V_MalzemeBirimleri _malzemeBirim;
        [NonPersistent, XmlIgnore(), VisibleInLookupListView(false)]
        [DataSourceCriteria("MalzemeId = '@This.MalzemeId'"), ImmediatePostData, XafDisplayName("Birim")]
        public V_MalzemeBirimleri MalzemeBirim
        {
            get
            {
                if (_malzemeBirim == null && this.BirimId > 0 && this.MalzemeId > 0)
                    this._malzemeBirim = this.Session.FindObject<V_MalzemeBirimleri>(CriteriaOperator.Parse(" MalzemeId = ? AND BirimId = ?", this.MalzemeId, this.BirimId));
                return _malzemeBirim;
            }
            set
            {
                this.BirimId = value != null ? value.BirimId : 0;
                this.Birim = value != null ? value.Birim : string.Empty;
                SetPropertyValue("MalzemeBirim", ref _malzemeBirim, value);
            }
        }

        protected decimal fMiktar = 0;
        [DbType(" DECIMAL(18,4) ")]
        public decimal Miktar
        {
            get { return fMiktar; }
            set
            {
                SetPropertyValue<decimal>("Miktar", ref fMiktar, value);
                if (!IsLoading)
                {
                    MiktarHesapla();
                }
            }
        }

        protected decimal fKalan = 0;
        [DbType(" DECIMAL(18,4) ")]
        public decimal Kalan
        {
            get { return fKalan; }
            set
            {
                SetPropertyValue<decimal>("Kalan", ref fKalan, value);
                if (!IsLoading)
                {
                    //MiktarHesapla();
                }
            }
        }

        private void MiktarHesapla()
        {
            try
            {
                V_MalzemeBirimleri HedefBirim = null;
                MalzemeBirimleri KaynakBirim = null;

                HedefBirim = this.Session.FindObject<V_MalzemeBirimleri>(CriteriaOperator.Parse(" MalzemeId=? and Miktar=? and Miktar2=? ", this.MalzemeId, 1, 1));
                KaynakBirim = this.Session.FindObject<MalzemeBirimleri>(CriteriaOperator.Parse(" Malzeme=? and BirimId=? ", this.MalzemeId, BirimId));

                if (HedefBirim != null && KaynakBirim != null)
                {
                    if (this.Birim2Id > 0 && HedefBirim.BirimId != this.Birim2Id)
                    {
                        HedefBirim = this.Session.FindObject<V_MalzemeBirimleri>(CriteriaOperator.Parse(" MalzemeId=? and BirimId=? ", this.MalzemeId, this.Birim2Id));
                        if (HedefBirim.IsNull())
                            throw new Exception(string.Format("Hedef malzeme birim kaydı bulunamadı. MalzemeId: {0}, BirimId: {1}", this.MalzemeId, this.Birim2Id));

                        Birim2 = HedefBirim.Birim;
                        Birim2Id = (int)HedefBirim.BirimId;

                        if (KaynakBirim.Miktar != KaynakBirim.Miktar2)
                        {
                            Miktar2 = (Miktar / HedefBirim.Miktar2) * KaynakBirim.Miktar2;
                            Kalan2 = (Kalan / HedefBirim.Miktar2) * KaynakBirim.Miktar2;
                        }
                        else
                        {
                            Miktar2 = (Miktar / KaynakBirim.Miktar2) * HedefBirim.Miktar2;
                            Kalan2 = (Kalan / KaynakBirim.Miktar2) * HedefBirim.Miktar2;
                        }
                    }
                    else                    
                    {
                        Birim2 = HedefBirim.Birim;
                        Birim2Id = (int)HedefBirim.BirimId;

                        if (KaynakBirim.Miktar == KaynakBirim.Miktar2)
                        {
                            Miktar2 = (Miktar / HedefBirim.Miktar2) * KaynakBirim.Miktar2;
                            Kalan2 = (Kalan / HedefBirim.Miktar2) * KaynakBirim.Miktar2;
                        }
                        else
                        {
                            Miktar2 = (Miktar / KaynakBirim.Miktar2) * HedefBirim.Miktar2;
                            Kalan2 = (Kalan / KaynakBirim.Miktar2) * HedefBirim.Miktar2;
                        }
                    }                    
                }
                else
                {
                    Birim2 = this.Birim;
                    Birim2Id = this.BirimId;
                    Miktar2 = this.Miktar;
                    Kalan2 = this.Kalan;
                }
            }
            catch (Exception exc)
            {
                System.Diagnostics.Trace.WriteLine(exc.Message);
                System.Diagnostics.Trace.WriteLine(exc.StackTrace);
            }
        }

        [ModelDefault("AllowEdit", "false"), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Birim2Id { get; set; }
        [ModelDefault("AllowEdit", "false"), Size(DbSize.KodLenght), VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Birim2 { get; set; }
        [ModelDefault("AllowEdit", "false"), DbType(" DECIMAL(18,4) "), VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public decimal Miktar2 { get; set; }

        [ModelDefault("AllowEdit", "false"), DbType(" DECIMAL(18,4) "), VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public decimal Kalan2 { get; set; }

        #endregion


        [Description("Erp deki alanlar"), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int ReferansId { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int TransferReferansDetayId { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int TransferReferansId { get; set; }

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

        public AmbalajDetaylari() { }
        public AmbalajDetaylari(Session session) : base(session) { }

        protected override void OnSaving()
        {
            
        }



        /*
        protected override void OnSaving()
        {
            if (!base.IsDeleted)
            {
                if (this.MalzemeId < 1)
                {
                    throw new Exception(string.Format(" MalzemeId olmadan detay kayıt edilemez.. {0} ", this.Ambalaj.Barkod));
                }
                if (this.BirimId < 1)
                {
                    throw new Exception(string.Format(" Birim olmadan detay kayıt edilemez.. {0} ", this.Ambalaj.Barkod));
                }
                if (string.IsNullOrEmpty(this.Birim))
                {
                    Birimler objectByKey = base.Session.GetObjectByKey<Birimler>(this.BirimId, true);
                    if (objectByKey == null)
                    {
                        throw new Exception(string.Format("Birim tanımlı değil {0}", this.BirimId));
                    }
                    this.Birim = objectByKey.Birim;
                }

                V_MalzemeBirimleri birimleri = new V_MalzemeBirimleri();
                MalzemeBirimleri birimleri2 = new MalzemeBirimleri();
                if (((this.KaynakModul == "ST005") || (this.KaynakModul == "SYM004")) || (this.KaynakModul == "ST006"))
                {
                    birimleri = base.Session.FindObject<V_MalzemeBirimleri>(CriteriaOperator.Parse(" MalzemeId=? and BirimId=? ", new object[] { this.MalzemeId, this.BirimId }));
                    birimleri2 = base.Session.FindObject<MalzemeBirimleri>(CriteriaOperator.Parse(" MalzemeId=? and Miktar=? and Miktar2=? ", new object[] { this.MalzemeId, 1, 1 }));
                }
                else
                {
                    birimleri = base.Session.FindObject<V_MalzemeBirimleri>(CriteriaOperator.Parse(" MalzemeId=? and Miktar=? and Miktar2=? ", new object[] { this.MalzemeId, 1, 1 }));
                    birimleri2 = base.Session.FindObject<MalzemeBirimleri>(CriteriaOperator.Parse(" MalzemeId=? and BirimId=? ", new object[] { this.MalzemeId, this.BirimId }));
                }

                if ((birimleri != null) && (birimleri2 != null))
                {
                    this.Birim2 = birimleri.Birim;
                    this.Birim2Id = birimleri.BirimId;
                    this.Miktar2 = (this.Miktar / birimleri.Miktar2) * birimleri2.Miktar2;
                    this.Kalan2 = (this.Kalan / birimleri.Miktar2) * birimleri2.Miktar2;
                }
                else
                {
                    this.Birim2 = this.Birim;
                    this.Birim2Id = this.BirimId;
                    this.Miktar2 = this.Miktar;
                    this.Kalan2 = this.Kalan;
                }
            }
        }
        */


    }
}