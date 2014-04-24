using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mikrobar.Module.BusinessObjects
{
    public enum UretimIslem
    {
        OperasyonBaslat,
        OperasyonBaslatBitir,
        OperasyonBitir,
        OperasyonBitirDevam,
        OperasyonBitirEtiketle,
        OperasyonBitirEtiketleDevam,
        IsEmriHurda,
        IsEmriDurus,
        IsEmriIscilik,
        UretimIptal,
        UretimMalzemeEkle,
        UretimAletEkle,
        UretimMalzemeSil,
        OtomasyonUretimDegeri
    };
}
