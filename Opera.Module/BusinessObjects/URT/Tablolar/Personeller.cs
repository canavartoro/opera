using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Xml.Serialization;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("PersonelKod"), NavigationItem(false),
     ImageName("BO_Organization"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    [ReferansTablo("PRDD_WSTATION_EMPLOYEES", SistemTipi.WebErp, false)]
    [ReferansTablo("pub.epersonel", SistemTipi.Progress, false)]
    [ReferansTablo("MRPISCI", SqlSorgu = "SELECT * FROM Barset.V_Personeller WITH (NOLOCK) WHERE ( 1 = 1 ) ", SqlWhere = "AND PersonelId = {0}", SistemTipi = SistemTipi.Netsis, QueryType = QueryType.Yoksa)]
    public class Personeller : XPBaseObject
    {
        [Key(AutoGenerate = false)]
        public int PersonelId { get; set; }
        [Size(DbSize.KodLenght), VisibleInLookupListView(true)]
        public string PersonelKod { get; set; }
        [Size(DbSize.AdLenght), VisibleInLookupListView(false), VisibleInListView(false)]
        public string PersonelAd { get; set; }
        [Size(DbSize.AdLenght), VisibleInLookupListView(false), VisibleInListView(false)]
        public string PersonelSoyAd { get; set; }

        [VisibleInLookupListView(true), VisibleInDetailView(false), PersistentAlias("Trim(Concat(Iif(IsNullOrEmpty(PersonelAd), '', Concat(PersonelAd, ' ')), Iif(IsNullOrEmpty(PersonelSoyAd), '', PersonelSoyAd)))")]
        public string PersonelIsim { get { return Convert.ToString(EvaluateAlias("PersonelIsim")); } }

        [Size(DbSize.NoLenght), VisibleInLookupListView(true)]
        public string DepartmanKod { get; set; }

        [Size(DbSize.NoLenght), XmlIgnore(), VisibleInLookupListView(true)]
        public string GorevKod { get; set; }

        [Size(DbSize.NoLenght), XmlIgnore(), VisibleInLookupListView(true)]
        public string IsyeriKod { get; set; }

        [Size(DbSize.AciklamaLenght), XmlIgnore(), VisibleInLookupListView(true)]
        public string Aciklama { get; set; }

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

        [XmlIgnore()]
        [Association("UretimIscilikleri.Personeller.Personel"), NoForeignKey, VisibleInDetailView(false)]
        public XPCollection<UretimIscilikleri> UretimOperasyonlari
        {
            get { return GetCollection<UretimIscilikleri>("UretimOperasyonlari"); }
        }

        [XmlIgnore()]
        [Association(@"Personeller.IstasyonIscilikleri.PersonelId"), NoForeignKey, VisibleInDetailView(false)]
        public XPCollection<IstasyonIscilikleri> IstasyonIscilikleri
        {
            get { return GetCollection<IstasyonIscilikleri>("IstasyonIscilikleri"); }
        }

        #region Butonlar
        [Action(Caption = "Guncelle", ImageName = "Action_Refresh", ToolTip = "Bilgileri guncelle..")]
        public void Entegrasyon()
        {
           
        }
        #endregion

        public Personeller() { }
        public Personeller(Session session) : base(session) { }

    }
}

/*sql sorgusu
 SELECT PRD_EMPLOYEE_ID AS "PersonelId", CITIZENSHIP_NO AS "PersonelKod", EMP_NAME AS "PersonelAd", EMP_SURNAME AS "PersonelSoyAd", EMPLOYEE_TASK_TYPE_ID AS "DepartmanKod", IS_OUTSOURCE_EMPLOYEE AS "GorevKod"  FROM PRDD_EMPLOYEE WHERE CO_ID = #Firma# AND BRANCH_ID = #Isyeri# ORDER BY EMPLOYEE_TASK_TYPE_ID, EMP_NAME 
 */
