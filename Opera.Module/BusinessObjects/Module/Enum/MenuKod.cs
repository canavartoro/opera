using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mikrobar.Module.BusinessObjects
{
    public enum MenuKod : int
    {
        STN001 = 1,	//	Satın Alma İşlemleri
        HKM001 = 2,	//	Hakkımızda
        SVK001 = 3,	//	Sevkiyat İşlemleri
        TRI001 = 4,	//	Transfer İşlemleri
        SBS004 = 5,	//	Sipariş Bazında Satın Alma
        CBS005 = 6,	//	Cari Bazında Satın Alma
        URT001 = 7,	//	Üretim İşlemleri (Tablet)
        URT002 = 8,	//	Üretime Malzeme Çıkışı
        URT003 = 9,	//	Üretim İşlemleri (Elterm)
        URT004 = 10,	//	Yurt Dışı
        URT005 = 11,	//	Yurt İçi
        ATR002 = 12,	//	Ambalaj Transfer
        SYM001 = 13,	//	Ambar Sayım
        SYM002 = 14,	//	Stok Sayımı
        SYM003 = 15,	//	Ambalaj Sayımı
        SVK002 = 16,	//	Toplama Emirleri
        ATR003 = 18,	//	Ambalaj   İşlemleri
        SYM004 = 23,	//	Geçmiş Sayım
        FSI003 = 24,    //  Fason İşlemleri  
        FSI002 = 25,    //  Fason Çıkış
        FSI001 = 26,     //  Fason Dönüş
        KLT001 = 27 //Kalite işlemleri

    }
}
