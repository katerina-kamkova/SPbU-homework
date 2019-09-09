using System;

namespace SimpleTests
{
    using Attributes;

    public class FailedTest
    {
        [Test]
        public void Fail()
        {
            throw new NullReferenceException();
        }
    }
}
