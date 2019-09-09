namespace WithBeforeAfter
{
    using Attributes;
    using System.IO;

    public class TestFail
    {
        public static int x;
        public static int y;

        [Test]
        public void TestFunc1()
        {
            y += 1;
            throw new System.Exception();
        }

        [Test]
        public void TestFunc2()
        {
            y += 2;
        }

        [BeforeClass]
        public static void BeforeClassFunc()
        {
            x += 1;
        }

        [Before]
        public void BeforeFunc()
        {
            x += 2;
        }

        [After]
        public void AfterFunc()
        {
            x += 2;
        }

        [AfterClass]
        public static void AfterClassFunc()
        {
            x += 3;
            Report($"{x} {y}");
        }

        private static void Report(string text)
        {
            using (FileStream fstream = new FileStream($@"{Path.GetTempPath()}\TestFail.txt", FileMode.OpenOrCreate))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(text);
                fstream.Write(array, 0, array.Length);
            }
        }
    }
}
