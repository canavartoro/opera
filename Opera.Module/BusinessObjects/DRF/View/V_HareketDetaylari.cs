using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;

namespace Mikrobar.Module.BusinessObjects
{
       [Persistent("V_HareketDetaylari")]
       public class V_HareketDetaylari : XPLiteObject
    {
            [Key]
            public int OID { get; set; }
            public int AmbalajHareket { get; set; } //BelgeNo
            public int Durum { get; set; } //BelgeNo
            public int HedefDepo{ get; set; } 
            public string MalzemeKod  { get; set; }
            public string MalzemeAd{ get; set; }
            public string Miktar { get; set; }
            public string Birim  { get; set; }
            public DateTime OlusturmaTarihi  { get; set; }
            public string Barkod  { get; set; }

         




        public V_HareketDetaylari() { }
        public V_HareketDetaylari(Session session) : base(session) { }
    }
}


/*
 CREATE VIEW [dbo].[V_HareketDetaylari]
AS
SELECT     dbo.AmbalajHareketDetaylari.OID, dbo.AmbalajHareketDetaylari.AmbalajHareket, dbo.AmbalajHareketDetaylari.MalzemeKod, dbo.Malzemeler.MalzemeAd, 
                      dbo.AmbalajDetaylari.Miktar, dbo.AmbalajHareketDetaylari.Birim, dbo.Ambalajlar.Barkod, dbo.Ambalajlar.OlusturmaTarihi, dbo.AmbalajHareketDetaylari.HedefDepo, 
                      dbo.AmbalajHareketDetaylari.Durum
FROM         dbo.AmbalajHareketDetaylari INNER JOIN
                      dbo.Ambalajlar ON dbo.AmbalajHareketDetaylari.Ambalaj = dbo.Ambalajlar.OID INNER JOIN
                      dbo.Malzemeler ON dbo.AmbalajHareketDetaylari.MalzemeKod = dbo.Malzemeler.MalzemeKod INNER JOIN
                      dbo.AmbalajDetaylari ON dbo.Ambalajlar.OID = dbo.AmbalajDetaylari.Ambalaj
 */