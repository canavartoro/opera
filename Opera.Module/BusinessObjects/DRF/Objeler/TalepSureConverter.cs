using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo.Metadata;
using Mikrobar.Module.BusinessObjects;

namespace MainDemo.Module
{
    public class TalepSureConverter : ValueConverter
    {
        public override object ConvertFromStorageType(object value)
        {
            if (object.ReferenceEquals(value, null)) return TalepSureleri.Hemen;

            if (Enum.IsDefined(typeof(TalepSureleri), value))
                return (TalepSureleri)Enum.Parse(typeof(TalepSureleri), value.ToString());
            else
                return TalepSureleri.Hemen;
        }

        public override object ConvertToStorageType(object value)
        {
            if (object.ReferenceEquals(value, null)) return 0;

            TalepSureleri tsr = (TalepSureleri)Enum.Parse(typeof(TalepSureleri), value.ToString());
            return tsr.GetHashCode();
        }

        public override Type StorageType
        {
            get { return typeof(string); }
        }
    }
}
