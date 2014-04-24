
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Xml.Serialization;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Web;
using Mikrobar.Islemler;
using DevExpress.ExpressApp.Filtering;
using DevExpress.ExpressApp.Actions;
using System.Web.UI;
using Mikrobar;
using Mikrobar.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;


namespace Mikrobar.Module.BusinessObjects
{
    [ListViewFilter("Tum Uretimler", "")]
    [ListViewFilter("Acik Uretimler", "[Durum] = 0")]
    [ListViewFilter("Bugunku Uretimler", "[BaslangicTarihi] >= LocalDateTimeToday()")]
    [ListViewFilter("Kapali Uretimler", "[Durum] = 4")]
    [ListViewFilter("Iptal Edilen Uretimler", "[Durum] = 2")]
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("Oid"), NavigationItem(false),
     ImageName("ModelEditor_Settings"), ModelDefault("DefaultListViewShowAutoFilterRow", "True"), ModelDefault("IsCloneable", "True")]
    [ReferansTablo("pub.erp_detay2", SistemTipi.Progress, false)]
    [ReferansTablo("PRDT_WORDER_AC_OP", SistemTipi.WebErp)]
    //[MapInheritance(MapInheritanceType.ParentTable)]
    public partial class UretimOperasyonlari : XPObject
    {
        #region Istasyon
        [/*ModelDefault("AllowEdit", "False"), */Indexed("Durum", Unique = false), PersistentAlias("Iif(Istasyon is null, 0, Istasyon.IstasyonId)"), 
        VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int IstasyonId
        {
            get 
            {
                try
                {
                    if (!IsLoading)
                        return Convert.ToInt32(EvaluateAlias("IstasyonId"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return 0; 
            }
            set 
            {
                if (!IsLoading && !IsSaving)
                {
                    SetPropertyValue("Istasyon", ref _istasyon, Session.GetObjectByKey<Istasyonlar>(value));
                    if (object.ReferenceEquals(this._istasyon, null))
                        throw new MikrobarException(Lang.Mesaj(GenelMesajlar.ERR_6002, value), 6002);
                    else
                        this.IstasyonKod = this._istasyon.IstasyonKod;
                }
            }
        }

        [ModelDefault("AllowEdit", "False"), VisibleInDetailView(false), Size(DbSize.KodLenght), SearchMemberOptions(SearchMemberMode.Include)]
        public string IstasyonKod { get; set; }

        private Istasyonlar _istasyon;
        [XmlIgnore(), XafDisplayName("Istasyon Kodu"), ImmediatePostData,
        VisibleInListView(false), VisibleInLookupListView(false), Association(@"Istasyon.Istasyonlar.IstasyonId"), Persistent("IstasyonId"), NoForeignKey]
        [ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxSearchEditButtonPropertyEditor")]
        //[Appearance("UretimOperasyonlari_Istasyon", Enabled = false, Criteria = "Iif(Oid == -1, 0, 1) == 1", AppearanceItemType = "ViewItem", Context = "DetailView", TargetItems = "Istasyon")]
        public Istasyonlar Istasyon
        {
            get
            {
                return _istasyon;
            }
            set
            {
                SetPropertyValue("Istasyon", ref _istasyon, value);
                if (!IsLoading && !object.ReferenceEquals(_istasyon, null))
                    this.IstasyonKod = this._istasyon.IstasyonKod;
            }
        }

        [VisibleInLookupListView(true), VisibleInDetailView(true), PersistentAlias("Iif(Istasyon is null, '', Istasyon.IstasyonAd)")]
        public string IstasyonAd 
        { 
            get 
            {
                try
                {
                    if (!IsLoading)
                        return Convert.ToString(EvaluateAlias("IstasyonAd"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return "";                 
            } 
        }
        #endregion

        #region IsEmri
        [PersistentAlias("Iif(IsEmri is null, 0, IsEmri.IsEmriId)")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int IsEmriId
        {
            get 
            {
                try
                {
                    if (!IsLoading)
                        return Convert.ToInt32(EvaluateAlias("IsEmriId"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return 0; 
            }
            set
            {
                if (!IsLoading && !IsSaving)
                {
                    SetPropertyValue("IsEmri", ref _isEmirleri, Session.GetObjectByKey<IsEmirleri>(value));
                    if (object.ReferenceEquals(this._isEmirleri, null))
                        throw new MikrobarException(Lang.Mesaj(GenelMesajlar.ERR_6002, value), 6002);
                    else
                        this.IsEmriNo = this._isEmirleri.IsEmriNo;
                }
            }
        }

        [Size(DbSize.KodLenght), VisibleInDetailView(false), SearchMemberOptions(SearchMemberMode.Include)]        
        public string IsEmriNo { get; set; }

        protected IsEmirleri _isEmirleri;
        [XmlIgnore(), XafDisplayName("Is Emri"), ImmediatePostData,
        VisibleInListView(false), VisibleInLookupListView(false)]
        [Association(@"UretimOperasyonlari.IsEmirleri.IsEmri"), NoForeignKey, Persistent("IsEmriId")]
        [ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxSearchEditButtonPropertyEditor")]
        [Appearance("UretimOperasyonlari_IsEmri",Enabled=false, Criteria = "Iif(Oid == -1, 0, 1) == 1", AppearanceItemType = "ViewItem", Context = "DetailView", TargetItems = "IsEmri")]
        public IsEmirleri IsEmri
        {
            get 
            {
                //if (_isEmirleri == null)
                //    _isEmirleri = Session.GetObjectByKey<IsEmirleri>(IsEmriId);
                //if (_isEmirleri != null && _isEmirleri.IsEmriId != IsEmriId)
                //    _isEmirleri = Session.GetObjectByKey<IsEmirleri>(IsEmriId);
                return _isEmirleri;
            }
            set
            {
                SetPropertyValue("IsEmri", ref _isEmirleri, value);
                if (!IsLoading && !IsSaving && !object.ReferenceEquals(this._isEmirleri, null))
                {
                    //this.IsEmriId = this._isEmirleri.IsEmriId;
                    this.IsEmriNo = this._isEmirleri.IsEmriNo;
                    SetPropertyValue<Malzemeler>("Malzeme", ref fMalzeme, Session.GetObjectByKey<Malzemeler>(this._isEmirleri.MalzemeId));
                    this.Birim = this._isEmirleri.Birim;
                    this.BirimId = this._isEmirleri.BirimId;
                    sourceBilesenler = null;
                    sourceMalzemeler = null;                 
                }
            }
        }                

        #endregion

        #region Operasyon

        [PersistentAlias("Iif(OperasyonBilgisi is null, 0, OperasyonBilgisi.IsEmriDetayId)")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int IsEmriDetayId
        {
            get 
            {
                try
                {
                    if (!IsLoading)
                        return Convert.ToInt32(EvaluateAlias("IsEmriDetayId"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return 0;                  
            }
            set
            {
                if (!IsLoading && !IsSaving)
                {
                    SetPropertyValue("OperasyonBilgisi", ref _isEmriDetay, Session.GetObjectByKey<IsEmriDetaylari>(value));
                    if (object.ReferenceEquals(this._isEmriDetay, null))
                        throw new MikrobarException(Lang.Mesaj(GenelMesajlar.ERR_6002, value), 6002);
                }
            }
        }

        protected IsEmriDetaylari _isEmriDetay;
        [XmlIgnore(), XafDisplayName("Operasyon Bilgisi"), ImmediatePostData]
        [Association(@"UretimOperasyonlari.IsEmriDetaylari.OperasyonBilgisi"), Persistent("IsEmriDetayId"), NoForeignKey]
        [VisibleInListView(false), VisibleInLookupListView(false), DataSourceCriteria("IsEmriId = '@This.IsEmriId'")]
        [Appearance("UretimOperasyonlari_OperasyonBilgisi", Enabled = false, Criteria = "Iif(Oid == -1, 0, 1) == 1", AppearanceItemType = "ViewItem", Context = "DetailView", TargetItems = "OperasyonBilgisi")]
        public IsEmriDetaylari OperasyonBilgisi
        {
            get
            {
                return _isEmriDetay;
            }
            set
            {
                SetPropertyValue("OperasyonBilgisi", ref _isEmriDetay, value);
                if (!IsLoading && !IsSaving && !object.ReferenceEquals(this._isEmriDetay, null))
                {
                    this.OperasyonNo = this._isEmriDetay.OperasyonNo;
                    this.OperasyonKod = this._isEmriDetay.OperasyonKod;
                    SetPropertyValue("Operasyon", ref _operasyon, Session.GetObjectByKey<Operasyonlar>(this._isEmriDetay.OperasyonId));
                }
            }
        }

        [ModelDefault("AllowEdit", "False"), PersistentAlias("Iif(OperasyonBilgisi is null, 0, OperasyonBilgisi.OperasyonId)"),
        VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int OperasyonId
        {
            get 
            {
                try
                {
                    if (!IsLoading)
                        return Convert.ToInt32(EvaluateAlias("OperasyonId"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return 0;                 
            }
            set
            {
                if (!IsLoading && !IsSaving)
                {
                    SetPropertyValue<IsEmriDetaylari>("OperasyonBilgisi", ref _isEmriDetay,
                        Session.FindObject<IsEmriDetaylari>(CriteriaOperator.Parse(" IsEmriId = ? And OperasyonId = ? ", IsEmriId, value)));
                    SetPropertyValue<Operasyonlar>("Operasyon", ref _operasyon, Session.GetObjectByKey<Operasyonlar>(value));
                    if (object.ReferenceEquals(_operasyon, null))
                        throw new MikrobarException(Lang.Mesaj(GenelMesajlar.ERR_6003), 6003);
                    else
                        this.OperasyonKod = this._operasyon.OperasyonKod;
                }
            }
        }

        [VisibleInListView(false), VisibleInLookupListView(false), ModelDefault("AllowEdit", "False")]
        public int OperasyonNo { get; set; }

        [Size(DbSize.KodLenght), ModelDefault("AllowEdit", "False"),
        VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public string OperasyonKod { get; set; }

        private Operasyonlar _operasyon;
        [XmlIgnore(), XafDisplayName("Operasyon Kodu"), ImmediatePostData,
        VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false),
        Association(@"Operasyonlar.Operasyon.OperasyonId"), Persistent("OperasyonId"), NoForeignKey]
        [ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxSearchEditButtonPropertyEditor")]
        public Operasyonlar Operasyon
        {
            get
            {
                return _operasyon;
            }
            set
            {
                SetPropertyValue("Operasyon", ref _operasyon, value);
                if (!IsLoading && !this._operasyon.IsNull())
                    this.OperasyonKod = this._operasyon.OperasyonKod;
            }
        }

        [VisibleInLookupListView(true), VisibleInDetailView(true), PersistentAlias("Iif(Operasyon is null, '', Operasyon.OperasyonAd)")]
        public string OperasyonAd 
        { 
            get 
            {
                try
                {
                    if (!IsLoading)
                        return Convert.ToString(EvaluateAlias("OperasyonAd"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return "";                  
            } 
        }

        [DXDescription("Onceki operasyon."), ModelDefault("DisplayFormat", "d"), ModelDefault("AllowEdit", "False"), ReadOnly(true), XmlIgnore(),
        VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int OncekiOperasyon { get; set; }

        [ModelDefault("AllowEdit", "False")]
        [Size(DbSize.AciklamaLenght), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int TasarimGrupId { get; set; }

        
        [VisibleInListView(true), VisibleInLookupListView(true), VisibleInDetailView(true)]
        public int KutuSiraNo { get; set; }
        #endregion

        #region Malzeme Bilgisi
        [Indexed, VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false), 
        PersistentAlias("Iif(Malzeme is null, 0, Malzeme.MalzemeId)")]
        public int MalzemeId
        {
            get 
            {
                try
                {
                    if (!IsLoading)
                        return Convert.ToInt32(EvaluateAlias("MalzemeId"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return 0;                
            }
            set 
            {
                if (!IsLoading && value > 0)
                {
                    SetPropertyValue<Malzemeler>("Malzeme", ref fMalzeme, Session.GetObjectByKey<Malzemeler>(value)); 
                    if (object.ReferenceEquals(this.fMalzeme, null))
                        throw new MikrobarException(Lang.Mesaj(GenelMesajlar.ERR_6018, value), 6018);
                    else
                        this.MalzemeKod = this.fMalzeme.MalzemeKod;
                }                
            }
        }

        [ModelDefault("AllowEdit", "false"), ReadOnly(true), Size(DbSize.KodLenght), 
        VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public string MalzemeKod { get; set; }

        Malzemeler fMalzeme;
        [XmlIgnore(), Association(@"Malzemeler.UretimOperasyonlari.MalzemeId"), NoForeignKey,
        Persistent("MalzemeId"), ModelDefault("AllowEdit", "False")]
        public Malzemeler Malzeme
        {
            get { return fMalzeme; }
            set 
            {
                SetPropertyValue<Malzemeler>("Malzeme", ref fMalzeme, value);
                if (!IsLoading && !object.ReferenceEquals(this.fMalzeme, null))
                    this.MalzemeKod = this.fMalzeme.MalzemeKod;
            }
        }

        [VisibleInLookupListView(true), VisibleInDetailView(true), PersistentAlias("Iif(Malzeme is null, '', Malzeme.MalzemeAd)")]
        public string MalzemeAd 
        { 
            get 
            {
                try
                {
                    if (!IsLoading)
                        return Convert.ToString(EvaluateAlias("MalzemeAd"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return "";                 
            } 
        }
        #endregion

        #region Birim
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int BirimId { get; set; }
        [Size(DbSize.KodLenght), VisibleInLookupListView(false), ModelDefault("AllowEdit", "False")]
        public string Birim { get; set; } 
        #endregion               

        #region Sayisal Alanlar

        private decimal _planlananMiktar = 0;
        [DbType(" DECIMAL(18,4) "), ModelDefault("AllowEdit", "False"), Browsable(false),
        VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(false), NonPersistent]
        public decimal PlanlananMiktar
        {
            get
            {
                if (_isEmirleri != null && _planlananMiktar == 0)
                    return _isEmirleri.PlanlananMiktar;
                else
                    return _planlananMiktar;
            }
            set { _planlananMiktar = value; }
        }

        private decimal _uretilenMiktar = -1;
        [XmlIgnore(), ModelDefault("AllowEdit", "False"),Browsable(false),
        VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(false), NonPersistent]
        public decimal UretilenMiktar
        {
            get
            {
                try
                {
                    if (!IsLoading && !IsSaving)
                    {
                        if (_uretilenMiktar == -1)
                        {
                            object sumUretilen = 0;

                            sumUretilen = Session.Evaluate<UretimOperasyonlari>(CriteriaOperator.Parse("Sum(NetMiktar)"),
                                             CriteriaOperator.Parse(" IsEmriId = ? AND IsEmriDetayId = ? AND Durum != ? ",
                                             this.IsEmriId, this.IsEmriDetayId, KayitDurumu.Iptal));

                            if (!object.ReferenceEquals(sumUretilen, null))
                                _uretilenMiktar = Convert.ToDecimal(sumUretilen);
                        }
                    }
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return _uretilenMiktar;
            }

        }        

        protected decimal fMiktar = 0;
        [DbType(" DECIMAL(18,4) "), ModelDefault("EditMask", "c")]
        public decimal Miktar 
        {
            get { return fMiktar; }
            set
            {
                SetPropertyValue<decimal>("Miktar", ref fMiktar, value);
            }
        }
        [DbType(" DECIMAL(18,4) "), ModelDefault("EditMask", "c"),
         ModelDefault("AllowEdit", "False"), VisibleInListView(false), VisibleInLookupListView(false)]
        public decimal NetMiktar { get; set; }
        [DbType(" DECIMAL(18,4) "), ModelDefault("EditMask", "c"),
         ModelDefault("AllowEdit", "False"), VisibleInListView(false), VisibleInLookupListView(false), XafDisplayName("Fire Miktari")]
        public decimal FireMiktari { get; set; }        
        
        [DbType(" DECIMAL(18,4) "), ModelDefault("EditMask", "c"), ModelDefault("AllowEdit", "false"), VisibleInListView(false), VisibleInLookupListView(false)]
        public decimal KatSayi1 { get; set; }
        [DbType(" DECIMAL(18,4) "), ModelDefault("EditMask", "c"), ModelDefault("AllowEdit", "false"), VisibleInListView(false), VisibleInLookupListView(false)]
        public decimal KatSayi2 { get; set; }

        [XmlIgnore(), ModelDefault("AllowEdit", "False"), VisibleInListView(false), VisibleInLookupListView(false)]
        public int UretimKatsayi { get; set; }
        #endregion

        #region Uretim Zamani
        protected DateTime fBaslangicTarihi = DateTime.Now;
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy HH:mm}"), ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxDateTimePropertyEditor")]
        public DateTime BaslangicTarihi
        {
            get { return fBaslangicTarihi; }
            set { SetPropertyValue<DateTime>("BaslangicTarihi", ref fBaslangicTarihi, value); }
        }

        protected DateTime fBitisTarihi = DateTime.Now.AddMinutes(1);
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy HH:mm}"), ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxDateTimePropertyEditor")]
        public DateTime BitisTarihi
        {
            get { return fBitisTarihi; }
            set { SetPropertyValue<DateTime>("BitisTarihi", ref fBitisTarihi, value); }
        }

        [DbType(" DECIMAL(18,4) "), ModelDefault("EditMask", "c"),
         ModelDefault("AllowEdit", "False"), VisibleInListView(false), VisibleInLookupListView(false)]
        public decimal UretimSuresi { get; set; }

        [DbType(" DECIMAL(18,4) "), ModelDefault("EditMask", "c"),
         ModelDefault("AllowEdit", "False"), VisibleInListView(false), VisibleInLookupListView(false)]
        public decimal NetUretimSuresi { get; set; }
        #endregion

        #region Surec Kalite
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int SKKId { get; set; }

        [DXDescription("Süreç kalite kontrol numarası"), Size(DbSize.KodLenght),
        VisibleInListView(false), VisibleInLookupListView(false)]
        public string SKKNo { get; set; }

        [DXDescription("Kalite kontrol gerekiyor")]
        public bool KaliteKontrol { get; set; }
        #endregion                

        #region Aciklamalar
        [Size(DbSize.AciklamaLenght), ModelDefault("RowCount", "2"), XafDisplayName("Genel Aciklama"), 
        VisibleInListView(false), VisibleInLookupListView(false)]
        public string Aciklama { get; set; }
        [Size(DbSize.AciklamaLenght), VisibleInListView(false), VisibleInLookupListView(false), XafDisplayName("Aciklama")]
        public string Aciklama2 { get; set; }
        [Size(DbSize.AciklamaLenght), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public string Aciklama3 { get; set; }
        [Size(DbSize.AciklamaLenght), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public string Aciklama4 { get; set; }
        [Size(DbSize.AciklamaLenght), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public string Aciklama5 { get; set; }
        [Size(DbSize.AciklamaLenght), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public string Aciklama6 { get; set; }

        [Size(DbSize.AciklamaLenght), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public string EkAlan1 { get; set; }
        [Size(DbSize.AciklamaLenght), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public string EkAlan2 { get; set; }
        [Size(DbSize.AciklamaLenght), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public string EkAlan3 { get; set; }
        [Size(DbSize.AciklamaLenght), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public string EkAlan4 { get; set; }
        

        #endregion

        #region CariKod
        [ModelDefault("AllowEdit", "False"), Indexed("Durum", Unique = false), PersistentAlias("Iif(Cari is null, 0, Cari.CariId)"), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int CariId
        {
            get 
            {
                try
                {
                    if (!IsLoading)
                        return Convert.ToInt32(EvaluateAlias("CariId"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return 0;                
            }
            set
            {
                if (!IsLoading && value > 0)
                {
                    SetPropertyValue<Cariler>("Cari", ref _cari, Session.GetObjectByKey<Cariler>(value));
                    if (object.ReferenceEquals(_cari, null))
                        throw new Exception(Lang.Mesaj(GenelMesajlar.ERR_2029, value));
                    else
                        this.CariKod = this._cari.CariKod;
                }
            }
        }

        [ModelDefault("AllowEdit", "False"), VisibleInDetailView(false), Size(DbSize.KodLenght), SearchMemberOptions(SearchMemberMode.Include)]
        public string CariKod { get; set; }

        protected Cariler _cari;
        [XmlIgnore(), XafDisplayName("Cari Kodu"), ImmediatePostData,
        VisibleInListView(false), VisibleInLookupListView(false), ModelDefault("AllowEdit", "False"),
        Association(@"Cariler-Operasyonlari"), Persistent("CariId"), NoForeignKey]
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
                if (!IsLoading && !object.ReferenceEquals(_cari, null))
                    this.CariKod = this._cari.CariKod;
            }
        }

        [VisibleInLookupListView(true), VisibleInDetailView(true), PersistentAlias("Iif(Cari is null, '', Cari.CariAd)")]
        public string CariAd 
        { 
            get 
            {
                try
                {
                    if (!IsLoading)
                        return Convert.ToString(EvaluateAlias("CariAd"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return "";                
            } 
        }
        #endregion        

        #region Operator
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false), ModelDefault("DisplayFormat", "d")]
        public int OperatorId { get; set; }

        private string operatorkod;
        [Size(DbSize.KodLenght), ModelDefault("AllowEdit", "False"),
        VisibleInListView(false), VisibleInLookupListView(true), VisibleInDetailView(false)]
        public string OperatorKod
        {
            get
            {
                return operatorkod;
            }
            set
            {
                SetPropertyValue("OperatorKod", ref operatorkod, value);
            }
        }

        private Personeller _operator;
        [XmlIgnore(), ModelDefault("AllowEdit", "False"), NonPersistent, XafDisplayName("Operator Kodu"), ImmediatePostData,
        VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        public Personeller Operator
        {
            get
            {
                try
                {
                    if (!IsLoading)
                    {
                        if (_operator == null && this.OperatorId > 0)
                            this._operator = this.Session.GetObjectByKey<Personeller>(this.OperatorId);
                    }
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }                
                return _operator;
            }
            set
            {
                SetPropertyValue("Operator", ref _operator, value);
                if (value != null)
                    SetPropertyValue("OperatorKod", ref operatorkod, value.PersonelKod);
            }
        }

        [PersistentAlias("Operator.PersonelIsim")]
        public string OperatorIsim 
        { 
            get 
            {
                try
                {
                    if (!IsLoading)
                        return Convert.ToString(EvaluateAlias("OperatorIsim"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return "";                   
            } 
        }
        #endregion

        [Association("Uretimler-Operasyonlar"), ModelDefault("AllowEdit", "False"), ReadOnly(true), XmlIgnore(), 
        VisibleInListView(false), VisibleInLookupListView(false),VisibleInDetailView(false)]
        public Uretimler Uretim { get; set; }

        [PersistentAlias("Oid"), ModelDefault("DisplayFormat", "d")]
        public int KayitId 
        { 
            get 
            {
                try
                {
                    if (!IsLoading)
                        return Convert.ToInt32(EvaluateAlias("KayitId"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return 0; 
            } 
        }

        #region Sanal Alanlar
        [NonPersistent, NonCloneable, Browsable(false), XmlIgnore(), VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(false)]
        public decimal XMiktar { get; set; }

        [NonPersistent, NonCloneable, Browsable(false), XmlIgnore(), VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(false)]
        public bool XMalzemeler { get; set; }

        [NonPersistent, NonCloneable, Browsable(false), XmlIgnore(), VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(false)]
        public bool XIscilikler { get; set; }

        [NonPersistent, NonCloneable, Browsable(false), XmlIgnore(), VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(false)]
        public bool XDuruslar { get; set; }

        [NonPersistent, NonCloneable, Browsable(false), XmlIgnore(), VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(false)]
        public bool XHurdalar { get; set; }

        [NonPersistent, NonCloneable, Browsable(false), XmlIgnore(), VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(false)]
        public bool XAletler { get; set; }

        [NonPersistent, NonCloneable, Browsable(false), XmlIgnore(), VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(false)]
        public KayitDurumu XDurum { get; set; }
        #endregion

        #region Vardiya

        [ModelDefault("AllowEdit", "False"), PersistentAlias("Iif(Vardiya is null, 0, Vardiya.VardiyaId)"), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int VardiyaId
        {
            get 
            {
                try
                {
                    if (!IsLoading)
                        return Convert.ToInt32(EvaluateAlias("VardiyaId"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return 0;                   
            }
            set
            {
                if (!IsLoading && value > 0)
                {
                    SetPropertyValue<Vardiyalar>("Vardiya", ref _vardiya, Session.GetObjectByKey<Vardiyalar>(value));
                    if (object.ReferenceEquals(_vardiya, null))
                        throw new Exception(Lang.Mesaj(GenelMesajlar.ERR_6006));
                }
            }
        }

        private Vardiyalar _vardiya;
        [Association("UretimOperasyonlari.Vardiya_Vardiyalar"), NoForeignKey,
        VisibleInListView(false), VisibleInLookupListView(false)]
        [ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxSearchEditButtonPropertyEditor")]
        public Vardiyalar Vardiya
        {
            get { return _vardiya; }
            set { SetPropertyValue<Vardiyalar>("Vardiya", ref _vardiya, value); }
        }

        [VisibleInLookupListView(true), VisibleInDetailView(false), 
        PersistentAlias("Iif(Vardiya is null, '', Vardiya.VardiyaKod)")]
        public string VardiyaKod 
        { 
            get
            {
                try
                {
                    if (!IsLoading)
                        return Convert.ToString(EvaluateAlias("VardiyaKod"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return "";                
            }
        }

        [VisibleInLookupListView(true), VisibleInDetailView(true), PersistentAlias("Iif(Vardiya is null, '', Vardiya.Aciklama)")]
        public string VardiyaAciklama 
        { 
            get 
            {
                try
                {
                    if (!IsLoading)
                        return Convert.ToString(EvaluateAlias("VardiyaAciklama"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return "";                  
            } 
        }
        #endregion

        #region Ambalaj

        [ModelDefault("AllowEdit", "False"), PersistentAlias("Iif(Ambalaj is null, 0, Ambalaj.Oid)"), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int AmbalajId 
        { 
            get 
            {
                try
                {
                    if (!IsLoading)
                        return Convert.ToInt32(EvaluateAlias("AmbalajId"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return 0;                 
            }
            set 
            {
                if (!IsLoading && value > 0)
                {
                    SetPropertyValue("Ambalaj", ref ambalaj, Session.GetObjectByKey<Ambalajlar>(value));
                    if (object.ReferenceEquals(this.ambalaj, null))
                        throw new MikrobarException(Lang.Mesaj(GenelMesajlar.ERR_6011, value), 6011);
                }
            }
        }

        private Ambalajlar ambalaj;
        [Association("Ambalaj-Operasyonlari")]
        [ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxSearchEditButtonPropertyEditor")]
        [Appearance("UretimOperasyonlari_Ambalaj", Enabled = false, Criteria = "Iif(Oid == -1, 0, 1) == 1", AppearanceItemType = "ViewItem", Context = "DetailView", TargetItems = "Ambalaj")]
        public Ambalajlar Ambalaj
        {
            get { return ambalaj; }
            set
            {
                SetPropertyValue<Ambalajlar>("Ambalaj", ref ambalaj, value);
            }
        }

        [VisibleInLookupListView(true), VisibleInDetailView(false), 
        PersistentAlias("Iif(Ambalaj is null, '', Ambalaj.Barkod)")]
        public string Barkod 
        { 
            get 
            {
                try
                {
                    if (!IsLoading)
                        return Convert.ToString(EvaluateAlias("Barkod"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return "";                   
            } 
        }

        [PersistentAlias("Iif(Ambalaj is null, '0', Ambalaj.Durum)")]
        public AmbalajDurumu AmbalajDurum
        {
            get 
            {
                try
                {
                    if (!IsLoading)
                        return (AmbalajDurumu)Convert.ToInt32(EvaluateAlias("AmbalajDurum"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return AmbalajDurumu.Kapali;
            }
        }

        [PersistentAlias("Ambalaj.AmbalajDetaylari.Sum(Kalan)")]
        public decimal AmbalajMiktar
        {
            get
            {
                try
                {
                    if (!IsLoading)
                        return Convert.ToDecimal(EvaluateAlias("AmbalajMiktar"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return 0;
            }
        }

        [ModelDefault("AllowEdit", "False"), Indexed("Durum", Unique = false), 
        PersistentAlias("Iif(AmbalajTur is null, 0, AmbalajTur.Oid)"), 
        VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int AmbalajTurId
        {
            get 
            {
                try
                {
                    if (!IsLoading)
                        return Convert.ToInt32(EvaluateAlias("AmbalajTurId"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return 0; 
            }
            set
            {
                SetPropertyValue("AmbalajTur", ref _ambalajTur, Session.GetObjectByKey<AmbalajTurleri>(value));
            }
        }

        private AmbalajTurleri _ambalajTur;
        [XmlIgnore(), ImmediatePostData, Browsable(false), NonPersistent]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        //[ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxSearchEditButtonPropertyEditor")]
        public AmbalajTurleri AmbalajTur
        {
            get
            {
                return _ambalajTur;
            }
            set
            {
                SetPropertyValue("AmbalajTur", ref _ambalajTur, value);
            }
        }

        #endregion

        #region Tabloldar       
        [DevExpress.Xpo.Aggregated, Association("UretimIscilikleri.UretimOperasyonlari.UretimOperasyon", typeof(UretimIscilikleri))]
        public XPCollection<UretimIscilikleri> Iscilikler
        {
            get { return GetCollection<UretimIscilikleri>("Iscilikler"); }
        }

        [Association("UretimOperasyon-Hurdalar", typeof(UretimHurdalari))]
        public XPCollection<UretimHurdalari> Hurdalar
        {
            get { return GetCollection<UretimHurdalari>("Hurdalar"); }
        }

        [DevExpress.Xpo.Aggregated, Association("UretimDuruslari-Duruslar", typeof(UretimDuruslari))]
        public XPCollection<UretimDuruslari> Duruslar
        {
            get { return GetCollection<UretimDuruslari>("Duruslar"); }
        }

        [Association("UretimMalzemeleri_OperasyonMalzemeleri")]
        public XPCollection<UretimMalzemeleri> Malzemeler
        {
            get { return GetCollection<UretimMalzemeleri>("Malzemeler"); }
        }
     
        [Association("UretimAletleri-Aletler")]
        public XPCollection<UretimAletleri> Aletler
        {
            get { return GetCollection<UretimAletleri>("Aletler"); }
        }

        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        [Association("AmbalajHareket-UretimOperasyon", typeof(AmbalajHareketleri)), VisibleInDetailView(true)]
        public XPCollection<AmbalajHareketleri> AmbalajHareket
        {
            get { return GetCollection<AmbalajHareketleri>("AmbalajHareket"); }
        }

        [XmlIgnore(), Association("Ambalajlar-Operasyonlari"),
        VisibleInDetailView(true), VisibleInListView(false)]
        public XPCollection<Ambalajlar> Ambalajlar
        {
            get { return GetCollection<Ambalajlar>("Ambalajlar"); }
        }

        [PersistentAlias("Ambalajlar[].Count")]
        public int AmbalajSayisi 
        { 
            get 
            {
                try
                {
                    if (!IsLoading && !IsSaving)
                        return Convert.ToInt32(EvaluateAlias("AmbalajSayisi"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return 0;                
            }
        }

        #endregion

        #region Erp Ref No
        [XafDisplayName("Kayıt Id (ERP)"), ModelDefault("AllowEdit", "False"),
        VisibleInListView(false), VisibleInLookupListView(false)]
        public int ReferansId { get; set; }

        [XafDisplayName("Kayıt Id 2 (ERP)"), ModelDefault("AllowEdit", "False"),
        VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int Referans2Id { get; set; } 
        #endregion                

        #region Field Class

        //public class Fields
        //{
        //    private Fields() { }
        //    [XmlIgnore()]
        //    public static OperandProperty IsEmriId
        //    {
        //        get { return new OperandProperty("IsEmriId"); }
        //    }
        //    [XmlIgnore()]
        //    public static OperandProperty IsEmriDetayId
        //    {
        //        get { return new OperandProperty("IsEmriDetayId"); }
        //    }
        //    [XmlIgnore()]
        //    public static OperandProperty Durum
        //    {
        //        get { return new OperandProperty("Durum"); }
        //    }
        //    [XmlIgnore()]
        //    public static OperandProperty NetMiktar
        //    {
        //        get { return new OperandProperty("NetMiktar"); }
        //    }
        //}

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
                try
                {
                    if (!IsLoading && !IsDeleted)
                    {
                        if (_olusturankullanici == null && this.Olusturan > 0)
                            this._olusturankullanici = this.Session.GetObjectByKey<Kullanicilar>(this.Olusturan);
                    }
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
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
                try
                {
                    if (!IsLoading && !IsDeleted)
                    {
                        if (_guncelleyenkullanici == null && this.Guncelleyen > 0)
                            this._guncelleyenkullanici = this.Session.GetObjectByKey<Kullanicilar>(this.Guncelleyen);
                    }
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
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

        protected KayitDurumu fDurum = KayitDurumu.Tamamlandi;
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public KayitDurumu Durum 
        {
            get { return fDurum; }
            set { SetPropertyValue<KayitDurumu>("Durum", ref fDurum, value); }
        }
        #endregion

        #region NoneBrowsable
        [Browsable(false), XmlIgnore(), NonPersistent]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool WebServis { get; set; }

        [Browsable(false), XmlIgnore(), NonPersistent]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool IptalKapali
        {
            get
            {
                try
                {
                    if (!IsLoading && !IsDeleted)
                    {
                        return (Durum == KayitDurumu.Iptal || Durum == KayitDurumu.Kapali);
                    }
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return false;
            }
        }

        [Browsable(false), XmlIgnore(), NonPersistent]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public decimal UrunHurdaMiktar
        {
            get
            {
                try
                {
                    if (!IsLoading && !IsDeleted)
                    {
                        return Hurdalar.Where(x => x.HurdaTipi == HurdaTipi.UrunHurdasi && x.Durum != KayitDurumu.Iptal).Sum(X => X.Miktar);
                    }
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return 0;
            }
        }

        [Browsable(false), XmlIgnore(), NonPersistent]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public decimal BilesenHurdaMiktar
        {
            get
            {
                try
                {
                    if (!IsLoading && !IsDeleted)
                    {
                        return Hurdalar.Where(x => x.HurdaTipi == HurdaTipi.MalzemeHurdasi && x.Durum != KayitDurumu.Iptal).Sum(X => X.Miktar);
                    }
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return 0;
            }
        }

        private List<Malzemeler> sourceMalzemeler = null;
        [Browsable(false), NonPersistent, XmlIgnore()]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public List<Malzemeler> SourceMalzemeler
        {
            get
            {
                try
                {
                    if (!IsLoading && !IsDeleted)
                    {
                        if (this.IsEmri != null && sourceMalzemeler == null)
                        {
                            XPQuery<IsEmriBilesenleri> xbilesenler = new XPQuery<IsEmriBilesenleri>(Session);
                            XPQuery<Malzemeler> xmalzemeler = new XPQuery<Malzemeler>(Session);

                            var mlzids = (from bm in xbilesenler
                                          where bm.IsEmriId == this.IsEmri.IsEmriId
                                          select bm.MalzemeId).ToArray();
                            sourceMalzemeler = (from m in xmalzemeler
                                                where mlzids.Contains(m.MalzemeId)
                                                select m).ToList();
                        }
                    }
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return sourceMalzemeler;
            }
        }


        private List<IsEmriBilesenleri> sourceBilesenler = null;
        [Browsable(false), NonPersistent, XmlIgnore()]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public List<IsEmriBilesenleri> SourceBilesenler
        {
            get
            {
                try
                {
                    if (!IsLoading && !IsDeleted)
                    {
                        if (this.IsEmri != null && sourceMalzemeler == null)
                        {
                            XPQuery<IsEmriBilesenleri> xbilesenler = new XPQuery<IsEmriBilesenleri>(Session, true);
                            sourceBilesenler = (from bm in xbilesenler
                                                where bm.IsEmriId == this.IsEmri.IsEmriId
                                                select bm).ToList();
                        }
                    }
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return sourceBilesenler;
            }
            set
            {
                sourceBilesenler = value;
            }
        } 
        #endregion

    }
}
