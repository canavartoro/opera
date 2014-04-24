using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mikrobar.Module.BusinessObjects
{
    public enum OtomasyonIslem : int
    {
        CihazList = 0,
        UretimOtomasyonKayit = 1,
        LogOtomasyonKayit = 2,
        OtomasyonUretimDegeri = 3,
        OtomasyonDurumUpdate = 4,
        OtomasyonParametre = 5,
        DurusIslem = 6,
        OtomasyonMiktarGetir = 7
    };
}
