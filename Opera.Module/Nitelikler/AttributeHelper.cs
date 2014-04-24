using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Mikrobar;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;

namespace Mikrobar.Module.Nitelikler
{
    public class AttributeHelper
    {
        private const string ReferansTabloAttributeType = "Mikrobar.Module.ReferansTabloAttribute";
        private const string ReferansAlanAttributeType = "Mikrobar.Module.ReferansAlanAttribute";
        private const string PersistentAttributeType = "DevExpress.Xpo.PersistentAttribute";
        public AttributeHelper() { }

        public AttributeHelper(Type classType) 
        {
            this.ClassType = classType;
            GetReferansTablo();
            GetFields();
            GetKeyMember();
            GetConstructor();
        }

        public ConstructorInfo Constructor { get; set; }
        private XPBaseObject BaseObject = null;
        public Type ClassType { get; set; }
        public XPMemberInfo KeyMember { get; set; }
        public ReferansTabloAttribute ReferansTablo { get; set; }
        public List<MikrobarField> Fields { get; set; }

        private void GetReferansTablo()
        {
            ReferansTabloAttribute refObj = null;
            object[] ModelDefaultAttributes = ClassType.GetCustomAttributes(true)
                .Where(x => x.GetType().ToString().Equals(ReferansTabloAttributeType))
                .ToArray();
            foreach (object obj in ModelDefaultAttributes)
            {
                refObj = obj as ReferansTabloAttribute;
                if (refObj != null) //&& refObj.SistemTipi == Utility.SistemParameter.ErpTur)
                    this.ReferansTablo = refObj;
            }
        }

        private void GetFields()
        {
            if (Fields == null)
            {
                Fields = new List<MikrobarField>();
                PropertyInfo[] prop = ClassType.GetProperties();
                foreach (PropertyInfo propertyInf in prop)
                {
                    object[] ModelDefaultAttributes = propertyInf.GetCustomAttributes(true)
                        .Where(x => x.GetType().ToString().Equals(ReferansAlanAttributeType))
                        .ToArray();
                    foreach (object ModelDefaultAttribute in ModelDefaultAttributes)
                    {
                        if (ModelDefaultAttribute != null && ModelDefaultAttribute is ReferansAlanAttribute)
                        {
                            ReferansAlanAttribute refAlan = ModelDefaultAttribute as ReferansAlanAttribute;
                            if (refAlan != null) //&& refAlan.SistemTipi == Utility.SistemParameter.ErpTur)
                            {
                                MikrobarField field = new MikrobarField();
                                field.PropertyName = propertyInf.Name;
                                object persistentAttribute = propertyInf.GetCustomAttributes(true)
                                    .Where(x => x.GetType().ToString().Equals(PersistentAttributeType))
                                    .FirstOrDefault();
                                if (persistentAttribute != null)
                                {
                                    field.DbFieldName = ((PersistentAttribute)persistentAttribute).MapTo;
                                }
                                else
                                {
                                    field.DbFieldName = propertyInf.Name;
                                }
                                field.RefFieldName = refAlan.KolonAd;
                                field.KeyField = refAlan.KeyField;
                                field.Index = refAlan.KeyIndex;
                                Fields.Add(field);
                            }
                        }
                    }
                }
                Fields.OrderBy(x => x.Index);
                Fields.TrimExcess();
            }
        }

        private void GetKeyMember()
        {
            try
            {
                BaseObject = (XPBaseObject)Activator.CreateInstance(ClassType);
                KeyMember = BaseObject.ClassInfo.KeyProperty;
            }
            catch (Exception exc1)
            {
                System.Diagnostics.Trace.WriteLine(exc1.Message);
            }
        }

        private void GetConstructor()
        {
            try
            {
                ConstructorInfo[] xpoConstructorInfo = ClassType.GetConstructors();
                if (xpoConstructorInfo != null && xpoConstructorInfo.Length > 0)
                {
                    foreach (ConstructorInfo cns in xpoConstructorInfo)
                    {
                        ParameterInfo[] param = cns.GetParameters();
                        if (param != null && param.Length == 1 && param[0].Name.Equals("session"))
                            Constructor = cns;
                    }
                }
            }
            catch (Exception exc1)
            {
                System.Diagnostics.Trace.WriteLine(exc1.Message);
            }
        }
    }

    public class MikrobarField
    {
        public MikrobarField() { }

        public MikrobarField(string propname, string dbfield, string reffield, bool iskey, int index) 
        {
            PropertyName = propname;
            DbFieldName = dbfield;
            RefFieldName = reffield;
            KeyField = iskey;
            Index = index;
        }

        public string PropertyName { get; set; }

        public string DbFieldName { get; set; }

        public string RefFieldName { get; set; }

        public bool KeyField { get; set; }

        public int Index { get; set; }

    }
}
