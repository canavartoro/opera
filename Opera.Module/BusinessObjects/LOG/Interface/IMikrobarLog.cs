using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mikrobar.Module.BusinessObjects
{
    public interface IMikrobarLog : IDisposable
    {
        void HataLog(int hataNo, int satirNo, string kaynakModul, string hata, string aciklama);

        void HataLog(int hataNo, int satirNo, string kaynakModul, string hata, string aciklama, int kayitId, string kayitTip, IslemTip islem);

        void UyariLog(int hataNo, int satirNo, string kaynakModul, string hata, string aciklama);

        void UyariLog(int hataNo, int satirNo, string kaynakModul, string hata, string aciklama, int kayitId, string kayitTip, IslemTip islem);

        void BilgiLog(int hataNo, int satirNo, string kaynakModul, string hata, string aciklama);

        void BilgiLog(int hataNo, int satirNo, string kaynakModul, string hata, string aciklama, int kayitId, string kayitTip, IslemTip islem);

        void Log(int hataNo, int satirNo, string kaynakModul, string hata, string aciklama);

        void Log(int hataNo, int satirNo, string kaynakModul, string hata, string aciklama, int kayitId, string kayitTip, IslemTip islem);
    }
}
