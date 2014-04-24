using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mikrobar;
using DevExpress.Data.Filtering;

namespace Mikrobar.Module.BusinessObjects
{
    public class stAmbalajHareketleriInfo
    {
        public int Oid { get; set; }
        public int StokHareketId { get; set; }
        public int CariId { get; set; }
        public int MenuId { get; set; }
        public int KullaniciId { get; set; }
        public int HareketId { get; set; }

        public string CariKod { get; set; }
        public string MenuKod { get; set; }
        public string IrsaliyeNo { get; set; }
        public string CihazNo { get; set; }

        public DateTime BelgeTarihi { get; set; }
        public DateTime SonKullanmaTarihi { get; set; }

        public string Aciklama1 { get; set; }
        public string Aciklama2 { get; set; }
        public string Aciklama3 { get; set; }
        public string Aciklama4 { get; set; }

        public KayitDurumu Durum { get; set; }
        public KaynakProgram KaynakProgram { get; set; }

        public string AmbTur { get; set; }

        //public AmbalajHareketleri AhDon()
        //{
        //    AmbalajHareketleri ah = null;

        //    if (Oid > 0)
        //    {
        //        ah = Utility.Session.GetObjectByKey<AmbalajHareketleri>(Oid);
        //        if (ah != null)
        //        {
        //            ah.Guncelleyen = KullaniciId;
        //            ah.GuncellemeTarihi = DateTime.Now;
        //        }
        //    }

        //    if (StokHareketId > 0 && Oid < 1)
        //    {
        //        ah = Utility.Session.FindObject<AmbalajHareketleri>(CriteriaOperator.Parse("StokHareket = ?", StokHareketId));
        //        if (ah != null)
        //        {
        //            ah.Guncelleyen = KullaniciId;
        //            ah.GuncellemeTarihi = DateTime.Now;
        //        }
        //    }

        //    if (ah == null)
        //    {
        //        ah = new AmbalajHareketleri(Utility.Session);
        //        ah.Olusturan = KullaniciId;
        //        ah.OlusturmaTarihi = DateTime.Now;
        //        ah.KaynakProgram = KaynakProgram;
        //        ah.KaynakModul = MenuKod;
        //    }

        //    ah.Aciklama1 = Aciklama1;
        //    ah.Aciklama2 = Aciklama2;
        //    ah.Aciklama3 = Aciklama3;
        //    ah.Aciklama4 = Aciklama4;

        //    ah.BelgeTarihi = BelgeTarihi;
        //    ah.BelgeTarihi1 = SonKullanmaTarihi;

        //    ah.StokHareket = Utility.Session.GetObjectByKey<StokHareketleri>(StokHareketId);
        //    ah.CariId = CariId;
        //    ah.CariKod = CariKod;
        //    ah.KaynakModul = MenuKod;
        //    ah.KaynakProgram = KaynakProgram;
        //    ah.Olusturan = KullaniciId;
        //    ah.HareketId = HareketId;
        //    ah.IrsaliyeNo = IrsaliyeNo;
        //    ah.CihazNo = CihazNo;
        //    ah.Durum = Durum;

        //    return ah;
        //}
    }    
}
