using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;

namespace Mikrobar.Module.BusinessObjects {
    [DefaultClassOptions]
    public class Payment : BaseObject {
        private double rate;
        private double hours;
        public Payment(Session session)
            : base(session) {
        }
        [PersistentAlias("Rate * Hours")]
        public double Amount {
            get {
                return Convert.ToDouble(EvaluateAlias("Amount"));
            }
        }
        public double Rate {
            get {
                return rate;
            }
            set {
                if (SetPropertyValue("Rate", ref rate, value))
                    OnChanged("Amount");
            }
        }
        public double Hours {
            get {
                return hours;
            }
            set {
                if (SetPropertyValue("Hours", ref hours, value))
                    OnChanged("Amount");
            }
        }
    }
}
