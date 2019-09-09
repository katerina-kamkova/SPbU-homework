using System;
namespace WithAttrParams
{
    using Attributes;

    public class MixedParams
    {
        [Test(Expected = typeof(Exception), Ignore = "Too complex to run")]
        public int Sum(int x, int y)
        {
            var z = x + y;
            throw new Exception();
        }
    }
}
