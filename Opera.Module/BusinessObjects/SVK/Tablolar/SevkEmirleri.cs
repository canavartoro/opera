using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Web;
using Mikrobar;
using DevExpress.ExpressApp.Filtering;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("SevkEmriNo"),
     NavigationItem(false), ImageName("BO_Role"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    public class SevkEmirleri : XPBaseObject
    {
        #region Field

        [Indexed(Unique = true)]
        public string SevkEmriNo { get; set; }
        [Key]
        public int SevkEmriId { get; set; }

        public int SirketNo { get; set; }
        [Size(DbSize.KodLenght)]
        public string SirketKod { get; set; }
        [Size(DbSize.KodLenght)]
        public string IsyeriNo { get; set; }
        [Size(DbSize.KodLenght)]
        public string IsyerKod { get; set; }
        public int SatirNo { get; set; }

        #endregion

        #region CariKod

        [ModelDefault("AllowEdit", "False"), Indexed("Durum", Unique = false), PersistentAlias("Iif(Cari is null, 0, Cari.CariId)"), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int CariId
        {
            get { return Convert.ToInt32(EvaluateAlias("CariId")); }
            set
            {
                if (!IsLoading && value > 0)
                {
                    SetPropertyValue<Cariler>("Cari", ref _cari, Session.GetObjectByKey<Cariler>(value));
                }
            }
        }

        [ModelDefault("AllowEdit", "False"), VisibleInDetailView(false), Size(DbSize.KodLenght), SearchMemberOptions(SearchMemberMode.Include)]
        public string CariKod { get; set; }

        protected Cariler _cari;
        [XmlIgnore(), XafDisplayName("Cari Kodu"), ImmediatePostData,
        VisibleInListView(false), VisibleInLookupListView(false),
        Association(@"Cariler-SevkEmirleri"), Persistent("CariId"), NoForeignKey]
        [ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxSearchEditButtonPropertyEditor")]
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
            get { return Convert.ToString(EvaluateAlias("CariAd")); }
        }

        #endregion

        #region Date Field

        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy HH:mm}")]
        public DateTime SevkTarih { get; set; }
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy HH:mm}")]
        public DateTime TeslimTarih { get; set; }
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy HH:mm}")]
        public DateTime BelgeTarih { get; set; }

        #endregion

        #region Association

        [Association(@"SevkEmriDetaylari.SevkEmirleri"), XmlIgnore()]
        public XPCollection<SevkEmriDetaylari> SevkEmriDetaylari
        {
            get { return GetCollection<SevkEmriDetaylari>(@"SevkEmriDetaylari"); }
        }

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

        #region Butonlar

        [Action(Caption = "Guncelle", ImageName = "Action_Refresh", ToolTip = "Bilgileri guncelle..")]
        public void Entegrasyon()
        {
            ////try
            ////{
            ////    Mikrobar.Entegre.DataQuery query = new Mikrobar.Entegre.DataQuery(this.GetType());
            ////    object ret = query.Entegre();

            ////    //DevExpress.Xpo.DB.SelectedData data = this.Session.ExecuteSproc("sp_CariGuncelleERP");
            ////    if (WebWindow.CurrentRequestWindow != null)
            ////        WebWindow.CurrentRequestWindow.RegisterClientScript("tmm" + this.GetType().Name, "alert('İşlem tamamlandı. Sonuc:" + query.HataMesaji + "');");
            ////}
            ////catch (Exception exc)
            ////{
            ////    throw exc;
            ////}
        }

        #endregion

        #region Override

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                object sevkEmriNo = null;
                if (string.IsNullOrEmpty(SevkEmriNo))
                {
                    sevkEmriNo = Session.Evaluate<SevkEmirleri>(CriteriaOperator.Parse("Max(SevkEmriId)"), null);
                    this.SevkEmriNo = string.Format("SVK{0:00000000}", Convert.ToInt32(sevkEmriNo) + 1);
                }

                SistemKullanicilari currentUser = SecuritySystem.CurrentUser as SistemKullanicilari;
                if (this.SevkEmriId < 1)
                {
                    if (this.Olusturan < 1)
                        this.Olusturan = currentUser != null ? currentUser.KullaniciId : 0;

                    this.SevkEmriId = Convert.ToInt32(sevkEmriNo) + 1;
                    this.OlusturmaTarihi = DateTime.Now;
                    this.KaynakProgram = KaynakProgram.Sevkiyat;
                    this.KaynakModul = GetType().Name;
                }
                else
                {
                    if (this.Guncelleyen < 1)
                        this.Guncelleyen = currentUser != null ? currentUser.KullaniciId : 0;

                    this.GuncellemeTarihi = DateTime.Now;
                }

                ExcelRead();
            }
        }
        protected override void OnDeleted()
        {
            if (this.IsDeleted)
            {
                if (this.SevkEmriDetaylari.Count > 0)
                {
                    for (int i = 0; i < this.SevkEmriDetaylari.Count; )
                    {
                        this.SevkEmriDetaylari[i].Delete();
                    }
                }
            }

            base.OnDeleted();
        }

        #endregion

        #region Excel İçeri Alma

        private DevExpress.Persistent.BaseImpl.FileData _EmirDetaylari;
        [ExpandObjectMembers(ExpandObjectMembers.Never), XmlIgnore(), NonPersistent(), VisibleInListView(false), VisibleInLookupListView(false)]
        public DevExpress.Persistent.BaseImpl.FileData EmirDetaylari
        {
            get
            {
                return _EmirDetaylari;
            }
            set
            {
                SetPropertyValue("Excel", ref _EmirDetaylari, value);
            }
        }

        private void ExcelRead()
        {
            /*if (!object.ReferenceEquals(_EmirDetaylari, null) && _EmirDetaylari.IsEmpty == false)
            {
                try
                {
                    System.IO.MemoryStream mStream = new System.IO.MemoryStream();
                    _EmirDetaylari.SaveToStream(mStream);
                    string malzemeKod = string.Empty;
                    string birimKod = string.Empty;
                    XPQuery<Malzemeler> malzemeObj = new XPQuery<Malzemeler>(Session);
                    XPQuery<Birimler> birimObj = new XPQuery<Birimler>(Session);

                    string file = System.Web.HttpContext.Current.Server.MapPath("~/Apps/" + _EmirDetaylari.FileName);

                    using (System.IO.FileStream fs = System.IO.File.Create(file))
                    {
                        System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

                        using (System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fs, encoding))
                        {
                            bWriter.Write(mStream.ToArray());
                            bWriter.Close();
                        }
                    }

                    using (C1.C1Excel.C1XLBook book = new C1.C1Excel.C1XLBook())
                    {
                        book.Load(file);
                        C1.C1Excel.XLSheet sheet = book.Sheets[0];
                        Object SevkEmriDetayIdMax = Utility.Session.Evaluate<SevkEmriDetaylari>(CriteriaOperator.Parse("Max(SevkEmriDetayId)"), null);

                        SevkEmriDetaylari emirDetay = null;
                        for (int i = 1; i < sheet.Rows.Count; i++)
                        {
                            #region Satırın ilk kolonunda veri yoksa döngüden çık

                            if (string.IsNullOrEmpty(sheet[i, 0].Value.ToString()))
                                break;

                            #endregion

                            SevkEmriDetayIdMax = Convert.ToInt32(SevkEmriDetayIdMax) + 1;
                            emirDetay = new SevkEmriDetaylari(Session);
                            emirDetay.SevkEmriDetayId = (Int32)SevkEmriDetayIdMax;

                            #region Malzeme

                            malzemeKod = sheet[i, SevkExcelField.MalzemeKod].Value.ToString();
                            var mlz = (from m in malzemeObj
                                       where m.MalzemeKod == malzemeKod
                                       select m).FirstOrDefault();

                            if (mlz.IsNull())
                                throw new Exception("Malzeme Tanımı Bulunamadı. Kod: " + malzemeKod);

                            emirDetay.MalzemeId = mlz.MalzemeId;

                            #endregion

                            #region Birim

                            birimKod = sheet[i, SevkExcelField.Birim].Value.ToString();
                            var brm = (from b in birimObj
                                       where b.Birim == birimKod
                                       select b).FirstOrDefault();

                            if (brm.IsNull())
                                throw new Exception("Birim Tanımı Bulunamadı. Kod: " + birimKod);

                            emirDetay.BirimId = brm.BirimId;
                            emirDetay.Birim = brm.Birim;

                            #endregion

                            emirDetay.Miktar = Convert.ToDecimal(sheet[i, SevkExcelField.Miktar].Value);
                            emirDetay.SevkMiktar = Convert.ToDecimal(sheet[i, SevkExcelField.Miktar].Value);

                            emirDetay.Olusturan = this.Olusturan;
                            emirDetay.OlusturmaTarihi = DateTime.Now;
                            emirDetay.KaynakProgram = KaynakProgram.Sevkiyat;
                            emirDetay.KaynakModul = GetType().Name;

                            this.SevkEmriDetaylari.Add(emirDetay);
                        }

                        book.Clear();
                    }

                    _EmirDetaylari = null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }*/
        }

        #endregion

        public SevkEmirleri() { }
        public SevkEmirleri(Session session) : base(session) { }
    }

    public abstract class SevkExcelField
    {
        public const int MalzemeKod = 0;
        public const int Miktar = 1;
        public const int Birim = 2;
    }
}
