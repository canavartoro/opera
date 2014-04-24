using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace Mikrobar.Module.BusinessObjects
{
    [DeferredDeletion(false), OptimisticLocking(false), NonPersistent()]
    public class T_DepoStok : XPBaseObject
    {
        public string MalzemeKod { get; set; }
        public string MalzemeAd { get; set; }
        public string DepoKod { get; set; }
        public decimal Miktar { get; set; }

        
        public T_DepoStok() { }
        public T_DepoStok(Session session) : base(session) { }
    }
}
/*
 [dbo].[DepoStokBulErp] '101001 FU 00025'
alter PROCEDURE [dbo].[DepoStokBulErp]
(	
	@MALZEMEKOD NVARCHAR(30), -- '512000 FS 0001985'
	@FIRMAKOD NVARCHAR(30) = 'ARMA2011'
)
AS BEGIN

DECLARE @TIRNAK CHAR(1)
DECLARE @SQLCOMMAND NVARCHAR(800)

IF LEN(@FIRMAKOD) < 1 BEGIN SET @FIRMAKOD = 'ARMA2011' END
SET @TIRNAK = ''''
SET @SQLCOMMAND = 'SELECT * FROM OPENQUERY(UYUMDB, ''select s.stok_kod as "MalzemeKod"'
SET @SQLCOMMAND = @SQLCOMMAND + ', s.stok_ad as "MalzemeAd"'
SET @SQLCOMMAND = @SQLCOMMAND + ', s.depo_kod as "DepoKod" '
SET @SQLCOMMAND = @SQLCOMMAND + ', s.stok_miktar as "Miktar" '
SET @SQLCOMMAND = @SQLCOMMAND + ' from pub.depo_stok s where s.firma_kod = ' + @TIRNAK + @TIRNAK + @FIRMAKOD + @TIRNAK + @TIRNAK
SET @SQLCOMMAND = @SQLCOMMAND + ' and s.stok_miktar > 0 '
IF LEN(@MALZEMEKOD) > 0 BEGIN
	SET @SQLCOMMAND = @SQLCOMMAND + ' and s.stok_kod like ' + @TIRNAK + @TIRNAK + '%'+ @MALZEMEKOD + '%'+  @TIRNAK +  @TIRNAK
END
SET @SQLCOMMAND = @SQLCOMMAND + @TIRNAK + ')'
PRINT @SQLCOMMAND

EXEC(@SQLCOMMAND)
END
 */