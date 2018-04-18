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

    }
}
