using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace Mikrobar.Module.BusinessObjects
{


    [NonPersistent]
    public class V_MalToplamaEmirleri : XPLiteObject
    {
        public int EmirId { get; set; }

        public int SevkEmriId { get; set; }
        public string SevkEmriNo { get; set; }

        public string CariKod { get; set; }

        public string CariAd { get; set; }

        public string Aciklama { get; set; }        

        public string DepoKod { get; set; }

        public DateTime SiparisTarihi { get; set; }

        public DateTime SevkTarihi { get; set; }

        public DateTime TeslimTarihi { get; set; }

        public int SiparisId { get; set; }

        public int CariId { get; set; }

        public int DepoId { get; set; }

        public decimal PaletSayisi { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }

        public string Ilce { get; set; }
        public string Il { get; set; }
        public string Ulke { get; set; }

        public bool Aktif { get; set; }


        public V_MalToplamaEmirleri(Session session) : base(session) { }
        public V_MalToplamaEmirleri() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }


}
