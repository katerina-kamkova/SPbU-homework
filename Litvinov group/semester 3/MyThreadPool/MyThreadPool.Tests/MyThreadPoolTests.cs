using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyThreadPool
{
    [TestClass]
    public class MyThreadPoolTests
    {
        [TestMethod]
        [ExpectedException(typeof(ThreadPoolClosedException))]
        public void TryContinueWithAfterShutdownWhenTaskIsCounted()
        {
            var threadAmount = 10;
            var myThreadPool = new MyThreadPool(threadAmount);

            var task = myThreadPool.AddTask(() =>
            {
                var answer = DateTime.Now;
                Thread.Sleep(1000);
                return answer;
            });

            myThreadPool.Shutdown();
            var result = task.Result;
            var newTask = task.ContinueWith(i => DateTime.Now.AddMilliseconds(10000));
            var newResult = newTask.Result;
        }

        [TestMethod]
        public void CheckItCanSolveTasks()
        {
            var threadAmount = 10;
            var myThreadPool = new MyThreadPool(threadAmount);

            var taskAmount = 20;
            var tasks = new IMyTask<int>[taskAmount];

            for (var i = 0; i < taskAmount; i++)
            {
                tasks[i] = myThreadPool.AddTask(new Func<int>(() => 2*21));
                Assert.AreEqual(42, tasks[i].Result);
            }

            myThreadPool.Shutdown();
        }
        
        [TestMethod]
        public void CheckContinueWithOnce()
        {
            var threadAmount = 10;
            var myThreadPool = new MyThreadPool(threadAmount);

            var task = myThreadPool.AddTask(() => 2);
            var newTask = task.ContinueWith(j => Convert.ToString(j));

            Assert.AreEqual(2, task.Result);
            Assert.AreEqual("2", newTask.Result);

            myThreadPool.Shutdown();
        }

        [TestMethod]
        public void LongCalculations()
        {
            var threadAmount = 10;
            var myThreadPool = new MyThreadPool(threadAmount);

            var task = myThreadPool.AddTask(() =>
            {
                var answer = DateTime.Now;
                Thread.Sleep(1000);
                return answer;
            });
            var newTask = task.ContinueWith(i => DateTime.Now.AddMilliseconds(1000));
            var result = task.Result;
            result = newTask.Result;

            myThreadPool.Shutdown();
        }

        [TestMethod]
        public void ExceptionMustNotStopProgram()
        {
            var threadAmount = 10;
            var myThreadPool = new MyThreadPool(threadAmount);

            var i = 8;
            var task = myThreadPool.AddTask(() => 1 / (i - 8));

            myThreadPool.Shutdown();
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void CalculationsWithException()
        {
            var threadAmount = 10;
            var myThreadPool = new MyThreadPool(threadAmount);

            var i = 8;
            var task = myThreadPool.AddTask(() => 1 / (i - 8));
            var result = task.Result;
        }

        [TestMethod]
        public void ExceptionInContinueWithMustNotStopProgram()
        {
            var threadAmount = 10;
            var myThreadPool = new MyThreadPool(threadAmount);

            var task = myThreadPool.AddTask(() => 42);
            var newTask = task.ContinueWith<int>(i => throw new Exception());
            var result = task.Result;

            myThreadPool.Shutdown();
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void CalculationsWithExceptionInConrinueWith()
        {
            var threadAmount = 10;
            var myThreadPool = new MyThreadPool(threadAmount);

            var task = myThreadPool.AddTask(() => 42);
            var newTask = task.ContinueWith<int>(i => throw new Exception());
            var result = task.Result;
            var newResult = newTask.Result;
        }

        [TestMethod]
        public void CheckSeveralContinueWith()
        {
            var threadAmount = 10;
            var myThreadPool = new MyThreadPool(threadAmount);

            var task = myThreadPool.AddTask(() => 2);
            var newTask = task.ContinueWith(j => Convert.ToString(j));
            var newNewTask = task.ContinueWith(j => j * 2);

            Assert.AreEqual(2, task.Result);
            Assert.AreEqual("2", newTask.Result);
            Assert.AreEqual(4, newNewTask.Result);

            myThreadPool.Shutdown();
        }

        [TestMethod]
        [ExpectedException(typeof(ThreadPoolClosedException))]
        public void TryAddTaskToShutThreadPool()
        {
            var threadAmount = 10;
            var myThreadPool = new MyThreadPool(threadAmount);

            myThreadPool.Shutdown();

            var taskAmount = 2000;
            var tasks = new IMyTask<int>[taskAmount];

            for (var i = 0; i < taskAmount; i++)
            {
                tasks[i] = myThreadPool.AddTask(() => 7 * 8);
                Assert.AreEqual(56, tasks[i].Result);
            }
        }
    }
}
