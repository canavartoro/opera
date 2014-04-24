using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.DC;
using DevExpress.Xpo;

namespace Mikrobar.Module.BusinessObjects
{
    [DebuggerDisplay(" MalzemeBarkodId= {MalzemeBarkodId}, MalzemeId = {MalzemeId}, BirimId = {BirimId}")]
    [Persistent("V_MalzemeBarkodlari"), XafDefaultProperty("Barkod")]
    public class V_MalzemeBarkodlari : XPLiteObject
    {
        public int MalzemeId{ get; set; }
        public string MalzemeKod{ get; set; }
        public string MalzemeAd{ get; set; }
        public string MalzemeAd2{ get; set; }
        [Key]
        public string Barkod{ get; set; }
        public int BirimId{ get; set; }
        public string Birim{ get; set; }
        public decimal Miktar{ get; set; }
        public int SiraNo{ get; set; }
        
        public int MalzemeBarkodId{ get; set; }
        public int MalzemeBirimId{ get; set; } 
        public decimal KdvOran{ get; set; }
        public decimal MinStok{ get; set; }
        public decimal MaxStok{ get; set; } 
        public HammaddeTakip HammaddeTakip{ get; set; }
        public decimal FazlaSipMiktar{ get; set; }
        public MalzemeTip Tip{ get; set; }
        public string TipKod{ get; set; }
        public int MalzemeGrupId { get; set; }
        public int MalzemeGrup2Id { get; set; }

        public V_MalzemeBarkodlari(Session session) : base(session) { }
        public V_MalzemeBarkodlari() : base(Session.DefaultSession) { }
    }
}


/*  ORACLE
 CREATE OR REPLACE VIEW V_MalzemeBarkodlari AS
SELECT     
"Malzemeler"."MalzemeId",
"Malzemeler"."MalzemeKod",
"Malzemeler"."MalzemeAd",
"Malzemeler"."MalzemeAd2",
nvl("MalzemeBarkodlari"."Barkod", "Malzemeler"."MalzemeKod") as "Barkod",
"MalzemeBarkodlari"."BirimId" ,
"Birimler"."Birim",
"MalzemeBarkodlari"."Miktar",
"MalzemeBarkodlari"."SiraNo",
"MalzemeBarkodlari"."MalzemeBarkodId", 
"Malzemeler"."BirimId"  AS "MalzemeBirimId", 
"Malzemeler"."KdvOran", 
"Malzemeler"."MinStok", 
"Malzemeler"."MaxStok", 
"Malzemeler"."HammaddeTakip", 
"Malzemeler"."FazlaSipMiktar", 
"Malzemeler"."Tip", 
"Malzemeler"."TipKod"
FROM "Malzemeler" LEFT OUTER JOIN "MalzemeBarkodlari" ON "Malzemeler"."MalzemeId" = "MalzemeBarkodlari"."MalzemeId" 
LEFT OUTER JOIN "Birimler" ON "MalzemeBarkodlari"."BirimId" = "Birimler"."BirimId"

 */




/*
 * 
 * SQL SERVER
 * 
 
 SELECT     dbo.Malzemeler.MalzemeId, dbo.Malzemeler.MalzemeKod, dbo.Malzemeler.MalzemeAd, dbo.Malzemeler.MalzemeAd2, ISNULL(dbo.MalzemeBarkodlari.Barkod, 
                      dbo.Malzemeler.MalzemeKod) AS Barkod, ISNULL(dbo.MalzemeBarkodlari.BirimId, dbo.Malzemeler.BirimId) AS BirimId, ISNULL(dbo.Birimler.Birim, 
                      dbo.Malzemeler.Birim) AS Birim, ISNULL(dbo.MalzemeBarkodlari.Miktar, 1) AS Miktar, dbo.MalzemeBarkodlari.SiraNo, dbo.MalzemeBarkodlari.MalzemeBarkodId, 
                      dbo.Malzemeler.BirimId AS MalzemeBirimId, dbo.Malzemeler.KdvOran, dbo.Malzemeler.MinStok, dbo.Malzemeler.MaxStok, dbo.Malzemeler.HammaddeTakip, 
                      dbo.Malzemeler.FazlaSipMiktar, dbo.Malzemeler.Tip, dbo.Malzemeler.TipKod
FROM         dbo.Malzemeler LEFT OUTER JOIN
                      dbo.MalzemeBarkodlari ON dbo.Malzemeler.MalzemeId = dbo.MalzemeBarkodlari.MalzemeId LEFT OUTER JOIN
                      dbo.Birimler ON dbo.MalzemeBarkodlari.BirimId = dbo.Birimler.BirimId 
 */