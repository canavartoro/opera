using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [NonPersistent]
    [ImageName("BO_Organization"), ModelDefault("DefaultListViewShowAutoFilterRow", "True"),
  XafDefaultProperty("HurdaKod"), XafDisplayName("Uretim Hurda Detaylari"), NavigationItem(false)]
    public class V_UretimHurdalari : XPLiteObject
    {
        public int UretimOperasyon { get; set; }
        public int HurdaMalzemeId { get; set; }
        public string HurdaMalzemeKod { get; set; }
        public string HurdaMalzemeAd { get; set; }
        public int HurdaTipi { get; set; }
        public string HurdaKod { get; set; }
        public string HurdaAd { get; set; }
        public string DepoKod { get; set; }
        public string DepoAd { get; set; }
        public string Birim { get; set; }
        public decimal Miktar { get; set; }
        public string Aciklama { get; set; }
        public int AmbalajId { get; set; }
        public string PartiNo { get; set; }
        [Key]
        public int UretimHurdaId { get; set; }

        public V_UretimHurdalari(Session session) : base(session) { }
        public V_UretimHurdalari() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}

