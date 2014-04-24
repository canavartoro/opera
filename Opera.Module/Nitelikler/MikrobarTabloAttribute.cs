using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using Mikrobar.Module.BusinessObjects;

namespace Mikrobar.Module
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class MikrobarTabloAttribute : Attribute
    {
        public MikrobarTabloAttribute()
        {
        }

        public MikrobarTabloAttribute(int tblNo,String tblAdi)
        {
            this.tabloAdi = tblAdi;
            this.tabloNo = tblNo;
            this.modul = "GNL";
            this.aciklama = "";
        }

        public MikrobarTabloAttribute(int tblNo,String tblAdi,string tblModul)
        {
            this.tabloAdi = tblAdi;
            this.tabloNo = tblNo;
            this.modul = tblModul;
            this.aciklama = "";
        }

        public MikrobarTabloAttribute(int tblNo,String tblAdi,string tblModul, string tblAciklama)
        {
            this.tabloAdi = tblAdi;
            this.tabloNo = tblNo;
            this.modul = tblModul;
            this.aciklama = tblAciklama;
        }

        protected String tabloAdi;
        protected int tabloNo;
        protected String modul;
        protected String aciklama;

        public String TabloAdi
        {
            get
            {
                return this.tabloAdi;

            }

            set
            {
                this.tabloAdi = value;

            }
        }

        public int TabloNo
        {
            get
            {
                return this.tabloNo;

            }

            set
            {
                this.tabloNo = value;

            }
        }

        public String Modul
        {
            get
            {
                return this.modul;

            }

            set
            {
                this.modul = value;

            }
        }

        public String Aciklama
        {
            get
            {
                return this.aciklama;

            }

            set
            {
                this.aciklama = value;

            }
        }
    }
}
