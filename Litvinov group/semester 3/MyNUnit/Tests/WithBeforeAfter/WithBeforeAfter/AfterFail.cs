namespace WithBeforeAfter
{
    using Attributes;
    using System.IO;

    public class AfterFail
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

        [After]
        public void AfterFunc2()
        {
            throw new System.Exception();
        }

        [After]
        public void AfterFunc3() { }

        [AfterClass]
        public static void AfterClassFunc()
        {
            Report($"AfterClassFunc");
        }

        private static void Report(string text)
        {
            using (FileStream fstream = new FileStream($@"{Path.GetTempPath()}\AfterFail.txt", FileMode.OpenOrCreate))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(text);
                fstream.Write(array, 0, array.Length);
            }
        }
    }
}
