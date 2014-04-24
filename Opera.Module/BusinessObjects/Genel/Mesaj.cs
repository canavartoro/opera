using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mikrobar
{
    public struct Mesaj
    {
        public Mesaj(int no, string kategori, string mesaj)
        {
            _no = no;
            _mesaj = mesaj;
            _kategori = kategori;
        }

        int _no;
        string _mesaj;
        string _kategori;

        public int HataNo
        {
            get { return _no; }
            set { _no = value; }
        }
        public string HataMesaj
        {
            get { return _mesaj; }
            set { _mesaj = value; }
        }
        public string Kategori
        {
            get { return _kategori; }
            set { _kategori = value; }
        }

        public override string ToString()
        {
            return "ERR_" + _no;
        }
    }
}
