using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Xml.Serialization;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using System.Diagnostics;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [NonPersistent,
    ImageName("BO_Organization"),
    ModelDefault("DefaultListViewShowAutoFilterRow", "True"),
    ModelDefault("DefaultListViewAllowEdit", "False"),
    ModelDefault("IsCreatableItem", "False"),
    ModelDefault("AllowDelete", "False"),
    XafDefaultProperty("IsMerkeziKod"), NavigationItem(false),
    XafDisplayName("Is Merkezi Tanimlari")]
    public class IsMerkezi : XPLiteObject
    {
        [Key]
        public int IsMerkeziId { get; set; }
        [VisibleInLookupListView(true)]
        public string IsMerkeziKod { get; set; }
        [VisibleInLookupListView(true)]
        public string Aciklama { get; set; }

        public IsMerkezi() { }
        public IsMerkezi(Session session) : base(session) { }
    }
}
