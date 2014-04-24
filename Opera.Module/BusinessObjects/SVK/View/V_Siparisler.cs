using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{


    [NonPersistent, ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    public class V_Siparisler : XPLiteObject
    {

        public string SiparisNo { get; set; }

        public string CariKod { get; set; }

        public string CariAd { get; set; }

        public string Aciklama { get; set; }

        public bool AlisSatis { get; set; }

        public string DepoKod { get; set; }

        public DateTime SiparisTarihi { get; set; }

        public DateTime SevkTarihi { get; set; }

        public DateTime TeslimTarihi { get; set; }

        public int SiparisId { get; set; }

        public int CariId { get; set; }

        public int DepoId { get; set; }

        public int SiparisCins { get; set; }


        public V_Siparisler(Session session) : base(session) { }
        public V_Siparisler() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }


}
