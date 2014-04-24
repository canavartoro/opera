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
    [DebuggerDisplay("AmbalajHareket = {AmbalajHareket}, AmbalajHareketDetay = {Oid}, MalzemeKod = {MalzemeKod}")]
    public class AmbalajHareketDetaylari : XPObject
    {
        #region Etiket alanlari<<Kullanmayın kaldırılacak!!!!>>
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int PersonelKontId { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int PersonelPaketId { get; set; }
        [Size(DbSize.AdLenght), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string PersonelKontKod { get; set; }
        [Size(DbSize.AciklamaLenght), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string PersonelKontAd { get; set; }
        [Size(DbSize.AdLenght), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string PersonelPaketKod { get; set; }
        [Size(DbSize.AciklamaLenght), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string PersonelPaketAd { get; set; }
        #endregion

        #region Malzeme Bilgisi
        [Indexed("IsEmriId", "OperasyonId", "Durum", Unique = false)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int MalzemeId { get; set; }

        [Size(DbSize.KodLenght), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(true)]
        public string MalzemeKod { get; set; }

        protected Malzemeler _malzeme;
        [XmlIgnore(), NonPersistent, XafDisplayName("Malzeme Bilgisi")]
        [VisibleInListView(true), VisibleInLookupListView(false), DataSourceCriteria("MalzemeId = '@This.MalzemeId'")]
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
                if (!IsLoading && !IsSaving)
                {
                    if (value != null)
                        this.MalzemeKod = value.MalzemeKod;
                }
            }
        }

        [PersistentAlias("Iif(Malzeme is null, '', Malzeme.MalzemeAd)")]
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

        [DbType(" DECIMAL(18,4) ")]
        public decimal Miktar { get; set; }

        [ModelDefault("AllowEdit", "false"), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Birim2Id { get; set; }
        [ModelDefault("AllowEdit", "false"), Size(DbSize.KodLenght), VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Birim2 { get; set; }
        [ModelDefault("AllowEdit", "false"), DbType(" DECIMAL(18,4) "), VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public decimal Miktar2 { get; set; }
        [ModelDefault("AllowEdit", "false"), DbType(" DECIMAL(18,4) "), VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public decimal OkunanMiktar { get; set; }
        [ModelDefault("AllowEdit", "false"), DbType(" DECIMAL(18,4) "), VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public decimal OkunanMiktar2 { get; set; }

        #endregion

        [Association(@"AmbalajHareketDetaylari.HedefDepo_Depolar")]
        public Depolar HedefDepo { get; set; }

        [Association(@"AmbalajHareketDetaylari.KaynakDepo_Depolar")]
        public Depolar KaynakDepo { get; set; }

        Raflar fHedefRaf;
        [Association(@"AmbalajHareketDetaylari.HedefRaf_Raflar")]
        public Raflar HedefRaf
        {
            get { return fHedefRaf; }
            set { SetPropertyValue<Raflar>("HedefRaf", ref fHedefRaf, value); }
        }

        Raflar fKaynakRaf;
        [Association(@"AmbalajHareketDetaylari.KaynakRaf_Raflar")]
        public Raflar KaynakRaf
        {
            get { return fKaynakRaf; }
            set { SetPropertyValue<Raflar>("KaynakRaf", ref fKaynakRaf, value); }
        }

        AmbalajHareketleri fAmbalajHareket;
        [XmlIgnore(), Association(@"AmbalajHareketDetaylari.AmbalajHareket_AmbalajHareketleri")]
        public AmbalajHareketleri AmbalajHareket
        {
            get { return fAmbalajHareket; }
            set { SetPropertyValue<AmbalajHareketleri>("AmbalajHareket", ref fAmbalajHareket, value); }
        }

        [ModelDefault("AllowEdit", "False"),
        PersistentAlias("Iif(KaynakAmbalaj is null, 0, KaynakAmbalaj.Oid)"),
        VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int KaynakAmbalajId
        {
            get { return Convert.ToInt32(EvaluateAlias("KaynakAmbalajId")); }
            set
            {
            }
        }

        [XmlIgnore(), Association(@"AmbalajHareketDetaylari.OID_Ambalajlar2")]
        public Ambalajlar KaynakAmbalaj { get; set; }


        [XmlIgnore(), Association(@"AmbalajHareketDetaylari.OID_Ambalajlar")]
        public Ambalajlar Ambalaj { get; set; }

        [Description("Mal hazirlama emri id"), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int MalToplamaDetayId { get; set; }
        [Description("Mal hazirlama emri id"), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int MalToplamaId { get; set; }

        [Description("Sistemdeki PickingDetailId alani"), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int ReferansId { get; set; }
        [Description("Sistemdeki PacketTraDId alani"), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int TransferDetayId { get; set; }
        [Description("Sistemdeki PackageTraMId alani"), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int TransferId { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public FasonCikisSanalStok SanalStok { get; set; }

        #region Uretim&Fason
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int IsEmriId { get; set; }
        [Size(DbSize.KodLenght), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string IsEmriNo { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int OperasyonId { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int OperasyonNo { get; set; }
        [Size(DbSize.KodLenght), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string OperasyonKod { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int IsEmriDetayId { get; set; }
        [Description("Fason'a mal gönderimi yada Fason dönüşte işemri tamamlandı."), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool Tamamlandi { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int HedefIstasyonId { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int KaynakIstasyonId { get; set; }

        public HurdaTipi Hurda { get; set; }
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

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int SiparisId { get; set; }
        [Size(DbSize.NoLenght), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string SiparisNo { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int SiparisDetayId { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int SatirTipi { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Diger1 { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Diger2 { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Diger3 { get; set; }

        [Size(DbSize.AciklamaLenght), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Kod1 { get; set; }
        [Size(DbSize.AciklamaLenght), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Kod2 { get; set; }
        [Size(DbSize.AciklamaLenght), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Kod3 { get; set; }
        [Size(DbSize.AciklamaLenght), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Aciklama1 { get; set; }
        [Size(DbSize.AciklamaLenght), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Aciklama2 { get; set; }
        [Size(DbSize.AciklamaLenght), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Aciklama3 { get; set; }

        protected override void OnSaving()
        {


        }

        public AmbalajHareketDetaylari() { }
        public AmbalajHareketDetaylari(Session session) : base(session) { }
    }
}
