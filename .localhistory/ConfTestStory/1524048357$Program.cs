using System;
using System.Reflection;

namespace ConfTestStory
{
    enum RunType
    {
        Debug,
        Release
    }

    class ExecutionConf
    {
        public RunType RunType { get; set; }
        public int NumOfThreads { get; set; }
    }
    
    class AppConfig
    {
        public string AppName { get; set; }
        public Version AppVersion { get; set; }

        public ExecutionConf ExecutionConf { get; set; }

        public AppConfig() // define defaults
        {
            AppName = "Test App";
            AppVersion = new Version(1, 1, 1);
            ExecutionConf = new ExecutionConf()
            {
                RunType = RunType.Debug,
                NumOfThreads = 4
            };
        }
    }

    class Configuration<T> where T : new()
    {
        private T config;
        public Configuration()
        {
            config = new T();
        }

        public void ApplySource(object source)
        {
            foreach (PropertyInfo configObjProp in config.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!IsSimpleType(configObjProp.PropertyType) && configObjProp.PropertyType == source.GetType())
                {

                    break;
                }
            }

                //config = (T)RecurseReplaceValues(config, source);
        }

        public void ApplyArguments(string[] args)
        {

        }

        public T Instance
        {
            get
            {
                return config;
            }
        }

        private dynamic RecurseReplaceValues(dynamic baseObj, dynamic sourceObj)
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
                        if ((baseObjProp.GetSetMethod().Attributes & MethodAttributes.Static) != 0)
                        {
                            break;
                        }

                        if (IsSimpleType(baseObjProp.PropertyType))
                        {
                            baseObjProp.SetValue(baseObj, sourceObjProp.GetValue(sourceObj));
                        }
                        /*
                        else
                        {
                            baseObjProp.SetValue(baseObj, RecurseReplaceValues(baseObjProp.GetValue(baseObj), sourceObjProp.GetValue(sourceObj)));
                        }
                        */
                        break;
                    }
                    else if (!IsSimpleType(baseObjProp.PropertyType) && baseObjProp.PropertyType == sourceObjProp.PropertyType)
                    {
                        baseObjProp.SetValue(baseObj, RecurseReplaceValues(baseObjProp.GetValue(baseObj), sourceObjProp.GetValue(sourceObj)));
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

    }

    class Program
    {
        static void Main(string[] args)
        {
            // coming from file
            ExecutionConf execConf = new ExecutionConf()
            {
                RunType = RunType.Release,
                NumOfThreads = 10
            };

            var arguments = new string[] 
            {
                "--AppName=\"Different name\"",
                "--ExecutionConf.NumOfThreads=3"
            };

            // idea is to use AppConfig default values
            // then map over config from file (execConf)
            // and finally map over arguments
            // and then get a resulting configuration

            var config = new Configuration<AppConfig>();

            config.ApplySource(execConf);
            config.ApplyArguments(arguments);


            Console.WriteLine(config.Instance.AppName);
            Console.WriteLine(config.Instance.AppVersion);
            Console.WriteLine(config.Instance.ExecutionConf.RunType.ToString());
            Console.WriteLine(config.Instance.ExecutionConf.NumOfThreads);

            Console.ReadLine();
        }
    }
}
