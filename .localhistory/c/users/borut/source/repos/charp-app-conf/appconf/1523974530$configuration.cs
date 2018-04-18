using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace JBorut.AppConf
{
    public class Configuration<T> where T : new()
    {
        private T config;

        public Configuration()
        {
            config = new T();
        }

        public Configuration(T defaults)
        {
            config = defaults;
        }

        public void ApplySource(object source)
        {

        }

        public T GetConfiguration()
        {
            return config;
        }

        public string DeserializeConfiguration()
        {
            return JsonConvert.SerializeObject(config);
        }
    }
}
