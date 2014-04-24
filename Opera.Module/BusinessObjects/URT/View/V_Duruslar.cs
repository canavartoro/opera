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
    [NonPersistent,
    ImageName("BO_Organization"),
    ModelDefault("DefaultListViewShowAutoFilterRow", "True"), 
    ModelDefault("DefaultListViewAllowEdit", "False"), 
    ModelDefault("IsCreatableItem", "False"), 
    ModelDefault("AllowDelete", "False"),
    XafDefaultProperty("DurusKod"), NavigationItem(false), 
    XafDisplayName("Durus Detaylari")]    
    public class V_Duruslar : XPLiteObject
    {

        public string DurusKod { get; set; }
        public string DurusAd { get; set; }
        public string DurusTip { get; set; }
        public int DurusTanimId { get; set; }
        public bool IsEmriBaglanti { get; set; }
        public string Aciklama { get; set; }

        [Key]
        public int UretimDurusId { get; set; }
        public int UretimOperasyon { get; set; }
        public int UretimId { get; set; }
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}")]
        public DateTime BaslangicTarihi { get; set; }
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy}")]
        public DateTime BitisTarihi { get; set; }
        public int DurusSuresi { get; set; }


        public V_Duruslar(Session session) : base(session) { }
        public V_Duruslar() : base(Session.DefaultSession) { }
    }

}
