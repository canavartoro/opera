using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [OptimisticLocking(false), DeferredDeletion(false), ModelDefault("DefaultListViewShowAutoFilterRow", "True"),
     DebuggerDisplay(" DosyaAdi = {DosyaAdi},  Versiyon = {Versiyon}, Program = {Program}")]
    [Serializable]
    public class ProgramDosyalari : XPObject
    {
        public string DosyaAdi { get; set;}
        public byte[] Dosya { get; set; }
        [Size(DbSize.Limitsiz)]
        public String DosyaOkunan { get; set; }
        public string Versiyon { get; set; }
        public string UzantiAdi { get; set; }
        public CihazTip ProgramTip { get; set; }
        public string KaynakModul { get; set; }
        public DateTime EklemeTarihi { get; set; }
        public DateTime GuncellemeTarihi { get; set; }


        public ProgramDosyalari() { }
        public ProgramDosyalari(Session session) : base(session) { }
    }
}
