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
        

    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
