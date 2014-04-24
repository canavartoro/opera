using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mikrobar.Module.BusinessObjects
{
    public enum IslemType : int
    {
        Mantiksal = 0,
        Metin = 1,
        Sayisal = 2,
        SeriPort = 3,
    };

    public enum DurusIslemType : int
    {
        DurusBaslangic = 0,
        DurusBitis = 1
    };

    public enum CihazUretimTur : int
    {
        Urun = 0,
        YenidenIsleme = 1,
        Fire = 2
    };
}
