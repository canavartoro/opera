using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;

namespace Mikrobar.Module.BusinessObjects
{
    public static class MikrobarSprocHelper
    {
        public static DevExpress.Xpo.DB.SelectedData ExecUretimIptal(
            Session session, int UretimId)
        {
            string modul = "";
            try
            {
                modul = (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().ToString();
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.StackTrace);
            }

            return session.ExecuteSproc("sp_UretimIptal", new OperandValue(UretimId));
        }

        public static DevExpress.Xpo.DB.SelectedData ExecAktarimLoglari(
            Session session, string IslemSonucu, string Detaylar, int KayitId, int MasterId, int SatirSayisi)
        {
            string modul = "";
            try
            {
                modul = (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().ToString();
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.StackTrace);
            }

            System.Diagnostics.Trace.WriteLine(Detaylar);

            try
            {
                using (UnitOfWork work = new UnitOfWork())
                {
                    if (string.IsNullOrEmpty(Detaylar)) Detaylar = "";
                    if (Detaylar.Length > 800) Detaylar = Detaylar.Substring(0, 800);

                    if (string.IsNullOrEmpty(IslemSonucu)) IslemSonucu = "";
                    if (IslemSonucu.Length > 400) IslemSonucu = IslemSonucu.Substring(0, 400);

                    SistemLoglari lg = new SistemLoglari(work);
                    lg.Aciklama = Detaylar;
                    lg.HataNo = KayitId;
                    lg.Hata = IslemSonucu;
                    lg.Modul = MasterId.ToString();
                    lg.OlusturmaTarihi = DateTime.Now;
                    lg.SatirNo = SatirSayisi;
                    lg.Save();
                    //return work.ExecuteSproc("sp_AktarimLoglari",
                    //new OperandValue(IslemSonucu), new OperandValue(Detaylar), new OperandValue(KayitId), new OperandValue(MasterId), new OperandValue(SatirSayisi));
                }
            }
            catch (Exception exc)
            {
                
                System.Diagnostics.Debug.WriteLine(exc.StackTrace);
            }

            return null;
        }

        private static void LogInsert(int KayitId, int p1, int MasterId, string p2, string IslemSonucu, string Detaylar, string modul)
        {
            System.Diagnostics.Trace.WriteLine(Detaylar);
        }

