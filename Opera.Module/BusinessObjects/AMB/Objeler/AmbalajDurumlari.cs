using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mikrobar.Module.BusinessObjects
{
    public static class AmbalajDurumlari
    {
        #region Fason&Satinalma
        /// <summary>
        /// Fasonda bekleyen sanal stok kasalari için
        /// </summary>
        public const string FASONDA_BEKLIYOR = "Fason tedarikçide bekliyor.";
        /// <summary>
        /// Fasona cikilan malzeme kasalari icin
        /// </summary>
        public const string FASONDA_MALZEME_BEKLIYOR = "Fason tedarikçide kullanımda.";        
        /// <summary>
        /// Fason donuste olusan kasalar icin
        /// </summary>
        public const string FASON_DONUS_BOSTA = "Fason tedarikciden geldi kullanılabilir.";
        /// <summary>
        /// Fason cikista kullanilan kasalar icin, malzeme cikilan kasalar icin
        /// </summary>
        public const string FASON_CIKIS_MALZEME_CIKILDI = "Fason'a malzeme gönderiminde kullanıldı.";
        /// <summary>
        /// Satin almadan geldi kullanilabilir
        /// </summary>
        public const string SATINALMA_BOSTA = "Alış siparişiyle giriş yapıldı kullanılabilir.";

        public const string FASONDA_CIKIS_IPTAL = "Fason cikis belgesi iptal edildi.";
        #endregion

        #region Uretim
        public const string KASA_URETIMDE_ISLENIYOR = "Ambalaj üretimde işleniyor";

        public const string KASA_URETIMDE_KULLANILDI = "Ambalaj üretimde kullanıldı";

        public const string KASA_URETIMDE_KULLANIMDA = "Ambalaj üretimde kullanımda";

        public const string KASA_URETIMDE_BOSTA = "Ambalaj üretimde Boşta";

        public const string KASA_URETIMDE_KAPANDI = "Ambalaj üretimi tamamlandi";

        public const string KASA_DEPO_GIRIS = "Ambalaj depo girisi yapildi bosta";

        public const string KASA_URETIM_HURDASI = "Ambalaj üretim hurda kasasi";
        #endregion

        #region Kalite
        public const string KALITE_BOSTA = "Kalite onayından geçti kullanılabilir.";

        public const string KALITE_KAPALI = "Kalite onayı tamamlandı ambalaj kapatıldı.";
        #endregion

        #region SEVKIYAT
        public const string SEVK_KILIT = "Sevkiyat için hazırlandı bekliyor.";
        public const string SEVK_BOSTA = "Sevkiyat için hazırlandı geri alındı.";
        #endregion

        #region Genel
        /// <summary>
        /// Kullanilmayan kasalari silmek yerine kapatiyoruz
        /// </summary>
        public const string IPTAL_KAPALI = "Ambalaj iptal edilerek kapatildi.";
        /// <summary>
        /// Miktari tamamlanarak kapanan kasalar icin
        /// </summary>
        public const string MIKTAR_SIFIR_KAPALI = "Ambalaj miktarı sıfırlandığı için kapatıldı.";
        /// <summary>
        /// Kasa miktari tamamlandiginda kapandiktan sonra, iade ile geri acildigi durumlar icin.
        /// </summary>
        public const string MIKTAR_IADE_KASA_ACILDI = "Ambalaj geri alindi Ambalaj kullanılabilir.";  
        #endregion
    }
}
