using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo.Metadata;
using Mikrobar.Module.BusinessObjects;

namespace Mikrobar.Module
{
    public class AmbalajDurumConverter : ValueConverter
    {
        public override object ConvertFromStorageType(object value)
        {
            if (object.ReferenceEquals(value, null)) return AmbalajDurumu.Bosta;

            if (Enum.IsDefined(typeof(AmbalajDurumu), value.ToString()))
                return (AmbalajDurumu)Enum.Parse(typeof(AmbalajDurumu), value.ToString());
            else
                return AmbalajDurumu.Bosta;
        }

        public override object ConvertToStorageType(object value)
        {
            if (object.ReferenceEquals(value, null)) return "Bosta";

            AmbalajDurumu drm = (AmbalajDurumu)Enum.Parse(typeof(AmbalajDurumu), value.ToString());
            return drm.ToString();
        }

        public override Type StorageType
        {
            get { return typeof(string); }
        }
    }
}
