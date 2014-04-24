using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Web;
using System.ComponentModel;
using System.Xml.Serialization;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [NavigationItem(false)]
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("AktiviteKod")]
    [ReferansTablo("PRDD_PRODUCT_ACTIVITY_TYPE", SistemTipi.WebErp, QueryType = QueryType.Yoksa,
      SqlSorgu = @"SELECT PRODUCT_ACTIVITY_TYPE_ID as ""AktiviteId"", PRODUCT_ACTIVITY_TYPE_CODE as ""AktiviteKod"",DESCRIPTION as ""AktiviteAd"", UNIT_ID as ""AktiviteBirimId"" , ACTIVITY_TYPE as ""AktiviteTipi"" FROM PRDD_PRODUCT_ACTIVITY_TYPE PA  WHERE PA.CO_ID = #Firma# AND PA.BRANCH_ID = #Isyeri#")]
    [ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    public class AktiviteTipleri : XPBaseObject
    {
        [Key(AutoGenerate = false), ModelDefault("DisplayFormat", "d")]
        public int AktiviteId { get; set; }
        [Size(DbSize.KodLenght), VisibleInLookupListView(true)]
        public string AktiviteKod { get; set; }
        [Size(DbSize.AdLenght), VisibleInLookupListView(true)]
        public string AktiviteAd { get; set; }
        [VisibleInLookupListView(true)]
        public AktiviteTipi AktiviteTipi { get; set; }

        [Size(DbSize.KodLenght), VisibleInLookupListView(true), ModelDefault("DisplayFormat", "d")]
        public int AktiviteBirimId { get; set; }


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
        }
        #endregion

        public AktiviteTipleri() { }
        public AktiviteTipleri(Session session) : base(session) { }
    }
}
