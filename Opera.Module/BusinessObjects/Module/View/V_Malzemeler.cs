using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;

namespace Mikrobar.Module.BusinessObjects
{
    [Persistent("V_Malzemeler")]
    public class V_Malzemeler : XPLiteObject
    {
        [Key]
        public int OID { get; set; }
        public int MalzemeId { get; set; }
        public string MalzemeKod { get; set; }
        public string MalzemeAd { get; set; }
        public string MalzemeAd2 { get; set; }
        public int BirimId { get; set; }
        public string Birim { get; set; }
        public decimal KdvOran { get; set; }
        public decimal MinStok { get; set; }
        public decimal MaxStok { get; set; }
        public decimal FazlaSipMiktar { get; set; }
        public HammaddeTakip HammaddeTakip { get; set; }
        public bool KaliteKontrol { get; set; }
        public string TipKod { get; set; }
        public MalzemeTip Tip { get; set; }
        public string Barkod { get; set; }
        public string Barkod2 { get; set; }
        public string LotKod { get; set; }
        public int LotId { get; set; }

        public static implicit operator Malzemeler(V_Malzemeler malz)
        {
            Malzemeler malzeme = null;
            if(malz != null)
            {
                malzeme = malz.Session.GetObjectByKey<Malzemeler>(malz.MalzemeId);
            }
            else
            {
                malzeme = (Malzemeler)XpoHelper.CloneBaseObject(malz, typeof(Malzemeler), malz.Session);
            }
            return malzeme;
        }

        public V_Malzemeler(Session session) : base(session) { }
        public V_Malzemeler() : base(Session.DefaultSession) { }
    }
}


/*view
 CREATE VIEW V_Malzemeler AS
SELECT TOP 100 PERCENT CAST(M.ITEM_ID AS INTEGER) AS "MalzemeId",
 M.ITEM_CODE AS "MalzemeKod", 
 M.ITEM_NAME AS "MalzemeAd", 
 M.ITEM_NAME2 AS "MalzemeAd2", 
 B.UNIT_ID AS "BirimId", 
 U.UNIT_CODE AS "Birim",
 0 AS "KdvOran", 
 M.QTY_MIN_INV AS "MinStok", 
 M.QTY_MAX_INV AS "MaxStok", 
 M.TOLERANCE_MAX_PO  AS "FazlaSipMiktar", 
 CONVERT(INT, ISNULL(K.CAT_CODE, '2')) AS "HammaddeTakip", 
 0 AS "KaliteKontrol", 
 C.ITEM_CLASS_CODE AS "TipKod", 
 C.INV_ITEM_CLASS AS "Tip" 
  FROM [UYUMDB]..[UYUMSOFT].[INVD_BRANCH_ITEM] B INNER JOIN [UYUMDB]..[UYUMSOFT].[INVD_ITEM] M ON B.ITEM_ID = M.ITEM_ID 
 LEFT OUTER JOIN [UYUMDB]..[UYUMSOFT].[INVD_UNIT] U ON B.UNIT_ID = U.UNIT_ID LEFT OUTER JOIN [UYUMDB]..[UYUMSOFT].[GNLD_CATEGORY] K ON B.CAT_CODE1_ID = K.CAT_CODE_ID 
 LEFT OUTER JOIN [UYUMDB]..[UYUMSOFT].[INVD_ITEM_CLASS] C ON B.ITEM_CLASS_ID = C.ITEM_CLASS_ID 
 WHERE B.CO_ID = 191 AND B.BRANCH_ID = 1010 AND ISNULL(K.CAT_CODE, '2') IN ('0', '1', '2')
 ORDER BY M.ITEM_CODE
 */

