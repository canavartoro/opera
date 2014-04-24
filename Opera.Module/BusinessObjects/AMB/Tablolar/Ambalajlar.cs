using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Diagnostics;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Filtering;
using Mikrobar;
using DevExpress.Xpo.DB;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [DefaultClassOptions, OptimisticLocking(false), DeferredDeletion(false), DebuggerDisplay("Barkod = {Barkod}, Oid = {Oid}, Durum = {Durum}")]
    [ImageName("BO_Sale_Item_v92"), ModelDefault("DefaultListViewShowAutoFilterRow", "True"), ModelDefault("IsClonable", "True"),
    NavigationItem(false), XafDefaultProperty("Barkod")]
    public class Ambalajlar : XPObject
    {

        #region Ambalaj Tur
        [NonPersistent, Description("Mobil cihazlarda kullanmak icin."), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int AmbalajTurId { get; set; }
        //[ModelDefault("AllowEdit", "False")]
        [XmlIgnore(), Association("Ambalajlar.AmbalajTur_AmbalajTurleri")]
        public AmbalajTurleri AmbalajTur { get; set; }
        #endregion

        #region Barkod
        [Indexed(Unique = true), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [Size(DbSize.SeriNo)]
        public string SeriNo { get; set; }
        [Indexed(Unique = true)]
        [Size(DbSize.BarKodLenght)]
        public string Barkod { get; set; }
        #endregion

        #region Depo/Raf
        [NonPersistent, Description("Mobil cihazlardan kullanmak icin."), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int DepoId { get; set; }

        [Association(@"Ambalajlar.Depo_DepoId"), Persistent("DepoId"), ImmediatePostData]
        public Depolar Depo { get; set; }

        [NonPersistent, Description("Mobil cihazlardan kullanmak icin."), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int RafId { get; set; }

        Raflar fRaf;
        [Association(@"Ambalajlar.RafIds_Raflar"), Persistent("RafId"), DataSourceCriteria("Depo = '@This.Depo'")]
        public Raflar Raf
        {
            get { return fRaf; }
            set { SetPropertyValue<Raflar>("Raf", ref fRaf, value); }
        }
        #endregion

        #region Renk
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int RenkId { get; set; }
        [Size(DbSize.KodLenght), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string RenkKod { get; set; }
        protected Renkler _renk;
        [XmlIgnore(), NonPersistent, XafDisplayName("Renk Bilgisi"),
        VisibleInListView(false), VisibleInLookupListView(false)]
        public Renkler Renk
        {
            get
            {
                if (_renk == null && this.RenkId > 0)
                    this._renk = this.Session.GetObjectByKey<Renkler>(this.RenkId);
                return _renk;
            }
            set
            {
                SetPropertyValue("Renk", ref _renk, value);
            }
        }
        #endregion

        #region uretimde doldurulacak alanlar
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int IstasyonId { get; set; }
        [Size(DbSize.KodLenght), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string IstasyonKod { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int HareketId { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int IsEmriTipId { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int IsEmriId { get; set; }
        [ModelDefault("AllowEdit", "false"), Size(DbSize.NoLenght), VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string IsEmriNo { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int OperasyonId { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int OperasyonNo { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string OperasyonKod { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int SiraNo { get; set; }
        #endregion

        #region sevkiyat ve satinalmada doldurulacak alanlar

        #region Cari Kod
        protected Cariler _cari;
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int CariId { get; set; }
        [Size(DbSize.NoLenght), VisibleInDetailView(false), SearchMemberOptions(SearchMemberMode.Include)]
        public string CariKod { get; set; }
        [XmlIgnore(), NonPersistent, XafDisplayName("Cari Bilgisi"),
        VisibleInListView(false), VisibleInLookupListView(false), DataSourceCriteria("CariId = '@This.CariId'")]
        public Cariler Cari
        {
            get
            {
                if (_cari == null && this.CariId > 0)
                    this._cari = this.Session.GetObjectByKey<Cariler>(this.CariId);
                return _cari;
            }
            set
            {
                SetPropertyValue("Cari", ref _cari, value);
            }
        }
        #endregion

        [Size(DbSize.NoLenght), VisibleInListView(false), VisibleInLookupListView(false)]
        public string IrsaliyeNo { get; set; }
        [Size(DbSize.NoLenght), VisibleInListView(false), VisibleInLookupListView(false)]
        public string FisNo { get; set; }
        [Size(DbSize.NoLenght), VisibleInListView(false), VisibleInLookupListView(false)]
        public string BelgeNo { get; set; }
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}"), VisibleInListView(false), VisibleInLookupListView(false)]
        public DateTime BelgeTarihi { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int SevkEmriId { get; set; }
        [ModelDefault("AllowEdit", "false"), Size(DbSize.NoLenght), VisibleInListView(false), VisibleInLookupListView(false)]
        public string SevkEmriNo { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int SevkEmriDetayId { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int SiparisId { get; set; }
        [ModelDefault("AllowEdit", "false"), Size(DbSize.NoLenght), VisibleInListView(false), VisibleInLookupListView(false)]
        public string SiparisNo { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int SiparisDetayId { get; set; }
        #endregion

        public int PaletSiraNo { get; set; }
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string PaletNo { get; set; }
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string PartiNo { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string OzellikKod1 { get; set; }
        [Size(DbSize.AciklamaLenght), VisibleInLookupListView(false)]
        public string Aciklama { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public decimal Dara { get; set; }

        #region Ortak Alanlar
        #region Olusturan
        // ReadOnly(true),ModelDefault("AllowEdit", "False"),
        [ XmlIgnore(), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
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

        #endregion

        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}"), VisibleInListView(false), VisibleInLookupListView(false)]
        public DateTime SonKullanmaTarihi { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int RafOmru { get; set; }

        [ModelDefault("AllowEdit", "false"), Description("Sistemdeki palet id"), VisibleInListView(false), VisibleInLookupListView(false)]
        public int ReferansId { get; set; }
        [ModelDefault("AllowEdit", "false"), Description("Sistemdeki palet transfer id"), VisibleInListView(false), VisibleInLookupListView(false)]
        public int TransferReferansId { get; set; }
        [ModelDefault("AllowEdit", "false"), Description("Fason cikis sanal malzeme kasasi..."), VisibleInListView(false), VisibleInLookupListView(false)]
        public FasonCikisSanalStok SanalStok { get; set; }

        /* GECİCİ
         ALTER TABLE dbo.Ambalajlar ADD AmbalajKey NVARCHAR(64) NULL

UPDATE dbo.Ambalajlar SET AmbalajKey = Barkod WHERE AmbalajKey IS NULL

ALTER TABLE dbo.Ambalajlar ALTER COLUMN AmbalajKey NVARCHAR(64) NOT NULL
         */
        //[Indexed(Unique = true), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //[Size(DbSize.KodLenght)]//ambalaj barkodunun tek yerden uretildigini kontrol etmek icin
        //public string AmbalajKey { get; set; }

        #region Uretim Bilgisi
        [ModelDefault("AllowEdit", "False"), XmlIgnore(), PersistentAlias("Iif(UretimOperasyon is null, 0, UretimOperasyon.Oid)"),
        VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int UretimOperasyonId
        {
            get { return Convert.ToInt32(EvaluateAlias("UretimOperasyonId")); }
            set
            {
                if (!IsLoading && !IsSaving)
                {
                    SetPropertyValue<UretimOperasyonlari>("UretimOperasyon", ref _UretimOperasyon, Session.GetObjectByKey<UretimOperasyonlari>(value));
                }
            }
        }

        protected UretimOperasyonlari _UretimOperasyon;
        [XmlIgnore(), XafDisplayName("Uretim Bilgisi"), ImmediatePostData,
        VisibleInListView(false), VisibleInLookupListView(false), Association(@"Ambalajlar-Operasyonlari")]
        public UretimOperasyonlari UretimOperasyon
        {
            get
            {
                return _UretimOperasyon;
            }
            set
            {
                SetPropertyValue("UretimOperasyon", ref _UretimOperasyon, value);
            }
        }
        #endregion

        

        #region Ambalaj Durumu
        AmbalajDurumu fdurum;
        public AmbalajDurumu Durum
        {
            get { return fdurum; }
            set { SetPropertyValue<AmbalajDurumu>("Durum", ref fdurum, value); SDurum = value; }
        }
        [ValueConverter(typeof(Mikrobar.Module.AmbalajDurumConverter)), Description("Ambalaj durumunun yazili hali"), XmlIgnore(), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public AmbalajDurumu SDurum { get; set; }
        [ModelDefault("AllowEdit", "false"), Description("Ambalaj kalite durum"), XmlIgnore(), VisibleInListView(false), VisibleInLookupListView(false)]
        public KaliteDurumu KaliteDurum { get; set; }
        [ModelDefault("AllowEdit", "false"), Size(DbSize.AciklamaLenght), Description("Ambalaj durumunun aciklamasi"), XmlIgnore()]
        public string DurumAciklama { get; set; }
        #endregion

        [XmlIgnore(), VisibleInListView(false), Association("Ambalaj-Operasyonlari"), VisibleInDetailView(false)]
        public XPCollection<UretimOperasyonlari> UretimOperasyonlari
        {
            get { return GetCollection<UretimOperasyonlari>("UretimOperasyonlari"); }
        }

        private Ambalajlar ambalaj;
        [XmlIgnore(), Association("Ambalajlar-Ambalaj"), VisibleInDetailView(false), VisibleInLookupListView(false), VisibleInListView(false)]
        public Ambalajlar UstAmbalaj
        {
            get { return ambalaj; }
            set
            {
                SetPropertyValue<Ambalajlar>("UstAmbalaj", ref ambalaj, value);
            }
        }

        [XmlIgnore(), VisibleInListView(false), Association("Ambalajlar-Ambalaj"), VisibleInLookupListView(false)]
        public XPCollection<Ambalajlar> AltAmbalajlar
        {
            get { return GetCollection<Ambalajlar>("AltAmbalajlar"); }
        }

        //[XmlIgnore(), VisibleInListView(false), VisibleInLookupListView(false)]
        //[NonPersistent]
        //public List<AmbalajDetaylari> AltAmbalajDetaylari
        //{
        //    get
        //    {
        //        List<int> ids = AltAmbalajlar.Select(x => x.Oid).ToList();
        //        ids.Add(this.Oid);
        //        return (from x in new XPQuery<AmbalajDetaylari>(Session)
        //                where ids.Contains(x.AmbalajId)
        //                select x).ToList();
        //    }
        //}

        //[PersistentAlias("AltAmbalajlar.")]
        //[XmlIgnore(), VisibleInListView(false), VisibleInLookupListView(false)]
        //public XPCollection<Ambalajlar> TumAltAmbalajlar
        //{
        //    get { return GetCollection<Ambalajlar>("AltAmbalajlar"); }
        //}

        [Association(@"AmbalajDetaylari.Ambalaj_Ambalajlar")]
        public XPCollection<AmbalajDetaylari> AmbalajDetaylari
        {
            get { return GetCollection<AmbalajDetaylari>(@"AmbalajDetaylari"); }
        }

        [XmlIgnore(), VisibleInListView(false), Association(@"AmbalajHareketDetaylari.OID_Ambalajlar"), VisibleInDetailView(false)]
        public XPCollection<AmbalajHareketDetaylari> AmbalajHareketDetaylari
        {
            get { return GetCollection<AmbalajHareketDetaylari>(@"AmbalajHareketDetaylari"); }
        }

        [XmlIgnore(), VisibleInListView(false), Association(@"AmbalajHareketDetaylari.OID_Ambalajlar2"), VisibleInDetailView(false)]
        public XPCollection<AmbalajHareketDetaylari> AmbalajHareketDetaylari2
        {
            get { return GetCollection<AmbalajHareketDetaylari>(@"AmbalajHareketDetaylari2"); }
        }

        [XmlIgnore(), VisibleInListView(false), Association(@"UretimMalzemeleri.AmbalajId"), VisibleInDetailView(false)]
        public XPCollection<UretimMalzemeleri> Uretimler
        {
            get { return GetCollection<UretimMalzemeleri>(@"Uretimler"); }
        }

        [XmlIgnore(), VisibleInListView(false), Association(@"UretimHurdalar.AmbalajId"), VisibleInDetailView(false)]
        public XPCollection<UretimHurdalari> Hurdalar
        {
            get { return GetCollection<UretimHurdalari>(@"Hurdalar"); }
        }

        [XmlIgnore(), VisibleInListView(false), Association(@"UretimHurdalar.KaynakAmbalajId"), VisibleInDetailView(false)]
        public XPCollection<UretimHurdalari> HurdalarKaynak
        {
            get { return GetCollection<UretimHurdalari>(@"HurdalarKaynak"); }
        }

        //[XmlIgnore(), Association(@"Ambalajlar.UretimKaydi_Ambalaj"), VisibleInListView(false),VisibleInDetailView(false)]
        //public XPCollection<UretimKaydi> UretimKayitlari
        //{
        //    get { return GetCollection<UretimKaydi>(@"UretimKayitlari"); }
        //}

        //Ambalaj turleri duzgun kullanilmadigi icin seri  kontrolu ekledik
        [Browsable(false), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false), NonPersistent, XmlIgnore()]
        public bool UrunSerisi
        {
            get
            {
                try
                {
                    if (AmbalajDetaylari != null)
                        return AmbalajDetaylari[0].Miktar == 1 && AmbalajDetaylari[0].Kalan == 1 && this.AmbalajTur.MalzemeTip == MalzemeTip.Urun;

                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return false;
            }
        }

        [PersistentAlias("Oid"), ModelDefault("DisplayFormat", "d")]
        public int KayitId
        {
            get
            {
                try
                {
                    return Convert.ToInt32(EvaluateAlias("KayitId"));
                }
                catch (ObjectDisposedException disp) { }
                catch (Exception disp) { }
                return 0;
            }
        }

        [Action(Caption = "Hareketlere Git", AutoCommit = false, TargetObjectsCriteria = "", ImageName = "ModelEditor_GoToObject", ToolTip = "Hareket kayitlarina git")]
        public void HareketlereGit()
        {
            try
            {
                WebApplication application = WebApplication.Instance;
                IObjectSpace objectSpace = application.CreateObjectSpace();
                string listViewId = application.FindListViewId(typeof(V_AmbalajHareket));
                CollectionSource collectionSource = new CollectionSource(objectSpace, typeof(V_AmbalajHareket), true);
                if ((collectionSource.Collection as XPBaseCollection) != null)
                {
                    ((XPBaseCollection)collectionSource.Collection).LoadingEnabled = false;
                }
                collectionSource.Criteria.Clear();
                collectionSource.Criteria.Insert(0, "bulCriteria", new BinaryOperator("Barkod", this.Barkod, BinaryOperatorType.Equal));
                DevExpress.ExpressApp.ListView listView = application.CreateListView(listViewId, collectionSource, false);
                ShowViewParameters svp = new ShowViewParameters();
                svp.CreatedView = listView;
                svp.TargetWindow = TargetWindow.Default;
                application.ShowViewStrategy.ShowView(svp, new ShowViewSource(null, null));
            }
            catch (Exception) { }
        }

        public Ambalajlar() { }
        public Ambalajlar(Session session) : base(session) { }        

        #region Events
        protected override void OnSaving()
        {
                   

        }

        protected override void OnDeleting()
        {
            
        }
        #endregion

        #region Operators
        public static implicit operator V_Ambalajlar(Ambalajlar amb)
        {
            if (!object.ReferenceEquals(amb, null))
            {
                #region Detayli
                if (amb.AmbalajDetaylari.Count > 0)
                {
                    if (amb.AmbalajDetaylari[0].Oid == -1)
                    {
                        V_Ambalajlar vAmb = (V_Ambalajlar)XpoHelper.CloneBaseObject(amb, typeof(V_Ambalajlar), amb.Session);
                        if (vAmb != null)
                        {
                            vAmb = (V_Ambalajlar)XpoHelper.CloneObj(amb.AmbalajDetaylari[0], vAmb);
                        }
                        return vAmb;
                    }
                    else
                    {
                        return amb.Session.GetObjectByKey<V_Ambalajlar>(amb.AmbalajDetaylari[0].Oid);
                    }
                } 
                #endregion
                #region Detaysiz
                else
                {
                    if (amb.Oid == -1)
                    {
                        V_Ambalajlar vAmb = (V_Ambalajlar)XpoHelper.CloneBaseObject(amb, typeof(V_Ambalajlar), amb.Session);
                        return vAmb;
                    }
                    else
                    {
                        return amb.Session.GetObjectByKey<V_Ambalajlar>(amb.Oid);
                    }
                } 
                #endregion
            }
            return null;
        }
        public static explicit operator XPCollection<V_Ambalajlar>(Ambalajlar amb)
        {
            if (object.ReferenceEquals(amb, null))
            {
                int[] ids = amb.AmbalajDetaylari.Where(x => x.Durum != KayitDurumu.Iptal).Select(x => x.Oid).ToArray();
                XPCollection<V_Ambalajlar> detaylar = new XPCollection<V_Ambalajlar>(amb.Session, new InOperator("AmbalajDetayId", ids), null);
                return detaylar;                    
            }
            return null;
        }
        #endregion

        [Browsable(false)]
        public AmbalajKalan KalanMiktar(int malzemeId)
        {
            return (from dty in this.AmbalajDetaylari
                    where dty.MalzemeId == malzemeId
                    group dty by new { dty.MalzemeId, dty.MalzemeKod, dty.BirimId, dty.Birim, dty.Birim2Id, dty.Birim2 } into gcv
                    select new AmbalajKalan()
                    {
                        MalzemeId = gcv.Key.MalzemeId,
                        MalzemeKod = gcv.Key.MalzemeKod,
                        BirimId = gcv.Key.BirimId,
                        Birim = gcv.Key.Birim,
                        Birim2Id = gcv.Key.Birim2Id,
                        Birim2 = gcv.Key.Birim2,
                        Miktar = gcv.Sum(cv => cv.Kalan),
                        Miktar2 = gcv.Sum(cv => cv.Kalan2)
                    }).FirstOrDefault();
        }
    }


    public struct AmbalajKalan
    {
        public int MalzemeId { get; set; }
        public string MalzemeKod { get; set; }
        public int BirimId { get; set; }
        public string Birim { get; set; }
        public int Birim2Id { get; set; }
        public string Birim2 { get; set; }
        public decimal Miktar { get; set; }
        public decimal Miktar2 { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is AmbalajKalan)
            {
                AmbalajKalan k = (AmbalajKalan)obj;
                return k.MalzemeId == this.MalzemeId && k.MalzemeKod == this.MalzemeKod &&
                    k.BirimId == this.BirimId && k.Birim == this.Birim && k.Birim2Id == this.Birim2Id && k.Birim2 == this.Birim2 &&
                    k.Miktar == this.Miktar && k.Miktar2 == this.Miktar2;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return MalzemeId.GetHashCode();
        }


        public static bool operator ==(AmbalajKalan f1, AmbalajKalan f2) 
        {
            return f1.Equals(f2); 
        }
        public static bool operator !=(AmbalajKalan f1, AmbalajKalan f2) 
        {
            return f1.Equals(f2) == false;
        }
    }

}




/* TRIGGER MSSQL
CREATE TRIGGER dbo.AmbalajBilgisi
ON dbo.Ambalajlar
FOR INSERT
AS BEGIN

DECLARE @T_AMBALAJ TABLE (OID INT, ISEMRIID INT, DURUM BIT)
DECLARE @SIRANO INT, @ACIKLAMA NVARCHAR(100), @OID INT, @ISEMRIID INT

INSERT INTO @T_AMBALAJ SELECT OID, IsEmriId, 0 AS DURUM FROM INSERTED 

WHILE ( SELECT COUNT(*) FROM @T_AMBALAJ WHERE DURUM = 0 ) > 0 BEGIN
	SELECT TOP 1 @OID = OID, @ISEMRIID = ISEMRIID FROM @T_AMBALAJ WHERE DURUM = 0
	IF @ISEMRIID > 0 BEGIN
		SELECT @SIRANO = COUNT(*) + 1 FROM dbo.Ambalajlar WITH (NOLOCK) WHERE IsEmriId = @ISEMRIID
		SELECT @ACIKLAMA = CizimNo FROM dbo.IsEmirleri WITH (NOLOCK) WHERE IsEmriId = @ISEMRIID
		UPDATE dbo.Ambalajlar SET SiraNo = ISNULL(@SIRANO, 1), Aciklama = @ACIKLAMA WHERE OID = @OID		
	END 
	UPDATE @T_AMBALAJ SET DURUM = 1 WHERE OID = @OID
END

END
 */