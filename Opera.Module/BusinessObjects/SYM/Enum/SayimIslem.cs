using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mikrobar.Module.BusinessObjects
{
    public enum SayimIslem : int
    {
        SayimKayitIslemleri = 0,
        EtiketlemeIslemleri = 1        
    }    
    public enum SayimKayitIslem : int 
    {
        KullaniciSayimKontrol = 0,
        RafBarkodOku = 1,
        RafSayimKapat = 2,
        BarkodEkle = 3,
        BarkodCikar = 4,
        SayimOzetListe = 5,
        SayimDetayListe = 6,
        MalzemeBul = 7,
        StokSayimDetayListe = 8,
        FiiliSayimKaydet = 9,
        FiiliSayimEkle = 10,
        FiiliSayimCikar = 11,
        FiiliSayimOzetListe = 12,
        StokBarkodOku = 13,
        AmbalajBarkodOku = 14,
        SayimBelgeKontrol = 15,
        SayimYeniBelge = 16,
        SayimBelgeIptal = 17,
        SayimBelgeTamamla = 18,
        SayimGecmisListe = 19,
        FiiliSayimDonemAktarim = 20,
        SayimEmirListesi = 21,
        SayimRaflariAl = 22
    }
    public enum EtiketlemeIslem : int
    {
        T_EtiketOku = 0,
        AmbalajKayitEkle = 1,
        AmbalajMiktarGuncelle = 2,
        MevcutBarkodaEkle = 3,
        SayimBelgesiKapat = 4,
        MevcutAmbalajlar = 5,
        AmbalajSilme = 6,
        AmbalajTurListesi = 7,
        RenkListesi = 8
    }
}