/*SQL-2
 






--EXECUTE dbo.MalzemeBulErp 68407, ''

ALTER PROCEDURE [dbo].[MalzemeBulErp]
(		
	@MALZEMEID INTEGER, -- '512000 FS 0001985'	
	@MALZEMEKOD NVARCHAR(30), -- '512000 FS 0001985'
	@FIRMAKOD NVARCHAR(30) = '',
	@FIRMAID INTEGER = 1010,
	@ISYERIKOD NVARCHAR(30) = '',
	@ISYERIID INTEGER = 191
)
AS BEGIN

IF @MALZEMEID = 0 BEGIN
	IF LEN(@MALZEMEKOD) = 0 BEGIN
		RAISERROR(N'HATA OLUSTU! %s (%d)', 16, 1, N'EN AZ BİR KOŞUL GEREKLİ!', 22) WITH NOWAIT, SETERROR
	END
END

DECLARE @XT dbo.T_Malzemeler

INSERT INTO  @XT
SELECT DISTINCT CAST(M.ITEM_ID AS INTEGER) AS "MalzemeId"
, M.ITEM_CODE AS "MalzemeKod"
, M.ITEM_NAME AS "MalzemeAd"
, M.ITEM_NAME2 AS "MalzemeAd2"
, B.UNIT_ID AS "BirimId"
, U.UNIT_CODE AS "Birim"
, 0 AS "KdvOran"
, M.QTY_MIN_INV AS "MinStok"
, M.QTY_MAX_INV AS "MaxStok"
, M.TOLERANCE_MAX_PO  AS "FazlaSipMiktar"
, CONVERT(INT, K.CAT_CODE) AS "HammaddeTakip"
, 0 AS "KaliteKontrol"
, C.ITEM_CLASS_CODE AS "TipKod"
, C.INV_ITEM_CLASS AS "Tip" 
 FROM [UYUMDB]..[UYUMSOFT].[INVD_BRANCH_ITEM] B INNER JOIN [UYUMDB]..[UYUMSOFT].[INVD_ITEM] M ON B.ITEM_ID = M.ITEM_ID 
 LEFT OUTER JOIN [UYUMDB]..[UYUMSOFT].[INVD_UNIT] U ON B.UNIT_ID = U.UNIT_ID LEFT OUTER JOIN [UYUMDB]..[UYUMSOFT].[GNLD_CATEGORY] K ON B.CAT_CODE1_ID = K.CAT_CODE_ID 
 LEFT OUTER JOIN [UYUMDB]..[UYUMSOFT].[INVD_ITEM_CLASS] C ON B.ITEM_CLASS_ID = C.ITEM_CLASS_ID 
 WHERE B.CO_ID = 191 AND B.BRANCH_ID = 1010 AND K.CAT_CODE IN ('0', '1', '2')
AND (B.ITEM_ID = @MALZEMEID OR @MALZEMEID = 0 ) AND (M.ITEM_CODE = @MALZEMEKOD OR LEN(@MALZEMEKOD) = 0)
ORDER BY M.ITEM_CODE

 UPDATE MX SET 
	MX.MalzemeId = DX.MalzemeId, MX.MalzemeKod = DX.MalzemeKod, MX.MalzemeAd = DX.MalzemeAd, 
	MX.MalzemeAd2 = DX.MalzemeAd2, MX.BirimId = DX.BirimId, MX.Birim = DX.Birim, 
	MX.KdvOran = DX.KdvOran, MX.MinStok = DX.MinStok, MX.MaxStok = DX.MaxStok, MX.FazlaSipMiktar = DX.FazlaSipMiktar, 
	MX.HammaddeTakip = DX.HammaddeTakip, MX.KaliteKontrol = DX.KaliteKontrol, MX.TipKod = DX.TipKod, MX.Tip = DX.Tip FROM [Mikrobar]..[Malzemeler] MX INNER JOIN @XT DX ON MX.MalzemeId = DX.MalzemeId
 
 INSERT INTO [Mikrobar]..[Malzemeler] (MalzemeId, MalzemeKod, MalzemeAd, MalzemeAd2, BirimId, Birim, KdvOran, MinStok, MaxStok, FazlaSipMiktar, HammaddeTakip, KaliteKontrol, TipKod, Tip) 
 SELECT MalzemeId, MalzemeKod, MalzemeAd, MalzemeAd2, BirimId, Birim, KdvOran, MinStok, MaxStok, FazlaSipMiktar, HammaddeTakip, KaliteKontrol, TipKod, Tip FROM @XT WHERE MalzemeId NOT IN (SELECT MalzemeId FROM [Mikrobar]..[Malzemeler] WITH (NOLOCK))

SELECT MalzemeId, MalzemeKod, MalzemeAd, MalzemeAd2, BirimId, Birim, KdvOran, MinStok, MaxStok, FazlaSipMiktar, HammaddeTakip, KaliteKontrol, TipKod, Tip FROM @XT

END


/*
CREATE TYPE T_Malzemeler AS TABLE
(
	MalzemeId INTEGER,
	MalzemeKod NVARCHAR(100),
	MalzemeAd NVARCHAR(100),
	MalzemeAd2 NVARCHAR(100),
	BirimId INTEGER,
	Birim NVARCHAR(100),
	KdvOran DECIMAL(8),
	MinStok DECIMAL(8,4),
	MaxStok DECIMAL(8,4),
	FazlaSipMiktar DECIMAL(8),
	HammaddeTakip INTEGER,
	KaliteKontrol BIT,
	TipKod NVARCHAR(100),
	Tip INTEGER
)
*/  


