using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lazy
{
    [TestClass]
    public class SeveralThreadsTests
    {
        [TestMethod]
        public void СheckThreadsAreWorking()
        {
            var lazy = LazyFactory.CreateMultiThreadLazy(() => DateTime.Now);

            const int length = 10000;
            var threads = new Thread[length];
            var results = new DateTime[length];

            for (var i = 0; i < length; i++)
            {
                var local = i;
                threads[i] = new Thread(() => results[local] = lazy.Get());

            }

            foreach (var element in threads)
            {
                element.Start();
            }

            foreach (var element in threads)
            {
                element.Join();
            }

            for (var i = 1; i < length; i++)
            {
                Assert.AreNotEqual(default(DateTime), results[i]);
            }
        }  

        [TestMethod]
        public void СheckWithTheRace()
        {
            var lazy = LazyFactory.CreateMultiThreadLazy(() => DateTime.Now);

            const int length = 10000;
            var threads = new Thread[length];
            var results = new DateTime[length];

            for (var i = 0; i < length; i++)
            {
                var local = i;
                threads[i] = new Thread(() => results[local] = lazy.Get());
            }

            foreach (var element in threads)
            {
                element.Start();
            }

            foreach (var element in threads)
            {
                element.Join();
            }

            for (var i = 1; i < length; i++)
            {
                Assert.AreEqual(results[0], results[i]);
            }
        }
    }
}