using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _7._1
{
    /// <summary>
    /// Tests for class List
    /// </summary>
    [TestClass]
    public class GenericListTests
    {
        /// <summary>
        /// Check add func, control the size
        /// </summary>
        [TestMethod]
        public void AddSomeElementsCheckSize()
        {
            var list = new List<int>();
            Assert.AreEqual(0, list.Count);
            list.Add(1);
            list.Add(2);
            list.Add(3);
            Assert.AreEqual(3, list.Count);
        }

        /// <summary>
        /// Try to add some elements when IsReadOnly = true
        /// Expected ReadOnlyException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ReadOnlyException))]
        public void TryToAddSomeElementsWhenIsReadOnlyTrue()
        {
            var list = new List<int>();
            list.IsReadOnly = true;
            list.Add(1);
        }

        /// <summary>
        /// Add some elements, then Clean, check size again
        /// then again add some elements and check size
        /// </summary>
        [TestMethod]
        public void AddSomeElementsThenCheckClean()
        {
            var list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Clear();
            Assert.AreEqual(0, list.Count);
            list.Add(1);
            list.Add(2);
            Assert.AreEqual(2, list.Count);
        }

        /// <summary>
        /// Try to Clear not empty List with IsReadOnly = true
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ReadOnlyException))]
        public void AddSomeElementsMakeListReadOnlyTryClear()
        {
            var list = new List<string>();
            list.Add("1");
            list.Add("2");
            list.IsReadOnly = true;
            list.Clear();
        }

        /// <summary>
        /// Check presence of some elements in the list
        /// </summary>
        [TestMethod]
        public void CheckPresenceOfSomeElementsInTheList()
        {
            var list = new List<string>();
            list.Add("11");
            list.Add("2");
            list.Add("11");
            Assert.IsTrue(list.Contains("2"));
            Assert.IsTrue(list.Contains("11"));
            Assert.IsFalse(list.Contains("3"));
            Assert.IsFalse(list.Contains(""));
            list.IsReadOnly = true;
            Assert.IsTrue(list.Contains("2"));
            list.IsReadOnly = false;
            list.Clear();
            Assert.IsFalse(list.Contains("11"));
            Assert.IsFalse(list.Contains(""));
        }

        /// <summary>
        /// Check the func CopyTo from first, middle and last element
        /// with IsReadOnly = true and false
        /// </summary>
        [TestMethod]
        public void CheckCopyTo()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 };

            var array1 = new int[5];
            list.CopyTo(array1, 0);
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(array1[i], i + 1);
            }

            var array2 = new int[7];
            list.CopyTo(array2, 2);
            for (int i = 2; i < 7; i++)
            {
                Assert.AreEqual(array2[i], i - 1);
            }

            list.IsReadOnly = true;
            var array3 = new int[10];
            list.CopyTo(array3, 4);
            for (int i = 4; i < 9; i++)
            {
                Assert.AreEqual(array3[i], i - 3);
            }
        }

        /// <summary>
        /// Try to copy from -1 index
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongIndexException))]
        public void TryToCopyFromMinus1Index()
        {
            var list = new List<int>() { 1, 2 };

            var array1 = new int[2];
            list.CopyTo(array1, -1);
        }

        /// <summary>
        /// Try to copy from the exceeding index
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongIndexException))]
        public void TryToCopyFromExceedingIndex()
        {
            var list = new List<int>() { 1, 2 };

            var array1 = new int[2];
            list.CopyTo(array1, 2);
        }

        /// <summary>
        /// Try to copy to array that is smaller then the list
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void CopyToNotBigEnoughArray()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 };
            var array = new int[6];
            list.CopyTo(array, 2);
        }

        /// <summary>
        /// Check Enumerator
        /// </summary>
        [TestMethod]
        public void CheckEnumerator()
        {
            var list = new List<int>();
            list.Add(1);
            list.Add(2);
            var counter = 0;
            foreach (var element in list)
            {
                ++counter;
                Assert.AreEqual(counter, element);
            }

            list.Clear();
            counter = 0;
            foreach (var element in list)
            {
                counter++;
            }
            Assert.AreEqual(counter, 0);
        }

        /// <summary>
        /// Check IndexOf including situation with two same elements
        /// </summary>
        [TestMethod]
        public void CheckIndexOf()
        {
            var list = new List<int>();
            list.Add(1);
            list.Add(1);
            list.Add(2);
            Assert.AreEqual(0, list.IndexOf(1));
            Assert.AreEqual(2, list.IndexOf(2));
        }

        /// <summary>
        /// Try to define index of nonexistent element
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NonexistentElementException))]
        public void CheckIndexOfNonexistentElement()
        {
            var list = new List<int>();
            list.Add(1);
            var index = list.IndexOf(3);
        }

        /// <summary>
        /// Insert elements in the first. middle and last positions,
        /// check amount each time
        /// </summary>
        [TestMethod]
        public void InsertElementsOnTheFirstMiddleAndLastPositions()
        {
            var list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            list.Insert(0, 0);
            Assert.AreEqual(list.Count, 4);
            Assert.AreEqual(list[0], 0);
            Assert.AreEqual(list[1], 1);

            list.Insert(2, 20);
            Assert.AreEqual(5, list.Count);
            Assert.AreEqual(list[2], 20);
            Assert.AreEqual(list[3], 2);

            list.Insert(5, 5);
            Assert.AreEqual(list.Count, 6);
            Assert.AreEqual(list[5], 5);
            Assert.AreEqual(list[4], 3);
        }

        /// <summary>
        /// Insert element on the -1 position, expected WrongIndexException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongIndexException))]
        public void InsertElementInMinus1PositionExpectedException()
        {
            var list = new List<int>();
            list.Insert(-1, 0);
        }

        /// <summary>
        /// Check remove func
        /// </summary>
        [TestMethod]
        public void CheckRemove()
        {
            var list = new List<int>() { 1, 1, 3, 1, 4, 5 };
            list.Remove(1);
            list.Remove(5);
            list.Remove(3);
            list.Remove(1);
            list.Remove(1);
            Assert.AreEqual(list.Count, 1);
            Assert.IsFalse(list.Contains(3));
        }

        /// <summary>
        /// Try to remove the element from empty list
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(EmptyListException))]
        public void RemoveElementFromEmptyList()
        {
            var list = new List<double>();
            list.Remove(1.5);
        }

        /// <summary>
        /// Try to Remove nonexistent element
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NonexistentElementException))]
        public void RemoveNonexistentElement()
        {
            var list = new List<int>() { 1, 2 };
            list.Remove(1);
            list.Remove(1);
        }

        /// <summary>
        /// Remove elements by index? check their presence and list.Count
        /// </summary>
        [TestMethod]
        public void CheckRemoveAt()
        {
            var list = new List<int>() { 1, 2, 3 };
            list.RemoveAt(0);
            Assert.AreEqual(2, list.Count);
            list.RemoveAt(1);
            Assert.AreEqual(1, list.Count);
            Assert.IsFalse(list.Contains(1));
            Assert.IsTrue(list.Contains(2));
            Assert.IsFalse(list.Contains(3));
        }

        /// <summary>
        /// Try to RemoveAt by -1 index, expected exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongIndexException))]
        public void RemoveAtMinus1Index()
        {
            var list = new List<int>() { 1, 2 };
            list.RemoveAt(-1);
        }

        /// <summary>
        /// Try to RemoveAt by exceeding index
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongIndexException))]
        public void RemoveAtByExceedingIndex()
        {
            var list = new List<int>() { 1, 2 };
            list.RemoveAt(5);
        }

        /// <summary>
        /// Try RemoveAt from empty list
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(EmptyListException))]
        public void RemoveAtFromEmptyList()
        {
            var list = new List<int>();
            list.RemoveAt(0);
        }
    }
}