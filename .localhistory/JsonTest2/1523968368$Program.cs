using System;
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
    class AuthorConfig
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Github { get; set; }

        public AuthorConfig()
        {
            Name = "Borut";
            Email = "borut@medianova.hr";
            Github = "jborut";
        }
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
        [DataMember]
        public AuthorConfig Author { get; set; }

        public AppConfig()
        {
            AppName = "Test App";
            AppVersion = new Version(1, 5, 2);
            RunType = RunType.Debug;
            Author = new AuthorConfig();
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
            stream.Position = 0;

            StreamReader reader = new StreamReader(stream);
            string serializedConfig = reader.ReadToEnd();

            Console.WriteLine(serializedConfig);

            Console.ReadLine();
        }
    }
}
