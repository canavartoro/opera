using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace MainDemo.Module.BusinessObjects
{
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions, XafDefaultProperty("IstasyonId"), NavigationItem(false),
     ImageName("BO_Organization"), Custom("DefaultListViewShowAutoFilterRow", "True")]
    public class UretimKaydi : XPLiteObject
    {
        [Key(AutoGenerate = true)]
        public int UretimId { get; set; }

        [NonPersistent, Custom("AllowEdit", "false"), VisibleInDetailView(true), VisibleInListView(false)]
        public int IstasyonId
        {
            get
            {
                if (this.Istasyon != null)
                    return this.Istasyon.IstasyonId;
                else
                    return 0;
            }
        }

        [Association(@"Istasyonlar.UretimKaydi_Istasyonlar"), ImmediatePostData]
        public Istasyonlar Istasyon 
        {
            get { return GetPropertyValue<Istasyonlar>("Istasyon"); ; }
            set { SetPropertyValue<Istasyonlar>("Istasyon", value); }
        }

        [NonPersistent, Custom("AllowEdit", "false"), VisibleInDetailView(true), VisibleInListView(false)]
        public string IstasyonAd
        {
            get
            {
                if (this.Istasyon != null)
                    return this.Istasyon.IstasyonAd;
                else
                    return string.Empty;
            }
        }

        private IsEmirleri fIsEmri;
        [NonPersistent, VisibleInListView(false)]
        public IsEmirleri IsEmri 
        { 
            get 
            {
                if (this.IsEmriOperasyon != null)
                {
                    if (fIsEmri != null)
                    {
                        if (fIsEmri.IsEmriId.Equals(this.IsEmriOperasyon.IsEmriId))
                            return fIsEmri;
                    }
                    fIsEmri = this.Session.GetObjectByKey<IsEmirleri>(this.IsEmriOperasyon.IsEmriId);
                }
                return fIsEmri;
            } 
        }

        [NonPersistent, Custom("AllowEdit", "false"), VisibleInDetailView(true), VisibleInListView(false)]
        public string IsEmriNo
        {
            get
            {
                if (this.IsEmri != null)
                    return this.IsEmri.IsEmriNo;
                else
                    return string.Empty;
            }
        }

        [NonPersistent, Custom("AllowEdit", "false"), VisibleInDetailView(true), VisibleInListView(false)]
        public string MalzemeKod
        {
            get
            {
                if (this.IsEmri != null)
                    return this.IsEmri.MalzemeKod;
                else
                    return string.Empty;
            }
        }

        [NonPersistent, Custom("AllowEdit", "false"), VisibleInDetailView(true), VisibleInListView(false)]
        public decimal PlanlananMiktar
        {
            get
            {
                if (this.IsEmri != null)
                    return this.IsEmri.PlanlananMiktar;
                else
                    return 0;
            }
        }

        [Appearance("Istasyon secilmedi", Enabled = false, Criteria = "IsNullOrEmpty(Istasyon)")]
        [Association(@"IsEmriDetaylari.UretimKaydi_IsEmriOperasyon"), ImmediatePostData]
        public IsEmriDetaylari IsEmriOperasyon
        {
            get { return GetPropertyValue<IsEmriDetaylari>("IsEmriOperasyon"); }
            set { SetPropertyValue<IsEmriDetaylari>("IsEmriOperasyon", value); }
        }

        [Appearance("Is Emri secilmedi", Enabled = false, Criteria = "IsNullOrEmpty(IsEmriOperasyon)")]
        [Association(@"Ambalajlar.UretimKaydi_Ambalaj"), ImmediatePostData]
        public Ambalajlar Ambalaj
        {
            get { return GetPropertyValue<Ambalajlar>("Ambalaj"); }
            set { SetPropertyValue<Ambalajlar>("Ambalaj", value); }
        }

        private V_Ambalajlar fV_Ambalaj;
        [NonPersistent, VisibleInListView(false)]
        public V_Ambalajlar V_Ambalaj
        {
            get
            {
                if (this.Ambalaj != null)
                {
                    if (fV_Ambalaj != null)
                    {
                        if (fIsEmri.IsEmriId.Equals(this.Ambalaj.IsEmriId))
                            return fV_Ambalaj;
                    }
                    fV_Ambalaj = this.Session.GetObjectByKey<V_Ambalajlar>(this.Ambalaj.Oid);
                }
                return fV_Ambalaj;
            }
        }

        [DbType(" DECIMAL(18,4) "), Custom("EditMask", "c")]
        public decimal UretilenMiktar { get; set; }

        public int Tolerans { get; set; }

        protected override void OnSaving()
        {
            try
            {
                if (IsDeleted == false && IsSaving)
                {
                    #region Yeni Uretim
                    if (UretimId < 1)
                    {
                        //MikWeb.UretimKaydiParam param = new MikWeb.UretimKaydiParam();
                    }
                    #endregion
                }
            }
            catch (Exception exc)
            {
            }
            finally
            { 
            }
            base.OnSaving();
        }

        public UretimKaydi() { }
        public UretimKaydi(Session session) : base(session) { }
    }
}
