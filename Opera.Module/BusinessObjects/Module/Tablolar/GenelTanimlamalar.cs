using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [DeferredDeletion(false), OptimisticLocking(false), DefaultClassOptions, XafDefaultProperty("TanimKod"),
    NavigationItem(false), ImageName("BO_Role"), Description("Genel parametre tanimlamak icin"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    public class GenelTanimlamalar : XPBaseObject
    {
        [Key(AutoGenerate = false), Size(DbSize.KodLenght)]
        public string TanimKod { get; set; }
        [Size(DbSize.AciklamaLenght)]
        public string TanimDeger { get; set; }
        [Size(DbSize.AciklamaLenght)]
        public string Aciklama { get; set; }

         public GenelTanimlamalar() { }
         public GenelTanimlamalar(Session session) : base(session) { }
    }
}
