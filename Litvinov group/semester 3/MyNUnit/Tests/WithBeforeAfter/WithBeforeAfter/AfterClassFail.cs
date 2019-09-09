namespace WithBeforeAfter
{
    using Attributes;
    using System.IO;

    public class AfterClassFail
    {
        public static int x;
        public static int y;

        [Test]
        public void TestFunc1()
        {
            throw new System.Exception();
        }

        [Test]
        public void TestFunc2() { }

        [BeforeClass]
        public static void BeforeClassFunc() { }

        [Before]
        public void BeforeFunc() { }

        [After]
        public void AfterFunc1() { }

        [AfterClass]
        public static void AfterClassFunc1() { }

        [AfterClass]
        public static void AfterClassFunc2()
        {
            throw new System.Exception();
        }

        [AfterClass]
        public static void AfterClassFunc3()
        {
            Report($"AfterClassFunc");
        }

        private static void Report(string text)
        {
            using (FileStream fstream = new FileStream($@"{Path.GetTempPath()}\AfterClassFail.txt", FileMode.OpenOrCreate))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(text);
                fstream.Write(array, 0, array.Length);
            }
        }
    }
}
