using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Xml.Serialization;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [NonPersistent]
    public abstract class MikrobarBaseObject : XPBaseObject
    {
        public MikrobarBaseObject() : base(Session.DefaultSession) { }
        public MikrobarBaseObject(Session session) : base(session) { }

        protected override void OnSaving()
        {
            SistemKullanicilari currentUser = SecuritySystem.CurrentUser as SistemKullanicilari;
            if (this.OlusturmaTarihi != new DateTime(1, 1, 1))
            {
                this.Guncelleyen = currentUser != null ? currentUser.KullaniciId : 0;
                this.GuncellemeTarihi = DateTime.Now;
            }
            else
            {
                this.Olusturan = currentUser != null ? currentUser.KullaniciId : 0;
                this.OlusturmaTarihi = DateTime.Now;
                if (string.IsNullOrEmpty(this.KaynakModul))
                {
                    this.KaynakModul = GetType().Name;
                    this.KaynakProgram = BusinessObjects.KaynakProgram.WebKonsol;
                }
            }
            base.OnSaving();
        }

        #region Ortak Alanlar
        [ModelDefault("AllowEdit", "false"), ReadOnly(true), XmlIgnore()]
        public int Olusturan { get; set; }
        [ModelDefault("AllowEdit", "false"), ReadOnly(true), XmlIgnore()]
        public DateTime OlusturmaTarihi { get; set; }
        [ModelDefault("AllowEdit", "false"), ReadOnly(true), XmlIgnore()]
        public int Guncelleyen { get; set; }
        [ModelDefault("AllowEdit", "false"), ReadOnly(true), XmlIgnore()]
        public DateTime GuncellemeTarihi { get; set; }
        [Size(DbSize.ModulLenght), ModelDefault("AllowEdit", "false"), ReadOnly(true), XmlIgnore()]
        public string KaynakModul { get; set; }
        [Description("Kaydın oluştuğu uygulama"), XmlIgnore()]
        public KaynakProgram KaynakProgram { get; set; }
        [Size(DbSize.CihazNoLenght), ModelDefault("AllowEdit", "false"), Browsable(false), ReadOnly(true), XmlIgnore()]
        public string CihazNo { get; set; }
        [ModelDefault("AllowEdit", "false"), ReadOnly(true), XmlIgnore()]
        public bool Entegre { get; set; }
        public KayitDurumu Durum { get; set; }
        #endregion
    }
}
