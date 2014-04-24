using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

namespace MainDemo.Module.BusinessObjects
{
    [NonPersistent]
    public class MikrobarMenu : XPLiteObject
    {
       public MikrobarMenu() : base(Session.DefaultSession) { }
       public MikrobarMenu(Session session) : base(session) { }

       [Size(DbSize.KodLenght)]
       public string MenuKod { get; set; }

       [Size(DbSize.ModulLenght)]
       public string Ekran { get; set; }

       [Size(DbSize.ModulLenght)]
       public string DllModul { get; set; }

       public CihazTip CihazTip { get; set; }
    }
}
