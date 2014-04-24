using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mikrobar
{
    public static class ExtensionMethod
    {
        public static bool IsNull(this object Obj)
        {
            if (object.ReferenceEquals(Obj, null))
                return true;

            return false;
        }
    }
}
