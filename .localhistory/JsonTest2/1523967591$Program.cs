using System;

System.Runtime.Serialization.Json

namespace JsonTest2
{
    enum RunType
    {
        Debug,
        Production
    }

    class AppConfig
    {

        public string AppName { get; set; }
        public Version AppVersion { get; set; }
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
            
        }
    }
}
