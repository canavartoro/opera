using DevExpress.ExpressApp.Model;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using System;
using System.Linq;
using DevExpress.ExpressApp.DC;
using System.ComponentModel;
using System.Xml.Serialization;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using System.Collections.Generic;
using DevExpress.ExpressApp.Web;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.IO;
//using C1.C1Excel;
using DevExpress.Data.Filtering;

namespace Mikrobar.Module.BusinessObjects
{
    //[Appearance(AppearanceItemType = "Action", TargetItems = "Save; SaveAndClose; SaveAndNew", Enabled = false, Criteria = "DurusId = 0", Context = "Any")]
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDisplayName("Sabit Durus Tanimlari"),
    XafDefaultProperty("DurusKod"), NavigationItem(false), ImageName("BO_Organization"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    public class SabitDuruslar : XPObject
    {
        protected SabitDurusTip _durusTip;
         [DevExpress.Xpo.Indexed(new string[] { "DurusId", "IstasyonId", "IsMerkeziId", "VardiyaId" }, Name = "IDX_I1_DurusTip", Unique = true)]
        [Description("Planli yada sabit durus"), XafDisplayName("Duruş Tipi"), ImmediatePostData]
        public SabitDurusTip DurusTip
        {
            get { return _durusTip; }
            set { SetPropertyValue<SabitDurusTip>("DurusTip", ref _durusTip, value); }
        }

        #region Durus Nedeni
        protected int _durusId = 0;
        [ModelDefault("AllowEdit", "false"), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int DurusId
        {
            get { return _durusId; }
            set { SetPropertyValue<int>("DurusId", ref _durusId, value); }
        }
        [ModelDefault("AllowEdit", "false"), VisibleInDetailView(false), Size(DbSize.KodLenght)]
        public string DurusKod { get; set; }
        private Duruslar _durus;
        [XmlIgnore(), NonPersistent, XafDisplayName("Duruş Kodu"), ImmediatePostData,
        VisibleInListView(false), VisibleInLookupListView(false)]
        [RuleRequiredField("Zorunlu alanlar bos birakilamaz (SabitDuruslar.Durus)", DefaultContexts.Save, "Bir durus kodu secin!")]
        public Duruslar Durus
        {
            get
            {
                if (_durus == null && this.DurusId > 0)
                    this._durus = this.Session.GetObjectByKey<Duruslar>(this.DurusId);
                return _durus;
            }
            set
            {
                DurusId = value != null ? value.DurusId : 0;
                SetPropertyValue("Durus", ref _durus, value);
            }
        }
        #endregion

        #region Istasyon&Ismerkezi
        private Istasyonlar _istasyon;
        [ModelDefault("AllowEdit", "false"), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int IstasyonId { get; set; }
        [ModelDefault("AllowEdit", "false"), VisibleInDetailView(false), Size(DbSize.KodLenght)]
        public string IstasyonKod { get; set; }
        [XmlIgnore(), NonPersistent, XafDisplayName("Istasyon Kodu"), ImmediatePostData,
        VisibleInListView(false), VisibleInLookupListView(false)]
        public Istasyonlar Istasyon
        {
            get
            {
                if (_istasyon == null && this.IstasyonId > 0)
                    this._istasyon = this.Session.GetObjectByKey<Istasyonlar>(this.IstasyonId);
                return _istasyon;
            }
            set
            {
                if (value != null)
                {
                    this.IstasyonId = value.IstasyonId;
                    this.IstasyonKod = value.IstasyonKod;
                    this.IsMerkeziId = value.IsMerkeziId;
                    this.IsMerkeziKod = value.IsMerkeziKod;
                    this.IsMerkezi = value.IsMerkezi;
                }
                else
                {
                    this.IstasyonId = 0;
                    this.IstasyonKod = string.Empty;
                }                
                SetPropertyValue("Istasyon", ref _istasyon, value);
            }
        }

        [PersistentAlias("Istasyonlar[].Count")]
        public int IstasyonSayisi { get { return Convert.ToInt32(EvaluateAlias("IstasyonSayisi")); } }

        [XmlIgnore(), Association("IstasyonlarSabitDuruslar", typeof(Istasyonlar)), NoForeignKey]
        public XPCollection Istasyonlar { get { return GetCollection("Istasyonlar"); } }

        private IsMerkezi _isMerkezi;
        [ModelDefault("AllowEdit", "false"), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int IsMerkeziId { get; set; }
        [ModelDefault("AllowEdit", "false"), VisibleInDetailView(false), Size(DbSize.KodLenght)]
        public string IsMerkeziKod { get; set; }
        [XmlIgnore(), NonPersistent, XafDisplayName("Is Merkezi Kod"), ImmediatePostData,
        VisibleInListView(false), VisibleInLookupListView(false), DataSourceProperty("IsMerkezleri")]
        public IsMerkezi IsMerkezi
        {
            get
            {
                if (_isMerkezi == null && this.IsMerkeziId > 0)
                {
                    this._isMerkezi = new IsMerkezi();
                    this.IsMerkezi.IsMerkeziId = this.IsMerkeziId;
                    this.IsMerkezi.IsMerkeziKod = this.IsMerkeziKod;
                }
                return _isMerkezi;
            }
            set
            {
                if (value != null)
                {
                    this.IsMerkeziId = value.IsMerkeziId;
                    this.IsMerkeziKod = value.IsMerkeziKod;
                }
                else
                {
                    this.IsMerkeziId = 0;
                    this.IsMerkeziKod = string.Empty;
                }
                SetPropertyValue("IsMerkezi", ref _isMerkezi, value);
            }
        }

        private List<IsMerkezi> _ismerkezleri;
        [Browsable(false), NonPersistent]
        public List<IsMerkezi> IsMerkezleri
        {
            get
            {
                if (_ismerkezleri == null)
                {
                    XPQuery<Istasyonlar> xistasyonlar = new XPQuery<Istasyonlar>(this.Session);
                    var query = from e in xistasyonlar
                                group e by new { e.IsMerkeziId, e.IsMerkeziKod, e.IsMerkeziAd } into ismer
                                select new { IsMerkeziId = ismer.Key.IsMerkeziId, IsMerkeziKod = ismer.Key.IsMerkeziKod, IsMerkeziAd = ismer.Key.IsMerkeziAd };
                    if (query != null && query.Count() > 0)
                    {
                        _ismerkezleri = new List<IsMerkezi>();
                        foreach (var x in query)
                        {
                            IsMerkezi ismerkez = new IsMerkezi(this.Session);
                            ismerkez.IsMerkeziId = x.IsMerkeziId;
                            ismerkez.IsMerkeziKod = x.IsMerkeziKod;
                            ismerkez.Aciklama = x.IsMerkeziAd;
                            _ismerkezleri.Add(ismerkez);
                        }
                        _ismerkezleri.TrimExcess();
                    }
                }
                return _ismerkezleri;
            }
        }

        #endregion

        #region Vardiya
        private Vardiyalar _vardiyalar;
        [ModelDefault("AllowEdit", "false"), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int VardiyaId { get; set; }
        [ModelDefault("AllowEdit", "false"), VisibleInDetailView(false), Size(DbSize.KodLenght)]
        public string VardiyaKod { get; set; }
        [XmlIgnore(), NonPersistent, XafDisplayName("Vardiya Kodu"), ImmediatePostData,
        VisibleInListView(false), VisibleInLookupListView(false)]
        public Vardiyalar Vardiya
        {
            get
            {
                if (_istasyon == null && this.VardiyaId > 0)
                    this._vardiyalar = this.Session.GetObjectByKey<Vardiyalar>(this.VardiyaId);
                return _vardiyalar;
            }
            set
            {
                if (value != null)
                {
                    this.VardiyaId = value.VardiyaId;
                    this.VardiyaKod = value.VardiyaKod;
                }
                else
                {
                    this.VardiyaId = 0;
                    this.VardiyaKod = string.Empty;
                }
                SetPropertyValue("Vardiya", ref _vardiyalar, value);
            }
        }
        #endregion

        #region Zamanlar<<TimeSpan>> Kullanılmalıydı
        [Index(1)]
        [ModelDefault("DisplayFormat", "{0:HH:mm}"), ModelDefault("EditMask", "t"), XafDisplayName("Başlangıç Zamanı")]
        //[ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxTimePropertyEditor")]
        //[ModelDefault("DisplayFormat", "{0:HH:mm}")]
        //[ModelDefaultAttribute("EditMaskType","Regex")]
        //[ModelDefaultAttribute("EditMask", @"(1?[1-9])|([12][0-4])")]
        [Appearance("Zamanlanmis durus icin baslangic.", Enabled = false, Criteria = "DurusTip != 'Zamanlanmis'", AppearanceItemType = "ViewItem", Context = "DetailView", TargetItems = "BaslangicTarihi")]
        public DateTime BaslangicTarihi { get; set; }

        [Index(2)]
        [ModelDefault("DisplayFormat", "{0:HH:mm}"), ModelDefault("EditMask", "t"), XafDisplayName("Bitiş Zamanı")]
        //[ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxTimePropertyEditor")]
        [Appearance("Zamanlanmis durus icin bitis.", Enabled = false, Criteria = "DurusTip != 'Zamanlanmis'", AppearanceItemType = "ViewItem", Context = "DetailView", TargetItems = "BitisTarihi")]
        public DateTime BitisTarihi { get; set; }

        [Appearance("Sabit durus icin sure belirleyin.", Enabled = false, Criteria = "DurusTip != 'Sabit'", AppearanceItemType = "ViewItem", Context = "DetailView", TargetItems = "DurusSuresi")]
        public int DurusSuresi { get; set; }

        //[Appearance("RuleMethod", AppearanceItemType = "ViewItem", TargetItems = "DurusSuresi", Context = "DetailView", Enabled = false)]
        //public bool RuleMethod()
        //{
        //    if (DurusTip != SabitDurusTip.Sabit)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
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
                if (!IsLoading && !IsSaving)
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

        private FileData file;
        [NonPersistent]
        [FileTypeFilter("Excel Dosyasi", 1, "*.xls")]
        [FileTypeFilter("Csv Dosyasi", 2, "*.csv", "*.txt")]
        [FileTypeFilter("Tum Dosyasi", 3, "*.*")]
        [ExpandObjectMembers(ExpandObjectMembers.Never), ImmediatePostData, VisibleInListView(false), VisibleInLookupListView(false)]
        public FileData File
        {
            get { return file; }
            set
            {
                SetPropertyValue("File", ref file, value);
            }
        }

        #region Butonlar
        [Action(Caption = "Excelden Al", TargetObjectsCriteria = "", ImageName = "Action_CloseAllWindows")]
        public void DataAl()
        {
            try
            {
                string filePath = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/" + file.FileName);
                using (FileStream SourceStream = System.IO.File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    file.SaveToStream(SourceStream);
                    SourceStream.Flush();
                    SourceStream.Close();
                }

                /*using (C1XLBook wb = new C1XLBook())
                {
                    wb.Load(filePath);
                    XLSheet sheet = wb.Sheets[0];
                    if (sheet.Rows.Count > 0)
                    {
                        using (UnitOfWork work = new DevExpress.Xpo.UnitOfWork())
                        {
                            for (int i = 1; i < sheet.Rows.Count; i++)
                            {
                                SabitDuruslar sb = new SabitDuruslar(work);
                                if (sheet[i, 1].Value != null)
                                    sb.DurusTip = (SabitDurusTip)Enum.Parse(typeof(SabitDurusTip), sheet[i, 1].Value.ToString());
                                if (sheet[i, 2].Value != null)
                                {
                                    Duruslar xdurus = work.FindObject<Duruslar>(CriteriaOperator.Parse(" DurusKod = ? ", sheet[i, 2].Value.ToString()));
                                    if (xdurus != null)
                                        sb.Durus = xdurus;
                                    else
                                        continue;
                                }
                                if (sheet[i, 3].Value != null)
                                {
                                    Istasyonlar xistasyon = work.FindObject<Istasyonlar>(CriteriaOperator.Parse(" IstasyonKod = ? ", sheet[i, 3].Value.ToString()));
                                    sb.Istasyon = xistasyon;
                                }
                                if (sheet[i, 4].Value != null)
                                    sb.IsMerkeziKod = sheet[i, 4].Value.ToString();
                                if (sheet[i, 5].Value != null)
                                {
                                    Vardiyalar xvardiya = work.FindObject<Vardiyalar>(CriteriaOperator.Parse(" IstasyonKod = ? ", sheet[i, 5].Value.ToString()));
                                    sb.Vardiya = xvardiya;
                                }
                                if (sheet[i, 6].Value != null)
                                    sb.BaslangicTarihi = DateTime.Parse(sheet[i, 6].Value.ToString());
                                if (sheet[i, 7].Value != null)
                                    sb.BitisTarihi = DateTime.Parse(sheet[i, 7].Value.ToString());
                                if (sheet[i, 8].Value != null)
                                    sb.DurusSuresi = int.Parse(sheet[i, 8].Value.ToString());                                
                                sb.Save();
                            }
                            work.CommitChanges();
                        }
                    }
                }
                try { System.IO.File.Delete(filePath); }
                catch { ;}*/
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        #endregion

        protected override void OnSaving()
        {
            try
            {
                SistemKullanicilari currentUser = DevExpress.ExpressApp.SecuritySystem.CurrentUser as Mikrobar.Module.BusinessObjects.SistemKullanicilari;

                if (this.IsDeleted == false)
                {
                    if (currentUser != null)
                    {
                        if (Oid < 1)
                        {
                            this.Olusturan = currentUser.KullaniciId;
                            this.OlusturmaTarihi = DateTime.Now;
                        }
                        else
                        {
                            this.Guncelleyen = currentUser.KullaniciId;
                            this.GuncellemeTarihi = DateTime.Now;
                        }

                        //if (WebWindow.CurrentRequestWindow != null)
                        //    WebWindow.CurrentRequestWindow.RegisterClientScript("asd", "alert('Operation Completed');");
                    }

                    #region Alanlari Setle
                    if (this.Durus != null)
                    {
                        this.DurusId = this.Durus.DurusId;
                        this.DurusKod = this.Durus.DurusKod;
                    }
                    if (this.Istasyon != null)
                    {
                        this.IstasyonId = this.Istasyon.IstasyonId;
                        this.IstasyonKod = this.Istasyon.IstasyonKod;
                    }
                    else
                    {
                        this.IstasyonId = 0;
                        this.IstasyonKod = string.Empty;
                    }
                    if (this.IsMerkezi != null)
                    {
                        this.IsMerkeziId = this.IsMerkezi.IsMerkeziId;
                        this.IsMerkeziKod = this.IsMerkezi.IsMerkeziKod;
                    }
                    else
                    {
                        this.IsMerkeziId = 0;
                        this.IsMerkeziKod = string.Empty; ;
                    }
                    if (this.Vardiya != null)
                    {
                        this.VardiyaId = this.Vardiya.VardiyaId;
                        this.VardiyaKod = this.Vardiya.VardiyaKod;
                    }
                    else
                    {
                        this.VardiyaId = 0;
                        this.VardiyaKod = string.Empty;
                    }
                    this.KaynakProgram = BusinessObjects.KaynakProgram.IsEmriUretim;
                    this.KaynakModul = GetType().Name;
                    #endregion


                    #region Kontrol
                    if (this.Oid < 1 && this.Olusturan < 1)
                        throw new Exception("Olsuturan bilgisi bos birakilamaz!");
                    if (this.Oid > 0 && this.Guncelleyen < 1)
                        throw new Exception("Guncelleyen bilgisi bos birakilamaz!");
                    if (this.DurusTip == SabitDurusTip.Sabit && this.DurusSuresi < 1)
                        throw new Exception("Sabit durus kaydinda durus suresi zorunludur!");
                    if (this.DurusTip == SabitDurusTip.Zamanlanmis && this.BaslangicTarihi.TimeOfDay == this.BitisTarihi.TimeOfDay)
                        throw new Exception("Planli durus kaydinda baslangic ve bitis saati ayni olamaz!");
                    if (this.DurusId < 1)
                        throw new Exception("Durus nedeni zorunludur!");
                    #endregion

                }
                base.OnSaving();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public SabitDuruslar() { }
        public SabitDuruslar(Session session) : base(session) { }
    }
}
