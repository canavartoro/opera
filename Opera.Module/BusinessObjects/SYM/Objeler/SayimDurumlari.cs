using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mikrobar.Module.BusinessObjects
{
    public static class SayimDurumlari
    {
        #region Sayım Emri

        public const string YeniSayimEmri = "Yeni Sayım Emri Oluşturuldu.";
        public const string IptalSayimEmri = "Sayım Emri İptal Edildi.";

        #endregion
        #region Sayımlar

        public const string YeniSayimBelgesi = "Yeni Bir Sayım Belgesi Oluşturuldu.";
        public const string TamamlananSayimBelgesi = "Sayım Belgesi Tamamlandı.";
        public const string AktarilanSayimBelgesi = "Sayım Belgesi Aktarıldı.";
        public const string AktarilacakSayimBelgesi = "Aktarılmayı Bekleyen Sayım Belgesi.";
        public const string KapananSayimBelgesi = "Sayım Belgesi Kapandı.";
        public const string IptalSayimBelgesi = "Sayım Belgesi Iptal Edildi.";

        #endregion
        #region Sayım Detayları

        public const string YeniSayimDetayi = "Yeni Bir Sayım Detayı Eklendi.";
        public const string TamamlananSayimDetayi = "Sayım Belgesi Detayı Tamamlandı.";
        public const string AktarilanSayimDetayi = "Sayım Belgesi Detayı Aktarıldı.";
        public const string AktarilacakSayimDetayi = "Aktarılmayı Bekleyen Sayım Belge Detayı.";
        public const string KapananSayimDetayi = "Sayım Belgesi Detayı Kapandı.";
        public const string IptalSayimDetayi = "Sayım Belgesi Detayı İptal Edildi.";
        /// <summary>
        /// Eğer Üst Belge İptal Edilirse, Detaylar Toplu Olarak İptal Edilecek.
        /// </summary>
        public const string TopluIptalDetayi = "Sayım Belgesi İle İptal Edildi.";

        #endregion
    }
}
