using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo.Metadata;

namespace MainDemo.Module
{
    public class DateToTimeConverter : ValueConverter
    {
        public override object ConvertFromStorageType(object value)
        {
            if (object.ReferenceEquals(value, null)) return new TimeSpan();
            DateTime dtTime = (DateTime)value;
            return dtTime.TimeOfDay;
        }

        public override object ConvertToStorageType(object value)
        {
            if (object.ReferenceEquals(value, null)) return DateTime.Now;

            TimeSpan tmTime = (TimeSpan)value;
            if (object.ReferenceEquals(tmTime, null)) return DateTime.Now;

            DateTime dtToTime = new DateTime(tmTime.Ticks);
            return dtToTime;
        }

        public override Type StorageType
        {
            get { return typeof(DateTime); }
        }
    }
}
