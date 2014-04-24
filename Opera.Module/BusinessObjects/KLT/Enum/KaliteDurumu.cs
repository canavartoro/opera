using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mikrobar.Module.BusinessObjects
{
    public enum KaliteDurumu : int
    {
        Bekliyor = 0,
        Kabul = 1,
        SartliKabul = 2,
        Red = 3, 
        KismiKabul = 4
    }
}
