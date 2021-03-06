﻿using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Opera
{
    public class XpoHelper
    {
        public static int GetCount<T>()
        {
            return (int)Utillity.Session.Evaluate<T>(new AggregateOperand("", Aggregate.Count), null);
        } 

        public static XPBaseObject CloneBaseObject(XPBaseObject source, Type target, DevExpress.Xpo.Session _session, bool getdb)
        {
            try
            {
                if (source == null) return null;
                object obj = null;
                object keyValue = null;
                XPBaseObject targetObj = null;
                ConstructorInfo constructor = null;

                #region PropertyInfo
                object[] attributes = target.GetCustomAttributes(true);
                PropertyInfo[] properti = target.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                PropertyInfo[] properties = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                #region Key Ara Bul Selecct Cek

                #region Constructor
                ConstructorInfo[] xpoConstructorInfo = target.GetConstructors();
                if (xpoConstructorInfo != null && xpoConstructorInfo.Length > 0)
                {
                    foreach (ConstructorInfo cns in xpoConstructorInfo)
                    {
                        ParameterInfo[] param = cns.GetParameters();
                        if (param != null && param.Length == 1 && param[0].Name.Equals("session"))
                            constructor = cns;
                    }
                }
                #endregion

                if (getdb == true /*&& // varsa db deki kaydi getir
                    attributes.Where(x => x.ToString().Equals("DevExpress.Xpo.NonPersistentAttribute")).Count() < 1*/)
                {
                    foreach (PropertyInfo property in properti)
                    {
                        object[] keys = property.GetCustomAttributes(typeof(DevExpress.Xpo.KeyAttribute), true);
                        if (keys != null && keys.Length > 0)
                        {
                            PropertyInfo[] pi = properties.Where(p => p.Name.Equals(property.Name)).ToArray();
                            if (pi != null && pi.Length > 0)
                            {
                                keyValue = pi[0].GetValue(source, null);
                                if (keyValue != null)
                                    targetObj = (XPBaseObject)_session.GetObjectByKey(target, Convert.ToInt32(keyValue));
                                if (targetObj == null && constructor != null)
                                    targetObj = (XPBaseObject)constructor.Invoke(new object[] { _session });
                            }
                            if (targetObj == null && constructor != null)
                                targetObj = (XPBaseObject)constructor.Invoke(new object[] { _session });
                            if (targetObj == null && constructor != null)
                                targetObj = (XPBaseObject)Activator.CreateInstance(target);
                            break;
                        }
                    }
                }
                else
                {
                    if (targetObj == null && constructor != null)
                        targetObj = (XPBaseObject)constructor.Invoke(new object[] { _session });
                }
                if (targetObj == null)
                    targetObj = (XPBaseObject)Activator.CreateInstance(target);
                #endregion

                foreach (PropertyInfo property in properties)
                {
                    PropertyInfo[] prop = properti.Where(x => x.Name == property.Name).ToArray();
                    if (prop != null && prop.Length > 0)
                    {
                        System.Diagnostics.Debug.WriteLine(string.Format("[{0}][{1}]", prop[0].Name, prop[0].PropertyType.FullName));
                        if (prop[0].CanWrite)
                        {
                            if (property.CanRead)
                            {
                                obj = property.GetValue(source, null);
                                if (obj != null && obj.GetType().IsClass && !obj.GetType().FullName.Equals("System.String"))
                                {
                                    obj = Clone((XPBaseObject)obj, source.Session, true);
                                }

                            }
                            try
                            {
                                PropertyInfo pInfo = targetObj.GetType().GetProperty(prop[0].Name);
                                if (pInfo != null)
                                {
                                    if (pInfo.PropertyType.BaseType == typeof(XPObject) || pInfo.PropertyType.BaseType == typeof(XPBaseObject))
                                    {
                                        prop[0].SetValue(targetObj, source.Session.GetObjectByKey(pInfo.PropertyType, obj), null);
                                    }
                                    else
                                    {
                                        if (obj != null)
                                        {
                                            //string digerId = "";
                                            //if (pInfo.Name.Equals("DigerId"))
                                            //    digerId = "diger";

                                            System.Diagnostics.Trace.WriteLine(string.Format("{0} -> {1}", pInfo.Name, obj));
                                            if (prop[0].PropertyType == obj.GetType())
                                                prop[0].SetValue(targetObj, obj, null);
                                            else
                                            {
                                                prop[0].SetValue(targetObj, Convert.ChangeType(obj, pInfo.PropertyType), null);
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception eex)
                            {
                                System.Diagnostics.Debug.WriteLine(eex.StackTrace);
                            }
                        }
                    }

                }
                #endregion

                return targetObj;
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.StackTrace);
            }

            return null;
        }

        public static XPBaseObject CloneBaseObject(XPBaseObject source, Type target, DevExpress.Xpo.Session _session)
        {
            return CloneBaseObject(source, target, _session, true);
        }

        public static XPBaseObject Clone(XPBaseObject source, Session session, bool subItems)
        {
            if (source == null) return null;
            if (session == null) return null;
            Type typ = source.GetType();

            XPBaseObject clone = null;

            #region ConstructorInfo
            XPMemberInfo keyMemberInfo = source.ClassInfo.KeyProperty;
            object keyValue = keyMemberInfo.GetValue(source);
            //if (keyValue != null)
            //{
            //    clone = (XPBaseObject)session.GetObjectByKey(typ, keyValue);
            //}
            if (clone == null)
            {
                ConstructorInfo[] xpoConstructorInfo = typ.GetConstructors();
                if (xpoConstructorInfo != null && xpoConstructorInfo.Length > 0)
                {
                    foreach (ConstructorInfo cns in xpoConstructorInfo)
                    {
                        ParameterInfo[] param = cns.GetParameters();
                        if (param != null && param.Length == 1 && param[0].Name.Equals("session"))
                        {
                            clone = (XPBaseObject)cns.Invoke(new object[] { session });
                        }
                    }
                }
            }
            if (clone == null)
                return null;
            #endregion

            #region PropertyInfo
            PropertyInfo[] properties = typ.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                if (property.CanWrite)
                {
                    object obj = null;
                    if (property.PropertyType.Name.StartsWith("XPCollection"))
                    {
                        obj = property.GetValue(source, null);
                        property.SetValue(clone, obj, null);
                    }
                    else
                    {
                        obj = property.GetValue(source, null);
                        //if (obj != null && obj.GetType().IsClass && !obj.GetType().FullName.Equals("System.String"))
                        //{
                        //    obj = Clone((XPBaseObject)obj, session, true);
                        //}
                        property.SetValue(clone, obj, null);
                    }
                }
            }
            #endregion

            return clone;

        }
    }
}