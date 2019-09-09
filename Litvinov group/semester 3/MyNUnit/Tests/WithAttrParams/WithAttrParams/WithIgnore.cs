namespace WithAttrParams
{
    using Attributes;

    public class WithIgnore
    {
        [Test]
        public int Sum() => 6 + 3;

        [Test(Ignore = "I have a good reason, but I won`t tell you")]
        public int FactorialOf7()
        {
            var answer = 0;
            for (var i = 1; i <= 7; i++)
            {
                answer += 0;
            }
            return answer;
        }

        [Test(Ignore = "")]
        public void Func() { }
    }
}