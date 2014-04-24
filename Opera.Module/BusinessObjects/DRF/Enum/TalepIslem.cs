using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mikrobar.Module.BusinessObjects
{
    public enum TalepIslem
    {
        YeniTalep = 0,
        TalepGuncelle = 1,
        Talepler = 2,
        BelgeBilgileri = 3,
        TalepDetaylari = 4,
        BarkodOku = 5,
        BarkodEkle = 6,
        BelgeKaydet = 7,
        Belgeler = 8,
        KabulBelgeBilgileri = 9,
        KabulBarkodEkle = 10,
        KabulBelgeKaydet = 11,
        TalepSevkBelge = 12,
        TalepSevkDetay = 13,
        TalepSevkKaydet = 14,
        TalepKabulBelge = 15,
        TalepKabulDetay = 16,
        TalepKabulKaydet = 17,
        SevkBarkodIslem = 18
    };

    public enum BelgeIslem
    {
        KriterListe = 0,
        AcikBelgeler = 1,
        TalepIptal = 2
    };
}
