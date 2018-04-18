using System;

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

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
