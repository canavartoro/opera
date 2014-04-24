#define MSSQL
//#define MYSQL
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mikrobar.Module.BusinessObjects
{
    public class DbSize
    {
        public const int KisaNoLenght = 6;
        public const int SeriNo = 36;
        public const int PrefixLenght = 3;
        public const int CihazNoLenght = 24;
        public const int AdresLenght = 64;
        public const int KodLenght = 64;
        public const int BarKodLenght = 24;
        public const int ModulLenght = 128;
        public const int AciklamaLenght = 128;
        public const int AdLenght = 128;
        public const int NoLenght = 24;
        public const int PassLenght = 32;
        public const int Mesaj = 800;
        public const int DetayliMesaj = 800;
        public const int SaatLenght = 5;
        public const int IkiYuzElli = 250;
        public const int Limitsiz = DevExpress.Xpo.SizeAttribute.Unlimited;
    }

    public class DbType
    {
        #if MSSQL
        public const string IntType = " [int] ";
        public const string IntDefaultType = " [int] DEFAULT ((0)) ";
        public const string IntIdentityType = " [int] IDENTITY(1,1) NOT FOR REPLICATION NOT ";
        public const string DateDefaultType = "  [DATETIME] DEFAULT (GETDATE()) ";
        #endif
        #if MYSQL
        public const string IntType = " int(11) ";
        public const string IntDefaultType = " int(11) DEFAULT '0' ";
        public const string IntIdentityType = " bigint(20) NOT NULL AUTO_INCREMENT NOT ";
        public const string DateDefaultType = " timestamp DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP NULL ";
        #endif        

    }

    public struct IsEmriNo
    { 
    }

    public class OtherSize
    {
        public const int ListItemCount = 30;
        public const int ListItemCountAll = 9999;
    }
}
