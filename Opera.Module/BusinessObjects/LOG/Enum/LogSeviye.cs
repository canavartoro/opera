using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace Mikrobar.Module.BusinessObjects
{
    public enum LogSeviye
    {
        [ImageName("Action_Debug_Breakpoint_Toggle"), DisplayName("Log Kaydi Yok")]
        Kapali = 0, // Off
        [ImageName("Action_Close"), DisplayName("Hata Logu")]
        Hata = 1,   // Error
        [ImageName("BO_Attention"), DisplayName("Uyari Logu")]
        Uyari = 2,  // Warning
        [ImageName("Action_AboutInfo"), DisplayName("Bilgi Logu")]
        Bilgi = 3,  // Info
        [ImageName("Action_Grant_Set"), DisplayName("Tum Loglar")]
        Hersey = 4  // Verbose
    }
}
