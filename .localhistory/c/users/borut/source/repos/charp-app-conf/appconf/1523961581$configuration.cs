using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JBorut.AppConf
{
    public class Configuration<T> where T : new()
    {
        private T config;

        public Configuration()
        {
            config = new T();
        }

        public void ApplySource(object source)
        {
            if (config == null)
            {
                throw new Exception("Default configuration object is not defined.");
            }

            if (source == null)
            {
                throw new Exception("Source object is null.");
            }

            config = (T)RecurseReplaceValues(config, source);
        }

        private object RecurseReplaceValues(object baseObj, object sourceObj)
        {
            foreach (PropertyInfo sourceObjProp in sourceObj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!sourceObjProp.CanRead)
                {
                    continue;
                }

                foreach (PropertyInfo baseObjProp in baseObj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (baseObjProp.Name == sourceObjProp.Name)
                    {
                        if (!baseObjProp.CanWrite)
                        {
                            break;
                        }

                        if (baseObjProp.GetSetMethod(true) != null && baseObjProp.GetSetMethod(true).IsPrivate)
                        {
                            break;
                        }

                        if ((baseObjProp.GetSetMethod().Attributes & MethodAttributes.Static) != 0)
                        {
                            break;
                        }

                        if (!baseObjProp.PropertyType.IsAssignableFrom(sourceObjProp.PropertyType))
                        {
                            break;
                        }

                        if (IsSimpleType(baseObjProp.PropertyType))
                        {
                            baseObjProp.SetValue(baseObj, sourceObjProp.GetValue(sourceObj));
                        }
                        else
                        {
                            baseObjProp.SetValue(baseObj, RecurseReplaceValues(baseObjProp.GetValue(baseObj), sourceObjProp.GetValue(sourceObj)));
                        }

                        break;
                    }
                }
            }

            return baseObj;
        }

        /// <summary>
        /// based on https://stackoverflow.com/questions/863881/how-do-i-tell-if-a-type-is-a-simple-type-i-e-holds-a-single-value
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool IsSimpleType(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            if (typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // nullable type, check if the nested type is simple.
                return IsSimpleType(typeInfo.GetGenericArguments()[0]);
            }
            return typeInfo.IsPrimitive
              || typeInfo.IsEnum
              || type.Equals(typeof(string))
              || type.Equals(typeof(decimal));
        }

        public T GetConfiguration()
        {
            return config;
        }
    }
}
