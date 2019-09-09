namespace WithBeforeAfter
{
    using Attributes;
    using System.IO;

    public class DifferentSingleAttr
    {
        public static int x;
        public static int y;

        [Test]
        public int Sum()
        {
            x += y;
            return x;
        }

        [BeforeClass]
        public static void FillX()
        {
            x = 2;
        }

        [Before]
        public void FillY()
        {
            y = 1;
        }

        [After]
        public void Equal()
        {
            y = x;
        }

        [AfterClass]
        public static void FixResults()
        {
            using (FileStream fstream = new FileStream($@"{Path.GetTempPath()}\DifferentSingleAttr.txt", FileMode.OpenOrCreate))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes($"{x} {y}");
                fstream.Write(array, 0, array.Length);
            }
        }
    }
}
