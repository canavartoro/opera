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
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("VardiyaKod"), NavigationItem(false),
    ImageName("BO_Organization"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    [ReferansTablo("UYUMSOFT.PRDD_SHIFTS", SistemTipi.WebErp, false)]
    [ReferansTablo("pub.uvardiya", SistemTipi.Progress, false)]
    public class Vardiyalar : XPBaseObject
    {
        [Key(AutoGenerate = false)]
        public int VardiyaId { get; set; }
        [Size(DbSize.KodLenght), Indexed, VisibleInLookupListView(true)]
        public string VardiyaKod { get; set; }
        [Size(DbSize.AciklamaLenght), VisibleInLookupListView(true)]
        public string Aciklama { get; set; }
        [Size(DbSize.KodLenght), Indexed, VisibleInLookupListView(true)]
        public string TakvimKod { get; set; }
        [VisibleInLookupListView(true)]
        public int Baslangic { get; set; }
        [VisibleInLookupListView(true)]
        public int Bitis { get; set; }
        [VisibleInLookupListView(true)]
        public int NetSure { get; set; }
        [Size(DbSize.SaatLenght), VisibleInLookupListView(true)]
        public string Baslangic2 { get; set; }
        [Size(DbSize.SaatLenght), VisibleInLookupListView(true)]
        public string Bitis2 { get; set; }
        [VisibleInLookupListView(true)]
        public int BrutSure { get; set; }

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

        [XmlIgnore(), Association("UretimOperasyonlari.Vardiya_Vardiyalar"), NoForeignKey,
        VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public XPCollection<UretimOperasyonlari> UretimOperasyonlari
        {
            get { return GetCollection<UretimOperasyonlari>("UretimOperasyonlari"); }
        }


        #region Butonlar
        [Action(Caption = "Guncelle", ImageName = "Action_Refresh", ToolTip = "Bilgileri guncelle..")]
        public void Entegrasyon()
        {
        }
        #endregion

        public Vardiyalar() { }
        public Vardiyalar(Session session) : base(session) { }
    }

    /*
     Trigger
     ALTER TRIGGER [dbo].[t_VardiyaSaatBul] 
   ON  [dbo].[Vardiyalar]
   AFTER INSERT, UPDATE
AS 
BEGIN
	SET NOCOUNT ON;
	UPDATE V SET V.VardiyaKod = S.SHIFTS_CODE, V.Aciklama = S.DESCRIPTION, V.Baslangic2 = CONVERT(NVARCHAR(5), S.START_DATE, 108), V.Bitis2 = CONVERT(NVARCHAR(5), S.END_DATE, 108) FROM dbo.Vardiyalar V INNER JOIN [UYUMDB]..[UYUMSOFT].[PRDD_SHIFTS] S ON V.VardiyaId = S.SHIFTS_ID
	WHERE V.VardiyaId IN ( SELECT VardiyaId FROM INSERTED NOLOCK)
	--SELECT SHIFTS_ID, DESCRIPTION, SHIFTS_CODE, START_DATE, END_DATE FROM  [UYUMDB]..[UYUMSOFT].[PRDD_SHIFTS] WHERE CO_ID = 191 AND BRANCH_ID = 1010
END
--Hidromas icin yapildigecici senfoni icine eklenecek      
     */
}