/*SQL
 
 * 
 * 

--EXECUTE dbo.MalzemeBulErp 'ARMA2011', '', 497574

ALTER PROCEDURE [dbo].[MalzemeBulErp]
(
	@MALZEMEID INTEGER, -- '512000 FS 0001985'	
	@MALZEMEKOD NVARCHAR(30), -- '512000 FS 0001985'
	@FIRMAKOD NVARCHAR(30) = 'ARMA2011',
	@FIRMAID INTEGER = 0,
	@ISYERIKOD NVARCHAR(30) = '',
	@ISYERIID INTEGER = 0
)
AS BEGIN

DECLARE @TIRNAK CHAR(1)
DECLARE @SQLCOMMAND NVARCHAR(800)

IF LEN(@FIRMAKOD) < 1 BEGIN SET @FIRMAKOD = 'ARMA2011' END

SET @TIRNAK = ''''
SET @SQLCOMMAND = 'SELECT * FROM OPENQUERY(UYUMDB, ''select cast(s.rowid as int) as "MalzemeId"'
SET @SQLCOMMAND = @SQLCOMMAND + ', s.stok_kod as "MalzemeKod" '
SET @SQLCOMMAND = @SQLCOMMAND + ', s.stok_ad as "MalzemeAd" '
SET @SQLCOMMAND = @SQLCOMMAND + ', s.stok_ad2 as "MalzemeAd2"'
SET @SQLCOMMAND = @SQLCOMMAND + ', cast(b.rowid as int) as "BirimId" '
SET @SQLCOMMAND = @SQLCOMMAND + ', s.birim as "Birim"'
SET @SQLCOMMAND = @SQLCOMMAND + ', s.kdv_oran as "KdvOran"'
SET @SQLCOMMAND = @SQLCOMMAND + ', s.min_stok as "MinStok"'
SET @SQLCOMMAND = @SQLCOMMAND + ', s.max_stok as "MaxStok"'
SET @SQLCOMMAND = @SQLCOMMAND + ', s.fazla_sipyuzde as "FazlaSipMiktar" '
SET @SQLCOMMAND = @SQLCOMMAND + ', s.int_sira as "HammaddeTakip" '
SET @SQLCOMMAND = @SQLCOMMAND + ', s.kkontrol AS "KaliteKontrol" '
SET @SQLCOMMAND = @SQLCOMMAND + ', s.urun_tip AS "TipKod" '
SET @SQLCOMMAND = @SQLCOMMAND + ', case s.urun_tip when  ' + @TIRNAK  + @TIRNAK + 'Mamul' + @TIRNAK   + @TIRNAK + ' then 3 when ' + @TIRNAK   + @TIRNAK + 'Y.Mamul' + @TIRNAK   + @TIRNAK + ' then 2 when ' + @TIRNAK   + @TIRNAK + 'Hammadde' + @TIRNAK   + @TIRNAK + ' then 1 else 0 end as Tip '
SET @SQLCOMMAND = @SQLCOMMAND + ''
SET @SQLCOMMAND = @SQLCOMMAND + ''
SET @SQLCOMMAND = @SQLCOMMAND + ''
SET @SQLCOMMAND = @SQLCOMMAND + ''
SET @SQLCOMMAND = @SQLCOMMAND + ' from pub.stok_kart s, pub.birim b where s.firma_kod = ' + @TIRNAK + @TIRNAK + @FIRMAKOD + @TIRNAK + @TIRNAK + ' '
IF @MALZEMEID > 0 BEGIN
	SET @SQLCOMMAND = @SQLCOMMAND + ' and cast(s.rowid as int) = ' + CONVERT(NVARCHAR(30),@MALZEMEID)
END
IF LEN(@MALZEMEKOD) > 0 BEGIN
	SET @SQLCOMMAND = @SQLCOMMAND + ' and s.stok_kod = ' + @TIRNAK + @TIRNAK +  @MALZEMEKOD + @TIRNAK + @TIRNAK + ' '
END
SET @SQLCOMMAND = @SQLCOMMAND + ' and b.birim = s.birim and b.firma_kod = s.firma_kod ' + @TIRNAK + ')'
PRINT @SQLCOMMAND

EXEC(@SQLCOMMAND)

END


  

  
 */


