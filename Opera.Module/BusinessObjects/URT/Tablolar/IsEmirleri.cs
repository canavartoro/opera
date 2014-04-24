using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Xml.Serialization;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using System.ComponentModel;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("IsEmriNo"), NavigationItem(false),
     ImageName("BO_Organization"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    [ReferansTablo("PRDT_WORDER_M", SistemTipi.WebErp, true)]
    [ReferansTablo("pub.erp_master", SistemTipi.Progress, false)]
    [ReferansTablo("TBLISEMRI", SqlSorgu = "SELECT * FROM Barset.V_IsEmirleri WHERE ( 1 = 1 ) ", SqlWhere = " AND IsEmriId = {0} ", SistemTipi = SistemTipi.Netsis, QueryType = QueryType.Yoksa)]
    public class IsEmirleri : XPBaseObject
    {
        [Key(AutoGenerate = false), VisibleInLookupListView(true)]
        public int IsEmriId { get; set; }

        [Size(DbSize.NoLenght), VisibleInLookupListView(true)]
        public string IsEmriNo { get; set; }

        [VisibleInLookupListView(true)]
        public int MalzemeId { get; set; }

        [Size(DbSize.KodLenght), VisibleInLookupListView(true)]
        public string MalzemeKod { get; set; }

        [VisibleInLookupListView(true)]
        public int BirimId { get; set; }

        [Size(DbSize.KodLenght), VisibleInLookupListView(true)]
        public string Birim { get; set; }

        [DbType(" DECIMAL(18,4) "), ModelDefault("DisplayFormat", "{0:0.000}"), VisibleInLookupListView(true)]
        public decimal PlanlananMiktar { get; set; }
        [DbType(" DECIMAL(18,4) "), ModelDefault("DisplayFormat", "{0:0.000}"), VisibleInLookupListView(true)]
        public decimal PlanlananBirimMiktar { get; set; }
        [DbType(" DECIMAL(18,4) "), ModelDefault("DisplayFormat", "{0:0.000}"), VisibleInLookupListView(true)]
        public decimal FireMiktari { get; set; }// Transfer ekranı için gerekli 


        public int IsYeriId { get; set; }

        public int IsMerkeziId { get; set; }

        public int FirmaId { get; set; }

        public int DigerId { get; set; }

        public int EntegreId { get; set; }

        public int ReceteId { get; set; }

        public int RotaId { get; set; }

        public int UrunRotaId { get; set; }

        public int IsEmriTipId { get; set; }

        [DbType(" DECIMAL(18,4) "), VisibleInLookupListView(true)]
        public decimal IsEmriMiktar { get; set; }

        [VisibleInLookupListView(true), ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}")]
        public DateTime AcilmaTarihi { get; set; }

        [VisibleInLookupListView(true), ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}")]
        public DateTime BaslangicTarihi { get; set; }

        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}")]
        public DateTime BitisTarihi { get; set; }

        [Size(DbSize.SaatLenght)]
        public string BaslangicSaat { get; set; }

        [Size(DbSize.SaatLenght)]
        public string BitisSaat { get; set; }

        [Size(DbSize.KodLenght)]
        public string OzelKod { get; set; }

        [Size(DbSize.KodLenght)]
        public string DigerKod1 { get; set; }

        [Size(DbSize.KodLenght)]
        public string TipKod { get; set; }

        [Size(DbSize.AciklamaLenght)]
        public string Aciklama { get; set; }

        public bool IsEmriDurum { get; set; }

        //[Persistent("RenkId"), Association(@"Renkler-IsEmirleri.RenkId")]
        public int RenkId { get; set; }

        [Size(DbSize.KodLenght), VisibleInLookupListView(true)]
        public string RenkKod { get; set; }

        [Size(DbSize.KodLenght), VisibleInLookupListView(true)]
        public string CizimNo { get; set; }

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

        [XmlIgnore(), Association(@"AmbalajHareketleri-IsEmirleri.IsEmri"), VisibleInDetailView(false), NoForeignKey]
        public XPCollection<AmbalajHareketleri> AmbalajHareketleri
        {
            get { return GetCollection<AmbalajHareketleri>(@"AmbalajHareketleri"); }
        }

        [XmlIgnore(), Association(@"UretimOperasyonlari.IsEmirleri.IsEmri"), NoForeignKey, VisibleInDetailView(false)]
        public XPCollection<UretimOperasyonlari> UretimOperasyonlari
        {
            get { return GetCollection<UretimOperasyonlari>(@"UretimOperasyonlari"); }
        }

        public IsEmirleri() { }
        public IsEmirleri(Session session) : base(session) { }
    }
}

/*
 web erp sql
  
 select CAST(WORDER_M_ID AS INTEGER) AS "IsEmriId", WORDER_NO AS "IsEmriNo", ITEM_ID AS "MalzemeId", UNIT_ID AS "BirimId", QTY AS "PlanlananMiktar", QTY_PRM AS "PlanlananBirimMiktar", WORDER_TYPE AS "IsEmriTipId" from PRDT_WORDER_M WHERE WORDER_STATUS = 2 AND CO_ID = #Firma# 
 
 */