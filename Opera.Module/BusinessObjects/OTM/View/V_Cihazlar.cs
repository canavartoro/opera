using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Diagnostics;

namespace Mikrobar.Module.BusinessObjects
{
    [DebuggerDisplay(" IsMerkezi = {IsMerkezi}, UretimSayisi = {UretimSayisi} ")]
    public class V_Cihazlar
    {
        public int OID { get; set; }
        public string IsMerkezi { get; set; }
        public string Hat { get; set; }
        public int IstasyonId { get; set; }        
        public string IstasyonKod { get; set; }
        public string IstasyonAdres { get; set; }
        public KayitDurumu Durum { get; set; }
        public IslemType VeriTipi { get; set; }
        public CihazUretimTur VeriTuru { get; set; }
        public bool UretimSayisi { get; set; }
        public bool Durus { get; set; }
        public int DurusSuresi { get; set; }        
    }
}