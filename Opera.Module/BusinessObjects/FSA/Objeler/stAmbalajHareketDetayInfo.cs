using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mikrobar;
using DevExpress.Data.Filtering;

namespace Mikrobar.Module.BusinessObjects
{
    public class stAmbalajHareketDetayInfo
    {
        public int KullaniciId { get; set; }

        public int MalzemeId { get; set; }

        public int BirimId { get; set; }
        public int Birim2Id { get; set; }

        public decimal Miktar { get; set; }
        public decimal Miktar2 { get; set; }

        public string BirimKod { get; set; }
        public string MalzemeKod { get; set; }

        public int SiparisId { get; set; }
        public int SiparisDetayId { get; set; }


        public AmbalajHareketDetaylari AhDtyDon()
        {

            AmbalajHareketDetaylari adt = new AmbalajHareketDetaylari();
            adt.Olusturan = KullaniciId;
            adt.Birim = BirimKod;

            adt.BirimId = BirimId;
            adt.Birim2Id = Birim2Id;

            adt.Miktar = Miktar;
            adt.Miktar2 = Miktar2;

            adt.MalzemeId = MalzemeId;
            adt.MalzemeKod = MalzemeKod;
            adt.SiparisId = SiparisId;
            adt.SiparisDetayId = SiparisDetayId;

            return adt;
        }
    }
}
