namespace WithBeforeAfter
{
    using Attributes;
    using System.IO;

    /// <summary>
    /// Check that if BeforeFunc falls, following funcs don`t go
    /// </summary>
    public class BeforeFail
    {
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
        public static void BeforeClassFunc() { }

        [Before]
        public void BeforeFunc1() { }

        [Before]
        public void BeforeFunc2()
        {
            throw new System.Exception();
        }

        [Before]
        public void BeforeFunc3() { }

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
            using (FileStream fstream = new FileStream($@"{Path.GetTempPath()}\BeforeFail.txt", FileMode.OpenOrCreate))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(text);
                fstream.Write(array, 0, array.Length);
            }
        }
    }
}