        public static DevExpress.Xpo.DB.SelectedData ExecLogInsert(Session session, int HataId, int KullaniciId, 
            int MenuId, string CihazNo, string HataAciklama, string Detaylar, string KanakPprogram)
        {
            DevExpress.Xpo.DB.SelectedData returnObj = null;
            string modul = "";
            try
            {
                modul = (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().ToString();
            }
            catch(Exception exc) 
            {
                System.Diagnostics.Debug.WriteLine(exc.StackTrace);
            }
            KanakPprogram += "." + modul;
            LogInsert(HataId, KullaniciId, MenuId, CihazNo, HataAciklama, Detaylar, KanakPprogram);

            try
            {
                using (UnitOfWork work = new UnitOfWork())
                {
                    if (string.IsNullOrEmpty(Detaylar)) Detaylar = "";
                    if (Detaylar.Length > 800) Detaylar = Detaylar.Substring(0, 800);

                    if (string.IsNullOrEmpty(HataAciklama)) HataAciklama = "";
                    if (HataAciklama.Length > 400) HataAciklama = HataAciklama.Substring(0, 400);

                    SistemLoglari lg = new SistemLoglari(work);
                    lg.Aciklama = Detaylar;
                    lg.HataNo = HataId;
                    lg.Hata = HataAciklama;
                    lg.Modul = MenuId.ToString();
                    lg.OlusturmaTarihi = DateTime.Now;
                    lg.SatirNo = MenuId;
                    lg.Save();

                    work.CommitChanges();
                    //returnObj = work.ExecuteSproc("sp_LogInsert", new OperandValue(HataId), new OperandValue(KullaniciId), new OperandValue(MenuId), new OperandValue(CihazNo), new OperandValue(HataAciklama), new OperandValue(Detaylar), new OperandValue(KanakPprogram));
                }
            }
            catch (Exception exc)
            {
                System.Diagnostics.Trace.WriteLine(exc.StackTrace);
            }
            return returnObj;
            //if (session == null) return null;
            //return session.ExecuteSproc("sp_LogInsert", new OperandValue(HataId), new OperandValue(KullaniciId), new OperandValue(MenuId), new OperandValue(CihazNo), new OperandValue(HataAciklama), new OperandValue(Detaylar), new OperandValue(KanakPprogram));
        }

        public static Ambalajlar YeniAmbalaj(Session session, int RafId, int HareketId, int Olusturan, int Guncelleyen, string KaynakModul, string CihazNo, decimal Dara, int IstasyonId, int IsEmriId, string IsEmriNo, int OperasyonId, int OperasyonNo, int CariId, string IrsaliyeNo, string FisNo, string BelgeNo, DateTime BelgeTarihi, int SevkEmriId, string SevkEmriNo, int SevkEmriDetayId, int SiparisId, string SiparisNo, int SiparisDetayId, object UstAmbalaj, int AmbalajTur, int DepoId, int MalzemeId, decimal Miktar, decimal Miktar2, decimal Kalan, decimal Kalan2, int BirimId, int Birim2Id, string MalzemeKod, string MalzemeAd, string Birim)
        {
            SelectedData DT = ExecYeniAmbalaj(session, RafId, HareketId, Olusturan, Guncelleyen, KaynakModul, CihazNo, Dara, IstasyonId, IsEmriId, IsEmriNo, OperasyonId, OperasyonNo, CariId, IrsaliyeNo, FisNo, BelgeNo, BelgeTarihi, SevkEmriId, SevkEmriNo, SevkEmriDetayId, SiparisId, SiparisNo, SiparisDetayId, UstAmbalaj, AmbalajTur, DepoId, MalzemeId, Miktar, Miktar2, Kalan, Kalan2, BirimId, Birim2Id, MalzemeKod, MalzemeAd, Birim);

            string aa = DT.ResultSet[0].Rows[0].Values[0].ToString();

            return session.GetObjectByKey<Ambalajlar>(DT.ResultSet[0].Rows[0].Values[0]);
        }            

        public static DevExpress.Xpo.DB.SelectedData ExecYeniAmbalaj(Session session, int RafId, int HareketId, int Olusturan, int Guncelleyen, string KaynakModul, string CihazNo, decimal Dara, int IstasyonId, int IsEmriId, string IsEmriNo, int OperasyonId, int OperasyonNo, int CariId, string IrsaliyeNo, string FisNo, string BelgeNo, DateTime BelgeTarihi, int SevkEmriId, string SevkEmriNo, int SevkEmriDetayId, int SiparisId, string SiparisNo, int SiparisDetayId, object UstAmbalaj, int AmbalajTur, int DepoId, int MalzemeId, decimal Miktar, decimal Miktar2, decimal Kalan, decimal Kalan2, int BirimId, int Birim2Id, string MalzemeKod, string MalzemeAd, string Birim)
        {
            return session.ExecuteSproc("YeniAmbalaj", new OperandValue(RafId), new OperandValue(HareketId), new OperandValue(Olusturan), new OperandValue(Guncelleyen), new OperandValue(KaynakModul), new OperandValue(CihazNo), new OperandValue(Dara), new OperandValue(IstasyonId), new OperandValue(IsEmriId), new OperandValue(IsEmriNo), new OperandValue(OperasyonId), new OperandValue(OperasyonNo), new OperandValue(CariId), new OperandValue(IrsaliyeNo), new OperandValue(FisNo), new OperandValue(BelgeNo), new OperandValue(BelgeTarihi), new OperandValue(SevkEmriId), new OperandValue(SevkEmriNo), new OperandValue(SevkEmriDetayId), new OperandValue(SiparisId), new OperandValue(SiparisNo), new OperandValue(SiparisDetayId), new OperandValue(UstAmbalaj), new OperandValue(AmbalajTur), new OperandValue(DepoId), new OperandValue(MalzemeId), new OperandValue(Miktar), new OperandValue(Miktar2), new OperandValue(Kalan), new OperandValue(Kalan2), new OperandValue(BirimId), new OperandValue(Birim2Id), new OperandValue(MalzemeKod), new OperandValue(MalzemeAd), new OperandValue(Birim));
        }

        public static System.Collections.Generic.ICollection<YeniAmbalaj> ExecYeniAmbalajIntoObjects(Session session, int RafId, int HareketId, int Olusturan, int Guncelleyen, string KaynakModul, string CihazNo, decimal Dara, int IstasyonId, int IsEmriId, string IsEmriNo, int OperasyonId, int OperasyonNo, int CariId, string IrsaliyeNo, string FisNo, string BelgeNo, DateTime BelgeTarihi, int SevkEmriId, string SevkEmriNo, int SevkEmriDetayId, int SiparisId, string SiparisNo, int SiparisDetayId, int UstAmbalaj, int AmbalajTur, int DepoId, int MalzemeId, decimal Miktar, decimal Miktar2, decimal Kalan, decimal Kalan2, int BirimId, int Birim2Id, string MalzemeKod, string MalzemeAd, string Birim)
        {
            return session.GetObjectsFromSproc<YeniAmbalaj>("YeniAmbalaj", new OperandValue(RafId), new OperandValue(HareketId), new OperandValue(Olusturan), new OperandValue(Guncelleyen), new OperandValue(KaynakModul), new OperandValue(CihazNo), new OperandValue(Dara), new OperandValue(IstasyonId), new OperandValue(IsEmriId), new OperandValue(IsEmriNo), new OperandValue(OperasyonId), new OperandValue(OperasyonNo), new OperandValue(CariId), new OperandValue(IrsaliyeNo), new OperandValue(FisNo), new OperandValue(BelgeNo), new OperandValue(BelgeTarihi), new OperandValue(SevkEmriId), new OperandValue(SevkEmriNo), new OperandValue(SevkEmriDetayId), new OperandValue(SiparisId), new OperandValue(SiparisNo), new OperandValue(SiparisDetayId), new OperandValue(UstAmbalaj), new OperandValue(AmbalajTur), new OperandValue(DepoId), new OperandValue(MalzemeId), new OperandValue(Miktar), new OperandValue(Miktar2), new OperandValue(Kalan), new OperandValue(Kalan2), new OperandValue(BirimId), new OperandValue(Birim2Id), new OperandValue(MalzemeKod), new OperandValue(MalzemeAd), new OperandValue(Birim));
        }

        public static XPDataView ExecYeniAmbalajIntoDataView(Session session, int RafId, int HareketId, int Olusturan, int Guncelleyen, string KaynakModul, string CihazNo, decimal Dara, int IstasyonId, int IsEmriId, string IsEmriNo, int OperasyonId, int OperasyonNo, int CariId, string IrsaliyeNo, string FisNo, string BelgeNo, DateTime BelgeTarihi, int SevkEmriId, string SevkEmriNo, int SevkEmriDetayId, int SiparisId, string SiparisNo, int SiparisDetayId, int UstAmbalaj, int AmbalajTur, int DepoId, int MalzemeId, decimal Miktar, decimal Miktar2, decimal Kalan, decimal Kalan2, int BirimId, int Birim2Id, string MalzemeKod, string MalzemeAd, string Birim)
        {
            DevExpress.Xpo.DB.SelectedData sprocData = session.ExecuteSproc("YeniAmbalaj", new OperandValue(RafId), new OperandValue(HareketId), new OperandValue(Olusturan), new OperandValue(Guncelleyen), new OperandValue(KaynakModul), new OperandValue(CihazNo), new OperandValue(Dara), new OperandValue(IstasyonId), new OperandValue(IsEmriId), new OperandValue(IsEmriNo), new OperandValue(OperasyonId), new OperandValue(OperasyonNo), new OperandValue(CariId), new OperandValue(IrsaliyeNo), new OperandValue(FisNo), new OperandValue(BelgeNo), new OperandValue(BelgeTarihi), new OperandValue(SevkEmriId), new OperandValue(SevkEmriNo), new OperandValue(SevkEmriDetayId), new OperandValue(SiparisId), new OperandValue(SiparisNo), new OperandValue(SiparisDetayId), new OperandValue(UstAmbalaj), new OperandValue(AmbalajTur), new OperandValue(DepoId), new OperandValue(MalzemeId), new OperandValue(Miktar), new OperandValue(Miktar2), new OperandValue(Kalan), new OperandValue(Kalan2), new OperandValue(BirimId), new OperandValue(Birim2Id), new OperandValue(MalzemeKod), new OperandValue(MalzemeAd), new OperandValue(Birim));
            return new XPDataView(session.Dictionary, session.GetClassInfo(typeof(YeniAmbalaj)), sprocData);
        }
        
        public static void ExecYeniAmbalajIntoDataView(XPDataView dataView, Session session, int RafId, int HareketId, int Olusturan, int Guncelleyen, string KaynakModul, string CihazNo, decimal Dara, int IstasyonId, int IsEmriId, string IsEmriNo, int OperasyonId, int OperasyonNo, int CariId, string IrsaliyeNo, string FisNo, string BelgeNo, DateTime BelgeTarihi, int SevkEmriId, string SevkEmriNo, int SevkEmriDetayId, int SiparisId, string SiparisNo, int SiparisDetayId, int UstAmbalaj, int AmbalajTur, int DepoId, int MalzemeId, decimal Miktar, decimal Miktar2, decimal Kalan, decimal Kalan2, int BirimId, int Birim2Id, string MalzemeKod, string MalzemeAd, string Birim)
        {
            DevExpress.Xpo.DB.SelectedData sprocData = session.ExecuteSproc("YeniAmbalaj", new OperandValue(RafId), new OperandValue(HareketId), new OperandValue(Olusturan), new OperandValue(Guncelleyen), new OperandValue(KaynakModul), new OperandValue(CihazNo), new OperandValue(Dara), new OperandValue(IstasyonId), new OperandValue(IsEmriId), new OperandValue(IsEmriNo), new OperandValue(OperasyonId), new OperandValue(OperasyonNo), new OperandValue(CariId), new OperandValue(IrsaliyeNo), new OperandValue(FisNo), new OperandValue(BelgeNo), new OperandValue(BelgeTarihi), new OperandValue(SevkEmriId), new OperandValue(SevkEmriNo), new OperandValue(SevkEmriDetayId), new OperandValue(SiparisId), new OperandValue(SiparisNo), new OperandValue(SiparisDetayId), new OperandValue(UstAmbalaj), new OperandValue(AmbalajTur), new OperandValue(DepoId), new OperandValue(MalzemeId), new OperandValue(Miktar), new OperandValue(Miktar2), new OperandValue(Kalan), new OperandValue(Kalan2), new OperandValue(BirimId), new OperandValue(Birim2Id), new OperandValue(MalzemeKod), new OperandValue(MalzemeAd), new OperandValue(Birim));
            dataView.PopulateProperties(session.GetClassInfo(typeof(YeniAmbalaj)));
            dataView.LoadData(sprocData);
        }
    }

