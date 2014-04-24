using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mikrobar.Module.BusinessObjects
{
    public enum OtomasyonDurumu : int
    {
        Bosta = 0,
        Bekliyor = 1,
        Iptal = 2,
        Tamamlandi = 3
    };
}
