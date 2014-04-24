
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mikrobar.Module.BusinessObjects
{
    public enum KaynakProgram : int
    {
        SatinAlmaSiparisden = 0,
        SatinAlmaCariden = 1,
        Transfer = 2,
        Sevkiyat = 3,
        SevkHazirlik = 4,
        IsEmrineMalzemeTransferi = 5,
        IsEmriUretim = 6,
        Sayim = 7,
        SayimGecis = 8,
        PaletBozma = 9,
        DbEntegrasyon = 99,
        KaliteOnay = 10,
        FasonCikis = 11,
        Ithalat = 12,
        FasonDonus = 13,
        EtiketBasim = 14,
        AmbalajSayim = 15,
        StokSayim = 16,
        FiiliStokSayim = 17,
        WebKonsol = 18,
        MalTalep = 19,
        Veritabani = 20,
        IadeIrsaliye = 21,
        SarfCikis = 22,
        Etiketleme = 23
    };
}
