using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace Mikrobar.Module.BusinessObjects
{
    public enum HurdaTipi
    {
        [ImageName("State_Priority_Normal"), DisplayName("Yok")]
        Bos = 0,
        [ImageName("Action_EditModel"), DisplayName("Kullanilan Malzeme Hurdasi")]
        MalzemeHurdasi = 1,
        [ImageName("Action_Clear"), DisplayName("Olusan Urun Hurdasi")]
        UrunHurdasi = 2
    }
}
