using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace Mikrobar.Module.BusinessObjects
{
    [NonPersistent]
    public class OneriSurrogate : XPLiteObject
    {
        public int Id { get; set; }
        public int StokId { get; set; }
        public string StokKod { get; set; }
        public string StokAd { get; set; }
        public int RafId { get; set; }
        public string RafKod { get; set; }
        public decimal Miktar { get; set; }
        public decimal SevkMiktar { get; set; }
        public decimal OkunanMiktar { get; set; }
        public int KaynakDId { get; set; }
        public int BirimId { get; set; }
        public string Birim { get; set; }               

        public int OkunanBirimId { get; set; }
        public string OkunanBirim { get; set; }
       
        public decimal QtyReadPrm { get; set; }
        public int PackageTraDId { get; set; }
        public int PackageTraMId { get; set; }

        public bool IsReal { get; set; }
        public bool IsFantom { get; set; }
        public int StokTip { get; set; } // 1=A, 2=S

        public OneriSurrogate(Session session) : base(session) { }
        public OneriSurrogate() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
