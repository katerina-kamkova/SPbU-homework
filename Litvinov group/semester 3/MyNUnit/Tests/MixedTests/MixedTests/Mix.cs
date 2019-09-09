using System;

namespace MixedTests
{
    using Attributes;
    using System.IO;

    public class Mix
    {
        public static int x;
        public static int a;
        public static int b;
        public static int c;
        public static int d;

        [BeforeClass]
        public static void AddOneX()
        {
            x = 1;
        }

        [Before]
        public void AddTwoX()
        {
            x += 2;
        }

        [Test]
        public void Test1()
        {
            x *= 5;
        }

        [Test]
        public void Test2()
        {
            a = 1;
            throw new Exception();
        }

        [Test(Expected = typeof(Exception))]
        public void Test3()
        {
            x *= 10;
            throw new Exception();
        }

        [Test(Expected = typeof(Exception))]
        public void Test4()
        {
            b = 1;
            throw new NullReferenceException();
        }

        [Test(Ignore = "Why not?")]
        public void Test5()
        {
            c = 1;
        }

        [Test(Expected = typeof(Exception), Ignore = "Just do it")]
        public void Test6()
        {
            d = 1;
        }

        [After]
        public void DivideOn5()
        {
            x /= 5;
        }
    }
}
