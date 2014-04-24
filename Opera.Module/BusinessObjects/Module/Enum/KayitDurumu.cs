using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace Mikrobar.Module.BusinessObjects
{
    public enum KayitDurumu
    {
        [ImageName("Action_New"), DisplayName("Yeni Kayıt")]
        Yeni = 0,
        [ImageName("Action_Grant"), DisplayName("Kayıt Tamamlandı")]
        Tamamlandi = 1,
        [ImageName("Action_CloseAllWindows"), DisplayName("Kayıt İptal Edildi")]
        Iptal = 2,
        [ImageName("State_Validation_Skipped"), DisplayName("Kayıt ERP'ye Aktarılacak")]
        Aktarilacak = 3,
        [ImageName("State_Validation_Valid"), DisplayName("Kayıt ERP'ye Aktarıldı")]
        Aktarildi = 4,
        [ImageName("Action_Delete"), DisplayName("Kayıt Kapandı")]
        Kapali = 5
    }
}