    [NonPersistent]
    public class YeniAmbalaj : PersistentBase
    {
        string fSeriNo;
        public string SeriNo
        {
            get { return fSeriNo; }
            set { SetPropertyValue<string>("SeriNo", ref fSeriNo, value); }
        }
        string fBarkod;
        public string Barkod
        {
            get { return fBarkod; }
            set { SetPropertyValue<string>("Barkod", ref fBarkod, value); }
        }
        string fMalzemeKod;
        public string MalzemeKod
        {
            get { return fMalzemeKod; }
            set { SetPropertyValue<string>("MalzemeKod", ref fMalzemeKod, value); }
        }
        string fMalzemeAd;
        public string MalzemeAd
        {
            get { return fMalzemeAd; }
            set { SetPropertyValue<string>("MalzemeAd", ref fMalzemeAd, value); }
        }
        string fBirim;
        public string Birim
        {
            get { return fBirim; }
            set { SetPropertyValue<string>("Birim", ref fBirim, value); }
        }
        decimal fMiktar;
        public decimal Miktar
        {
            get { return fMiktar; }
            set { SetPropertyValue<decimal>("Miktar", ref fMiktar, value); }
        }
        string fBirim2;
        public string Birim2
        {
            get { return fBirim2; }
            set { SetPropertyValue<string>("Birim2", ref fBirim2, value); }
        }
        decimal fMiktar2;
        public decimal Miktar2
        {
            get { return fMiktar2; }
            set { SetPropertyValue<decimal>("Miktar2", ref fMiktar2, value); }
        }
        decimal fKalan;
        public decimal Kalan
        {
            get { return fKalan; }
            set { SetPropertyValue<decimal>("Kalan", ref fKalan, value); }
        }
        decimal fKalan2;
        public decimal Kalan2
        {
            get { return fKalan2; }
            set { SetPropertyValue<decimal>("Kalan2", ref fKalan2, value); }
        }
        decimal fAmbalajDarasi;
        public decimal AmbalajDarasi
        {
            get { return fAmbalajDarasi; }
            set { SetPropertyValue<decimal>("AmbalajDarasi", ref fAmbalajDarasi, value); }
        }
        bool fDara;
        public bool Dara
        {
            get { return fDara; }
            set { SetPropertyValue<bool>("Dara", ref fDara, value); }
        }
        bool fSevkiyat;
        public bool Sevkiyat
        {
            get { return fSevkiyat; }
            set { SetPropertyValue<bool>("Sevkiyat", ref fSevkiyat, value); }
        }
        string fAmbalajTip;
        public string AmbalajTip
        {
            get { return fAmbalajTip; }
            set { SetPropertyValue<string>("AmbalajTip", ref fAmbalajTip, value); }
        }
        bool fBolunebilirlik;
        public bool Bolunebilirlik
        {
            get { return fBolunebilirlik; }
            set { SetPropertyValue<bool>("Bolunebilirlik", ref fBolunebilirlik, value); }
        }
        int fMalzemeId;
        public int MalzemeId
        {
            get { return fMalzemeId; }
            set { SetPropertyValue<int>("MalzemeId", ref fMalzemeId, value); }
        }
        int fBirim2Id;
        public int Birim2Id
        {
            get { return fBirim2Id; }
            set { SetPropertyValue<int>("Birim2Id", ref fBirim2Id, value); }
        }
        string fBirimId;
        public string BirimId
        {
            get { return fBirimId; }
            set { SetPropertyValue<string>("BirimId", ref fBirimId, value); }
        }
        string fPrefix;
        public string Prefix
        {
            get { return fPrefix; }
            set { SetPropertyValue<string>("Prefix", ref fPrefix, value); }
        }
        int fAmbalajTur;
        public int AmbalajTur
        {
            get { return fAmbalajTur; }
            set { SetPropertyValue<int>("AmbalajTur", ref fAmbalajTur, value); }
        }
        int fAmbalajId;
        public int AmbalajId
        {
            get { return fAmbalajId; }
            set { SetPropertyValue<int>("AmbalajId", ref fAmbalajId, value); }
        }
        int fDurum;
        public int Durum
        {
            get { return fDurum; }
            set { SetPropertyValue<int>("Durum", ref fDurum, value); }
        }
        int fAmbalajDetayId;
        public int AmbalajDetayId
        {
            get { return fAmbalajDetayId; }
            set { SetPropertyValue<int>("AmbalajDetayId", ref fAmbalajDetayId, value); }
        }
        public YeniAmbalaj(Session session) : base(session) { }
        public YeniAmbalaj() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }


}