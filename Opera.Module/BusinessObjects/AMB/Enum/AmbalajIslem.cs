using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mikrobar.Module.BusinessObjects
{
    public enum AmbalajIslem : int
    {
        AmbalajBul = 0,
        YeniAmbalaj = 1,
        AmbalajDetayEkle = 2,
        AmbalajDetaySil = 3,
        AmbalajDetaylari = 4,
        AmbalajTurleri = 5,
        AmbalajSil = 6,
        AmbalajSevkHazirlik = 7,
        AmbalajBoz = 8,
        AmbalajDetayAmbalajEkle = 9,
        AmbalajDetayAmbalajCikart = 10,
        EtiketTanimBul = 11,
        AmbalajHareketBul = 12,
        KonveyorRapor = 13,
        TasarimGrubuBul = 14,
        Paketleme=15
    }
}
