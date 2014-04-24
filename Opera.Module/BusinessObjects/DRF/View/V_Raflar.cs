using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [Persistent("V_Raflar"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    public class V_Raflar : XPLiteObject
    {
        public string DepoKod { get; set; }
        public string DepoAd { get; set; }
        public string DepoBarkod { get; set; }
        public string RafKod { get; set; }
        public string RafAd { get; set; }
        public string RafBarkod { get; set; }
        public bool Sayim { get; set; }
        public bool EksiStok { get; set; }
        public bool Rapor { get; set; }
        public int Seviye { get; set; }
        public int HiyerArsi { get; set; }
        public bool Sevkiyat { get; set; }
        public bool IstasyonMu { get; set; }
        public bool Hurda { get; set; }
        public bool Fason { get; set; }
        public int IsyeriId { get; set; }
        public string IsyeriKod { get; set; }

        [Key]
        public int RafId { get; set; }
        public int DepoId { get; set; }
        
       

        public V_Raflar(Session session) : base(session) { }
        public V_Raflar() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}


/*
 ALTER VIEW [dbo].[V_Raflar]
AS
SELECT     dbo.Depolar.DepoKod, dbo.Depolar.DepoAd, dbo.Depolar.Barkod AS DepoBarkod, dbo.Raflar.RafKod, dbo.Raflar.Aciklama AS RafAd, dbo.Raflar.Barkod AS RafBarkod, 
                      dbo.Raflar.Sayim, dbo.Raflar.EksiStok, dbo.Raflar.Rapor, dbo.Raflar.Seviye, dbo.Raflar.HiyerArsi, dbo.Raflar.Sevkiyat, dbo.Raflar.IstasyonMu, dbo.Depolar.Hurda, 
                      dbo.Depolar.Fason, dbo.Raflar.RafId, dbo.Raflar.DepoId,  dbo.Raflar.IsyeriKod,  dbo.Raflar.IsyeriId
FROM         dbo.Raflar LEFT OUTER JOIN
                      dbo.Depolar ON dbo.Raflar.DepoId = dbo.Depolar.DepoId

GO
 */