/*ORA
 
 
--EXECUTE dbo.MalzemeBulErp 68407, ''

ALTER PROCEDURE [dbo].[MalzemeBulErp]
(		
	@MALZEMEID INTEGER, -- '512000 FS 0001985'	
	@MALZEMEKOD NVARCHAR(30), -- '512000 FS 0001985'
	@FIRMAKOD NVARCHAR(30) = '',
	@FIRMAID INTEGER = 1010,
	@ISYERIKOD NVARCHAR(30) = '',
	@ISYERIID INTEGER = 191
)
AS BEGIN
DECLARE @TIRNAK CHAR(1)
DECLARE @SQLCOMMAND NVARCHAR(1300)
SET @TIRNAK = ''''
SET @SQLCOMMAND = ''
SET @SQLCOMMAND = @SQLCOMMAND + ' DECLARE @XT dbo.T_Malzemeler  INSERT INTO  @XT '
SET @SQLCOMMAND = @SQLCOMMAND + ' SELECT * FROM OPENQUERY(UYUMDB, ''SELECT DISTINCT CAST(M.ITEM_ID AS INTEGER) AS "MalzemeId"'
SET @SQLCOMMAND = @SQLCOMMAND + ', M.ITEM_CODE AS "MalzemeKod"'
SET @SQLCOMMAND = @SQLCOMMAND + ', M.ITEM_NAME AS "MalzemeAd"'
SET @SQLCOMMAND = @SQLCOMMAND + ', M.ITEM_NAME2 AS "MalzemeAd2"'
SET @SQLCOMMAND = @SQLCOMMAND + ', B.UNIT_ID AS "BirimId"'
SET @SQLCOMMAND = @SQLCOMMAND + ', U.UNIT_CODE AS "Birim"'
SET @SQLCOMMAND = @SQLCOMMAND + ', CAST(0 AS NUMBER(9,2)) AS "KdvOran"'
SET @SQLCOMMAND = @SQLCOMMAND + ', M.QTY_MIN_INV AS "MinStok"'
SET @SQLCOMMAND = @SQLCOMMAND + ', M.QTY_MAX_INV AS "MaxStok"'
SET @SQLCOMMAND = @SQLCOMMAND + ', M.TOLERANCE_MAX_PO  AS "FazlaSipMiktar"'
SET @SQLCOMMAND = @SQLCOMMAND + ', CAST(K.CAT_CODE AS NUMBER(7,0)) AS "HammaddeTakip"'
SET @SQLCOMMAND = @SQLCOMMAND + ', 0 AS "KaliteKontrol"'
SET @SQLCOMMAND = @SQLCOMMAND + ', C.ITEM_CLASS_CODE AS "TipKod"'
SET @SQLCOMMAND = @SQLCOMMAND + ', C.INV_ITEM_CLASS AS "Tip" '
SET @SQLCOMMAND = @SQLCOMMAND + ''
SET @SQLCOMMAND = @SQLCOMMAND + ' FROM INVD_BRANCH_ITEM B INNER JOIN INVD_ITEM M ON B.ITEM_ID = M.ITEM_ID '
SET @SQLCOMMAND = @SQLCOMMAND + ' LEFT OUTER JOIN INVD_UNIT U ON B.UNIT_ID = U.UNIT_ID LEFT OUTER JOIN GNLD_CATEGORY K ON B.CAT_CODE1_ID = K.CAT_CODE_ID '
SET @SQLCOMMAND = @SQLCOMMAND + ' LEFT OUTER JOIN INVD_ITEM_CLASS C ON B.ITEM_CLASS_ID = C.ITEM_CLASS_ID '
SET @SQLCOMMAND = @SQLCOMMAND + ' WHERE B.CO_ID = 191 AND B.BRANCH_ID = 1010 '
IF @MALZEMEID > 0 BEGIN
	SET @SQLCOMMAND = @SQLCOMMAND + ' AND B.ITEM_ID = ' + CONVERT(NVARCHAR(30),@MALZEMEID)
END
IF LEN(@MALZEMEKOD) > 0 BEGIN
	SET @SQLCOMMAND = @SQLCOMMAND + ' AND M.ITEM_CODE = ' + @TIRNAK + @TIRNAK +  @MALZEMEKOD + @TIRNAK + @TIRNAK + ' '
END
SET @SQLCOMMAND = @SQLCOMMAND + ' ORDER BY M.ITEM_CODE ' + @TIRNAK + ') '
SET @SQLCOMMAND = @SQLCOMMAND + ' '
SET @SQLCOMMAND = @SQLCOMMAND + ' SELECT MalzemeId, MalzemeKod, MalzemeAd, MalzemeAd2, BirimId, Birim, KdvOran, MinStok, MaxStok, FazlaSipMiktar, HammaddeTakip, KaliteKontrol, TipKod, Tip FROM @XT '
PRINT @SQLCOMMAND

EXEC(@SQLCOMMAND)

END


/*
CREATE TYPE T_Malzemeler AS TABLE
(
	MalzemeId INTEGER,
	MalzemeKod NVARCHAR(100),
	MalzemeAd NVARCHAR(100),
	MalzemeAd2 NVARCHAR(100),
	BirimId INTEGER,
	Birim NVARCHAR(100),
	KdvOran DECIMAL(8),
	MinStok DECIMAL(8,4),
	MaxStok DECIMAL(8,4),
	FazlaSipMiktar DECIMAL(8),
	HammaddeTakip INTEGER,
	KaliteKontrol BIT,
	TipKod NVARCHAR(100),
	Tip INTEGER
)
*/    