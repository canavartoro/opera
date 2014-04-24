using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mikrobar.Module.BusinessObjects;

namespace Mikrobar.Module
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    public class ReferansAlanAttribute : Attribute
    {
        public ReferansAlanAttribute(String kolonadi)
        {
            this.kolonadiAttribute = kolonadi;
        }

        public ReferansAlanAttribute(String kolonadi, SistemTipi tip)
        {
            this.kolonadiAttribute = kolonadi;
            this.erpAttribute = tip;
        }

        public ReferansAlanAttribute(String kolonadi, SistemTipi tip, bool isKey)
        {
            this.kolonadiAttribute = kolonadi;
            this.erpAttribute = tip;
            this.erpkeyFieldAttribute = isKey;
        }

        public ReferansAlanAttribute(String kolonadi, SistemTipi tip, bool isKey, int index)
        {
            this.kolonadiAttribute = kolonadi;
            this.erpAttribute = tip;
            this.erpkeyFieldAttribute = isKey;
            this.erpkeyIndexFieldAttribute = index;
        }

        public ReferansAlanAttribute(params String[] kolonadi)
        {
            if (kolonadi != null && kolonadi.Length > 0)
            {
                for (int i = 0; i < kolonadi.Length; i++)
                {
                    if (i > 0)
                        this.kolonadiAttribute += ",";
                    this.kolonadiAttribute += kolonadi[i];
                }
            }
        }

        public ReferansAlanAttribute(String tabloAdi, String kolonAdi, SistemTipi tip)
        {
            this.tabloAdiAttribute = tabloAdi;
            this.kolonadiAttribute = kolonAdi;
            this.erpAttribute = tip;
        }


        public ReferansAlanAttribute(String tabloAdi, String kolonAdi, String aciklama)
        {
            this.tabloAdiAttribute = tabloAdi;
            this.kolonadiAttribute = kolonAdi;
            this.aciklamaAttribute = aciklama;
        }

        protected SistemTipi erpAttribute = SistemTipi.Diger;
        protected String tabloAdiAttribute = string.Empty;
        protected String kolonadiAttribute = string.Empty;
        protected String aciklamaAttribute = string.Empty;
        protected bool erpkeyFieldAttribute = false;
        protected int erpkeyIndexFieldAttribute = 99;

        public String TabloAd
        {
            get
            {
                return this.tabloAdiAttribute;

            }

            set
            {
                this.tabloAdiAttribute = value;

            }
        }        

        public String KolonAd
        {
            get
            {
                return this.kolonadiAttribute;

            }

            set
            {
                this.kolonadiAttribute = value;

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

        public bool KeyField
        {
            get
            {
                return this.erpkeyFieldAttribute;

            }

            set
            {
                this.erpkeyFieldAttribute = value;

            }
        }

        public int KeyIndex
        {
            get
            {
                return this.erpkeyIndexFieldAttribute;

            }

            set
            {
                this.erpkeyIndexFieldAttribute = value;

            }
        }
    }
}
