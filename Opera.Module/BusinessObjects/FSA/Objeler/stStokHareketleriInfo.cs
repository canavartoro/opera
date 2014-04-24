using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mikrobar;
using DevExpress.Data.Filtering;

namespace Mikrobar.Module.BusinessObjects
{
    public class stStokHareketleriInfo
    {
        public int Oid { get; set; }
        public int CariId { get; set; }
        public int MenuId { get; set; }
        public int KullaniciId { get; set; }
        public int HareketId { get; set; }
        public int IsEmriId { get; set; }

        public int SevkEmriId { get; set; }
        public string SevkEmriNo { get; set; }
        public string IsEmriNo { get; set; }

        public string CariKod { get; set; }
        public string Aciklama1 { get; set; }
        public string Aciklama2 { get; set; }
        public string Aciklama3 { get; set; }
        public string Aciklama4 { get; set; }
        public string MenuKod { get; set; }
        public string IrsaliyeNo { get; set; }
        public string CihazNo { get; set; }


        public DateTime BelgeTarihi { get; set; }
        public DateTime IrsaliyeTarihi { get; set; }

        public KayitDurumu Durum { get; set; }
        public KaynakProgram KaynakProgram { get; set; }

        //public StokHareketleri ShDon()
        //{
        //    if (Oid > 0)
        //    {
        //        StokHareketleri dbSh = Utility.Session.GetObjectByKey<StokHareketleri>(Oid);

        //        dbSh.IrsaliyeNo = IrsaliyeNo;
        //        dbSh.Aciklama1 = Aciklama1;
        //        dbSh.Aciklama2 = Aciklama2;
        //        dbSh.BelgeTarihi = BelgeTarihi;
        //        dbSh.IrsaliyeTarihi = IrsaliyeTarihi;
        //        dbSh.IsEmriId = IsEmriId;

        //        return dbSh;
        //    }

        //    StokHareketleri xSh = new StokHareketleri(Utility.Session);
        //    xSh.CariId = CariId;
        //    xSh.Olusturan = KullaniciId;
        //    xSh.HareketId = HareketId;
        //    xSh.IsEmriId = IsEmriId;
        //    xSh.IsEmriNo = IsEmriNo;
        //    xSh.SevkEmriId = SevkEmriId;
        //    xSh.SevkEmriNo = SevkEmriNo;
        //    xSh.CariKod = CariKod;
        //    xSh.IrsaliyeNo = IrsaliyeNo;

        //    xSh.KaynakModul = MenuKod;
        //    xSh.CihazNo = CihazNo;

        //    xSh.KaynakProgram = KaynakProgram;
        //    xSh.Durum = Durum;
        //    xSh.BelgeTarihi = BelgeTarihi;
        //    xSh.IrsaliyeTarihi = IrsaliyeTarihi;

        //    xSh.Aciklama1 = Aciklama1;
        //    xSh.Aciklama2 = Aciklama2;
        //    xSh.Aciklama3 = Aciklama3;
        //    xSh.Aciklama4 = Aciklama4;

        //    return xSh;
        //}
    }
}
