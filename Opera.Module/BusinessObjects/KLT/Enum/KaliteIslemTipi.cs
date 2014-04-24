using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mikrobar.Module.BusinessObjects
{
    public enum KaliteIslemTipi : int
    {
        BelgeGetir = 0,
        AmbalajOlustur = 1,
        BelgeKaydet = 2,
        BelgeleriGetir = 3,
        AmbalajSil = 4,
        SurecEmri = 5,
        KontrolGrubuGetir = 6,
        IsEmriDokumanlari = 7,
        SurecOnay = 8,
        SurecEmirOnay = 9,
        KaliteParametre = 10
    }
}
