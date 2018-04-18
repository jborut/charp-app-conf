﻿using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace JsonTest2
{
    enum RunType
    {
        Debug,
        Production
    }

    [DataContract]
    class AppConfig
    {
        [DataMember]
        public string AppName { get; set; }
        [DataMember]
        public Version AppVersion { get; set; }
        [DataMember]
        public RunType RunType { get; set; }

        public AppConfig()
        {
            AppName = "Test App";
            AppVersion = new Version(1, 5, 2);
            RunType = RunType.Debug;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            AppConfig config = new AppConfig();

            var jsonSerializer = new DataContractJsonSerializer(typeof(AppConfig));

            var stream = new MemoryStream();

            jsonSerializer.WriteObject(stream, config);

            StreamReader reader = new StreamReader(stream);
            string serializedConfig = reader.ReadToEnd();

            Console.WriteLine(serializedConfig);

            Console.ReadLine();
        }
    }
}
