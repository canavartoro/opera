using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace Mikrobar.Module.BusinessObjects
{

    public class SayimGecmisListe
    {
        public int BelgeNo { get; set; }
        public string DepoKod { get; set; }
        public string RafKod { get; set; }
        public KayitDurumu Durum { get; set; }
    }
}