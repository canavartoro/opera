using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace Mikrobar.Module.BusinessObjects
{

    [Persistent("V_Viewler")]
    public class V_Viewler : XPLiteObject
    {
        [Key]
        public int ViewId { get; set; }
        public string ViewName { get; set; }

        public V_Viewler(Session session) : base(session) { }
        public V_Viewler() { }

    }


}
