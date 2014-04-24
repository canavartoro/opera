using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Xml.Serialization;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Web;
using System.Net.Sockets;
using System.Net;
using System.IO;
using DevExpress.ExpressApp.DC;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [OptimisticLocking(false), DeferredDeletion(false)]
    public class CihazDetaylari : XPObject
    {
        Cihazlar fCihazlar;
        [XmlIgnore()]
        [Association(@"Cihazlar.Cihaz_CihazDetay")]
        public Cihazlar Cihaz
        {
            get { return fCihazlar; }
            set { SetPropertyValue<Cihazlar>("Cihaz", ref fCihazlar, value); }
        }

        #region Istasyon
        [Indexed("Durum", Unique = false), PersistentAlias("Iif(Istasyon is null, 0, Istasyon.IstasyonId)"),
        VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int IstasyonId
        {
            get
            {
                try
                {
                    if (!IsLoading)
                        return Convert.ToInt32(EvaluateAlias("IstasyonId"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return 0;
            }
            set
            {
                if (!IsLoading && !IsSaving)
                {
                    SetPropertyValue("Istasyon", ref _istasyon, Session.GetObjectByKey<Istasyonlar>(value));
                    if (object.ReferenceEquals(this._istasyon, null))
                        throw new MikrobarException(Lang.Mesaj(GenelMesajlar.ERR_6002, value), 6002);                    
                }
            }
        }

        [ModelDefault("AllowEdit", "False"), VisibleInDetailView(false), PersistentAlias("Iif(Istasyon is null, '', Istasyon.IstasyonKod)")]
        public string IstasyonKod
        {
            get
            {
                try
                {
                    if (!IsLoading)
                        return Convert.ToString(EvaluateAlias("IstasyonKod"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return string.Empty;
            }
        }

        private Istasyonlar _istasyon;
        [XmlIgnore(), XafDisplayName("Istasyon"), ImmediatePostData,
        VisibleInListView(false), VisibleInLookupListView(false), Association(@"CihazDetaylari.Istasyonlar.IstasyonId"), Persistent("IstasyonId"), NoForeignKey]                
        public Istasyonlar Istasyon
        {
            get
            {
                return _istasyon;
            }
            set
            {
                SetPropertyValue("Istasyon", ref _istasyon, value);                
            }
        }

        [VisibleInLookupListView(true), VisibleInDetailView(true), PersistentAlias("Iif(Istasyon is null, '', Istasyon.IstasyonAd)")]
        public string IstasyonAd
        {
            get
            {
                try
                {
                    if (!IsLoading)
                        return Convert.ToString(EvaluateAlias("IstasyonAd"));
                }
                catch (ObjectDisposedException) { }
                catch (Exception) { }
                return "";
            }
        }

        [XafDisplayName("Adres")]
        public string IstasyonAdres { get; set; }

        #endregion

        public bool CihazDurum { get; set; }
        public string Aciklama { get; set; }

        [XafDisplayName("İşlenecek VeriTipi")]
        public IslemType VeriTipi { get; set; }

        [XafDisplayName("Çıkacak Urun Türü")]
        public CihazUretimTur VeriTuru { get; set; }

        public bool UretimSayisi { get; set; }
        public bool Durus { get; set; }
        public int DurusSuresi { get; set; }

        [XmlIgnore(), Association(@"OtomasyonKayitlari.CihazDetaylari.CihazDetay"), NoForeignKey, VisibleInDetailView(false)]
        public XPCollection<OtomasyonKayitlari> OtomasyonKayitlari
        {
            get { return GetCollection<OtomasyonKayitlari>(@"OtomasyonKayitlari"); }
        }

        #region Buttons
        [Action(Caption = "Cihaz Test", ImageName = "Action_Refresh", ToolTip = "Cihazı test et...")]
        public void Test()
        {
            try
            {
                if (this.CihazDurum)
                {
                    if (this.VeriTipi == IslemType.SeriPort)
                    {

                        Socket baglanti = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(this.IstasyonAdres.Split(':')[0]), Convert.ToInt32(this.IstasyonAdres.Split(':')[1]));
                        try
                        {
                            baglanti.Connect(ipep);
                        }
                        catch (Exception)
                        {
                            WebWindow.CurrentRequestWindow.RegisterClientScript("tmm" + this.GetType().Name, "alert('Okuma sırasında hata oldu. IP ve Port bilgilerinin doğru olduğundan emin olunuz');");
                            return;
                        }
                        NetworkStream agAkisi = new NetworkStream(baglanti);
                        BinaryReader binaryOkuyucu = new BinaryReader(agAkisi, Encoding.ASCII);
                        byte b = binaryOkuyucu.ReadByte();
                        List<byte> bList = new List<byte>();
                        while ((b = binaryOkuyucu.ReadByte()) != (byte)'\r')
                        {
                            bList.Add(b);
                        }
                        string mesaj = System.Text.Encoding.ASCII.GetString(bList.ToArray());
                        if (WebWindow.CurrentRequestWindow != null)
                            WebWindow.CurrentRequestWindow.RegisterClientScript("tmm" + this.GetType().Name, "alert('Okunan veri: " + mesaj + "');");
                    }
                    else
                    {
                        if (WebWindow.CurrentRequestWindow != null)
                            WebWindow.CurrentRequestWindow.RegisterClientScript("tmm" + this.GetType().Name, "alert('Bu veri tipine ait test mevcut değildir.');");

                    }
                }
                else
                {
                    if (WebWindow.CurrentRequestWindow != null)
                        WebWindow.CurrentRequestWindow.RegisterClientScript("tmm" + this.GetType().Name, "alert('Cihazın durumu kapalıdır.');");
                }

            }
            catch (Exception exc)
            {
                if (WebWindow.CurrentRequestWindow != null)
                    WebWindow.CurrentRequestWindow.RegisterClientScript("tmm" + this.GetType().Name, "alert('Hata oluştu. Hata:" + exc.Message + "');");
            }
        }
        #endregion
        #region Ortak Alanlar
        #region Olusturan
        [ModelDefault("AllowEdit", "False"), ReadOnly(true), XmlIgnore(), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int Olusturan { get; set; }

        private Kullanicilar _olusturankullanici;
        [XmlIgnore(), NonPersistent, XafDisplayName("Olusturan Kullanıcı"), ImmediatePostData,
        VisibleInListView(false), VisibleInLookupListView(false)]
        public Kullanicilar OlusturanKullanici
        {
            get
            {
                if (!IsLoading && !IsSaving)
                {
                    if (_olusturankullanici == null && this.Olusturan > 0)
                        this._olusturankullanici = this.Session.GetObjectByKey<Kullanicilar>(this.Olusturan);
                }
                return _olusturankullanici;
            }
        }
        #endregion

        [ModelDefault("AllowEdit", "False"), ModelDefault("DisplayFormat", "{0:dd.MM.yyyy HH:mm}"), ReadOnly(true), XmlIgnore(), VisibleInListView(false), VisibleInLookupListView(false)]
        public DateTime OlusturmaTarihi { get; set; }

        #region Guncelleyen
        [ModelDefault("AllowEdit", "False"), ReadOnly(true), XmlIgnore(), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int Guncelleyen { get; set; }

        private Kullanicilar _guncelleyenkullanici;
        [XmlIgnore(), NonPersistent, XafDisplayName("Guncelleyen Kullanıcı"), ImmediatePostData,
        VisibleInListView(false), VisibleInLookupListView(false)]
        public Kullanicilar GuncelleyenKullanici
        {
            get
            {
                if (!IsLoading && !IsSaving)
                {
                    if (_guncelleyenkullanici == null && this.Guncelleyen > 0)
                        this._guncelleyenkullanici = this.Session.GetObjectByKey<Kullanicilar>(this.Guncelleyen);
                }
                return _guncelleyenkullanici;
            }
        }
        #endregion

        [ModelDefault("AllowEdit", "False"), ModelDefault("DisplayFormat", "{0:dd.MM.yyyy HH:mm}"), ReadOnly(true), XmlIgnore(), VisibleInListView(false), VisibleInLookupListView(false)]
        public DateTime GuncellemeTarihi { get; set; }
        [Size(DbSize.ModulLenght), ModelDefault("AllowEdit", "False"), ReadOnly(true), XmlIgnore(), VisibleInListView(false), VisibleInLookupListView(false)]
        public string KaynakModul { get; set; }
        [ModelDefault("AllowEdit", "False"), ReadOnly(true), Description("Kaydın oluştuğu uygulama"), XmlIgnore(), VisibleInListView(false), VisibleInLookupListView(false)]
        public KaynakProgram KaynakProgram { get; set; }
        [Size(DbSize.CihazNoLenght), ModelDefault("AllowEdit", "False"), Browsable(false), ReadOnly(true), XmlIgnore(), VisibleInListView(false), VisibleInLookupListView(false)]
        public string CihazNo { get; set; }
        [ModelDefault("AllowEdit", "False"), ReadOnly(true), XmlIgnore(), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool Entegre { get; set; }
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public KayitDurumu Durum { get; set; }
        #endregion

        public CihazDetaylari() { }
        public CihazDetaylari(Session session) : base(session) { }
    }
}
