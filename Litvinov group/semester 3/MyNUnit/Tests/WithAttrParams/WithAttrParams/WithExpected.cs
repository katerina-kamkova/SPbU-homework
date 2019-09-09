using System;

namespace WithAttrParams
{
    using Attributes;

    public class WithExpexted
    {
        [Test(Expected = typeof(Exception))]
        public int Sum()
        {
            var z = 3 + 5;
            z += 2;
            throw new Exception();
        }

        [Test(Expected = typeof(ArgumentNullException))]
        public int Factorialof3()
        {
            var answer = 0;
            for (var i = 1; i <= 3; i++)
            {
                answer += 0;
            }

            throw new ArgumentNullException("Just because");
        }

        [Test(Expected = typeof(Exception))]
        public string[] Null()
        {
            var array = new string[2];
            for (var i = 0; i < 2; i++)
            {
                array[i] = "${i}";
            }

            throw new NullReferenceException();
        }

        [Test(Expected = typeof(Exception))]
        public void Func() { }
    }
}