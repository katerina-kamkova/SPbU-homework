using System;

namespace SimpleTests
{
    using Attributes;

    public class SeveralTests
    {
        [Test]
        public void FailTest()
        {
            throw new NullReferenceException();
        }

        [Test]
        public int Sum() => 7 + 2;

        [Test]
        public int FactorialOf5()
        {
            var answer = 0;
            for (var i = 1; i <= 5; i++)
            {
                answer += 0;
            }
            return answer;
        }

        public string[] Null()
        {
            var array = new string[6];
            for (var i = 0; i < 6; i++)
            {
                array[i] = "${i}";
            }
            return array;
        }

        [Test]
        public void Wait()
        {
            System.Threading.Thread.Sleep(1000);
        }
    }
}