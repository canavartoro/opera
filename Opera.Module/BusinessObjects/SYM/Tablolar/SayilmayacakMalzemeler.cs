﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Xml.Serialization;
using System.Diagnostics;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;

namespace Mikrobar.Module.BusinessObjects
{
    [OptimisticLocking(false), DeferredDeletion(false), DefaultClassOptions,
    DebuggerDisplay("Oid = {Oid}"), XafDefaultProperty("Malzeme")]
    [ImageName("BO_Sale_Item_v92"), ModelDefault("DefaultListViewShowAutoFilterRow", "True"), NavigationItem(false)]
    public class SayilmayacakMalzemeler : XPObject
    {
        #region Malzeme

        [ModelDefault("AllowEdit", "False"), PersistentAlias("Iif(Malzeme is null, 0, Malzeme.MalzemeId)"), 
        VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int MalzemeId
        {
            get
            {
                return Convert.ToInt32(EvaluateAlias("MalzemeId"));
            }
            set
            {
                SetPropertyValue("Malzeme", ref _malzeme, Session.GetObjectByKey<Malzemeler>(value));
                OnChanged("Malzeme");
            }
        }

        protected Malzemeler _malzeme;
        [XmlIgnore(), XafDisplayName("Malzeme Kodu"), ImmediatePostData,
        VisibleInListView(false), VisibleInLookupListView(false), 
        Association(@"SayilmayacakMalzemeler.Malzeme.MalzemeId"), Persistent("MalzemeId"), NoForeignKey]
        public Malzemeler Malzeme
        {
            get
            {
                return _malzeme;
            }
            set
            {
                SetPropertyValue("Malzeme", ref _malzeme, value);
            }
        }

        [VisibleInLookupListView(true), VisibleInDetailView(true), PersistentAlias("Iif(Malzeme is null, '', Malzeme.MalzemeAd)")]
        public string MalzemeAd { get { return Convert.ToString(EvaluateAlias("MalzemeAd")); } }


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
                if (!IsSaving && !IsLoading)
                {
                    if (_guncelleyenkullanici == null && this.Guncelleyen > 0)
                        this._guncelleyenkullanici = this.Session.GetObjectByKey<Kullanicilar>(this.Guncelleyen);
                }
                return _guncelleyenkullanici;
            }
        }
        #endregion

        [Size(DbSize.AciklamaLenght)]
        public string Aciklama { get; set; }
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

        [NonPersistent]
        private KayitDurumu _Durum;
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public KayitDurumu Durum
        {
            get { return _Durum; }
            set
            {
                this._Durum = value;

            }
        }
        #endregion

        public SayilmayacakMalzemeler() { }
        public SayilmayacakMalzemeler(Session session) : base(session) { }

    }

}
