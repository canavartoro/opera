using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Mikrobar.Module.BusinessObjects
{
    [DomainComponent, ModelDefault("Caption", "Giris")]
    public class CustomLogonParameters
    {

        private SistemKullanici sistemKullanici;
        [DataSourceProperty("AvailableUsers")]//, ImmediatePostData]
        [XafDisplayName("Kullanici :")]
        [ModelDefault("PropertyEditorType", "Mikrobar.Module.Web.Controllers.ASPxSearchSistemKullanici")]
        public SistemKullanici SistemKullanici
        {
            get { return sistemKullanici; }
            set
            {
                sistemKullanici = value;
            }
        }

        private void RefreshAvailableUsers()
        {
            if (availableUsers == null) return;
            //availableUsers.Criteria = null;
            //availableUsers.Criteria = new BinaryOperator("Durum", KayitDurumu.Yeni);            
        }

        private string password;
        [PasswordPropertyText(true),XafDisplayName("Parola :")]
        public string Password
        {
            get { return password; }
            set
            {
                if (password == value) return;
                password = value;
            }
        }

        private IObjectSpace objectSpace;
        private List<SistemKullanici> availableUsers;
        [Browsable(false)]
        public IObjectSpace ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }

        [Browsable(false)]
        [CollectionOperationSet(AllowAdd = false)]
        public List<SistemKullanici> AvailableUsers
        {
            get
            {
                if (availableUsers == null)
                {
                    XPQuery<SistemKullanicilari> kullanicilar = new XPQuery<SistemKullanicilari>(((XPObjectSpace)ObjectSpace).Session);
                    availableUsers = (from x in kullanicilar
                                      where x.Durum == KayitDurumu.Yeni
                                      orderby x.KullaniciKod2
                                      select new SistemKullanici(((XPObjectSpace)ObjectSpace).Session)
                                      {
                                          Oid = x.Oid,
                                          UserName = x.KullaniciKod2,
                                          KullaniciKod = x.KullaniciKod2,
                                          Aciklama = x.Aciklama
                                      }).ToList();
                    //availableUsers = ObjectSpace.GetObjects<SistemKullanicilari>() as XPCollection<SistemKullanicilari>;
                    //RefreshAvailableUsers();
                    //availableUsers = new XPCollection<SistemKullanicilari>(((XPObjectSpace)ObjectSpace).Session, new BinaryOperator("Durum", KayitDurumu.Yeni), new SortProperty[] { new SortProperty() { PropertyName = "KullaniciAd", Direction = DevExpress.Xpo.DB.SortingDirection.Ascending } });
                    //availableUsers.TopReturnedObjects = 10;
                }
                return availableUsers;
            }
            }
    }

    [NonPersistent, NavigationItem(false), XafDefaultProperty("UserName")]
    public class SistemKullanici : XPLiteObject
    {
        public SistemKullanici() : base(Session.DefaultSession) { }
        public SistemKullanici(Session session) : base(session) { }
        [Browsable(false)]
        public Guid Oid { get; set; }
        public string UserName { get; set; }
        public string KullaniciKod { get; set; }
        public string Aciklama { get; set; }
        [Browsable(false)]
        public string StoredPassword { get; set; }

        public bool ComparePassword(SistemKullanici xuser, string password)
        {
            SistemKullanicilari user = xuser.Session.GetObjectByKey<SistemKullanicilari>(xuser.Oid);
            if (user != null)
                return user.ComparePassword(password);
            return false;
            //return SecurityUserBase.ComparePassword(StoredPassword, password);
        }
    }
}
