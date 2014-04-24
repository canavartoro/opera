using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace Mikrobar.Module.BusinessObjects
{
    [Persistent("V_IsEmriDokumanlari")]
    [ReferansTablo("pub.ky_dokuman, pub.stok_belge", SistemTipi = SistemTipi.Progress)]
    public class V_IsEmriDokumanlari : XPLiteObject
    {
        [Key(AutoGenerate = false)]
        public int IsEmriDokumanId { get; set; }
        public int IsEmriId { get; set; }
        public string IsEmriNo { get; set; }
        public string OperasyonKod { get; set; }
        public int OperasyonId { get; set; }
        public int MalzemeId { get; set; }
        public string MalzemeKod { get; set; }
        public string DosyaYolu { get; set; }
        public string DosyaAdi { get; set; }

        public V_IsEmriDokumanlari() { }
        public V_IsEmriDokumanlari(Session session) : base(session) { }

    }
}
/*
create or replace view "V_IsEmriDokumanlari" as
select 

'' as IsEmriDokumanId
W.WORDER_M_ID as "IsEmriId",
W.WORDER_NO as "IsEmriNo", 
'' as "MalzemeId",
'' as "MalzemeKod",
W.PARENT_WORDER_M_ID as "UstIsEmriId", --Kaldırıldı.
U.RELATION_ID as "RotaId",  --Kaldırıldı.
U.LONG_FILE_NAME as "DosyaYolu", 
U.SH0RT_FILE_NAME as "DosyaAdi"

from UYUMSOFT.PRDT_WORDER_M W 
INNER JOIN UYUMSOFT.PRDD_PRODUCT_ROUTE_M R ON W.PRODUCT_ROUTE_M_ID = R.PRODUCT_ROUTE_M_ID
INNER JOIN UYUMSOFT.GNLD_UPLOAD_FILE U ON R.PRODUCT_ROUTE_M_ID = U.RELATION_ID;

 * 
 * sql
 * 
 *   create View [dbo].[V_IsEmriDokumanlari] as  
Select top 100 percent
MalzemeDokumanId as IsEmriDokumanId,
'' as "IsEmriId",
'' as "IsEmriNo",
MalzemeDokumanId as MalzemeId,
MalzemeKod as MalzemeKod,
DosyaYolu as "DosyaYolu",
 DosyaAdi as "DosyaAdi"
 from [MalzemeDokumanlari]  with (nolock) order by MalzemeKod
GO
*/