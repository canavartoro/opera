using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Diagnostics;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;

namespace Mikrobar.Module.BusinessObjects
{
    public class V_EtiketTanimlari : XPLiteObject
    {
        public int MalzemeId { get; set; }
        public string MalzemeKod { get; set; }
        public string MalzemeAd { get; set; }
        public string Barkod { get; set; }
        public int MalzemeBirimId { get; set; }
        public string Birim { get; set; }
        public decimal Miktar { get; set; }
        public decimal En { get; set; }
        public decimal Boy { get; set; }
        public decimal Yukekseklik { get; set; }
        public decimal Hacim { get; set; }
        public decimal Agirlik { get; set; }
        public decimal Derinlik { get; set; }
        [Size(DbSize.AdLenght)]
        public string UrunAdi { get; set; }
        [Size(DbSize.AdLenght)]
        public string MarkaAdi { get; set; }
        public string Amper { get; set; }
        [Size(DbSize.IkiYuzElli)]
        public string Resim { get; set; }
        [Size(DbSize.AdLenght)]
        public string MadeIn { get; set; }
        [Size(DbSize.IkiYuzElli)]
        public string Adres { get; set; }
        public string Turkce { get; set; }
        public string Rusca { get; set; }
        public string Ingilizce { get; set; }
        public string Hollandaca { get; set; }
        public string Romence { get; set; }
        public string Arapca { get; set; }
        public string Ibranice { get; set; }
        public string Slovence { get; set; }
        public string Ispanyolca { get; set; }
        public string Almanca { get; set; }
        public string Esyonya { get; set; }
        public string Letonya { get; set; }
        public string Litvanya { get; set; }
        public string Ukrayna { get; set; }
        public string Italyanca { get; set; }
        public string Hirvatca { get; set; }
        public string Isvetce { get; set; }
        public string Lethce { get; set; }
        public string Fransizca { get; set; }
        public string Portekizce { get; set; }
        public string Cekce { get; set; }
        public string Polonya { get; set; }
        public string TurkceRenk { get; set; }
        public string RuscaRenk { get; set; }
        public string IngilizceRenk { get; set; }
        public string HollandacaRenk { get; set; }
        public string RomenceRenk { get; set; }
        public string ArapcaRenk { get; set; }
        public string IbraniceRenk { get; set; }
        public string SlovenceRenk { get; set; }
        public string IspanyolcaRenk { get; set; }
        public string AlmancaRenk { get; set; }
        public string EstonyaRenk { get; set; }
        public string LetonyaRenk { get; set; }
        public string LitvanyaRenk { get; set; }
        public string UkraynaRenk { get; set; }
        public string ItalyancaRenk { get; set; }
        public string HirvatcaRenk { get; set; }
        public string IsvetceRenk { get; set; }
        public string LethceRenk { get; set; }
        public string FransizcaRenk { get; set; }
        public string PortekizceRenk { get; set; }
        public string CekceRenk { get; set; }
        public string PolonyaRenk { get; set; }
        public string Tse { get; set; }
        public string Ce { get; set; }
        public string Pct { get; set; }
        public string Esc { get; set; }
        public string Ww { get; set; }
        public string Cb { get; set; }
        public string SeriliBarkod { get; set; }


        public V_EtiketTanimlari() { }
        public V_EtiketTanimlari(Session session) : base(session) { }
    }
}
