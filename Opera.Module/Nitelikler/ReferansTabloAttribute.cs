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
    public class ReferansTabloAttribute : Attribute
    {
        public ReferansTabloAttribute()
        {
        }

        public ReferansTabloAttribute(String tabloAdi)
        {
            this.referansTabloAttribute = tabloAdi;
            this.erpAttribute = SistemTipi.Progress;
            this._sorgu = false;
        }

        public ReferansTabloAttribute(String tabloAdi, SistemTipi sistemtip)
        {
            this.referansTabloAttribute = tabloAdi;
            this.erpAttribute = sistemtip;
            this._sorgu = false;
        }

        public ReferansTabloAttribute(bool sorgu, SistemTipi tip)
        {
            this._sorgu = sorgu;
            this.erpAttribute = tip;
        }

        public ReferansTabloAttribute(params String[] tabloAdi)
        {
            if (tabloAdi != null && tabloAdi.Length > 0)
            {
                for (int i = 0; i < tabloAdi.Length; i++)
                {
                    if (i > 0)
                        this.referansTabloAttribute += ",";
                    this.referansTabloAttribute += tabloAdi[i];
                }
            }
            this.erpAttribute = SistemTipi.Progress;
        }

        public ReferansTabloAttribute(String tabloAdi, SistemTipi tip, bool sorgu)
        {
            this.referansTabloAttribute = tabloAdi;
            this.erpAttribute = tip;
            this._sorgu = sorgu;
        }

        public ReferansTabloAttribute(String tabloAdi, SistemTipi tip, bool sorgu, String aciklama)
        {
            this.referansTabloAttribute = tabloAdi;
            this.erpAttribute = tip;
            this._sorgu = sorgu;
            this.aciklamaAttribute = aciklama;
        }

        protected String referansTabloAttribute;
        protected SistemTipi erpAttribute;
        protected String aciklamaAttribute;
        protected bool _sorgu = false;
        protected QueryType queryType;
        protected int entegrasyonSure = 1;

        protected string _sqlsorgu = "";
        protected string _sqlsorguWhere = "";

        public String SqlSorgu
        {
            get
            {
                return this._sqlsorgu;

            }

            set
            {
                this._sqlsorgu = value;

            }
        }

        public String SqlWhere
        {
            get
            {
                return this._sqlsorguWhere;

            }

            set
            {
                this._sqlsorguWhere = value;

            }
        }

        public String TabloAdi
        {
            get
            {
                return this.referansTabloAttribute;

            }

            set
            {
                this.referansTabloAttribute = value;

            }
        }

        public String Aciklama
        {
            get
            {
                return this.aciklamaAttribute;

            }

            set
            {
                this.aciklamaAttribute = value;

            }
        }

        public QueryType QueryType
        {
            get { return this.queryType; }
            set { this.queryType = value; }
        }

        public int EntegrasyonSure
        {
            get { return this.entegrasyonSure; }
            set { this.entegrasyonSure = value; }
        }

        public bool Sorgu
        {
            get
            {
                return this._sorgu;

            }

            set
            {
                this._sorgu = value;

            }
        }

        public SistemTipi SistemTipi
        {
            get
            {
                return this.erpAttribute;

            }

            set
            {
                this.erpAttribute = value;

            }
        }
    }
}
