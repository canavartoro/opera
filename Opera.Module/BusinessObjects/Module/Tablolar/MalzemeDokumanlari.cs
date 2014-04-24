using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Mikrobar.Module.BusinessObjects
{
    //[ReferansTablo("pub.stok_belge", SistemTipi = SistemTipi.Progress)]
    [DeferredDeletion(false), OptimisticLocking(false), DefaultClassOptions, XafDefaultProperty("MalzemeDokumanId"), ModelDefault("DefaultListViewShowAutoFilterRow", "True")]
    [NavigationItem(false), ImageName("BO_Role"), DebuggerDisplay("MalzemeId = {MalzemeId}, MalzemeKod = {MalzemeKod}, DosyaYolu = {DosyaYolu}, DosyaAdi = {DosyaAdi}")]
    [ReferansTablo("pub.stok_belge", SqlSorgu=@"select cast(b.rowid as int) as ""MalzemeDokumanId"", cast(s.rowid as int) as ""MalzemeId"", b.stok_kod as ""MalzemeKod"", b.dosya_ad as ""DosyaAdi"", b.dosya_ad_web as ""DosyaYolu"" from pub.stok_belge b, pub.stok_kart s where b.firma_kod = '#FirmaKod#' and b.stok_kod = s.stok_kod and b.firma_kod = s.firma_kod ", SistemTipi = SistemTipi.Progress)]
    public class MalzemeDokumanlari : XPBaseObject
    {
        [ModelDefault("DisplayFormat", "d"), Key(AutoGenerate = false)]
        [ReferansAlan("cast(rowid as int)", SistemTipi = SistemTipi.Progress)]
        public int MalzemeDokumanId { get; set; }

        #region Malzeme Bilgisi
        //protected Malzemeler _malzeme;
        [Indexed, VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false), PersistentAlias("Iif(Malzeme is null, 0, Malzeme.MalzemeId)")]
        public int MalzemeId
        {
            get { return Convert.ToInt32(EvaluateAlias("MalzemeId")); }
            set
            {
                if (!IsLoading && value > 0)
                {
                    SetPropertyValue<Malzemeler>("Malzeme", ref fMalzeme, Session.GetObjectByKey<Malzemeler>(value));
                    //if (object.ReferenceEquals(this.fMalzeme, null))
                    //    throw new MikrobarException(Lang.Mesaj(GenelMesajlar.ERR_6018, value), 34);
                    //else
                    //    this.MalzemeKod = this.fMalzeme.MalzemeKod;
                }
            }
        }

        [VisibleInDetailView(false)]
        [ReferansAlan("stok_kod", SistemTipi = SistemTipi.Progress)]
        [ModelDefault("AllowEdit", "false"), ReadOnly(true), Size(DbSize.KodLenght), VisibleInLookupListView(true)]
        public string MalzemeKod { get; set; }

        Malzemeler fMalzeme;
        [XmlIgnore(), Association(@"MalzemeDokumanlari.Malzeme.MalzemeId"), NoForeignKey, Persistent("MalzemeId")]
        public Malzemeler Malzeme
        {
            get { return fMalzeme; }
            set { SetPropertyValue<Malzemeler>("Malzeme", ref fMalzeme, value); }
        }
        #endregion

        [ReferansAlan("dosya_ad_web", SistemTipi = SistemTipi.Progress )]
        public string DosyaYolu { get; set; }

        [ReferansAlan("dosya_ad", SistemTipi = SistemTipi.Progress )]
        public string DosyaAdi { get; set; }
         
        public MalzemeDokumanlari() { }
        public MalzemeDokumanlari(Session session) : base(session) { }
    }
}
//select cast(b.rowid as int) as "MalzemeDokumanId", cast(s.rowid as int) as "MalzemeId", b.stok_kod as "MalzemeKod", b.dosya_ad as "DosyaAdi", b.dosya_ad_web as "DosyaYolu" from pub.stok_belge b, pub.stok_kart s where b.firma_kod = '#FirmaKod#' and b.stok_kod = s.stok_kod and b.firma_kod = s.firma_kod 
