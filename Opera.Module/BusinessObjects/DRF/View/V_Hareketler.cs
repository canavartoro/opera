using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace Mikrobar.Module.BusinessObjects
{
    [Persistent("V_Hareketler")]
    public class V_Hareketler : XPLiteObject
    {

        public string HareketKod { get; set; }
        public string HareketAd { get; set; }
        public string HareketTurKod { get; set; }
        public string HareketTurAd { get; set; }
        public HareketTipi HareketTipi { get; set; }

        public string HedefDepoKod { get; set; }
        public string KaynakDepoKod { get; set; }

        public string HedefRafKod { get; set; }
        public string KaynakRafKod { get; set; }
        
        public string OzelKod { get; set; }
        public string OzelKod1 { get; set; }

        public bool AlisSatis { get; set; }
        public bool Iade { get; set; }
        public bool Fason { get; set; }
        public bool IrsaliyeZorunlu { get; set; }
        public bool IsemriZorunlu { get; set; }
        public bool CariZorunlu { get; set; }

        public int HedefRafId { get; set; }
        public int KaynakRafId { get; set; }
        public int HedefDepoId { get; set; }
        public int KaynakDepoId { get; set; }
        public int HareketTanimId { get; set; }
        [Key]
        public int OID { get; set; }

        
        public V_Hareketler() { }
        public V_Hareketler(Session session) : base(session) { }
    }
}


/*
 SELECT DOC_TRA_ID, DOC_TRA_CODE, DESCRIPTION, PURCHASE_SALES, SOURCE_APP, INVENTORY_STATUS, ISPASSIVE, IS_CONSIGNED FROM GNLD_DOC_TRA WHERE SOURCE_APP = 1000 AND  INVENTORY_STATUS = 2 AND PURCHASE_SALES = 3 

 SELECT DOC_TRA_ID, DOC_TRA_CODE, DESCRIPTION, PURCHASE_SALES, SOURCE_APP, INVENTORY_STATUS, ISPASSIVE, IS_CONSIGNED FROM GNLD_DOC_TRA 
WHERE 1 = 1
AND SOURCE_APP = 210 
AND  INVENTORY_STATUS = 3 
--AND PURCHASE_SALES = 1
 * 
 */