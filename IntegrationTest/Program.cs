using System;
using System.Collections.Generic;
using JBorut.AppConf;

namespace IntegrationTest
{
    class AppConfig
    {
        private string appName = "ConfTest";
        private float appVersion = 0.1f;

        private LangConfig language = new LangConfig();
        private AuthorConfig author = new AuthorConfig();

        public string AppName { get => appName; set => appName = value; }
        public float AppVersion { get => appVersion; set => appVersion = value; }
        public LangConfig Language { get => language; set => language = value; }
        public AuthorConfig Author { get => author; set => author = value; }
    }

    class LangConfig
    {
        private string description = "Test app suite";

        private List<string> possibleWords = new List<string>()
        {
            "yes",
            "no"
        };

        private Dictionary<string, int> possibleValues = new Dictionary<string, int>()
        {
            { "Key1", 1 },
            { "Key2", 2 }
        };

        public string Description { get => description; set => description = value; }
        public List<string> PossibleWords { get => possibleWords; set => possibleWords = value; }
        public Dictionary<string, int> PossibleValues { get => possibleValues; set => possibleValues = value; }
    }

    class AuthorConfig
    {
        private string name = "Borut Jegrisnik";
        private string github = "jborut";
        private string email = "borut@medianova.hr";

        public string Name { get => name; set => name = value; }
        public string Github { get => github; set => github = value; }
        public string Email { get => email; set => email = value; }
    }

    class OverrideConfig
    {
        private float appVersion = 0.5f;
        private OverrideAuthorConfig author = new OverrideAuthorConfig();

        public float AppVersion { get => appVersion; set => appVersion = value; }
        public OverrideAuthorConfig Author { get => author; set => author = value; }
    }

    class OverrideAuthorConfig
    {
        private string name = "Borut Jegrisnik - master";

        public string Name { get => name; set => name = value; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var config = new Configuration<AppConfig>();

            config.ApplySource(new OverrideConfig());

            Console.WriteLine(config.GetConfiguration().AppVersion);

            Console.ReadLine();
        }
    }
}
