using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MapFilterFold
{
    /// <summary>
    /// Class of tests for List functions Map, Filter and Folder
    /// </summary>
    [TestClass]
    public class MapFilterFolderTests
    {
        /// <summary>
        /// Fields, which are used in following tests
        /// </summary>
        private int current;
        private int length;
        private List<int> list;
        private ListFunctions functions;

        /// <summary>
        /// Initialize needed variables
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            length = 5;
            list = new List<int>() { 1, 2, 3, 4, 5 };
            functions = new ListFunctions();
        }

        /// <summary>
        /// Check Map, shall return number * 2
        /// </summary>
        [TestMethod]
        public void CheckMap()
        {
            list = functions.Map(list, current => current * 2);
            var expected = new List<int>() { 2, 4, 6, 8, 10 };
            for (int i = 0; i < length; ++i)
            {
                Assert.AreEqual(expected[i], list[i]);
            }
        }

        /// <summary>
        /// Check Map, shall return number - 1
        /// </summary>
        [TestMethod]
        public void CheckMap2()
        {
            list = functions.Map(list, current => current - 1);
            var expected = new List<int>() { 0, 1, 2, 3, 4 };
            for (int i = 0; i < length; ++i)
            {
                Assert.AreEqual(expected[i], list[i]);
            }
        }

        /// <summary>
        /// Check Filter, shall return odd numbers
        /// </summary>
        [TestMethod]
        public void CheckFilter()
        {
            length = 3;
            list = functions.Filter(list, current => current % 2 == 1);
            var expected = new List<int>() { 1, 3, 5 };
            for (int i = 0; i < length; ++i)
            {
                Assert.AreEqual(expected[i], list[i]);
            }
        }

        /// <summary>
        /// Check Filter, shall return numbers > 3
        /// </summary>
        [TestMethod]
        public void CheckFilter2()
        {
            length = 2;
            list = functions.Filter(list, current => current > 3);
            var expected = new List<int>() { 4, 5 };
            for (int i = 0; i < length; ++i)
            {
                Assert.AreEqual(expected[i], list[i]);
            }
        }

        /// <summary>
        /// Check Filter, shall return numbers > -1
        /// </summary>
        [TestMethod]
        public void CheckFilter3()
        {
            length = 5;
            list = functions.Filter(list, current => current > -1);
            var expected = new List<int>() { 1, 2, 3, 4, 5 };
            for (int i = 0; i < length; ++i)
            {
                Assert.AreEqual(expected[i], list[i]);
            }
        }

        /// <summary>
        /// Check Fold, shall return 360
        /// </summary>
        [TestMethod]
        public void CheckFoldShallReturn360()
        {
            current = 3;
            var result = functions.Fold(list, current, (element, current) => current * element);
            Assert.AreEqual(result, 360);
        }

        /// <summary>
        /// Check Fold, shall return 17
        /// </summary>
        [TestMethod]
        public void CheckFoldShallReturn17()
        {
            current = 2;
            var result = functions.Fold(list, current, (element, current) => current + element);
            Assert.AreEqual(result, 17);
        }

        /// <summary>
        /// Check Fold, expected DivideByZeroException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void CheckFoldExpectedDivideByZeroException()
        {
            current = 3;
            var result = functions.Fold(list, current, (element, current) => current / element);
        }
    }
}