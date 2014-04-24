using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mikrobar.Module.BusinessObjects
{
    public enum PaletBozmaIslemleri : int
    {
        AmbalajBarkodOkut = 0,
        MalzemeBarkodOkut = 1,
        AmbalajDetayMalzemeEkle = 2,
        AmbalajDetayDiziGetir = 3,
        YeniAmbalajOlustur = 4,
        AmbalajCikart = 5
    };
}
