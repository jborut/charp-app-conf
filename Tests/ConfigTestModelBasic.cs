namespace Tests
{
    public enum EnumType
    {
        Enum1,
        Enum2
    }

    public class ConfigTestModelBasic
    {
        public int IntValue = 5;
        public float FloatValue = 1.3f;
        public double DoubleValue = 6.7d;
        public bool BoolValue = true;
        public string StringValue = "some value";

        public EnumType EnumValue = EnumType.Enum1;
    }
}
