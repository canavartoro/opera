using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mikrobar;
using DevExpress.Data.Filtering;

namespace Mikrobar.Module.BusinessObjects
{
    public class stStokHareketDetayInfo
    {
        public int Oid { get; set; }
        public int SiparisDetayId { get; set; }
        public int SiparisId { get; set; }
        public int MalzemeId { get; set; }
        public string MalzemeKod { get; set; }
        public decimal Miktar { get; set; }
        public decimal Miktar2 { get; set; }
        public int BirimId { get; set; }
        public int Birim2Id { get; set; }
        public string BirimKod { get; set; }
        public string DepoKod { get; set; }
        public int DepoId { get; set; }
        public int HizmetKartId { get; set; }
        public int SatirTipi { get; set; }
        public int RafId { get; set; }
        public int PartiId { get; set; }
        public string PartiKod { get; set; }


        public StokHareketDetaylari ShDDon()
        {
            //if (Oid > 0)
            //    return Utility.Session.GetObjectByKey<StokHareketDetaylari>(Oid);
            StokHareketDetaylari dty = null;//new StokHareketDetaylari(Utility.Session);

            Raflar xRaf = null;//Utility.Session.FindObject<Raflar>(CriteriaOperator.Parse(" Depo.DepoId = ? ", DepoId));

            //if (xRaf == null && Utility.SistemParameter.RafKontrol == true)
            //    throw new Exception("Hedef raf bulunamadi! Depoda raf tanimi yok!");

            dty.SiparisDetayId = SiparisDetayId;
            dty.SiparisId = SiparisId;
            dty.MalzemeId = MalzemeId;
            dty.DepoId = DepoId;
            dty.HedefRaf = xRaf;
            dty.SatirTipi = SatirTipi;

            dty.Miktar = Miktar;
            dty.Miktar2 = Miktar2;

            dty.BirimId = BirimId;
            dty.Birim2Id = dty.Birim2Id;

            dty.MalzemeKod = MalzemeKod;
            dty.Birim = BirimKod;
            dty.DepoKod = DepoKod;

            dty.PartiId = PartiId;
            dty.PartiKod = PartiKod;

            return dty;
        }
    }
}
