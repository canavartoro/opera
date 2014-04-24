using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using System.Xml.Serialization;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Web;
using System.IO;
using C1.C1Excel;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [ReferansTablo(TabloAdi = "INVT_ITEM_PICKING_M", SistemTipi=SistemTipi.WebErp)]
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("EmirId"),
     NavigationItem(false), ImageName("BO_Role"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    public class MalHazirlama : XPBaseObject
    {
        #region Excel
        [XmlIgnore]
        private FileData file;
        [NonPersistent]
        [XmlIgnore, ExpandObjectMembers(ExpandObjectMembers.Never), ImmediatePostData, VisibleInListView(false), VisibleInLookupListView(false)]
        public FileData File
        {
            get
            {
                if (file != null && file.FileName != null && file.FileName != "")
                {
                    string[] FileName = file.FileName.Split('.');
                    if (FileName[FileName.Length - 1] != "xls" && FileName[FileName.Length - 1] != "xlsx")
                    {
                        if (WebWindow.CurrentRequestWindow != null)
                            WebWindow.CurrentRequestWindow.RegisterClientScript("tamam" + this.GetType().Name, "alert('Secilen dosya excel dosyası olmalıdır.');");
                        SetPropertyValue("File", ref file, null);
                    }
                    else if (FileName[FileName.Length - 1] == "xls" || FileName[FileName.Length - 1] == "xlsx")
                    {
                        string filePath = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/" + file.FileName);
                        using (FileStream SourceStream = System.IO.File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        {
                            file.SaveToStream(SourceStream);
                            SourceStream.Flush();
                            SourceStream.Close();
                        }
                        using (C1XLBook wb = new C1XLBook())
                        {
                            wb.Load(filePath);
                            XLSheet sheet = wb.Sheets[0];
                            if (sheet.Rows.Count > 0)
                            {
                                using (UnitOfWork work = new DevExpress.Xpo.UnitOfWork())
                                {
                                    SevkEmirleri SevkEmir = new SevkEmirleri(work);
                                    SevkEmriDetaylari SevkEmirDetay = new SevkEmriDetaylari(work);
                                    for (int i = 1; i < sheet.Rows.Count; i++)
                                    {
                                        if (sheet[i, 1].Value != null)//palet no
                                        {

                                        }

                                        if (sheet[i, 2].Value != null)//palet bilgisi
                                        {

                                        }
                                        if (sheet[i, 3].Value != null)//Ürün miktarı
                                        {
                                            SevkEmirDetay.SevkMiktar = Convert.ToDecimal(sheet[i, 3].Value);

                                        }
                                        if (sheet[i, 4].Value != null)//birim
                                        {
                                            Birimler Birim = work.FindObject<Birimler>(CriteriaOperator.Parse("BirimKod=?", sheet[i, 4].Value));
                                            SevkEmirDetay.BirimId = Birim.BirimId;
                                        }

                                        if (sheet[i, 6].Value != null)//barkod
                                        {
                                            V_Ambalajlar Amb = work.FindObject<V_Ambalajlar>(CriteriaOperator.Parse("Barkod=?", sheet[i, 6].Value));
                                            SevkEmirDetay.MalzemeId = Amb.MalzemeId;
                                            SevkEmirDetay.MalzemeKod = Amb.MalzemeKod;
                                            SevkEmirDetay.Miktar = Amb.Miktar;
                                            SevkEmirDetay.DepoId = Amb.DepoId;
                                            SevkEmirDetay.DepoKod = Amb.DepoKod;
                                            SevkEmir.CariId = Amb.CariId;
                                            SevkEmir.CariKod = Amb.CariKod;
                                            SevkEmir.Durum = KayitDurumu.Yeni;
                                        }
                                        if (sheet[i, 7].Value != null)// net agırlık
                                        {

                                        }

                                        if (sheet[i, 8].Value != null)//brüt agırlık
                                        {

                                        }
                                        if (sheet[i, 10].Value != null)//palet en
                                        {

                                        }
                                        if (sheet[i, 11].Value != null)//palet boy
                                        {

                                        }
                                        if (sheet[i, 12].Value != null)//palet yükseklik
                                        {

                                        }
                                        if (sheet[i, 13].Value != null)//siparis no
                                        {
                                            SevkEmirDetay.SiparisId = Convert.ToInt32(sheet[i, 13].Value);

                                        }
                                        SevkEmirDetay.SevkEmriId = SevkEmir.SevkEmriId;
                                        SevkEmirDetay.Save();
                                    }
                                    SevkEmir.SevkEmriDetaylari.Add(SevkEmirDetay);
                                    work.CommitChanges();
                                }
                            }
                        }
                        try { System.IO.File.Delete(filePath); }
                        catch { ;}

                    }
                }
                return file;
            }
            set
            {
                SetPropertyValue("File", ref file, value);
            }
        }

        #endregion
        [Key(AutoGenerate=false)]
        public int EmirId { get; set; }
        public int CariId { get; set; }  
        [Size(DbSize.NoLenght)]
        public string CariKod { get; set; }               

        [Size(DbSize.NoLenght)]
        public string BelgeNo { get; set; }
        public DateTime BelgeTarihi { get; set; }
        
        [Size(DbSize.AciklamaLenght)]
        public string Aciklama1 { get; set; }
        [Size(DbSize.AciklamaLenght)]
        public string Aciklama2 { get; set; }
        [Size(DbSize.AciklamaLenght)]
        public string Aciklama3 { get; set; }
        [Size(DbSize.AciklamaLenght)]
        public string Aciklama4 { get; set; }

        public int KullaniciRefId { get; set; }
        public int HareketId { get; set; }
             
        public int ReferansId { get; set; }

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

        #region iliskiler
        [Association(@"MalHazirlamaDetaylari.MalHazirlamaEmri_MalHazirlama"), System.Xml.Serialization.XmlIgnore()]
        public XPCollection<MalHazirlamaDetaylari> MalHazirlamaEmirDetaylari
        {
            get { return GetCollection<MalHazirlamaDetaylari>(@"MalHazirlamaEmirDetaylari"); }
        }
        #endregion
   
        public MalHazirlama() { }
        public MalHazirlama(Session session) : base(session) { }



    }
}





