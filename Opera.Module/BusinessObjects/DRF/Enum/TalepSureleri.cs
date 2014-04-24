using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace Mikrobar.Module.BusinessObjects
{
    public enum TalepSureleri : int
    {
        [DisplayName("Hemen (0 dk)")]
        Hemen = 0, // 30
        [DisplayName("Yarim Saat (30 dk)")]
        YarimSaat = 30,
        [DisplayName("Bir Saat (1 st)")]
        BirSaat = 60,
        [DisplayName("Iki Saat (2 st)")]
        IkiSaat = 120,
        [DisplayName("Dort Saat (4 st)")]
        DortSaat = 240,
        [DisplayName("Bir Gun (1 gn)")]
        BirGun = 1440,
        [DisplayName("Iki Gun (2 gn)")]
        IkiGun = 2880
    }
}
