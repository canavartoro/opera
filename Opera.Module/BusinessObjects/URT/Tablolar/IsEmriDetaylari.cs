using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using System.Xml.Serialization;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("OperasyonKod"), NavigationItem(false),
     ImageName("BO_Organization"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    [ReferansTablo("PRDT_WORDER_OP_D",SistemTipi.WebErp, true)]
    [ReferansTablo("pub.erp_detay",SistemTipi.Progress, true)]
    [ReferansTablo("TBLISEMRI", SqlSorgu = "SELECT * FROM Barset.V_IsEmriDetaylari WHERE ( 1 = 1 ) ", SqlWhere = " AND IsEmriDetayId = {0} ", SistemTipi = SistemTipi.Netsis, QueryType = QueryType.Yoksa)]
    public class IsEmriDetaylari : XPBaseObject
    {
        [Key(false)]
        public int IsEmriDetayId { get; set; }

        [VisibleInLookupListView(true)]
        public int IsEmriId { get; set; }

        [Size(DbSize.NoLenght), VisibleInLookupListView(true)]
        public string IsEmriNo { get; set; }

        //[NonPersistent, VisibleInLookupListView(true), XmlIgnore(), DataSourceCriteria("IsEmriId = '@This.IsEmirleri.IsEmriId'")]
        //public IsEmirleri IsEmri { get; set; }

        [VisibleInLookupListView(true)]
        public int IstasyonId { get; set; }

        [Size(DbSize.KodLenght), VisibleInLookupListView(true)]
        public string IstasyonKod { get; set; }

        [VisibleInLookupListView(true)]
        public int OperasyonId { get; set; }

        [VisibleInLookupListView(true)]
        public int OperasyonNo { get; set; }

        [Size(DbSize.KodLenght), VisibleInLookupListView(true)]
        public string OperasyonKod { get; set; }
        
        public int DigerId { get; set; }

        public int EntegreId { get; set; }

        [Size(DbSize.KodLenght)]
        public string RotaKod { get; set; }
               
        [Size(DbSize.KodLenght)]
        public string OzelKod { get; set; }
        
        [Size(DbSize.KodLenght)]
        public string DigerKod1 { get; set; }

        public bool IlkOperasyon { get; set; }

        public bool SonOperasyon { get; set; }
        
        public bool TemelTas { get; set; }
        
        public bool HayaletOperasyon { get; set; }

        public string OncekiOperasyon { get; set; }
        public string SonrakiOperasyon { get; set; }

        [Description("Stok donusumu var/yok, varsa urun olusup tuketim olur yoksa yoktur")]
        public bool StokUpdate { get; set; }/*Web erp de degisti yeni alanlar eklenecek*/

        [Description("Fason operasyonu olup olmAdgi"), VisibleInLookupListView(true)]
        public bool Fason { get; set; }

        [VisibleInLookupListView(true)]
        public int SiraNo { get; set; }

        [XmlIgnore(), Association(@"UretimOperasyonlari.IsEmriDetaylari.OperasyonBilgisi"), NoForeignKey, VisibleInDetailView(false)]
        public XPCollection<UretimOperasyonlari> UretimOperasyonlari
        {
            get { return GetCollection<UretimOperasyonlari>(@"UretimOperasyonlari"); }
        }

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

        #region Butonlar
        [Action(Caption = "Guncelle", ImageName = "Action_Refresh", ToolTip = "Bilgileri guncelle..")]
        public void Entegrasyon()
        {
          
        }
        #endregion

        public IsEmriDetaylari() { }
        public IsEmriDetaylari(Session session) : base(session) { }
    }
}


/*
 web erp sql
 
 select CAST(WORDER_OP_D_ID AS INTEGER) AS "IsEmriDetayId", WORDER_M_ID AS "IsEmriId", WSTATION_ID AS "IstasyonId", OPERATION_ID AS "OperasyonId", OPERATION_NO AS "OperasyonNo", IS_FIRST_OPR AS "IlkOperasyon", IS_LAST_OPR AS "SonOperasyon", IS_MILESTONE AS "TemelTas", ISPHANTOM AS "HayaletOperasyon", PREVIOUS_OP_NO_LIST AS "OncekiOperasyon", NEXT_OP_NO_LIST AS "SonrakiOperasyon" from PRDT_WORDER_OP_D where CO_ID = #Firma# 
 */