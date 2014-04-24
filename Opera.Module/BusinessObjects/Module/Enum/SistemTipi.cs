using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace Mikrobar.Module.BusinessObjects
{
    public enum SistemTipi : int
    {
        [DisplayName("UyumSoft (WebErp)")]
        WebErp = 0,
        [DisplayName("UyumSoft (Progress)")]
        Progress = 1,
        [DisplayName("Logo")]
        Logo = 2, 
        Sap = 3, 
        Netsis = 4, 
        Mikro = 5, 
        Axapta = 6, 
        Diger = 7
    }
}
