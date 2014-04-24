using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Xml.Serialization;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using System.Diagnostics;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("Oid")]
    [ImageName("BO_Sale_Item_v92"), ModelDefault("DefaultListViewShowAutoFilterRow", "True"),
   NavigationItem(false)]
    [DebuggerDisplay("Sayim = {Sayim}")]
    public class SayimDetaylari : XPObject
    {

        Sayimlar fSayimlar;
        [XmlIgnore(), Association(@"SayimDetaylari.SayimD_Sayimlar")]
        public Sayimlar Sayim
        {
            get { return fSayimlar; }
            set { SetPropertyValue<Sayimlar>("SayimD", ref fSayimlar, value); }
        }
        public int SayimEmriId { get; set; }

        [DbType(" DECIMAL(18,0) "), ModelDefault("DisplayFormat", "{0:0}")]
        public decimal ReferansNo { get; set; }

        #region Ambalaj Bilgisi

        protected Ambalajlar _ambalaj;

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false), Indexed(Unique = false)]
        public int AmbalajId { get; set; }

        [Size(DbSize.KodLenght), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(true), Indexed(Unique = false)]
        public string Barkod { get; set; }

        [XmlIgnore(), NonPersistent, XafDisplayName("Ambalaj Bilgisi"),
        VisibleInListView(true), VisibleInLookupListView(false), DataSourceCriteria("Oid = '@This.AmbalajId'")]
        public Ambalajlar Ambalaj
        {
            get
            {
                if (_ambalaj == null && this.AmbalajId > 0)
                    this._ambalaj = this.Session.GetObjectByKey<Ambalajlar>(this.AmbalajId);
                return _ambalaj;
            }
            set
            {
                SetPropertyValue("Ambalaj", ref _ambalaj, value);
            }
        }

        public string PartiNo { get; set; }

        #endregion
        #region Malzeme Bilgisi

        protected Malzemeler _malzeme;

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int MalzemeId { get; set; }

        [Size(DbSize.KodLenght), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(true), Indexed(Unique = false)]
        public string MalzemeKod { get; set; }

        [XmlIgnore(), NonPersistent, XafDisplayName("Malzeme Bilgisi"),
        VisibleInListView(true), VisibleInLookupListView(false), DataSourceCriteria("MalzemeId = '@This.MalzemeId'")]
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
                SetPropertyValue("Malzeme", ref _malzeme, value);
            }
        }

        [NonPersistent, ModelDefault("AllowEdit", "False"), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        public string MalzemeAd
        {
            get
            {
                if (Malzeme != null)
                    return this.Malzeme.MalzemeAd;

                return "";
            }
        }

        #endregion
        #region Birim & Miktar

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

        [DbType(" DECIMAL(18,4) "), ModelDefault("DisplayFormat", "{0:0}")]
        public decimal OkunanMiktar { get; set; }

        [DbType(" DECIMAL(18,4) "), ModelDefault("DisplayFormat", "{0:0}")]
        public decimal Miktar { get; set; }

        [ModelDefault("AllowEdit", "false"), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Birim2Id { get; set; }

        [ModelDefault("AllowEdit", "false"), Size(DbSize.KodLenght), VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Birim2 { get; set; }

        [ModelDefault("AllowEdit", "false"), DbType(" DECIMAL(18,4) "), VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false), ModelDefault("DisplayFormat", "{0:0}")]
        public decimal Miktar2 { get; set; }

        #endregion
        #region Depo Bilgisi

        protected Depolar _depo;

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int DepoId { get; set; }

        [Size(DbSize.KodLenght), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(true)]
        public string DepoKod { get; set; }

        [XmlIgnore(), NonPersistent, XafDisplayName("Depo Bilgisi"),
        VisibleInListView(true), VisibleInLookupListView(false), DataSourceCriteria("DepoId = '@This.DepoId'")]
        public Depolar Depo
        {
            get
            {
                if (_depo == null && this.DepoId > 0)
                    this._depo = this.Session.GetObjectByKey<Depolar>(this.DepoId);
                return _depo;
            }
            set
            {
                SetPropertyValue("Depo", ref _depo, value);
            }
        }

        #endregion
        #region Raf Bilgisi

        protected Raflar _raf;

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int RafId { get; set; }

        [Size(DbSize.KodLenght), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(true)]
        public string RafKod { get; set; }

        [XmlIgnore(), NonPersistent, XafDisplayName("Raf Bilgisi"),
        VisibleInListView(true), VisibleInLookupListView(false), DataSourceCriteria("RafId = '@This.RafId'")]
        public Raflar Raf
        {
            get
            {
                if (_raf == null && this.RafId > 0)
                    this._raf = this.Session.GetObjectByKey<Raflar>(this.RafId);
                return _raf;
            }
            set
            {
                SetPropertyValue("Raf", ref _raf, value);
            }
        }

        #endregion

        public string DurumAciklama { get; set; }
        public SayimOkumaTip OkumaTip { get; set; }
        public string LotKod { get; set; }
        public int LotId { get; set; }

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
                    this.DurumAciklama = SayimDurumlari.IptalSayimDetayi;
                else if (this.Durum == KayitDurumu.Yeni)
                    this.DurumAciklama = SayimDurumlari.YeniSayimDetayi;
                else if (this.Durum == KayitDurumu.Tamamlandi)
                    this.DurumAciklama = SayimDurumlari.TamamlananSayimDetayi;
            }
        }
        #endregion

        protected override void OnSaving()
        {
            if (IsDeleted == false)
            {
                if (MalzemeId < 1)
                    throw new Exception(string.Format(" MalzemeId olmadan detay kayıt edilemez."));

                if (BirimId < 1)
                    throw new Exception(string.Format(" Birim olmadan detay kayıt edilemez."));

                if (string.IsNullOrEmpty(Birim))
                {
                    Birimler brm = this.Session.GetObjectByKey<Birimler>(BirimId, true);
                    if (brm == null)
                        throw new Exception(string.Format("Birim tanımlı değil {0}", BirimId));

                    this.Birim = brm.Birim;
                }
                
                //AnaBirim
                V_MalzemeBirimleri HedefBirim = new V_MalzemeBirimleri();
                MalzemeBirimleri KaynakBirim = new MalzemeBirimleri();

                HedefBirim = this.Session.FindObject<V_MalzemeBirimleri>(CriteriaOperator.Parse(" MalzemeId = ? AND Miktar = ? AND Miktar2 = ? ", this.MalzemeId, 1, 1));
                KaynakBirim = this.Session.FindObject<MalzemeBirimleri>(CriteriaOperator.Parse(" MalzemeId = ? AND BirimId = ? ", this.MalzemeId, BirimId));


                if (HedefBirim != null && KaynakBirim != null)
                {
                    Birim2 = HedefBirim.Birim;
                    Birim2Id = (int)HedefBirim.BirimId;

                    if (KaynakBirim.Miktar == KaynakBirim.Miktar2)
                    {
                        Miktar2 = (Miktar / HedefBirim.Miktar2) * KaynakBirim.Miktar2;
                    }
                    else
                    {
                        Miktar2 = (Miktar / KaynakBirim.Miktar2) * HedefBirim.Miktar2;
                    }
                }
                else
                {
                    Birim2 = this.Birim;
                    Birim2Id = this.BirimId;
                    Miktar2 = this.Miktar;
                }
            }

        }

        public SayimDetaylari() { }
        public SayimDetaylari(Session session) : base(session) { }
    }
}
