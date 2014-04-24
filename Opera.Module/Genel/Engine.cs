using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Reflection;
using System.Collections;

namespace Mikrobar.Module.Genel
{
    public class Engine
    {

        public object CloneProperties(object origin, Type t)
        {
            object destination = Activator.CreateInstance(t);
            // Instantiate if necessary
            if (destination == null) throw new ArgumentNullException("destination", "Destination object must first be instantiated.");
            if (origin == null) throw new ArgumentNullException("origin", "Destination object must first be instantiated.");
            // Loop through each property in the destination
            foreach (PropertyInfo destinationProperty in destination.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                // find and set val if we can find a matching property name and matching type in the origin with the origin's value
                if (destinationProperty.CanWrite)
                {
                    PropertyInfo p = origin.GetType().GetProperty(destinationProperty.Name);
                    if (p != null)
                    {
                        destinationProperty.SetValue(destination, p.GetValue(origin, null), null);
                    }
                }
            }

            return destination;
        }

        public void CloneProperties(object origin, ref object TargetObj, Type t)
        {
            object destination = Activator.CreateInstance(t);
            // Instantiate if necessary
            if (destination == null) throw new ArgumentNullException("destination", "Destination object must first be instantiated.");
            if (origin == null) throw new ArgumentNullException("origin", "Destination object must first be instantiated.");
            // Loop through each property in the destination
            foreach (PropertyInfo destinationProperty in destination.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                // find and set val if we can find a matching property name and matching type in the origin with the origin's value
                if (destinationProperty.CanWrite)
                {
                    PropertyInfo p = origin.GetType().GetProperty(destinationProperty.Name);
                    if (p != null)
                    {
                        destinationProperty.SetValue(destination, p.GetValue(origin, null), null);
                    }
                }
            }

            TargetObj = destination;
        }

        public object CloneObj(object source, Type t)
        {
            try
            {
                object target = Activator.CreateInstance(t);
                if (source != null)
                {
                    object result = Activator.CreateInstance(t);
                    foreach (FieldInfo field in source.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
                    {
                        if (field.FieldType.GetInterface("IList", false) == null)
                        {
                            field.SetValue(result, field.GetValue(source));
                        }
                        else
                        {
                            IList listObject = (IList)field.GetValue(result);
                            if (listObject != null)
                            {
                                foreach (object item in ((IList)field.GetValue(source)))
                                {
                                    listObject.Add(CloneObj(item, t));
                                }
                            }
                        }
                    }
                    return result;
                }
            }
            catch (Exception exc)
            {
            }
            finally
            {
            }

            return null;
        }

        public object CloneObject(object source, Type typ)
        {
            object newObject = Activator.CreateInstance(typ);

            //We get the array of fields for the new type instance.
            FieldInfo[] fields = newObject.GetType().GetFields(BindingFlags.Public);

            int i = 0;

            foreach (FieldInfo fi in source.GetType().GetFields(BindingFlags.Public))
            {
                //We query if the fiels support the ICloneable interface.
                Type ICloneType = fi.FieldType.GetInterface("ICloneable", true);

                if (ICloneType != null)
                {
                    //Getting the ICloneable interface from the object.
                    ICloneable IClone = (ICloneable)fi.GetValue(source);

                    try
                    {
                        //We use the clone method to set the new value to the field.
                        fields[i].SetValue(newObject, IClone.Clone());
                    }
                    catch { ;}
                }
                else
                {
                    try
                    {
                        // If the field doesn't support the ICloneable 
                        // interface then just set it.
                        fields[i].SetValue(newObject, fi.GetValue(source));
                    }
                    catch { ;}
                }

                //Now we check if the object support the 
                //IEnumerable interface, so if it does
                //we need to enumerate all its items and check if 
                //they support the ICloneable interface.
                Type IEnumerableType = fi.FieldType.GetInterface("IEnumerable", true);

                if (IEnumerableType != null)
                {
                    //Get the IEnumerable interface from the field.
                    IEnumerable IEnum = (IEnumerable)fi.GetValue(source);

                    //This version support the IList and the 
                    //IDictionary interfaces to iterate on collections.
                    Type IListType = fields[i].FieldType.GetInterface("IList", true);

                    Type IDicType = fields[i].FieldType.GetInterface("IDictionary", true);

                    int j = 0;
                    if (IListType != null)
                    {
                        //Getting the IList interface.
                        IList list = (IList)fields[i].GetValue(newObject);

                        foreach (object obj in IEnum)
                        {
                            //Checking to see if the current item 
                            //support the ICloneable interface.
                            ICloneType = obj.GetType().GetInterface("ICloneable", true);

                            if (ICloneType != null)
                            {
                                //If it does support the ICloneable interface, 
                                //we use it to set the clone of
                                //the object in the list.
                                ICloneable clone = (ICloneable)obj;

                                list[j] = clone.Clone();
                            }

                            //NOTE: If the item in the list is not 
                            //support the ICloneable interface then in the 
                            //cloned list this item will be the same 
                            //item as in the original list
                            //(as long as this type is a reference type).

                            j++;
                        }
                    }
                    else if (IDicType != null)
                    {
                        //Getting the dictionary interface.
                        IDictionary dic = (IDictionary)fields[i].GetValue(newObject);

                        j = 0;

                        foreach (DictionaryEntry de in IEnum)
                        {
                            //Checking to see if the item 
                            //support the ICloneable interface.
                            ICloneType = de.Value.GetType().GetInterface("ICloneable", true);

                            if (ICloneType != null)
                            {
                                ICloneable clone = (ICloneable)de.Value;

                                dic[de.Key] = clone.Clone();
                            }
                            j++;
                        }
                    }
                }
                i++;
            }
            return newObject;
        }

        public object Clone(object source, Type typ)
        {
            object newObject = Activator.CreateInstance(typ);

            //We get the array of fields for the new type instance.
            FieldInfo[] fields = newObject.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (FieldInfo fi in source.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                fi.SetValue(newObject, fi.GetValue(fi.Name));
            }

            return newObject;
        }


        public  object CloneObj(object src, object trgt)
        {
            if (src == null || trgt == null) return trgt;

            PropertyInfo[] src_ps = src.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] trgt_ps = trgt.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo srcp in src_ps)
            {
                PropertyInfo trg = trgt_ps.Where(x => x.Name == srcp.Name).First();

                if (!trg.CanWrite) continue;
                trg.SetValue(trgt, srcp.GetValue(src, null), null);
            }

            return trgt;
        }
    }
}
