namespace WithBeforeAfter
{
    using Attributes;
    using System.IO;

    public class Mixed
    {
        public static int x;
        public static int y;

        [BeforeClass]
        public static void Init()
        {
            x = 11;
            y = 13;
        }

        [Test]
        public void AddOneY()
        {
            y++;
        }

        [Before]
        [After]
        [Test]
        public void AddOneX()
        {
            x++;
        }

        [AfterClass]
        public static void Check()
        {
            using (FileStream fstream = new FileStream($@"{Path.GetTempPath()}\Mixed.txt", FileMode.OpenOrCreate))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes($"{x} {y}");
                fstream.Write(array, 0, array.Length);
            }
        }
    }
}
