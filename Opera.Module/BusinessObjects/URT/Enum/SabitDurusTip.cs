using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace Mikrobar.Module.BusinessObjects
{
    public enum SabitDurusTip
    {
        [ImageName("Action_Change_State"), DisplayName("Sabit Duruş")]
        Sabit = 0, // Off
        [ImageName("BO_Scheduler"), DisplayName("Planlı Duruş")]
        Zamanlanmis = 1
    }
}
