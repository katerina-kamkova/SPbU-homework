namespace WithBeforeAfter
{
    using Attributes;
    using System.IO;

    /// <summary>
    /// Check, that if one of BeforeClass falls, Before and following don`t execute
    /// </summary>
    public class BeforeClassFail
    {
        public static int x;

        [Test]
        public void TestFunc1()
        {
            ReportMistake("TestFunc1");
        }

        [Test]
        public void TestFunc2()
        {
            ReportMistake("TestFunc2");
        }

        [BeforeClass]
        public static void BeforeClassFunc1()
        {
            x = 2;
        }

        [BeforeClass]
        public static void BeforeClassFunc2()
        {
            x += 2;
            throw new System.Exception($"Expected x = 4, real x = {x}");
        }

        [BeforeClass]
        public static void BeforeClassFunc3()
        {
            x += 2;
            ReportMistake("BeforeClassFunc3");
        }

        [Before]
        public void BeforeFunc()
        {
            ReportMistake("BeforeFunc");
        }

        [After]
        public void AfterFunc()
        {
            ReportMistake("AfterFunc");
        }

        [AfterClass]
        public static void AfterClassFunc()
        {
            ReportMistake("AfterClassFunc");
        }

        private static void ReportMistake(string text)
        {
            using (FileStream fstream = new FileStream($@"{Path.GetTempPath()}\BeforeClassFail.txt", FileMode.OpenOrCreate))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(text);
                fstream.Write(array, 0, array.Length);
            }
        }
    }
}