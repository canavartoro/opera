using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mikrobar.Module.BusinessObjects
{
    public enum HizliSevkiyat : int
    {
        YeniBelge = 0,
        BarkodEkle = 1,
        BarkodCikar = 2,
        Bitir = 3,
        KayitDon = 4,
        KayitIptal = 5
    };
}
