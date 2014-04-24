using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Diagnostics;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [Persistent("V_Ambalajlar"),
    ModelDefault("DefaultListViewShowAutoFilterRow", "True"),
    ImageName("BO_Sale_Item_v92"),
        //NavigationItem(true, GroupName = @"Ambalaj Yonetimi","Ambalaj Bilgisi"),
    XafDefaultProperty("Barkod"),
    XafDisplayName("Ambalaj Detay Bilgisi")]
    [DebuggerDisplay("AmbalajDetayId = {AmbalajDetayId}, AmbalajId = {AmbalajId}, Barkod = {Barkod}, Durum = {Durum}")]
    public class V_Ambalajlar : XPLiteObject
    {
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public string SeriNo { get; set; }
        public string Barkod { get; set; }
        public string MalzemeKod { get; set; }
        public string MalzemeAd { get; set; }
        public string IsEmriNo { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public string RafBarkod { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public string DepoBarkod { get; set; }
        public string Birim { get; set; }
        public decimal Miktar { get; set; }
        public string Birim2 { get; set; }
        public decimal Miktar2 { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public decimal AmbalajDarasi { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public decimal Dara { get; set; }
        public string DepoKod { get; set; }
        public string RafKod { get; set; }
        public string PartiNo { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public string Prefix { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public bool Sevkiyat { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public bool Bolunebilirlik { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public bool ParcaliUretim { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int MalzemeId { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int Birim2Id { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int BirimId { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int AmbalajTurId { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int AmbalajId { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int CariId { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public string CariKod { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public string CariAd { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public string CizimNo { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int AmbalajSayisi { get; set; }
        [Key]
        public int AmbalajDetayId { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int IstasyonId { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public string IstasyonKod { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int RafId { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int DepoId { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public KaliteDurumu KaliteDurum { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int IsEmriId { get; set; }
        public decimal Kalan { get; set; }
        public decimal Kalan2 { get; set; }
        public AmbalajDurumu Durum { get; set; }
        public string SDurum { get; set; }
        public string DurumAciklama { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public MalzemeTip MalzemeTip { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false), ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}")]
        public DateTime OlusturmaTarihi { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false), ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}")]
        public DateTime SonKullanmaTarihi { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public FasonCikisSanalStok SanalStok { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int SevkEmriId { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int SevkEmriDetayId { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int ReferansId { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int RenkId { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int SiraNo { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int RafOmru { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public string IrsaliyeNo { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int TransferReferansId { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int TransferReferansDetayId { get; set; }
        public int UstAmbalaj { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int OperasyonId { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int OperasyonNo { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public string OperasyonKod { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int UretimOperasyon { get; set; }        
        public string PaletNo { get; set; }        
        public int PaletSiraNo { get; set; }
        public string KullaniciKod { get; set; }
        public string KaynakModul { get; set; }

        public V_Ambalajlar() { }
        public V_Ambalajlar(Session session) : base(session) { }
    }
}

/*V_Ambalajlar



CREATE VIEW [dbo].[V_Ambalajlar]
AS
SELECT     dbo.Ambalajlar.SeriNo, dbo.Ambalajlar.Barkod, dbo.Raflar.Barkod AS RafBarkod, dbo.Depolar.Barkod AS DepoBarkod, dbo.Birimler.Birim, 
                      dbo.AmbalajDetaylari.Miktar, dbo.AmbalajDetaylari.Kalan, dbo.AmbalajDetaylari.Miktar2, dbo.AmbalajDetaylari.Kalan2, 
                      dbo.Ambalajlar.Dara AS AmbalajDarasi, dbo.AmbalajTurleri.Dara, dbo.Depolar.DepoKod, dbo.Raflar.RafKod, dbo.AmbalajTurleri.Sevkiyat, 
                      dbo.AmbalajTurleri.Bolunebilirlik, dbo.AmbalajDetaylari.MalzemeId, dbo.AmbalajDetaylari.Birim2Id, dbo.AmbalajDetaylari.BirimId, 
                      dbo.AmbalajTurleri.Prefix, dbo.Ambalajlar.AmbalajTur AS AmbalajTurId, dbo.Ambalajlar.OID AS AmbalajId, dbo.Ambalajlar.Durum, 
                      dbo.AmbalajDetaylari.OID AS AmbalajDetayId, dbo.Ambalajlar.IstasyonId, dbo.Ambalajlar.RafId, dbo.Ambalajlar.IsEmriId, dbo.Ambalajlar.IsEmriNo, 
                      dbo.Ambalajlar.IsEmriTipId, dbo.AmbalajTurleri.MalzemeTip, dbo.Malzemeler.MalzemeKod, dbo.Malzemeler.MalzemeAd, 
                      ISNULL(dbo.Ambalajlar.OlusturmaTarihi, GETDATE()) AS OlusturmaTarihi, dbo.Ambalajlar.SevkEmriId, dbo.AmbalajDetaylari.ReferansId, 
                      dbo.AmbalajDetaylari.TransferReferansId, dbo.AmbalajDetaylari.TransferReferansDetayId, dbo.Ambalajlar.DepoId, dbo.Ambalajlar.UstAmbalaj, 
                      dbo.Ambalajlar.KaliteDurum, dbo.Ambalajlar.PartiNo, dbo.AmbalajDetaylari.Birim2, dbo.Istasyonlar.IstasyonKod, dbo.Istasyonlar.IstasyonAd, 
                      dbo.Renkler.RenkKod, dbo.Ambalajlar.CariId, dbo.Cariler.CariKod, dbo.Cariler.CariAd, dbo.Renkler.Aciklama, dbo.Ambalajlar.BelgeNo, 
                      dbo.Ambalajlar.SiparisId, dbo.Siparisler.SiparisNo, CONVERT(float, dbo.AmbalajDetaylari.Kalan) AS rKalan,
                          (SELECT     COUNT(*) AS Expr1
                            FROM          dbo.Ambalajlar AS X1
                            WHERE      (UstAmbalaj = dbo.Ambalajlar.OID)) AS AmbalajSayisi, dbo.Ambalajlar.SonKullanmaTarihi, dbo.Ambalajlar.RenkId, 
                      dbo.AmbalajTurleri.ParcaliUretim, ISNULL(dbo.IsEmirleri.PlanlananMiktar, 0) AS IsEmriMiktar, ISNULL(dbo.Ambalajlar.SiraNo, 0) AS SiraNo, 
                      ISNULL(dbo.Ambalajlar.RafOmru, 0) AS RafOmru,
                          (SELECT     TOP (1) S.IrsaliyeNo
                            FROM          dbo.AmbalajHareketDetaylari AS D WITH (NOLOCK) INNER JOIN
                                                   dbo.AmbalajHareketleri AS H WITH (NOLOCK) ON D.AmbalajHareket = H.OID INNER JOIN
                                                   dbo.StokHareketleri AS S WITH (NOLOCK) ON H.StokHareket = S.OID
                            WHERE      (D.Ambalaj = dbo.Ambalajlar.OID)
                            ORDER BY H.OID) AS IrsaliyeNo, dbo.IsEmirleri.CizimNo, dbo.Ambalajlar.OperasyonId, dbo.Ambalajlar.SDurum, dbo.Ambalajlar.DurumAciklama, 
                      dbo.Ambalajlar.SanalStok, dbo.Ambalajlar.OperasyonNo, dbo.Ambalajlar.OperasyonKod
FROM         dbo.Cariler RIGHT OUTER JOIN
                      dbo.Ambalajlar LEFT OUTER JOIN
                      dbo.AmbalajDetaylari ON dbo.Ambalajlar.OID = dbo.AmbalajDetaylari.Ambalaj INNER JOIN
                      dbo.AmbalajTurleri ON dbo.Ambalajlar.AmbalajTur = dbo.AmbalajTurleri.OID LEFT OUTER JOIN
                      dbo.IsEmirleri INNER JOIN
                      dbo.Renkler ON dbo.IsEmirleri.RenkId = dbo.Renkler.RenkId ON dbo.Ambalajlar.IsEmriId = dbo.IsEmirleri.IsEmriId AND 
                      dbo.Ambalajlar.RenkId = dbo.Renkler.RenkId LEFT OUTER JOIN
                      dbo.Siparisler ON dbo.Ambalajlar.SiparisId = dbo.Siparisler.SiparisId ON dbo.Cariler.CariId = dbo.Ambalajlar.CariId LEFT OUTER JOIN
                      dbo.Istasyonlar ON dbo.Ambalajlar.IstasyonId = dbo.Istasyonlar.IstasyonId LEFT OUTER JOIN
                      dbo.Malzemeler ON dbo.AmbalajDetaylari.MalzemeId = dbo.Malzemeler.MalzemeId LEFT OUTER JOIN
                      dbo.Raflar ON dbo.Ambalajlar.RafId = dbo.Raflar.RafId LEFT OUTER JOIN
                      dbo.Depolar ON dbo.Raflar.DepoId = dbo.Depolar.DepoId LEFT OUTER JOIN
                      dbo.Birimler ON dbo.AmbalajDetaylari.BirimId = dbo.Birimler.BirimId
WHERE     (dbo.AmbalajDetaylari.OID IS NOT NULL) AND (dbo.Ambalajlar.GCRecord IS NULL)

GO


GO

 */


/*V_AmbalajDetaylari


alter VIEW [dbo].[V_AmbalajDetaylari]
AS
SELECT     
	dbo.Ambalajlar.UstAmbalaj AS Ambalaj, 
	dbo.AmbalajDetaylari.OID, 
	dbo.AmbalajDetaylari.MalzemeId, 
	dbo.AmbalajDetaylari.Miktar, 
	dbo.AmbalajDetaylari.Miktar2, 
	dbo.AmbalajDetaylari.Kalan, 
	dbo.AmbalajDetaylari.Kalan2, 
	dbo.AmbalajDetaylari.BirimId, 
	dbo.AmbalajDetaylari.Birim2Id, 
	dbo.AmbalajDetaylari.MalzemeKod,
	dbo.AmbalajDetaylari.MalzemeAd, 
	dbo.AmbalajDetaylari.Birim, 
	dbo.AmbalajDetaylari.Olusturan, 
	dbo.AmbalajDetaylari.OlusturmaTarihi,
	dbo.AmbalajDetaylari.Guncelleyen, 
	dbo.AmbalajDetaylari.GuncellemeTarihi, 
	dbo.AmbalajDetaylari.KaynakModul, 
	dbo.AmbalajDetaylari.KaynakProgram,
	dbo.AmbalajDetaylari.CihazNo, 
	dbo.AmbalajDetaylari.OptimisticLockField, 
	dbo.AmbalajDetaylari.GCRecord, 
	dbo.AmbalajDetaylari.AmbalajId,
	dbo.AmbalajDetaylari.ReferansId, 
	dbo.AmbalajDetaylari.TransferReferansDetayId, 
	dbo.AmbalajDetaylari.TransferReferansId, 
	dbo.Ambalajlar.OID AS AmbalajId2,
	dbo.AmbalajDetaylari.Birim2, 
	dbo.Ambalajlar.Barkod AS SeriNo2
FROM         dbo.AmbalajDetaylari INNER JOIN
                      dbo.Ambalajlar ON dbo.AmbalajDetaylari.Ambalaj = dbo.Ambalajlar.OID
WHERE     (dbo.Ambalajlar.UstAmbalaj IS NULL) 



 */