using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

namespace Mikrobar.Module.BusinessObjects
{
    public enum AmbalajDurumu
    {
        [System.ComponentModel.Description("Kasada herhangi bir islem yok, bosta")]
        [ImageName("State_Validation_Valid"), DisplayName("Bosta")]
        Bosta = 0,
        [System.ComponentModel.Description("Kasa istasyonun onunde bekliyor, uretim icin bekliyor")]
        [ImageName("State_Validation_Skipped"), DisplayName("Bekliyor Kullanilacak")]
        Bekliyor = 1,
        [System.ComponentModel.Description("Kasa uretimde isleniyor, uretim suruyor")]
        [ImageName("ModelEditor_Settings"), DisplayName("Uretimde Isleniyor")]
        Isleniyor = 2,
        [System.ComponentModel.Description("Kasa uretimde uretim ise durusta, uretim devam ediyor")]
        [ImageName("BO_Rules"), DisplayName("Uretimde Durusta")]
        Durusta = 3,
        [System.ComponentModel.Description("Kasa tamamlanmis kapatilmis")]
        [ImageName("Action_Deny"), DisplayName("Kapali")]
        Kapali = 4,
        [System.ComponentModel.Description("Kasa uretim icin bekliyor, rezerve")]
        [ImageName("BO_Security_Permission"), DisplayName("Kilitli")]
        Kilitli = 5,
        [System.ComponentModel.Description("Kasa fasona sevkedilmis, fasonda")]
        [ImageName("BO_Organization"), DisplayName("Fasonda Bekliyor")]
        Fasonda = 6,
        [System.ComponentModel.Description("Kasa kullanim icin ayrilmis, rezerve")]
        [ImageName("BO_Attention"), DisplayName("Rezerv Edildi")]
        Rezerv = 7,
        [System.ComponentModel.Description("Kasa cikis durumunda, sevkedilmis")]
        [ImageName("BO_Vendor"), DisplayName("Sevk Edildi")]
        Cikis = 8,
        [System.ComponentModel.Description("Kasanin uretimi tamamlandi kapandi")]
        [ImageName("Action_Grant"), DisplayName("Islemleri Tamamlandi")]
        Tamamlandi = 9
    }
}
