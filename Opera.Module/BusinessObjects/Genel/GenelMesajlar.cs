using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mikrobar.Module.BusinessObjects
{
    public class GenelMesajlar
    {

        public static Mesaj ERR_1001 = new Mesaj(1001, "GNL", "ERR_1001: Kullanıcı adı veya şifre hatalı");
        public static Mesaj ERR_1002 = new Mesaj(1002, "GNL", "ERR_1002: Sistem parametreleri yüklenmemiş..");
        public static Mesaj ERR_1003 = new Mesaj(1003, "GNL", "ERR_1003: Uygulamanizin versiyonu uymuyor guncelleme yapin!");
        public static Mesaj ERR_1004 = new Mesaj(1004, "GNL", "ERR_1004: Nesne bos olamaz! {0}");
        public static Mesaj ERR_1005 = new Mesaj(1005, "GNL", "ERR_1005: Sistem hatasi! {0}");
        public static Mesaj ERR_1006 = new Mesaj(1006, "GNL", "ERR_1006: Kullanici tanimi bulunamadi! {0}");


        //FSA modülü 4000
        //AMB modülü 3000
        //KLT modülü 7000
        //SVK modülü 2000

        //URT modülü 6000
        //SYM modülü 9000
        //OTM modülü 8000
        //DRF modülü 5000

        #region FSA Hata mesajları
        //Fason satinalma mesajlari
        public static Mesaj ERR_4001 = new Mesaj(4001, "FSA", "ERR_4001: AmbalajHareket kaydı bulunamadı...");
        public static Mesaj ERR_4002 = new Mesaj(4002, "FSA", "ERR_4002: Belge sunucuya gönderilirken hata! {0}");
        public static Mesaj ERR_4005 = new Mesaj(4005, "FSA", "ERR_4005: Iş emri bilgisi bulunamadı!");
        public static Mesaj ERR_4006 = new Mesaj(4006, "FSA", "ERR_4006: Malzeme tanımı bulunamadı! {0}");
        public static Mesaj ERR_4007 = new Mesaj(4007, "FSA", "ERR_4007: Ambalaj tanımı bulunamadı!  {0}");
        public static Mesaj ERR_4008 = new Mesaj(4008, "FSA", "ERR_4008: Ambalaj deposu bilinmiyor!");
        public static Mesaj ERR_4009 = new Mesaj(4009, "FSA", "ERR_4009: Ambalaj deposu fason istasyon koltuk deposunda değil! {0}");
        public static Mesaj ERR_4010 = new Mesaj(4010, "FSA", "ERR_4010: Ambalaj rafı Fason istasyon rafında değil! {0}");
        public static Mesaj ERR_4011 = new Mesaj(4011, "FSA", "ERR_4011: Ambalaj iş emri uymuyor!");
        public static Mesaj ERR_4012 = new Mesaj(4012, "FSA", "ERR_4012: Ambalaj durumu kullanım için uygun değil! {0}");
        public static Mesaj ERR_4013 = new Mesaj(4013, "FSA", "ERR_4013: Ambalaj bolunemez! Ambalaj turunu kontrol edin! {0}");
        public static Mesaj ERR_4014 = new Mesaj(4014, "FSA", "ERR_4014: Ambalaj türü bilinmiyor! Ambalaj turunu kontrol edin! {0}");
        public static Mesaj ERR_4015 = new Mesaj(4015, "FSA", "ERR_4015: Ambalaj malzemesi is emrine uymuyor!");
        public static Mesaj ERR_4016 = new Mesaj(4016, "FSA", "ERR_4016: Ambalaj kalan miktari yetersiz, miktar hatali!");
        public static Mesaj ERR_4017 = new Mesaj(4017, "FSA", "ERR_4017: Malzeme tanimi bulunamadi!  {0}");
        public static Mesaj ERR_4018 = new Mesaj(4018, "FSA", "ERR_4018: Recete birim tanimi bulunamadi! (MalzemeBirimleri) {0}");
        public static Mesaj ERR_4019 = new Mesaj(4019, "FSA", "ERR_4019: ID:{0}");
        public static Mesaj ERR_4020 = new Mesaj(4020, "FSA", "ERR_4020: Ambalaj birim tanimi bulunamadi! (MalzemeBirimleri){0}");
        public static Mesaj ERR_4021 = new Mesaj(4021, "FSA", "ERR_4021: Is Emri bilesenlerinde seri takibi zorunlu olan malzeme var. Malzeme:{0}, Gereken:{1}, Eksik:{2} Uretim yapilamaz!");
        public static Mesaj ERR_4022 = new Mesaj(4022, "FSA", "ERR_4022: Depo tanımı bulunamadı! {0}");
        public static Mesaj ERR_4023 = new Mesaj(4023, "FSA", "ERR_4023: Stok hareketi bulunamadi  {0}");
        public static Mesaj ERR_4024 = new Mesaj(4024, "FSA", "ERR_4024: Hareket kaydı bulunamadı!");
        public static Mesaj ERR_4025 = new Mesaj(4025, "FSA", "ERR_4025: Kayıt sunucuya gönderilemedi!{0}");
        public static Mesaj ERR_4026 = new Mesaj(4026, "FSA", "ERR_4026: Ambalaj bulunamadı..{0}");
        public static Mesaj ERR_4027 = new Mesaj(4027, "FSA", "ERR_4027: Fason istasyon kodu tanimli degil! {0}");
        public static Mesaj ERR_4028 = new Mesaj(4028, "FSA", "ERR_4028: Fason istasyon malzeme cikis deposu tanimli degil! {0}");
        public static Mesaj ERR_4029 = new Mesaj(4029, "FSA", "ERR_4029: Ithalat belgesi bulunamadi!");
        public static Mesaj ERR_4030 = new Mesaj(4030, "FSA", "ERR_4030: Fason'a malzeme gönderiminde, aynı belgede tek iş emrine malzeme gönderilebilir!  {0}");
        public static Mesaj ERR_4031 = new Mesaj(4031, "FSA", "ERR_4031: Miktar hatalı..");
        public static Mesaj ERR_4032 = new Mesaj(4032, "FSA", "ERR_4032: Ambalaj durumu kullanıma uygun değil! Ambalaj Durumu:{0}");
        public static Mesaj ERR_4033 = new Mesaj(4033, "FSA", "ERR_4033: Ambalaj turu malzeme cikisina uygun degil..");
        public static Mesaj ERR_4034 = new Mesaj(4034, "FSA", "ERR_4034: Ambalaj turu Hammadde/YariMamul turunde olmalidir..");
        public static Mesaj ERR_4035 = new Mesaj(4035, "FSA", "ERR_4035: Depo kodu bulunamadı kontrol edin!");
        public static Mesaj ERR_4036 = new Mesaj(4036, "FSA", "ERR_4036:Ambalaj depo bilgisinde hata var!");
        public static Mesaj ERR_4037 = new Mesaj(4037, "FSA", "ERR_4037:Ambalaj çıkış deposunda değil! Ambalaj Deposu:{0}, Çıkış Deposu:{1}");
        public static Mesaj ERR_4038 = new Mesaj(4038, "FSA", "ERR_4038:Okutulan malzeme '{0}' iş emri bileşenlerinde bulunamadı!");
        public static Mesaj ERR_4039 = new Mesaj(4039, "FSA", "ERR_4039:Bileşen malzeme birim tanımı bulunamadı! Birim:{0}, {1} Malzeme:{2}, {3}");
        public static Mesaj ERR_4040 = new Mesaj(4040, "FSA", "ERR_4040:Malzeme birim tanımı bulunamadı! Birim:{0}, {1} Malzeme:{2}, {3}");
        public static Mesaj ERR_4041 = new Mesaj(4041, "FSA", "ERR_4041: Bileşen miktarı aşılıyor.. ");
        public static Mesaj ERR_4043 = new Mesaj(4043, "FSA", "ERR_4043:Hareket detay kaydı bulunamadı! (1097)");
        public static Mesaj ERR_4044 = new Mesaj(4044, "FSA", "ERR_4044:Uretim parametresi tanimli degil!");
        public static Mesaj ERR_4045 = new Mesaj(4045, "FSA", "ERR_4045:Uretim yeni ambalaj turu tanimli degil!");
        public static Mesaj ERR_4047 = new Mesaj(4047, "FSA", "ERR_4047:Transfer parametreleri tanimli degil!");
        public static Mesaj ERR_4048 = new Mesaj(4048, "FSA", "ERR_4048:Sipariş detayındaki depo tanımsız..  ");
        public static Mesaj ERR_4049 = new Mesaj(4049, "FSA", "ERR_4049:Ambalajhareket detayları boş olamaz..");
        public static Mesaj ERR_4050 = new Mesaj(4050, "FSA", "ERR_4050:Silinecek detay bilgisi bulunamadı..  ");
        public static Mesaj ERR_4051 = new Mesaj(4051, "FSA", "ERR_4051:Belge bulunamadı..");
        public static Mesaj ERR_4054 = new Mesaj(4054, "FSA", "ERR_4054:Ambalaj hareket numarası bulunamadı.");
        public static Mesaj ERR_4055 = new Mesaj(4055, "FSA", "ERR_4055:Ambalaj hareket kaydı bulunamadı.");
        public static Mesaj ERR_4056 = new Mesaj(4056, "FSA", "ERR_4056:Silinecek kayıt bulunamadı..");
        public static Mesaj ERR_4057 = new Mesaj(4057, "FSA", "ERR_4057:Güncelleme için bilgiler eksik..");
        public static Mesaj ERR_4059 = new Mesaj(4059, "FSA", "ERR_4059:Kayıt bulunamadı : {0}");
        public static Mesaj ERR_4060 = new Mesaj(4060, "FSA", "ERR_4060:Stokhareket prametresi eksik");
        public static Mesaj ERR_4061 = new Mesaj(4061, "FSA", "ERR_4061:Stokhareket kaydı bulunamadı...");
        public static Mesaj ERR_4062 = new Mesaj(4062, "FSA", "ERR_4062:Ambalaj bilgisi kaydı bulunamadı..  ");
        public static Mesaj ERR_4064 = new Mesaj(4064, "FSA", "ERR_4064:Belge numarası bulunamadı.");
        public static Mesaj ERR_4065 = new Mesaj(4065, "FSA", "ERR_4065:Detay kaydı bulunamadı.");
        public static Mesaj ERR_4066 = new Mesaj(4066, "FSA", "ERR_4066:Malzeme birim tanımında hata! Miktar 2 sıfır olamaz! Malzeme:{0}, Birim:{1}.");
        public static Mesaj ERR_4067 = new Mesaj(4067, "FSA", "ERR_4067:Belge detayi bos kayit gonderilemez! Belge:{0}");
        public static Mesaj ERR_4068 = new Mesaj(4068, "FSA", "ERR_4068:Fason gelis yapilan cikis belgesi silinemez! Belge:{0}");
        #endregion

        #region AMB 3000
        //Ambalaj hata mesajları 
        public static Mesaj ERR_3066 = new Mesaj(3066, "AMB", "ERR_3066: {0}  Kullanicisi tarafindan ambalaj rezervli durumuna getirilmistir. Transfer Edilemez..");
        public static Mesaj ERR_3065 = new Mesaj(3065, "AMB", "ERR_3065: Uretim parametresi tanimli degil!");
        public static Mesaj ERR_3001 = new Mesaj(3001, "AMB", "ERR_3001: Malzeme çıkış yeni ambalaj turu tanimli degil!");
        public static Mesaj ERR_3002 = new Mesaj(3002, "AMB", "ERR_3002: Ambalaj turu sorgulanamadı.\n {0}");
        public static Mesaj ERR_3003 = new Mesaj(3003, "AMB", "ERR_3003: Ambalaj hareket kaydinda hata! Stok hareket kaydi bulunamadı!");
        public static Mesaj ERR_3004 = new Mesaj(3004, "AMB", "ERR_3004: Malzeme cikis islemi icin Ambalaj zorunludur!");
        public static Mesaj ERR_3005 = new Mesaj(3005, "AMB", "ERR_3005: Malzeme cikis islemi icin Ürün ağacı zorunludur!");
        public static Mesaj ERR_3006 = new Mesaj(3006, "AMB", "ERR_3006: Malzeme cikis islemi icin miktar zorunludur!");
        public static Mesaj ERR_3007 = new Mesaj(3007, "AMB", "ERR_3007: Istasyon bos geçilemez!");
        public static Mesaj ERR_3008 = new Mesaj(3008, "AMB", "ERR_3008: Istasyon rafi tanımlı değil!");
        public static Mesaj ERR_3009 = new Mesaj(3009, "AMB", "ERR_3009: Ambalaj bilgilerinde hata!");
        public static Mesaj ERR_3010 = new Mesaj(3010, "AMB", "ERR_3010: Belge hareket kaydında hata var! Hatali kayit {0}");
        public static Mesaj ERR_3011 = new Mesaj(3011, "AMB", "ERR_3011: Stok hareket ön kaydinda hata var! Hatali kayit {0}");
        public static Mesaj ERR_3012 = new Mesaj(3012, "AMB", "ERR_3012: İş emri tipi zorunludur!");
        public static Mesaj ERR_3013 = new Mesaj(3013, "AMB", "ERR_3013: İş emri tipi zorunludur! İş emri tipini kontrol edin.");
        public static Mesaj ERR_3014 = new Mesaj(3014, "AMB", "ERR_3014: Raf boş geçilemez!");
        public static Mesaj ERR_3015 = new Mesaj(3015, "AMB", "ERR_3015: Belge kayıt edilmemiş. ");
        public static Mesaj ERR_3016 = new Mesaj(3016, "AMB", "ERR_3016: Belge detayları gönderilmemiş. ");
        public static Mesaj ERR_3017 = new Mesaj(3017, "AMB", "ERR_3017: Transfer parametresi bos olamaz!");
        public static Mesaj ERR_3018 = new Mesaj(3018, "AMB", "ERR_3018: Hedef raf boş olamaz");
        public static Mesaj ERR_3019 = new Mesaj(3019, "AMB", "ERR_3019: Kaynak raf boş olamaz");
        public static Mesaj ERR_3020 = new Mesaj(3020, "AMB", "ERR_3020: Ambalaj  boş olamaz");
        public static Mesaj ERR_3021 = new Mesaj(3021, "AMB", "ERR_3021: Hedef raf  tanımsız olamaz..");
        public static Mesaj ERR_3022 = new Mesaj(3022, "AMB", "ERR_3022: Kaynak raf tanımsız olamaz..");
        public static Mesaj ERR_3023 = new Mesaj(3023, "AMB", "ERR_3023: Sanal transfer deposu tanımsız olamaz..");
        public static Mesaj ERR_3024 = new Mesaj(3024, "AMB", "ERR_3024: Transfer isleminde Tek depodan cikis yapilabilir! Cikilan:{0}, Ambalaj Depo:{1}");
        public static Mesaj ERR_3026 = new Mesaj(3026, "AMB", "ERR_3026: Ambalaj hareket bilgisi bulunamadı.");
        public static Mesaj ERR_3028 = new Mesaj(3028, "AMB", "ERR_3028: Ambalaj detayları boş olamaz.");
        public static Mesaj ERR_3029 = new Mesaj(3029, "AMB", "ERR_3029: Raf bilgisi boş olamaz..");
        public static Mesaj ERR_3030 = new Mesaj(3030, "AMB", "ERR_3030: Ambalajhareket bilgisi kayıt edilmemiş.");
        public static Mesaj ERR_3031 = new Mesaj(3031, "AMB", "ERR_3031: Ambalajhareket bilgisinin stokhareket ilişkisi kurulmamış işlem devam edemez..");
        public static Mesaj ERR_3034 = new Mesaj(3034, "AMB", "ERR_3034:Malzeme bilgisi bulunamadı! :{0}");
        public static Mesaj ERR_3035 = new Mesaj(3035, "AMB", "ERR_3035:MalzemeId olmadan detay kayıt edilemez.. {0} ");
        public static Mesaj ERR_3036 = new Mesaj(3036, "AMB", "ERR_3036: Birim olmadan detay kayıt edilemez.. {0}");
        public static Mesaj ERR_3037 = new Mesaj(3037, "AMB", "ERR_3037:Birim tanımlı değil {0}");
        public static Mesaj ERR_3038 = new Mesaj(3038, "AMB", "ERR_3038:Raf bulunamadi Raf tanimini kontrol edin! {0} ");
        public static Mesaj ERR_3039 = new Mesaj(3039, "AMB", "ERR_3039: Ambalaj bulunamadı!:{0}");
        public static Mesaj ERR_3040 = new Mesaj(3040, "AMB", "ERR_3040: Ambalaj detayı bulunamadı!");
        public static Mesaj ERR_3041 = new Mesaj(3041, "AMB", "ERR_3041: Sunucu hatasi! :{0}");
        public static Mesaj ERR_3042 = new Mesaj(3042, "AMB", "ERR_3042: Ambalaj bilgisi eksik ambalaj silinemez!");
        public static Mesaj ERR_3046 = new Mesaj(3046, "AMB", "ERR_3046: Ambalaj detay bilgisi eksik ambalaj silinemez!");
        public static Mesaj ERR_3047 = new Mesaj(3047, "AMB", "ERR_3047: Ambalaj detayi bulunamadı!:{0}");
        public static Mesaj ERR_3048 = new Mesaj(3048, "AMB", "ERR_3048: Hedef ambalaj bulunamadi:{0}");
        public static Mesaj ERR_3049 = new Mesaj(3049, "AMB", "ERR_3049: Ambalajlarin depolari farkli bu islem icin kullanilamazlar {0},{1}");
        public static Mesaj ERR_3050 = new Mesaj(3050, "AMB", "ERR_3050: Ambalajlarin Parti numaralari farkli bu islem icin kullanilamazlar {0},{1}");
        public static Mesaj ERR_3051 = new Mesaj(3051, "AMB", "ERR_3051: Kaynak ambalaj bolunemez!");
        public static Mesaj ERR_3052 = new Mesaj(3052, "AMB", "ERR_3052: Kaynak ambalajin durumunu kontrol edin bolunemez!:{0}");
        public static Mesaj ERR_3053 = new Mesaj(3053, "AMB", "ERR_3053: Ambalaj miktari yetersiz! Ambalaj miktari:{0}, Cikilan:{1}");
        public static Mesaj ERR_3054 = new Mesaj(3054, "AMB", "ERR_3054: Ambalaj miktari2 yetersiz! Ambalaj miktari2:{0}, Cikilan:{1}");
        public static Mesaj ERR_3055 = new Mesaj(3055, "AMB", "ERR_3055: Ambalaj bulunamadi :{0}");
        public static Mesaj ERR_3056 = new Mesaj(3056, "AMB", "ERR_3056: Hedef ambalaj bilinmiyor!");
        public static Mesaj ERR_3059 = new Mesaj(3059, "AMB", "ERR_3059: Ambalaj bilgisi eksik ambalaj oluşturulamaz!");
        public static Mesaj ERR_3060 = new Mesaj(3060, "AMB", "ERR_3060: Ambalajin Depo/Raf bilgisi eksik ambalaj oluşturulamaz!");
        public static Mesaj ERR_3061 = new Mesaj(3061, "AMB", "ERR_3061: Ambalajin tur bilgisi eksik ambalaj oluşturulamaz!");
        public static Mesaj ERR_3063 = new Mesaj(3063, "AMB", "ERR_3063: Kullanıcı bilgileri eksik oturum açın!");
        public static Mesaj ERR_3064 = new Mesaj(3064, "AMB", "ERR_3064: Ambalaj bilgisi eksik ambalaj oluşturulamaz!");
        public static Mesaj ERR_3076 = new Mesaj(3066, "AMB", "ERR_3066: Ambalaj olusturulamadi!");
        
        #endregion

        #region KLT 7000
        //kalite hata mesajları
        public static Mesaj ERR_7001 = new Mesaj(7001, "KLT", "ERR_7001: Kaynak ambalaj bulunamadi!");
        public static Mesaj ERR_7002 = new Mesaj(7002, "KLT", "ERR_7002: Kaynak ambalaj bilgisinde hata var!");
        public static Mesaj ERR_7003 = new Mesaj(7003, "KLT", "ERR_7003: Kaynak ambalaj miktari yetersiz!");
        public static Mesaj ERR_7004 = new Mesaj(7004, "KLT", "ERR_7004: Kaynak ambalaj birden fazla malzeme iceriyor!");
        public static Mesaj ERR_7005 = new Mesaj(7005, "KLT", "ERR_7005: Transfer parametresi tanimli degil!");
        public static Mesaj ERR_7006 = new Mesaj(7006, "KLT", "ERR_7006: Hedef depo kodu bulunamadi!");
        public static Mesaj ERR_7007 = new Mesaj(7007, "KLT", "ERR_7007: Kalite onayi bulunamadi!");
        public static Mesaj ERR_7008 = new Mesaj(7008, "KLT", "ERR_7008: Ambalaj bulunamadi!");
        public static Mesaj ERR_7009 = new Mesaj(7009, "KLT", "ERR_7009: Ambalaj hareket kaydi bulunamadi! : {0}");
        public static Mesaj ERR_7010 = new Mesaj(7010, "KLT", "ERR_7010: Ambalaj hareket kaydinda hata var! Belge bos. : {0}");
        public static Mesaj ERR_7011 = new Mesaj(7011, "KLT", "ERR_7011: Satinalma - FasonÇıkış belgesi bulunamadi! {0}");
        public static Mesaj ERR_7012 = new Mesaj(7012, "KLT", "ERR_7012: Satinalma - Fason Dönüş belgesi bulunamadi! {0} {1}");
        public static Mesaj ERR_7013 = new Mesaj(7013, "KLT", "ERR_7013: Satinalma - Fason Çıkış belgesinin Ambalaj hareket bilgisi bulunamadi! {0}");
        public static Mesaj ERR_7015 = new Mesaj(7015, "KLT", "ERR_7015: Satin alma belgesinin Ambalaj hareket bilgisinde hata var! Belge bos! {0}");
        
        public static Mesaj ERR_7016 = new Mesaj(7016, "KLT", "ERR_7016: İş Emri bilgisi bulunamadı.");
        public static Mesaj ERR_7017 = new Mesaj(7017, "KLT", "ERR_7017: Operasyon bilgisi bulunamadı.");
        public static Mesaj ERR_7018 = new Mesaj(7018, "KLT", "ERR_7018: {0} iş emrine ait kontrol grubu bulunamadı.");
        public static Mesaj ERR_7019 = new Mesaj(7019, "KLT", "ERR_7019: Kalite parametresi tanimli degil. Lutfen Tanimlayin.");
        
        #endregion

        //sevkiyat hata mesajları 
        #region SVK 2000

        public static Mesaj ERR_2001 = new Mesaj(2001, "SVK", "ERR_2001: Ambalaj sorgulanamadı. Sunucu yanıt vermiyor!");
        public static Mesaj ERR_2002 = new Mesaj(2002, "SVK", "ERR_2002: Ambalaj sorgulanamadı. :{0}");
        public static Mesaj ERR_2003 = new Mesaj(2003, "SVK", "ERR_2003: Sıfır miktarlı sevkiyat yapılamaz!");
        public static Mesaj ERR_2004 = new Mesaj(2004, "SVK", "ERR_2004: Sevkiyat depo parametresi tanımlı değil!");
        public static Mesaj ERR_2005 = new Mesaj(2005, "SVK", "ERR_2005: Malzeme tanımı bulunamadı!  :{0}");
        public static Mesaj ERR_2006 = new Mesaj(2006, "SVK", "ERR_2006: Malzeme birim tanımı bulunamadı! :{0}");
        public static Mesaj ERR_2007 = new Mesaj(2007, "SVK", "ERR_2007: Stok hareket bilgisinde hata var!");
        public static Mesaj ERR_2008 = new Mesaj(2008, "SVK", "ERR_2008: Stok hareket bilgisinde hata var! Belge kapatılmış.");
        public static Mesaj ERR_2009 = new Mesaj(2009, "SVK", "ERR_2009: Sipariş bilgileri bulunamadı!");
        public static Mesaj ERR_2010 = new Mesaj(2010, "SVK", "ERR_2010: Sipariş listesinde olmayan ürün sevk edilemez!:{0}");
        public static Mesaj ERR_2011 = new Mesaj(2011, "SVK", "ERR_2011: Sipariş miktarı yetersiz fazla sevk!: {0}");
        public static Mesaj ERR_2012 = new Mesaj(2012, "SVK", "ERR_2012: Depo stok miktarı yetersiz! Okunan:{0}, Stok:{1}");
        public static Mesaj ERR_2013 = new Mesaj(2013, "SVK", "ERR_2013: Ambalaj bilgisi bulunamadı! :{0}");
        public static Mesaj ERR_2014 = new Mesaj(2014, "SVK", "ERR_2014: Ambalaj kullanıma uygun degil {0}");
        public static Mesaj ERR_2015 = new Mesaj(2015, "SVK", "ERR_2015: Ambalaj sevk edilemez!");
        public static Mesaj ERR_2016 = new Mesaj(2016, "SVK", "ERR_2016: Ambalaj sevk deposunda degil! : {0}");
        public static Mesaj ERR_2018 = new Mesaj(2018, "SVK", "ERR_2018: Ambalaj Durumu Kullanım İçin Uygun Değildir. Durum {0}");
        public static Mesaj ERR_2023 = new Mesaj(2023, "SVK", "ERR_2023: Okutulmamıs ürün çıkartılamaz!");
        public static Mesaj ERR_2024 = new Mesaj(2024, "SVK", "ERR_2024: Ambalaj bilgilerine ulaşılamadı!");
        public static Mesaj ERR_2025 = new Mesaj(2025, "SVK", "ERR_2025: Gönderilecek belge bilgisi boş geçilemez!");
        public static Mesaj ERR_2028 = new Mesaj(2028, "SVK", "ERR_2028: Belge Erp sistemine gonderilemedi! :{0}");
        public static Mesaj ERR_2029 = new Mesaj(2029, "SVK", "ERR_2029: Cari tanımı bulunamadı!:{0}");
        public static Mesaj ERR_2030 = new Mesaj(2030, "SVK", "ERR_2030: Sipariş bilgisi boş geçilemez!");
        public static Mesaj ERR_2031 = new Mesaj(2031, "SVK", "ERR_2031: Sevkiyat parametreleri tanımlı değil!");
        public static Mesaj ERR_2033 = new Mesaj(2033, "SVK", "ERR_2033: Stok hareket belgesi oluşturulurken hata: {0}");
        public static Mesaj ERR_2034 = new Mesaj(2034, "SVK", "ERR_2034: Stok Hareket Belgesi Bulunamadı.");
        public static Mesaj ERR_2035 = new Mesaj(2035, "SVK", "ERR_2035: Seçilen Paketin Sistemde Tanımı Bulunmamaktadır.");
        public static Mesaj ERR_2036 = new Mesaj(2036, "SVK", "ERR_2036: Sevk Emri Bilgileri Bulunamadı.");
        public static Mesaj ERR_2037 = new Mesaj(2037, "SVK", "ERR_2037: Okutulan Ambalaj Sistemde Bulunamadı. Barkod: {0}");
        public static Mesaj ERR_2038 = new Mesaj(2038, "SVK", "ERR_2038: Okutulan Ambalaj Sevk İçin Uygun Değildir. Ambalaj Durumu: {0}");
        public static Mesaj ERR_2039 = new Mesaj(2039, "SVK", "ERR_2039: . Raf: {0}");
        public static Mesaj ERR_2040 = new Mesaj(2040, "SVK", "ERR_2040: Sevkiyat Parametresinde Hedef RafId Değeri Tanımlı Değil.");
        public static Mesaj ERR_2041 = new Mesaj(2041, "SVK", "ERR_2041: Eklenecek Ambalajın Deposu, Sevk Deposu İle Uyuşmamaktadır. (0)");
        public static Mesaj ERR_2042 = new Mesaj(2042, "SVK", "ERR_2042: Okutulan Ambalaj, Sipariş Listesindeki Malzemeler İle Uyuşmamaktadır. Okutulan Malzeme: {0}");
        public static Mesaj ERR_2043 = new Mesaj(2043, "SVK", "ERR_2043: Okutulan Toplam Miktar Sipariş Miktarını Aşmaktadır.");
        public static Mesaj ERR_2044 = new Mesaj(2044, "SVK", "ERR_2044: Okutulan Miktar, Sipariş Listesindeki Satır Miktarını Aşmaktadır. Ambalaj Bölme İşlemini Uygulayınız. MalzemeKod: {0}. ");
        public static Mesaj ERR_2045 = new Mesaj(2045, "SVK", "ERR_2045: Okutulan Miktar, Sipariş Miktarını Aşmaktadır. Sipariş Kalan: {0}, Okunan Miktar: {1}.");
        public static Mesaj ERR_2046 = new Mesaj(2046, "SVK", "ERR_2046: Okutulan ambalaj bulunamadı. ");
        public static Mesaj ERR_2047 = new Mesaj(2047, "SVK", "ERR_2047: Emir bilgileri bulunamadı.");
        public static Mesaj ERR_2048 = new Mesaj(2048, "SVK", "ERR_2048: Emir detaylarında okutulan ambalajın malzemesi bulunamadı.");
        public static Mesaj ERR_2049 = new Mesaj(2049, "SVK", "ERR_2049: Hareket kaydı bulunamadı.");
        public static Mesaj ERR_2050 = new Mesaj(2050, "SVK", "ERR_2050: Transfer depo kaydı bulunamadı.");
        public static Mesaj ERR_2051 = new Mesaj(2051, "SVK", "ERR_2051: Ambalaj kaydı bulunamadı.");
        public static Mesaj ERR_2052 = new Mesaj(2052, "SVK", "ERR_2052: Okutulan ambalaj listeye daha önce eklenmiştir.");
        public static Mesaj ERR_2053 = new Mesaj(2053, "SVK", "ERR_2053: Ambalajın durumu kullanım için uygun değildir.");
        public static Mesaj ERR_2054 = new Mesaj(2054, "SVK", "ERR_2054: Liste bilgileri alınırken hata oluştu.");
        public static Mesaj ERR_2055 = new Mesaj(2055, "SVK", "ERR_2055: Çıkartılmak için okutulan ambalaj listede bulunmamaktadır.");
        public static Mesaj ERR_2056 = new Mesaj(2056, "SVK", "ERR_2056: Hareket detay kaydı bulunamadı.");
        public static Mesaj ERR_2057 = new Mesaj(2057, "SVK", "ERR_2057: Hareket özet kaydı bulunamadı.");
        public static Mesaj ERR_2058 = new Mesaj(2058, "SVK", "ERR_2058: Stok hareket kaydı bulunamadı.");
        public static Mesaj ERR_2059 = new Mesaj(2059, "SVK", "ERR_2059: Ambalaj detay kaydı bulunamadı.");
        public static Mesaj ERR_2060 = new Mesaj(2060, "SVK", "ERR_2060: Belge kapalıdır, işlem yapılamaz.");
        public static Mesaj ERR_2061 = new Mesaj(2061, "SVK", "ERR_2061: Parti Kodu Girilmemiş");
        public static Mesaj ERR_2062 = new Mesaj(2062, "SVK", "ERR_2062: Sevk Emir yada Stok Seçilmemiş !");

        
        #endregion

        //DRF HATA MESAJLARİ 
        #region DRF 5000

        public static Mesaj ERR_5001 = new Mesaj(5001, "DRF", "ERR_5001: Ambalaj bilgisi bulunamadı.!");
        public static Mesaj ERR_5002 = new Mesaj(5002, "DRF", "ERR_5002: Ambalaj detayları bulunamadı. {0} ");
        public static Mesaj ERR_5003 = new Mesaj(5003, "DRF", "ERR_5003: Malzeme barkodu bulunamadı.");
        public static Mesaj ERR_5004 = new Mesaj(5004, "DRF", "ERR_5004: Birimler bulunamadı.");
        public static Mesaj ERR_5005 = new Mesaj(5005, "DRF", "ERR_5005: Okutulan ambalaj detayı sistemde bulunamadı.");
        public static Mesaj ERR_5006 = new Mesaj(5006, "DRF", "ERR_5006: Okutulan ambalaj sistemde bulunamadı.");
        public static Mesaj ERR_5007 = new Mesaj(5007, "DRF", "ERR_5007: Bu ambalaj daha önce eklenmiştir.");
        public static Mesaj ERR_5008 = new Mesaj(5008, "DRF", "ERR_5008: Ambalajların depoları aynı olmalıdır.");
        public static Mesaj ERR_5009 = new Mesaj(5009, "DRF", "ERR_5009: Ambalajların rafları aynı olmalıdır.");
        public static Mesaj ERR_5010 = new Mesaj(5010, "DRF", "ERR_5010: Okutulan ürün ambalaj içerisine eklenemez.");
        public static Mesaj ERR_5011 = new Mesaj(5011, "DRF", "ERR_5011: Okutulan ambalajın detayı bulunamadı. ");
        public static Mesaj ERR_5012 = new Mesaj(5012, "DRF", "ERR_5012: Ambalaj detay kaydı bulunamadı. ");
        public static Mesaj ERR_5013 = new Mesaj(5013, "DRF", "ERR_5013: Ambalaj kaydı bulunamadı.");
        public static Mesaj ERR_5014 = new Mesaj(5014, "DRF", "ERR_5014: Depo Stok Sorgulaması İçin Bir Depo Kodu Giriniz. ");
        public static Mesaj ERR_5015 = new Mesaj(5015, "DRF", "ERR_5015: Raf Stok Sorgulaması İçin Bir Depo Kodu Giriniz.");
        public static Mesaj ERR_5016 = new Mesaj(5016, "DRF", "ERR_5016: Serili Stok Sorgulaması İçin Bir Depo Kodu Giriniz.");
        public static Mesaj ERR_5017 = new Mesaj(5017, "DRF", "ERR_5017: Belge kayit edilmemiş  ");
        public static Mesaj ERR_5018 = new Mesaj(5018, "DRF", "ERR_5018: Belge detayları bulunamadı ");
        public static Mesaj ERR_5019 = new Mesaj(5019, "DRF", "ERR_5019: Bu belge kapalı devam edilemez.. ");
        public static Mesaj ERR_5020 = new Mesaj(5020, "DRF", "ERR_5020: Belge detay kayıtlarında hata.\nAynı malzeme ve depo bilgisi birden fazla satırda olamaz..");
        public static Mesaj ERR_5021 = new Mesaj(5021, "DRF", "ERR_5021: Detay bilgilerde hata.\nHedef raf boş olamaz.. ");
        public static Mesaj ERR_5022 = new Mesaj(5022, "DRF", "ERR_5022: Belge detay kayıtlarında malzeme birden fazla satırda olamaz.. ");
        public static Mesaj ERR_5026 = new Mesaj(5026, "DRF", "ERR_5026: Detay bilgisi kayıtlı değil. ");
        public static Mesaj ERR_5027 = new Mesaj(5027, "DRF", "ERR_5027: Ambalaj sorgulanamadı. Sunucu yanıt vermiyor!");
        public static Mesaj ERR_5028 = new Mesaj(5028, "DRF", "ERR_5028: Ambalaj sorgulanamadı. {0}");
        public static Mesaj ERR_5029 = new Mesaj(5029, "DRF", "ERR_5029: Transfer parametresinde talep hareket kodu tanımlı değil!");
        public static Mesaj ERR_5030 = new Mesaj(5030, "DRF", "ERR_5030: Talep bilgisi boş geçilemez!");
        public static Mesaj ERR_5031 = new Mesaj(5031, "DRF", "ERR_5031: Talep bilgisi bulunamadı! {0}");
        public static Mesaj ERR_5032 = new Mesaj(5032, "DRF", "ERR_5032: Talep kaydı kapanmış işlem yapılamaz!");
        public static Mesaj ERR_5033 = new Mesaj(5033, "DRF", "ERR_5033: Stok hareket bilgisinde hata var!");
        public static Mesaj ERR_5034 = new Mesaj(5034, "DRF", "ERR_5034: Stok hareket bilgisinde hata var! Belge kapatılmış.");
        public static Mesaj ERR_5035 = new Mesaj(5035, "DRF", "ERR_5035: Sistem hatasi(202) Stok hareket bilgisinde hata var! ");
        public static Mesaj ERR_5036 = new Mesaj(5036, "DRF", "ERR_5036: Talep tam olarak sevk edilmelidir! Talep Edilen:{0}, Okunan:{1} Malzeme:{2}\n");
        public static Mesaj ERR_5037 = new Mesaj(5037, "DRF", "ERR_5037: Aktarım hatası (226) Ambalaj hareket belgesinde hata var! Kaynak depo bilgisi boş olamaz! ");
        public static Mesaj ERR_5039 = new Mesaj(5039, "DRF", "ERR_5039: Belge Erp sistemine gonderilemedi! {0} ");
        public static Mesaj ERR_5040 = new Mesaj(5040, "DRF", "ERR_5040: Aktarım hatası (226) Ambalaj hareket belgesinde hata var! Hedef depo bilgisi boş olamaz!");
        public static Mesaj ERR_5043 = new Mesaj(5043, "DRF", "ERR_5043: Talep sevk belgesi bulunamadı! {0}  ");
        public static Mesaj ERR_5044 = new Mesaj(5044, "DRF", "ERR_5044: Talep sevk belgesinde hata var! {0} ");
        public static Mesaj ERR_5045 = new Mesaj(5045, "DRF", "ERR_5045: Talep bilgisi bulunamadı! ");
        public static Mesaj ERR_5048 = new Mesaj(5048, "DRF", "ERR_5048: Talep sevk belgesindeki malzemelerin tamamı okutulmadı! Sevk:{0}, Kabul:{1}");
        public static Mesaj ERR_5054 = new Mesaj(5054, "DRF", "ERR_5054: Depo bilgisi boş olamaz!");
        public static Mesaj ERR_5055 = new Mesaj(5055, "DRF", "ERR_5055: Aynı depoya talep açılamaz!");
        public static Mesaj ERR_5059 = new Mesaj(5059, "DRF", "ERR_5059: Sıfır miktarlı sevkiyat yapılamaz!");
        public static Mesaj ERR_5060 = new Mesaj(5060, "DRF", "ERR_5060: Malzeme tanımı bulunamadı {0} !");
        public static Mesaj ERR_5061 = new Mesaj(5061, "DRF", "ERR_5061: Malzeme birim tanımı bulunamadı  {0} !");
        public static Mesaj ERR_5064 = new Mesaj(5064, "DRF", "ERR_5064: Ambalaj deposu bilinmiyor hatalı kayıt! ");
        public static Mesaj ERR_5065 = new Mesaj(5065, "DRF", "ERR_5065: Ambalaj bilgisi bulunamadı! ");
        public static Mesaj ERR_5066 = new Mesaj(5066, "DRF", "ERR_5066: Ambalaj kullanıma uygun degil ");
        public static Mesaj ERR_5067 = new Mesaj(5067, "DRF", "ERR_5067: Ambalaj talep deposunda degil!");
        public static Mesaj ERR_5069 = new Mesaj(5069, "DRF", "ERR_5069: Ambalaj Durumu Kullanım İçin Uygun Değildir. Durum {0}");
        public static Mesaj ERR_5072 = new Mesaj(5072, "DRF", "ERR_5072: Talep sevk belgesinde hata var!");
        public static Mesaj ERR_5075 = new Mesaj(5075, "DRF", "ERR_5075: Malzeme birim tanımı bulunamadı!  ");
        public static Mesaj ERR_5079 = new Mesaj(5079, "DRF", "ERR_5079: Talep sevk listesinden fazla malzeme kabul edilemez! {0}, Sevk:{1}");
        public static Mesaj ERR_5080 = new Mesaj(5080, "DRF", "ERR_5080: Ambalaj bilgisi bulunamadı! {0} ");
        public static Mesaj ERR_5081 = new Mesaj(5081, "DRF", "ERR_5081: Ambalaj kullanıma uygun degil  {0} ");
        public static Mesaj ERR_5082 = new Mesaj(5082, "DRF", "ERR_5082: Ambalaj talep deposunda degil!, {0} ");
        public static Mesaj ERR_5083 = new Mesaj(5083, "DRF", "ERR_5083: Talep sevk belgesinin seri belgesi bulunamadı! {0} ");
        public static Mesaj ERR_5084 = new Mesaj(5084, "DRF", "ERR_5084: Talep sevk belgesinde okutulmayan ambalaj kabul edilemez! {0} {1} ");
        public static Mesaj ERR_5091 = new Mesaj(5091, "DRF", "ERR_5091: Malzeme birim tanımı bulunamadı!   {0} ");
        public static Mesaj ERR_5094 = new Mesaj(5094, "DRF", "ERR_5094: Okutulan ürün çıkartılamaz! ");
        public static Mesaj ERR_5095 = new Mesaj(5095, "DRF", "ERR_5095: Talep listesinde olmayan malzeme sevk edilemez! {0} ");
        public static Mesaj ERR_5096 = new Mesaj(5096, "DRF", "ERR_5096: Ambalaj bilgilerine ulaşılamadı!");
        public static Mesaj ERR_5098 = new Mesaj(5098, "DRF", "ERR_5098: Transfer parametreleri tanimli degil!");
        public static Mesaj ERR_5099 = new Mesaj(5099, "DRF", "ERR_5099: Belge Numarası Parametre İle Alınamadı.");
        public static Mesaj ERR_5100 = new Mesaj(5100, "DRF", "ERR_5100: Belge Bulunamadı. ");
        public static Mesaj ERR_5101 = new Mesaj(5101, "DRF", "ERR_5101: Geri alınacak detay satırı bulunamadı..  ");
        public static Mesaj ERR_5102 = new Mesaj(5102, "DRF", "ERR_5102: Bu satır silinmiştir. ");
        public static Mesaj ERR_5103 = new Mesaj(5103, "DRF", "ERR_5103: Raf bulunamadı.");
        public static Mesaj ERR_5104 = new Mesaj(5104, "DRF", "ERR_5104: Rapor için okutulan satır bilgisi bulunamadı.");
        public static Mesaj ERR_5105 = new Mesaj(5105, "DRF", "ERR_5105: Stok hareket kaydı bulunamadı.");
        public static Mesaj ERR_5106 = new Mesaj(5106, "DRF", "ERR_5106: Hareket kaydı bulunamadı.");
        public static Mesaj ERR_5107 = new Mesaj(5107, "DRF", "ERR_5107: Belge kapalıdır, işlem yapılamaz.");
        public static Mesaj ERR_5108 = new Mesaj(5108, "DRF", "ERR_5108: Hareket detayı bulunamadı.");
        public static Mesaj ERR_5111 = new Mesaj(5111, "DRF", "ERR_5111: Hareket Id değeri girilmelidir.");
        public static Mesaj ERR_5113 = new Mesaj(5113, "DRF", "ERR_5113: Kullanıcı Bilgisi Alınamadı.");
        public static Mesaj ERR_5114 = new Mesaj(5114, "DRF", "ERR_5114: Stok Hareket Id Bilgisi Alınamadı. ");
        public static Mesaj ERR_5115 = new Mesaj(5115, "DRF", "ERR_5115: Stok Hareket Bilgisi Alınamadı. ");
        public static Mesaj ERR_5116 = new Mesaj(5116, "DRF", "ERR_5116: Transfer parametresi tanimli degil. ");
        
        #endregion
        //URT HATA MESAJLARİ 
        #region URT 6000


        public static Mesaj ERR_6001 = new Mesaj(6001, "URT", "ERR_6001: Uretim yeni ambalaj turu tanimli degil!");
        public static Mesaj ERR_6002 = new Mesaj(6002, "URT", "ERR_6002: İş Emri bilgisi olmadan üretim kaydı girilemez! IsEmri:{0}, Detay:{1}, Operasyon:{2}");
        public static Mesaj ERR_6003 = new Mesaj(6003, "URT", "ERR_6003: İş Emri Operasyon bilgisi olmadan üretim kaydı girilemez!");
        public static Mesaj ERR_6004 = new Mesaj(6004, "URT", "ERR_6004: Üretim kaydında Vardiya zorunludur! Üretim başlatılamaz.");
        public static Mesaj ERR_6005 = new Mesaj(6005, "URT", "ERR_6005: İstasyonda açık üretim bulunmaktadır. Bu istasyon çoklu üretime kapalı.\n({0})");
        public static Mesaj ERR_6006 = new Mesaj(6006, "URT", "ERR_6006: Vardiya tanimi bulunamadi!");
        public static Mesaj ERR_6007 = new Mesaj(6007, "URT", "ERR_6007: Bu operasyon bu istasyonda tanimli degil!");
        public static Mesaj ERR_6008 = new Mesaj(6008, "URT", "ERR_6008: Ambalaj turu sorgulanamadı.\n({0})");
        public static Mesaj ERR_6009 = new Mesaj(6009, "URT", "ERR_6009: Istasyon koltuk Depo/Raf gozu tanimli degil!");
        public static Mesaj ERR_6011 = new Mesaj(6011, "URT", "ERR_6011: Ambalaj bilgisi bulunamadı! {0} ");
        public static Mesaj ERR_6012 = new Mesaj(6012, "URT", "ERR_6006: Bu ambalaj bu operasyondan geçmiş! {0} ");
        public static Mesaj ERR_6013 = new Mesaj(6013, "URT", "ERR_6013: Ambalajin rafi bilinmiyor {0}");
        public static Mesaj ERR_6014 = new Mesaj(6014, "URT", "ERR_6014: Ambalaj turu bilinmiyor! {0} ");
        public static Mesaj ERR_6015 = new Mesaj(6015, "URT", "ERR_6015: Ambalaj baska istasyonda! {0}");
        public static Mesaj ERR_6017 = new Mesaj(6017, "URT", "ERR_6017: Ambalaj rafi bos olamaz ");
        public static Mesaj ERR_6018 = new Mesaj(6018, "URT", "ERR_6018: Malzeme tanimi bulunamadi! \n({0}");
        public static Mesaj ERR_6019 = new Mesaj(6019, "URT", "ERR_6019: Istasyon Urun giris deposu bos olamaz! {0} ");
        public static Mesaj ERR_6020 = new Mesaj(6020, "URT", "ERR_6020: Istasyon Yarimamul giris deposu bos olamaz! {0}");
        public static Mesaj ERR_6021 = new Mesaj(6021, "URT", "ERR_6021: Secilen Ambalaj turu tanimi bulunamadi!");
        public static Mesaj ERR_6022 = new Mesaj(6022, "URT", "ERR_6022: Ambalaj turu bulunamadi! {0} ");
        public static Mesaj ERR_6023 = new Mesaj(6023, "URT", "ERR_6023: Üretim kaydı bilinmiyor!");
        public static Mesaj ERR_6025 = new Mesaj(6025, "URT", "ERR_6025: Malzeme tanımı bulunamadı! Kontrol edin. Malzeme:{0}");
        public static Mesaj ERR_6026 = new Mesaj(6026, "URT", "ERR_6026: Birim tanımı bulunamadı! Kontrol edin! Birim:{0}");
        public static Mesaj ERR_6027 = new Mesaj(6027, "URT", "ERR_6027: Depo tanımı bulunamadı! Kontrol edin! Depo:{0}");
        public static Mesaj ERR_6028 = new Mesaj(6028, "URT", "ERR_6028: Hurda kaydinda Ambalaj zorunludur!");
        public static Mesaj ERR_6029 = new Mesaj(6029, "URT", "ERR_6029: Ambalaj bulunamadi! {0}");
        public static Mesaj ERR_6031 = new Mesaj(6031, "URT", "ERR_6031: Ambalaj bolunemez hurda kaydi girilemez! {0}, {1}");
        public static Mesaj ERR_6032 = new Mesaj(6032, "URT", "ERR_6032: Ambalaj {0} hurda kaydi girilemez! {1}");
        public static Mesaj ERR_6033 = new Mesaj(6033, "URT", "ERR_6033: Ambalaj girilen malzemeyi icermiyor! Malzeme:{0}, {1}");
        public static Mesaj ERR_6034 = new Mesaj(6034, "URT", "ERR_6034: Istasyon koltuk Depo/Raf gozu tanimli degil!");
        public static Mesaj ERR_6035 = new Mesaj(6035, "URT", "ERR_6035: Ambalajin rafi bilinmiyor! {0} ");
        public static Mesaj ERR_6036 = new Mesaj(6036, "URT", "ERR_6036: Ambalaj istasyonun koltuk rafinda degil");
        public static Mesaj ERR_6037 = new Mesaj(6037, "URT", "ERR_6037: Istasyon tanimi bulunamadi! {0} ");
        public static Mesaj ERR_6038 = new Mesaj(6038, "URT", "ERR_6038: Istasyon depo tanimi bulunamadi! {0} ");
        public static Mesaj ERR_6039 = new Mesaj(6039, "URT", "ERR_6039: Ambalajin depo/rafi bilinmiyor! {0}");
        public static Mesaj ERR_6040 = new Mesaj(6040, "URT", "ERR_6040: Ambalaj istasyonun koltuk depo/rafinda degil! {0} ");
        public static Mesaj ERR_6041 = new Mesaj(6041, "URT", "ERR_6041: Hurda ambalaj turu tanımlı değil! {0}");
        public static Mesaj ERR_6042 = new Mesaj(6042, "URT", "ERR_6042: Duruş bitiş tarihi başlangıç tarihinden küçük olamaz! Durus:{0}, Baslangic:{1}");
        public static Mesaj ERR_6043 = new Mesaj(6043, "URT", "ERR_6043: Durus nedeni tanımı bulunamadı! {0} ");
        public static Mesaj ERR_6044 = new Mesaj(6044, "URT", "ERR_6044: İş emrine bağlı duruş nedeninde üretim kaydı zorunludur! {0}");
        public static Mesaj ERR_6045 = new Mesaj(6045, "URT", "ERR_6045: İstasyonda açık üretim kaydı mevcut, İş emrinden bağımsız duruş başlatılamaz!");
        public static Mesaj ERR_6046 = new Mesaj(6046, "URT", "ERR_6046: Istasyonda acik duruş mevcut yeni duruş baslatılamaz!");
        public static Mesaj ERR_6047 = new Mesaj(6047, "URT", "ERR_6047: İstasyonda açık durus kaydı mevcut, Yeni duruş kaydı başlatılamaz!");
        public static Mesaj ERR_6048 = new Mesaj(6048, "URT", "ERR_6048: Durus baslangici uretim baslangicini gecemez! Uretim:{0}, Durus:{1}");
        public static Mesaj ERR_6049 = new Mesaj(6049, "URT", "ERR_6049: Manuel duruş kaydı yapılamaz, Sadece otomatik duruş kaydı yapılabilir! {0}");
        public static Mesaj ERR_6050 = new Mesaj(6050, "URT", "ERR_6050: Üretim olan istasyonda iş emrine bağlı olmayan duruş başlatılamaz!");
        public static Mesaj ERR_6051 = new Mesaj(6051, "URT", "ERR_6051: Istasyon bilgisi eksik! {0} ");
        public static Mesaj ERR_6052 = new Mesaj(6052, "URT", "ERR_6052: Üretim kaydı bulunamadı! {0}");
        public static Mesaj ERR_6053 = new Mesaj(6053, "URT", "ERR_6053: Üretim kaydı kapanmis! {0}");
        public static Mesaj ERR_6054 = new Mesaj(6054, "URT", "ERR_6054: Personel kaydı bulunamadı! {0}");
        public static Mesaj ERR_6057 = new Mesaj(6057, "URT", "ERR_6057:Duruş tanımı bulunamadı! {0}");
        public static Mesaj ERR_6058 = new Mesaj(6058, "URT", "ERR_6058: İstasyon bilgisi buluanamadı! ({0})");
        public static Mesaj ERR_6062 = new Mesaj(6062, "URT", "ERR_6062: Ambalaj/Kasa kaydı bulunamadı! {0}");
        public static Mesaj ERR_6063 = new Mesaj(6063, "URT", "ERR_6063: Ambalaj/Kasa rafi bilinmiyor!");
        public static Mesaj ERR_6064 = new Mesaj(6064, "URT", "ERR_6064: Malzeme tanimi bulunamadi! {0}");
        public static Mesaj ERR_6065 = new Mesaj(6065, "URT", "ERR_6065: Ambalaj/Kasa Baska istasyonda!");
        public static Mesaj ERR_6066 = new Mesaj(6066, "URT", "ERR_6066: Uretim kaydinda iscilik kaydi zorunludur!");
        public static Mesaj ERR_6070 = new Mesaj(6070, "URT", "ERR_6070: Kasa miktari yetersiz uretilebilecek miktardan fazla uretim yapilamaz! Kalan Miktar:{0}");
        public static Mesaj ERR_6071 = new Mesaj(6071, "URT", "ERR_6071: Uretim kaydi sunucuya gonderilemedi [1725] : {0}  ");
        public static Mesaj ERR_6072 = new Mesaj(6072, "URT", "ERR_6072: Recete birim tanimi bulunamadi! (MalzemeBirimleri) {0}");
        public static Mesaj ERR_6073 = new Mesaj(6073, "URT", "ERR_6073: İş emrinde kullanilan malzemede Malzeme kodu zorunludur! {0}");
        public static Mesaj ERR_6074 = new Mesaj(6074, "URT", "ERR_6074: İş emrinde kullanilan malzemede Ambalaj zorunludur! {0}");
        public static Mesaj ERR_6075 = new Mesaj(6075, "URT", "ERR_6075: Ambalaj birim tanimi bulunamadi! (MalzemeBirimleri) {0}");
        public static Mesaj ERR_6081 = new Mesaj(6081, "URT", "ERR_6081: Ambalaj birim tanimi bulunamadi! (MalzemeBirimleri) {0} ");
        public static Mesaj ERR_6083 = new Mesaj(6083, "URT", "ERR_6083: İş Emri bilesenlerinde seri takibi zorunlu olan malzeme var. Malzeme:{0}, Gereken:{1}, Eksik:{2} Uretim yapilamaz! {3} ");
        public static Mesaj ERR_6084 = new Mesaj(6084, "URT", "ERR_6084: İş emrinde kullanilan malzemede Malzeme kodu zorunludur! {0}");
        public static Mesaj ERR_6085 = new Mesaj(6085, "URT", "ERR_6085: İş emrinde kullanilan malzemede Ambalaj zorunludur! {0}");
        public static Mesaj ERR_6086 = new Mesaj(6086, "URT", "ERR_6086: Recete birim tanimi bulunamadi! (MalzemeBirimleri) {0} ");
        public static Mesaj ERR_6088 = new Mesaj(6088, "URT", "ERR_6088: Malzeme birim tanimi bulunamadi! (MalzemeBirimleri) {0} ");
        public static Mesaj ERR_6089 = new Mesaj(6089, "URT", "ERR_6089: Hurda girisinde Ambalaj zorunludur {0}");
        public static Mesaj ERR_6092 = new Mesaj(6092, "URT", "ERR_6092: Ambalaj hurda miktarini karsilamiyor! Miktar:{0}, Gereken:{1}");
        public static Mesaj ERR_6093 = new Mesaj(6093, "URT", "ERR_6093: Hurda miktari uretim miktarinden fazla olamaz! Hurda miktar:{0}, Uretim miktar:{1}");
        public static Mesaj ERR_6095 = new Mesaj(6095, "URT", "ERR_6095: Malzeme kodu tanimli degil! {0} {1}");
        public static Mesaj ERR_6096 = new Mesaj(6096, "URT", "ERR_6096: İş Emri bileseni bilinmiyor! ");
        public static Mesaj ERR_6097 = new Mesaj(6097, "URT", "ERR_6097: Ambalaj bilgisi olmayan malzeme okutulamaz!");
        public static Mesaj ERR_6098 = new Mesaj(6098, "URT", "ERR_6098: Ambalaj bilgisi bulunamadi! {0}  ");
        public static Mesaj ERR_6099 = new Mesaj(6099, "URT", "ERR_6099: Ambalaj durumu kullanima uygun degil!");
        public static Mesaj ERR_6100 = new Mesaj(6100, "URT", "ERR_6100: Ambalajin rafi bilinmiyor! {0}");
        public static Mesaj ERR_6101 = new Mesaj(6101, "URT", "ERR_6101: Ambalaj istasyonun koltuk rafinda degil {0}  ");
        public static Mesaj ERR_6103 = new Mesaj(6103, "URT", "ERR_6103: Ambalaj istasyonun koltuk depo/rafinda degil {0}");
        public static Mesaj ERR_6105 = new Mesaj(6105, "URT", "ERR_6105: Ambalaj turu bilinmiyor hatali kayit! {0}");
        public static Mesaj ERR_6106 = new Mesaj(6106, "URT", "ERR_6106: Ambalaj turu malzeme kullanimina uygun degil! (Bolunemez) {0}");
        public static Mesaj ERR_6107 = new Mesaj(6107, "URT", "ERR_6107: Ambalaj urun iceriyor uretimde kullanilamaz! {0}");
        public static Mesaj ERR_6108 = new Mesaj(6108, "URT", "ERR_6108: Kasada farkli malzeme var uretimde kullanilamaz! {0} ");
        public static Mesaj ERR_6109 = new Mesaj(6109, "URT", "ERR_6109: Kasa miktari yetersiz uretim yapilamaz! {0}");
        public static Mesaj ERR_6110 = new Mesaj(6110, "URT", "ERR_6110: Kasa kullanilan miktari karsilamiyor! Kasa:{0}, Kullanilan:{1}");
        public static Mesaj ERR_6114 = new Mesaj(6114, "URT", "ERR_6114: Parametrede hic malzeme yok ");
        public static Mesaj ERR_6117 = new Mesaj(6117, "URT", "ERR_6117: Üretim bilgisi eksik!");
        public static Mesaj ERR_6118 = new Mesaj(6118, "URT", "ERR_6118: Üretim kaydı bulunamadı! {0} ");
        public static Mesaj ERR_6120 = new Mesaj(6120, "URT", "ERR_6120: Platform tanimi bulunamadi! {0}");
        public static Mesaj ERR_6121 = new Mesaj(6121, "URT", "ERR_6121: Platform kodu üretime eklenmemiş! {0}  ");
        public static Mesaj ERR_6122 = new Mesaj(6122, "URT", "ERR_6122: Platform kodu üretime eklenmiş! {0} ");
        public static Mesaj ERR_6123 = new Mesaj(6123, "URT", "ERR_6123: Malzeme tanimi bulunamadı ! ");
        public static Mesaj ERR_6124 = new Mesaj(6124, "URT", "ERR_6124: Malzeme kodu tanimli degil!");
        public static Mesaj ERR_6125 = new Mesaj(6125, "URT", "ERR_6125: Kullanilan malzeme alternatif malzeme olarak kullanilamaz! ");
        public static Mesaj ERR_6126 = new Mesaj(6126, "URT", "ERR_6126: Okutulan ambalaj bilgisi bulunamadi! {0} ");
        public static Mesaj ERR_6133 = new Mesaj(6133, "URT", "ERR_6133: Ambalaj turu malzeme kullanimina uygun degil, bolunemez! {0}  ");
        public static Mesaj ERR_6142 = new Mesaj(6142, "URT", "ERR_6142: Ambalaj bilgisi bulunamadi!");
        public static Mesaj ERR_6146 = new Mesaj(6146, "URT", "ERR_6146: Ambalajin depo/rafi bilinmiyor! ");
        public static Mesaj ERR_6147 = new Mesaj(6147, "URT", "ERR_6147: Ambalaj istasyonun koltuk depo/rafinda degil! ");
        public static Mesaj ERR_6148 = new Mesaj(6148, "URT", "ERR_6148: Ambalaj turu bilinmiyor hatali kayit! ");
        public static Mesaj ERR_6149 = new Mesaj(6149, "URT", "ERR_6149: Ambalaj turu malzeme kullanimina uygun degil! ");
        public static Mesaj ERR_6150 = new Mesaj(6150, "URT", "ERR_6150: Ambalaj urun iceriyor uretimde kullanilamaz!");
        public static Mesaj ERR_6153 = new Mesaj(6153, "URT", "ERR_6153: Kasada farkli malzeme var uretimde kullanilamaz!");
        public static Mesaj ERR_6154 = new Mesaj(6154, "URT", "ERR_6154: Kasa miktari yetersiz uretim yapilamaz!");
        public static Mesaj ERR_6155 = new Mesaj(6155, "URT", "ERR_6155: İs emri bilesenleri sorgulanamadi! ");
        public static Mesaj ERR_6156 = new Mesaj(6156, "URT", "ERR_6156: Ambalaj tanimi bulunamadi! ");
        public static Mesaj ERR_6157 = new Mesaj(6157, "URT", "ERR_6157: İş istasyonu bilgisi olmadan üretim kaydı girilemez! ");
        public static Mesaj ERR_6158 = new Mesaj(6158, "URT", "ERR_6158: Hedef istasyon bilgisi bulunamadı! ");
        public static Mesaj ERR_6159 = new Mesaj(6159, "URT", "ERR_6159: İstasyon tuketim deposu bilinmiyor!");
        public static Mesaj ERR_6162 = new Mesaj(6162, "URT", "ERR_6162: İstasyon Depo/Raf gozu tanimli degil! Istasyona tuketim Deposuna Raf tanimlayin. Istasyon:{0}");
        public static Mesaj ERR_6163 = new Mesaj(6163, "URT", "ERR_6163: Ambalajin deposu bilinmiyor!");
        public static Mesaj ERR_6164 = new Mesaj(6164, "URT", "ERR_6164: İş istasyonunda uretim bilgisi bulunamadi! ");
        public static Mesaj ERR_6165 = new Mesaj(6165, "URT", "ERR_6165: Üretim kaydi kapanmis! {0} ");
        public static Mesaj ERR_6166 = new Mesaj(6166, "URT", "ERR_6166: Üretim parametresi tanimi degil! ");
        public static Mesaj ERR_6167 = new Mesaj(6167, "URT", "ERR_6167: Girilen tolerans izin verilen sureden buyuk! {0}  ");
        public static Mesaj ERR_6171 = new Mesaj(6171, "URT", "ERR_6171: Ambalaj turu malzeme kullanimina uygun degil, bolunemez! ");
        public static Mesaj ERR_6172 = new Mesaj(6172, "URT", "ERR_6172: Is emri bilesenlerinde zorunlu olan malzeme var ve rafta bulunamadi! Uretim yapilamaz. ");
        public static Mesaj ERR_6173 = new Mesaj(6173, "URT", "ERR_6173: Duruş mevcut!");
        public static Mesaj ERR_6174 = new Mesaj(6174, "URT", "ERR_6174: Platform kodu secilmelidir!");
        public static Mesaj ERR_6175 = new Mesaj(6175, "URT", "ERR_6175: Uretim kaydi bulunamadi ");
        public static Mesaj ERR_6176 = new Mesaj(6176, "URT", "ERR_6176: İş emri duruş baslangici Is emri operasyon baslangicini gecemez! Durus:{0} ");
        public static Mesaj ERR_6177 = new Mesaj(6177, "URT", "ERR_6177: Uretim bitis zamani baslangicindan kucuk olamaz! Baslangic:{0}, Bitis:{1} ");
        public static Mesaj ERR_6178 = new Mesaj(6178, "URT", "ERR_6178: Uretim suresi sifir olamaz!");
        public static Mesaj ERR_6179 = new Mesaj(6179, "URT", "ERR_6179: Uretim girisi yapabilmek icin iscilik kaydi zorunludur! ");
        public static Mesaj ERR_6180 = new Mesaj(6180, "URT", "ERR_6180: Istasyon bilgisi bulunamadi tanimlari kontrol edin!");
        public static Mesaj ERR_6181 = new Mesaj(6181, "URT", "ERR_6181: Üretim girisinde Ambalaj zorunludur!");
        public static Mesaj ERR_6182 = new Mesaj(6182, "URT", "ERR_6182: Üretim girisinde Is emri zorunludur!");
        public static Mesaj ERR_6183 = new Mesaj(6183, "URT", "ERR_6183: Üretim girisinde Operasyon zorunludur!");
        public static Mesaj ERR_6184 = new Mesaj(6184, "URT", "ERR_6184: Üretim girisinde Istasyon zorunludur!");
        public static Mesaj ERR_6185 = new Mesaj(6185, "URT", "ERR_6185: Üretim girisinde Malzeme zorunludur!");
        public static Mesaj ERR_6186 = new Mesaj(6186, "URT", "ERR_6186: Üretim girisinde Birim zorunludur! ");
        public static Mesaj ERR_6187 = new Mesaj(6187, "URT", "ERR_6187: Üretim bitiş tarihi başlangıç tarihinden küçük olamaz! Uretim:{0}, Baslangic:{1}, Bitis:{2} ");
        public static Mesaj ERR_6188 = new Mesaj(6188, "URT", "ERR_6188: Duruş neden tanımı bulunamadı. Kod: {0}");
        public static Mesaj ERR_6189 = new Mesaj(6189, "URT", "ERR_6189: Uretim miktari sifir olamaz!");
        public static Mesaj ERR_6190 = new Mesaj(6190, "URT", "ERR_6190: Üretim iscilik baslangici uretim baslangicindan kucuk olamaz!");
        public static Mesaj ERR_6191 = new Mesaj(6191, "URT", "ERR_6191: Malzeme birim miktarı sıfır olamaz!");
        public static Mesaj ERR_6192 = new Mesaj(6192, "URT", "ERR_6192: Kullanılan malzemelerde ambalaj bilgisi zorunludur!");
        public static Mesaj ERR_6193 = new Mesaj(6193, "URT", "ERR_6193: Hurda kaydinda hurda nedeni zorunludur.");
        public static Mesaj ERR_6194 = new Mesaj(6194, "URT", "ERR_6194: Ambalaj transfer edilmis uretim degistirilemez! Ambalaj Depo:{0}, Istasyon Depo:{1}");
        public static Mesaj ERR_6195 = new Mesaj(6195, "URT", "ERR_6195: Uretim kasa hareketi eklenemedi !");
        public static Mesaj ERR_6196 = new Mesaj(6196, "URT", "ERR_6196: Recetede olan malzeme farkli stok olarak eklenemez! {0}");
        public static Mesaj ERR_6197 = new Mesaj(6196, "URT", "ERR_6197: Uretim miktari basilan etiket miktarini karsilamiyor! {0} ");
        public static Mesaj ERR_6198 = new Mesaj(6196, "URT", "ERR_6198: Uretim miktari basilan etiket miktarinindan fazla! {0} ");
        #endregion

        //OTM HATA MESAJLARİ xx
        #region OTM 8000

        public static Mesaj ERR_8001 = new Mesaj(8001, "OTM", "ERR_8001: Istasyon id değeri null olamaz.");
        public static Mesaj ERR_8002 = new Mesaj(8002, "OTM", "ERR_8002: Uretim operasyonuna ait kayıt bilgisi bulunamadi.");
        public static Mesaj ERR_8005 = new Mesaj(8005, "OTM", "ERR_8005: Okutulan ambalaj detayı sistemde bulunamadı.");
        public static Mesaj ERR_8006 = new Mesaj(8006, "OTM", "ERR_8006: İstasyon hazırlamak için gerekli cihaz bilgisi alınamadı.");
        public static Mesaj ERR_8007 = new Mesaj(8007, "OTM", "ERR_8007: {0}. ID' li kaydın Is Merkezi Adı bulunamadı.");
        public static Mesaj ERR_8008 = new Mesaj(8008, "OTM", "ERR_8008: {0}. ID' li kaydın Hat Adı bulunamadı.");
        public static Mesaj ERR_8009 = new Mesaj(8009, "OTM", "ERR_8009: {0}. ID' li kaydın Istasyon Adı bulunamadı.");
        public static Mesaj ERR_8010 = new Mesaj(8010, "OTM", "ERR_8010: Otomasyon parametresi bulunamadı.");
        public static Mesaj ERR_8012 = new Mesaj(8012, "OTM", "ERR_8012: İlgili İstasyona Ait Açık Duruş Kaydı Bulunmaktadır. ");
        public static Mesaj ERR_8013 = new Mesaj(8013, "OTM", "ERR_8013: Bitirilecek Duruş Kaydı Bulunamadı.");
        
        #endregion

        //SYM HATA MESAJLARİ
        #region SYM 9000

        public static Mesaj ERR_9001 = new Mesaj(9001, "SYM", "ERR_9001: Sayım Parametresi Bulunamadı");
        public static Mesaj ERR_9002 = new Mesaj(9002, "SYM", "ERR_9002: Kayıt için gerekli Sayım Emri bilgisi bulunamadı.");
        public static Mesaj ERR_9003 = new Mesaj(9003, "SYM", "ERR_9003: Kullanıcı detay bilgisi bulunamadı.");
        public static Mesaj ERR_9004 = new Mesaj(9004, "SYM", "ERR_9004: Sayım emri id değeri alınamadı.");
        public static Mesaj ERR_9007 = new Mesaj(9007, "SYM", "ERR_9007: Silinecek Sayım Emri kaydı bulunamadı.");
        public static Mesaj ERR_9009 = new Mesaj(9009, "SYM", "ERR_9009: Güncellenecek Sayım Emri bilgileri bulunamadı");
        public static Mesaj ERR_9011 = new Mesaj(9011, "SYM", "ERR_9011: Güncellenecek Sayım Emri kaydı bulunamadı. ");
        public static Mesaj ERR_9014 = new Mesaj(9014, "SYM", "ERR_9014: Raflar kaydı bulunamadı.");
        public static Mesaj ERR_9016 = new Mesaj(9016, "SYM", "ERR_9016: Kullanıcı bilgisi bulunamadı.");
        public static Mesaj ERR_9017 = new Mesaj(9017, "SYM", "ERR_9017: {0} kodlu kullanıcının sayım yetkisi bulunmamaktadır.");
        public static Mesaj ERR_9018 = new Mesaj(9018, "SYM", "ERR_9018: Raf bulunamadı");
        public static Mesaj ERR_9020 = new Mesaj(9020, "SYM", "ERR_9020: Sayım Detay Bilgisi Bulunamadı.");
        public static Mesaj ERR_9021 = new Mesaj(9021, "SYM", "ERR_9021: Raf kodu bulunamadı. ");
        public static Mesaj ERR_9022 = new Mesaj(9022, "SYM", "ERR_9022: Barkod bilgisi bulunamadı. ");
        public static Mesaj ERR_9023 = new Mesaj(9023, "SYM", "ERR_9023: Ambalaj bulunamadı.");
        public static Mesaj ERR_9026 = new Mesaj(9026, "SYM", "ERR_9026: Sayım Emri detayı bulunamadı.");
        public static Mesaj ERR_9028 = new Mesaj(9028, "SYM", "ERR_9028: Sayım Belgesi Bulunamadı.");
        public static Mesaj ERR_9029 = new Mesaj(9029, "SYM", "ERR_9029: Malzeme Kaydı Bulunamadı.");
        public static Mesaj ERR_9030 = new Mesaj(9030, "SYM", "ERR_9030: Ambalaj Kaydı Bulunamadı.");
        public static Mesaj ERR_9032 = new Mesaj(9032, "SYM", "ERR_9032: Bu ambalaj daha önce eklenmiştir. ");
        public static Mesaj ERR_9033 = new Mesaj(9033, "SYM", "ERR_9033: Raf Kaydı Bulunamadı. Raf Tanımını Kontrol Ediniz.");
        public static Mesaj ERR_9034 = new Mesaj(9034, "SYM", "ERR_9034: Ambalaj Türü Bulunamadı. Tür Tanımlarını Kontrol Ediniz.");
        public static Mesaj ERR_9035 = new Mesaj(9035, "SYM", "ERR_9035: Malzeme Bilgisi Bulunamadı. Malzeme Tanımlarını Kontrol Ediniz.");
        public static Mesaj ERR_9036 = new Mesaj(9036, "SYM", "ERR_9036: Birim Bilgisi Bulunamadı. Birim Tanımlarını Kontrol Ediniz.");
        public static Mesaj ERR_9037 = new Mesaj(9037, "SYM", "ERR_9037: Barkod numarasını kontrol ediniz.");
        public static Mesaj ERR_9038 = new Mesaj(9038, "SYM", "ERR_9038: Kayıtlı ambalaj bilgisi bulunamadı.");
        public static Mesaj ERR_9039 = new Mesaj(9039, "SYM", "ERR_9039: Ambalaj bilgisi bulunamadı.");
        public static Mesaj ERR_9040 = new Mesaj(9040, "SYM", "ERR_9040: Açık ambalaj hareket kaydı bulunamadı.");
        public static Mesaj ERR_9041 = new Mesaj(9041, "SYM", "ERR_9041: Ambalaj türleri bulunamadı. ");
        public static Mesaj ERR_9042 = new Mesaj(9042, "SYM", "ERR_9042: Renkler bulunamadı. ");
        public static Mesaj ERR_9043 = new Mesaj(9043, "SYM", "ERR_9043: Seçili Sayım Emri Bulunamadı. Listeden Bir Sayım Emri Seçiniz.");
        public static Mesaj ERR_9044 = new Mesaj(9044, "SYM", "ERR_9044: Ambalaj Bilgisi Parametre İle Alınamadı.");
        public static Mesaj ERR_9045 = new Mesaj(9045, "SYM", "ERR_9045: Malzeme Bilgisi Parametre İle Alınamadı.");
        public static Mesaj ERR_9046 = new Mesaj(9046, "SYM", "ERR_9046: Okutulan Malzemenin ERP' de Seçilen Depoda Tanımı Bulunmamaktadır.");
        public static Mesaj ERR_9048 = new Mesaj(9048, "SYM", "ERR_9048: Raf Parametresi Bulunamadı. Raf Seçiniz.");
        public static Mesaj ERR_9049 = new Mesaj(9049, "SYM", "ERR_9049: Silinecek Sayım Kaydı Bulunamadı.");
        public static Mesaj ERR_9050 = new Mesaj(9050, "SYM", "ERR_9050: Depo Id Bilgisi Bulunamadı.");
        public static Mesaj ERR_9051 = new Mesaj(9051, "SYM", "ERR_9051: Sayım Belge Numarası Bulunamadı. ");
        public static Mesaj ERR_9052 = new Mesaj(9052, "SYM", "ERR_9052: Sayım Belgesinin Durumu Kullanım İçin Uygun Değildir. Sayım Belge No: {0}");
        public static Mesaj ERR_9053 = new Mesaj(9053, "SYM", "ERR_9053: Sayım Emri Belge Numarası Bulunamadı");
        public static Mesaj ERR_9054 = new Mesaj(9054, "SYM", "ERR_9054: Sayım Emri Belgesi Bulunamadı");
        public static Mesaj ERR_9055 = new Mesaj(9055, "SYM", "ERR_9055: Sayım Emri Belgesinin Durumu Kullanım İçin Uygun Değildir. Sayım Emri Belge No: {0}.");
        public static Mesaj ERR_9056 = new Mesaj(9056, "SYM", "ERR_9056: Depo Kod Bilgisi Bulunamadı.");
        public static Mesaj ERR_9057 = new Mesaj(9057, "SYM", "ERR_9057: Raf Kod Bilgisi Bulunamadı.");
        public static Mesaj ERR_9058 = new Mesaj(9058, "SYM", "ERR_9058: Kayıt İçin Gönderilecek Sayım Detayı Bulunamadı.");
        public static Mesaj ERR_9059 = new Mesaj(9059, "SYM", "ERR_9059: Sayım Aktarma İşleminde Bir Hata Oluştu.");
        
        #endregion
    }


}
