using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mikrobar.Module.BusinessObjects
{
    [Serializable]
    public class MikrobarException : Exception
    {
        public MikrobarException() { }
        public MikrobarException(string err)
            : base(err)
        {

        }
        public MikrobarException(string err, int errNo)
            : base(err)
        {
            this.ErrorNo = errNo;
        }
        public MikrobarException(string err, int errNo, string sourceModule)
            : base(err)
        {
            this.ErrorNo = errNo;
            this.SourceModule = sourceModule;
        }

        public int ErrorNo { get; set; }
        public string SourceModule { get; set; }
    }
}
