using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace Mikrobar.Module.BusinessObjects
{
    [Persistent("V_TransferOkunan")]
    public class V_TransferOkunan : XPLiteObject
    {

        public string HedefRaf { get; set; }
        public string Raf { get; set; }
        public string KaynakRaf { get; set; }
        public string DepoKod { get; set; }
        public string HedefDepoKod { get; set; }
        public string Barkod { get; set; }
        public string MalzemeKod { get; set; }
        public string MalzemeAd { get; set; }
        public string Birim { get; set; }
        public string Birim2 { get; set; }
        public decimal Miktar { get; set; }
        public decimal Miktar2 { get; set; }
        [NonPersistent]
        public decimal OkunanMiktar { get; set; }
        [NonPersistent]
        public decimal OkunanMiktar2 { get; set; }
        public string SeriNo { get; set; }
        
        [Key]
        public int AmbalajHareketDetayId { get; set; }
        public int AmbalajHareketId { get; set; }
        public int AmbalajId { get; set; }
        public int MalzemeId { get; set; }
        public int BirimId { get; set; }
        public int Birim2Id { get; set; }        
        public int RafId { get; set; }
        public int DepoId { get; set; }
        public int HedefDepoId { get; set; }
        public int HedefRafId { get; set; }
        public int KaynakRafId { get; set; }

        [NonPersistent]
        public string Aciklama1 { get; set; }
        [NonPersistent]
        public string Aciklama2 { get; set; }
        [NonPersistent]
        public string Aciklama3 { get; set; }
        [NonPersistent]
        public string PartiNo { get; set; }
        [NonPersistent]
        public string PaletNo { get; set; }


        public V_TransferOkunan(Session session) : base(session) { }
        public V_TransferOkunan() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
