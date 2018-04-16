using System;
using System.Collections.Generic;

namespace AppConf
{
    /*
     * config is an object with properties that can be
     * int, float, string, list<int|floa|string>, dictionary<string, int|float|string>, object with same type of properties
    */

    public class Class1
    {
        public string key1 = "val1";
        public string key2 = "val2";

        public List<string> list1 = new List<string>()
        {
            "item1",
            "item2"
        };

        public Class2 subconf = new Class2();

    }

    public class Class2
    {
        public string key1 = "valX";
        public string key2 = "valY";

        Dictionary<string, int> vals = new Dictionary<string, int>()
        {
            { "prop1", 3 },
            { "prop2", 2 }
        };
    }
}
