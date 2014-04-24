using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mikrobar.Module.BusinessObjects
{
    public enum SevkEmriSevk : byte
    {
        SevkEmirleri = 0,
        SevkEmirDetaylari = 1,
        SevkEmirDetayEkle = 2,
        SevkEmirDetayCikar = 3,
        SevkEmirBelgeIptal = 4,
        SevkEmirIrsaliyeKaydet = 5,
        TopluIrsaliyeBelgeleri = 6
    };
}
