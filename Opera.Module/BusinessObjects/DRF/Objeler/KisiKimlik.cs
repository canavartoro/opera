using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mikrobar.Module.BusinessObjects
{
    public class KisiKimlik
    {
        public int KisiId { get; set; }
        public string KisiAd { get; set; }
        public string KisiSoyad { get; set; }
        public string Kod { get; set; }
        public int IlceId { get; set; }
        public int SehirId { get; set; }
        public int UlkeId { get; set; }
    }
}
