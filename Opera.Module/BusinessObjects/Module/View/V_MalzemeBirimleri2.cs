using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.DC;
using DevExpress.Xpo;

namespace Mikrobar.Module.BusinessObjects
{
    [DebuggerDisplay("BirimId = {BirimId}, MalzemeId = {MalzemeId}, Birim = {Birim}")]
    [NonPersistent, XafDefaultProperty("Birim")]
    [ReferansTablo(TabloAdi = "INVD_ITEM_UNIT", SistemTipi = SistemTipi.WebErp)]
    public class V_MalzemeBirimleri2 : XPLiteObject
    {
        [Key]
        public int MalzemeBirimId { get; set; }     
        public int BirimId { get; set; }
        public string Birim { get; set; }
        public int Birim2Id { get; set; }
        public int MalzemeId { get; set; }
        public decimal Miktar { get; set; }
        public decimal Miktar2 { get; set; }
        public int SiraNo { get; set; }
        public decimal En { get; set; }
        public decimal Boy { get; set; }
        public decimal Yukekseklik { get; set; }
        public decimal Hacim { get; set; }
        public decimal Agirlik { get; set; }
        public decimal Derinlik { get; set; }

        public V_MalzemeBirimleri2(Session session) : base(session) { }
        public V_MalzemeBirimleri2() : base(Session.DefaultSession) { }
    }
}

