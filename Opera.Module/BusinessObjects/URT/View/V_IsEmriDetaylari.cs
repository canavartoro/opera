using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;

namespace Mikrobar.Module.BusinessObjects
{
   //  [Persistent("V_IsEmriDetaylari")]
    [XafDefaultProperty("IsEmriNo"), XafDisplayName("Is Emri Detaylari"), NavigationItem(false)]
    public class V_IsEmriDetaylari : XPLiteObject
    {
        public int SatirSayisi { get; set; }
        [Key]
        public string IsEmriNo { get; set; } 
        public string StokKodu { get; set; }
        public string Aciklama1 { get; set; }
        public string Aciklama2 { get; set; }
        public string Aciklama3 { get; set; }
        public string Aciklama4 { get; set; }
        public string Aciklama5 { get; set; }
        public string Aciklama6 { get; set; }
        public string Aciklama7 { get; set; }
        public string Aciklama8 { get; set; } 

        public V_IsEmriDetaylari() { }
        public V_IsEmriDetaylari(Session session) : base(session) { }
    }
}
