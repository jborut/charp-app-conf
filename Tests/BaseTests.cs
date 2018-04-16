using Microsoft.VisualStudio.TestTools.UnitTesting;
using JBorut.AppConf;

namespace Tests
{
    [TestClass]
    public class BaseTests
    {
        [TestMethod]
        public void TestConfigCtr()
        {
            var config = new Configuration<ConfigTestModelBasic>();

            Assert.AreEqual(config.GetConfiguration().BoolValue, true);
            Assert.AreEqual(config.GetConfiguration().StringValue, "some value");
            Assert.AreEqual(config.GetConfiguration().EnumValue, EnumType.Enum1);
        }

        [TestMethod]
        public void TestNestedConfig()
        {
            var config = new Configuration<ConfigTestModelNested>();

            Assert.AreEqual(config.GetConfiguration().ParentStringKey, "parent key value");
            Assert.AreEqual(config.GetConfiguration().ChildValue.SubKeyStringValue, "sub key value");
        }

        [TestMethod]
        public void TestApplyMethod()
        {
            var config = new Configuration<ConfigTestModelApply1>();

            config.ApplySource(new ConfigTestModelApply2());

            Assert.AreEqual(config.GetConfiguration().ValueInt, 10);
            Assert.AreEqual(config.GetConfiguration().ValueString, "new value");

            Assert.IsNull(config.GetConfiguration().GetType().GetProperty("AdditionalKey"));
        }
    }
}
