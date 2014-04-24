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
 XafDefaultProperty("PersonelKod"), XafDisplayName("Uretim Iscilik Detaylari"), NavigationItem(false)]
    public class V_UretimIscilik : XPLiteObject
    {
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}")]
        public DateTime BaslangicTarihi { get; set; }
        public string PersonelKod { get; set; }
        public string PersonelAd { get; set; }
        public string PersonelSoyAd { get; set; }
        [Key]
        public int PersonelId { get; set; }
        public int UretimOperasyon { get; set; }
        public KayitDurumu Durum { get; set; }

        public V_UretimIscilik() { }
        public V_UretimIscilik(Session session) : base(session) { }
    }
}