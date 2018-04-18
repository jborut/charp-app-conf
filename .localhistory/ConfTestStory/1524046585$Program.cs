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

    class Configuration<T> where T : new()
    {
        private T config;
        public Configuration()
        {
            config = new T();
        }

        public void ApplySource(dynamic source)
        {

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
        }
    }
}
