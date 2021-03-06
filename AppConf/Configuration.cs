﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        public void ApplyJson(ref object rootNode, string source)
        {
            JObject sourceObj = JObject.Parse(source);
            //rootN
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
