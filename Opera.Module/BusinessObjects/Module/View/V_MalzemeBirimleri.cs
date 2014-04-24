using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.DC;
using DevExpress.Xpo;

namespace Mikrobar.Module.BusinessObjects
{
    [DebuggerDisplay("BirimId = {BirimId}, MalzemeId = {MalzemeId}, Birim = {Birim}")]
    [Persistent("V_MalzemeBirimleri"), XafDefaultProperty("Birim")]
    [ReferansTablo(TabloAdi = "INVD_ITEM_UNIT", SistemTipi = SistemTipi.WebErp)]
    [ReferansTablo(TabloAdi = "dbo.TBLSTSABIT", SistemTipi = SistemTipi.Netsis, SqlSorgu = "SELECT * FROM Barset.V_MalzemeBirimleri WHERE (1 = 1) ")]
    public class V_MalzemeBirimleri : XPLiteObject
    {
        
        public int BirimId { get; set; }
        public int MalzemeId { get; set; } 
        public string Birim { get; set; }
        public string Aciklama { get; set; }
        [Key]
        public int MalzemeBirimId { get; set; }
        public decimal Miktar { get; set; }
        public decimal Miktar2 { get; set; }
        public int SiraNo { get; set; }
        public decimal En { get; set; }
        public decimal Boy { get; set; }
        public decimal Yukekseklik { get; set; }
        public decimal Hacim { get; set; }
        public decimal Agirlik { get; set; }
        public bool AnaBirim { get; set; }

        public static implicit operator MalzemeBirimleri(V_MalzemeBirimleri malzBirim)
        {
            MalzemeBirimleri malzemeBirim = null;
            if (malzBirim != null)
            {
                malzemeBirim = malzBirim.Session.GetObjectByKey<MalzemeBirimleri>(malzBirim.MalzemeBirimId);
            }
            else
            {
                malzemeBirim = (MalzemeBirimleri)XpoHelper.CloneBaseObject(malzBirim, typeof(MalzemeBirimleri), malzBirim.Session);
            }
            return malzemeBirim;
        }

        public V_MalzemeBirimleri(Session session) : base(session) { }
        public V_MalzemeBirimleri() : base(Session.DefaultSession) { }
    }
}

/*
 alter VIEW [dbo].[V_MalzemeBirimleri] AS 
SELECT     dbo.Birimler.Birim, dbo.Birimler.Aciklama AS Aciklama, dbo.MalzemeBirimleri.MalzemeBirimId, dbo.MalzemeBirimleri.Miktar, 
                      dbo.MalzemeBirimleri.Miktar2, dbo.MalzemeBirimleri.SiraNo, dbo.MalzemeBirimleri.En, dbo.MalzemeBirimleri.Boy, 
                      dbo.MalzemeBirimleri.Yukekseklik, dbo.MalzemeBirimleri.Hacim, dbo.MalzemeBirimleri.Agirlik, dbo.MalzemeBirimleri.BirimId, 
                      dbo.MalzemeBirimleri.MalzemeId, CONVERT(bit, 1) AS AnaBirim, dbo.Birimler.Aciklama AS BirimAd
FROM         dbo.MalzemeBirimleri INNER JOIN
                      dbo.Birimler ON dbo.MalzemeBirimleri.BirimId = dbo.Birimler.BirimId
 */