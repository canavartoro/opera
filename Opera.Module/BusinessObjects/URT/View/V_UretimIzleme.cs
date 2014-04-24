using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [Persistent("V_UretimIzleme"), ImageName("BO_Organization")]
    [ModelDefault("DefaultListViewShowAutoFilterRow", "True"), NavigationItem(false)]
    [XafDefaultProperty("UretimId"), XafDisplayName("Uretim Saha Durumu")]
    public class V_UretimIzleme : XPLiteObject    
    {
        [Key]
        public int KayitId { get; set; }
        public string IstasyonKod { get; set; }
        public string IstasyonAd { get; set; }
        public string IsMerkeziKod { get; set; }
        public string IsMerkeziAd { get; set; }
        public int IstasyonSayisi { get; set; }
        public string Durum { get; set; }
        public string IsEmriNo { get; set; }
        public string MalzemeKod { get; set; }
        public string MalzemeAd { get; set; }
        [DbType(" DECIMAL(18,4) ")]
        public decimal PlanlananMiktar { get; set; }
        [DbType(" DECIMAL(18,4) ")]
        public decimal UretilenMiktar { get; set; }
        [DbType(" DECIMAL(18,4) ")]
        public decimal KalanMiktar { get; set; }
        public string Vardiya { get; set; }
        public string Operator { get; set; }
        public string Performans { get; set; }

        public V_UretimIzleme() { }
        public V_UretimIzleme(Session session) : base(session) { }
    }
}
