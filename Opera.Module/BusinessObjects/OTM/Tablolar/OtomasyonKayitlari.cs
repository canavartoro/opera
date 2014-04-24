using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using DevExpress.Xpo;
using Mikrobar.Module.BusinessObjects;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Xml.Serialization;
using DevExpress.ExpressApp.DC;

namespace Mikrobar.Module.BusinessObjects
{
    [OptimisticLocking(false), DeferredDeletion(false),
     DebuggerDisplay(" Istasyon = {Istasyon},  Miktar = {Miktar}, Durum = {Durum}")]
    public class OtomasyonKayitlari : XPObject
    {
        public int IstasyonId { get; set; }
        public string Istasyon { get; set; }

        [PersistentAlias("Iif(CihazDetay is null, 0, CihazDetay.Oid)")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int CihazDetayId
        {
            get
            {
                try
                {
                    if (!IsLoading)
                        return Convert.ToInt32(EvaluateAlias("CihazDetayId"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return 0;
            }
            set
            {
                if (!IsLoading && !IsSaving)
                {
                    SetPropertyValue("CihazDetay", ref fCihazDetay, Session.GetObjectByKey<CihazDetaylari>(value));
                }
            }
        }

        //[PersistentAlias("Iif(CihazDetay is null, 0, CihazDetay.VeriTuru)")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int CihazUretimTur
        {
            get;

            set;
        }
        

        protected CihazDetaylari fCihazDetay;
        [XmlIgnore(), XafDisplayName("Cihaz Detayi"), ImmediatePostData,
        VisibleInListView(false), VisibleInLookupListView(false)]
        [Association(@"OtomasyonKayitlari.CihazDetaylari.CihazDetay"), NoForeignKey, Persistent("CihazDetayId")]
        [ModelDefault("PropertyEditorType", "Mikrobar.Module.BusinessObjects.ASPxSearchEditButtonPropertyEditor")]
        public CihazDetaylari CihazDetay
        {
            get
            {
                return fCihazDetay;
                
            }
            set
            {
                SetPropertyValue("CihazDetay", ref fCihazDetay, value);
            }
        }

        



        [DbType(" DECIMAL(18,4) ")]
        public decimal Miktar { get; set; }
        [DbType(" DECIMAL(18,4) ")]
        public decimal Miktar2 { get; set; }
        public OtomasyonDurumu Durum { get; set; }
        public IstasyonDurumu IstasyonDurum { get; set; }
        [Size(DbSize.Mesaj)]
        public string Aciklama { get; set; }


        public OtomasyonKayitlari() { }
        public OtomasyonKayitlari(Session session) : base(session) { }
    }
}
