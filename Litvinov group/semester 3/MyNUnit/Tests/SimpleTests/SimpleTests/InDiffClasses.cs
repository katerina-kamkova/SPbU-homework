namespace SimpleTests
{
    using Attributes;

    public class InDiffClasses
    {
        int x = 5;
        int y = 1;

        [Test]
        public int Sum() => x + y;
    }

    public class AnotherClass
    {
        [Test]
        public string[] Null()
        {
            var array = new string[5];
            for (var i = 0; i < 5; i++)
            {
                array[i] = "${i}";
            }
            return array;
        }
    }
}
