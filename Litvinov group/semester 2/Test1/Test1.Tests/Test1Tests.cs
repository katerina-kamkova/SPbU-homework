using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test1
{
    /// <summary>
    /// Tests for priority queue
    /// </summary>
    [TestClass]
    public class Test1Tests
    {
        private IPriorityQueue<int> intQueue;
        private IPriorityQueue<string> stringQueue;

        /// <summary>
        /// Initialisation of queues
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            intQueue = new PriorityQueue<int>();
            stringQueue = new PriorityQueue<string>();
        }

        /// <summary>
        /// Check whether the Queue of int elements is capable to add and delete elements the right way
        /// </summary>
        [TestMethod]
        public void AddSomeElementsToIntQueueThenGetThemAll()
        {
            intQueue.Enqueue(3, -1);
            intQueue.Enqueue(2, 4);
            Assert.AreEqual(3, intQueue.Dequeue());
            Assert.AreEqual(2, intQueue.Dequeue());
        }

        /// <summary>
        /// Check wether the exception will be thrown if user tries to get int element from empty queue
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(EmptyQueueException))]
        public void TryToGetElementFromEmptyIntQueue()
        {
            intQueue.Dequeue();
        }

        /// <summary>
        /// Check whether the Queue of string elements is capable to add and delete elements the right way
        /// </summary>
        [TestMethod]
        public void AddSomeElementsToStringQueueThenGetThemAll()
        {
            stringQueue.Enqueue("aaa", 17);
            stringQueue.Enqueue("bbb", 0);
            Assert.AreEqual("bbb", stringQueue.Dequeue());
            Assert.AreEqual("aaa", stringQueue.Dequeue());
        }

        /// <summary>
        /// Check wether the exception will be thrown if user tries to get string element from empty queue
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(EmptyQueueException))]
        public void TryToGetElementFromEmptyStringQueue()
        {
            stringQueue.Dequeue();
        }
    }
}
