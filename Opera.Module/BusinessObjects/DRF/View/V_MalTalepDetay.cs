using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

namespace Mikrobar.Module.BusinessObjects
{
    [NonPersistent, NavigationItem(false)]
    public class V_MalTalepDetay : XPLiteObject
    {
        [Key]
        public int MalTalepDetayId { get; set; }
        public int MalzemeId { get; set; }
        public string MalzemeKod { get; set; }
        public string MalzemeAd { get; set; }
        public int DepoId { get; set; }
        public string DepoKod { get; set; }
        public string Birim { get; set; }
        public int BirimId { get; set; }
        public int Satir { get; set; }
        public decimal Miktar { get; set; }
        public decimal Okunan { get; set; }
        public decimal Kalan { get; set; }

        public V_MalTalepDetay() { }
        public V_MalTalepDetay(Session session) : base(session) { }


    }

   //public class V_MalTalepDetay: XPLiteObject
   // {
   //    public string MalzemeKod { get; set; }
   //    public string MalzemeAd { get; set; }
   //    public int Miktar { get; set; }
   //    public int Miktar2 { get; set; }
   //    public string Birim { get; set; }
   //    public string IsemriNo { get; set; }
   //    [Key]
   //    public int MalTalepDetayId { get; set; }
   //    public int MalTalep { get; set; }
   //    public int Okunan { get; set; }
   //    public int Kalan { get; set; }
   //    public DateTime OlusturmaTarihi { get; set; }

   //    public int MalzemeId { get; set; }

   //    public V_MalTalepDetay() { }
   //    public V_MalTalepDetay(Session session) : base(session) { }

       
   // }
}
/*
 SELECT     dbo.Malzemeler.MalzemeKod, dbo.Malzemeler.MalzemeAd, dbo.MalTalepDetaylari.Miktar, dbo.MalTalepDetaylari.Miktar2, dbo.Malzemeler.Birim, 
                      dbo.MalTalepDetaylari.IsemriNo, dbo.MalTalepDetaylari.MalTalep, dbo.MalTalepDetaylari.Okunan, dbo.Malzemeler.OlusturmaTarihi, dbo.MalTalepDetaylari.Okunan2, 
                      dbo.MalTalepDetaylari.Kalan
FROM         dbo.Malzemeler RIGHT OUTER JOIN
                      dbo.MalTalepDetaylari ON dbo.Malzemeler.MalzemeId = dbo.MalTalepDetaylari.MalzemeId
 
 
 
 */