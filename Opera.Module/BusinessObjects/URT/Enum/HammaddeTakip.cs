using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace Mikrobar.Module.BusinessObjects
{
    //resimler eklendi
    public enum HammaddeTakip
    {
        [ImageName("State_Validation_Invalid"), DisplayName("Birebir Kullanim")]
        BirebirKontrol = 0, //AlternatifKullanilamaz = 0,
        [ImageName("Action_Grant_Set"), DisplayName("Alternatif Kullanim")]
        AlternatifStok = 1, //AlternatifKullanilabilir = 1,
        [ImageName("State_Validation_Skipped"), DisplayName("Farklistok Kullanim")]
        ZorunluDegil = 2 //HammaddeTakibiZorunluDegil = 2,
    }
}
