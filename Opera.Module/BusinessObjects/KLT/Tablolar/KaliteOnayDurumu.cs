using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace Mikrobar.Module.BusinessObjects
{
    public class KaliteOnayDurumu
    {
        public int OnayId { get; set; }

        public string OnayKod { get; set; }
        public string OnayAciklama { get; set; }
        public int OnayStatuId { get; set; }

        public KaliteOnayDurumu() { }
    }
}
