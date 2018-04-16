using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    public class ConfigTestModelNestedChild
    {
        public string SubKeyStringValue = "sub key value";
        public int SubKeyIntegerValue = 5;
    }

    public class ConfigTestModelNested
    {
        public string ParentStringKey = "parent key value";
        public ConfigTestModelNestedChild ChildValue = new ConfigTestModelNestedChild();
    }
}
