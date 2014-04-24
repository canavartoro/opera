using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.Data;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [DeferredDeletion(false), OptimisticLocking(false), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    public class AllDesing : XPBaseObject
    {
        [Key(AutoGenerate = true)]
        public int ids { get; set; }
        [Indexed]
        public string name { get; set; }
        [Persistent("xrDizayn"), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public byte[] Dizayn { get; set; }
        public string ViewName { get; set; }

        public AllDesing() { }
        public AllDesing(Session session) : base(session) { }

        public static explicit operator DataTable(AllDesing d)
        {
            DataTable dtDesing = new DataTable("AllDesing");
            dtDesing.Columns.Add("ids", typeof(System.Int32));
            dtDesing.Columns.Add("name", typeof(System.String));
            dtDesing.Columns.Add("ViewName", typeof(System.String));
            dtDesing.Columns.Add("Dizayn", typeof(System.Byte[]));
            DataRow drDesing = dtDesing.NewRow();
            drDesing[0] = d.ids;
            drDesing[1] = d.name;
            drDesing[2] = d.ViewName;
            drDesing[3] = d.Dizayn;
            dtDesing.Rows.Add(drDesing);
            dtDesing.AcceptChanges();
            return dtDesing;
        }

        public static implicit operator byte[](AllDesing d)
        {
            return d.Dizayn;
        }
    }
}
