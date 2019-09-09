using System;
using static System.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HashTable
{
    /// <summary>
    /// Tests for hash table
    /// </summary>
    [TestClass]
    public class HashTableTest
    {
        private HashTable table;
        private HashTable tableWithNewFunc;
        private const ulong max = 18446744073709551615;

        [TestInitialize]
        public void Init()
        {
            table = new HashTable();
            tableWithNewFunc = new HashTable(HashCode);
        }

        /// <summary>
        /// Function counting hash code
        /// </summary>
        /// <param name="input">String, which hash code we want to find</param>
        /// <returns>Wanted hash code</returns>
        private static ulong HashCode(string input)
        {
            const int primeNumber = 13;
            ulong answer = 0;
            if (input == "")
            {
                return 0;
            }

            var str = input.Split();
            for (int i = 0; i < str.Length; ++i)
            {
                answer = (answer + ulong.Parse(str[i]) * (ulong)Pow(primeNumber, i)) % max;
            }
            return answer;
        }

        /// <summary>
        /// Add elements and check their presence, shall return true
        /// </summary>
        [TestMethod]
        public void AddElementsAndCheckTheirPresenceShallReturnTrue()
        {
            table.AddElement("111");
            table.AddElement("111");
            table.AddElement("222");
            Assert.IsTrue(table.Check("111"));
            Assert.IsTrue(table.Check("222"));
        }

        /// <summary>
        /// Check nonexistent elements
        /// </summary>
        [TestMethod]
        public void CheckNonexistentElements()
        {
            Assert.IsFalse(table.Check("111"));
            table.AddElement("111");
            Assert.IsFalse(table.Check("222"));
        }

        /// <summary>
        /// Delete elements and check their existence shall return false true false
        /// </summary>
        [TestMethod]
        public void DeleteElementsAndCheckTheirExistenceShallReturnFalseTrueFalse()
        {
            table.AddElement("111");
            table.AddElement("222");
            table.Delete("111");
            Assert.IsFalse(table.Check("111"));
            Assert.IsTrue(table.Check("222"));
            table.Delete("222");
            Assert.IsFalse(table.Check("222"));
        }

        /// <summary>
        /// Try to delete nonexistent elements
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NonexistentElementException))]
        public void TryToDeleteNonexistentElements()
        {
            table.Delete("111");
        }

        /// <summary>
        /// Try to delete nonexistent elements
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NonexistentElementException))]
        public void OneMoreTryToDeleteNonexistentElements()
        {
            table.AddElement("111");
            table.Delete("111");
            table.Delete("111");
        }

        /// <summary>
        /// Try to add empty string
        /// </summary>
        [TestMethod]
        public void AddEmptyString()
        {
            table.AddElement("");
        }

        /// <summary>
        /// Try to check presence of empty string
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void CheckEmptyString()
        {
            table.Check("");
        }

        /// <summary>
        /// Try to delete empty string
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void DeleteEmptyString()
        {
            table.Delete("");
        }

        /// <summary>
        /// Add elements and check their presence, shall return true (New hash function)
        /// </summary>
        [TestMethod]
        public void AddElementsAndCheckTheirPresenceShallReturnTrueNewFunc()
        {
            tableWithNewFunc.AddElement("111");
            tableWithNewFunc.AddElement("111");
            tableWithNewFunc.AddElement("222");
            Assert.IsTrue(tableWithNewFunc.Check("111"));
            Assert.IsTrue(tableWithNewFunc.Check("222"));
        }

        /// <summary>
        /// Check nonexistent elements (New hash function)
        /// </summary>
        [TestMethod]
        public void CheckNonexistentElementsNewFunc()
        {
            Assert.IsFalse(tableWithNewFunc.Check("111"));
            tableWithNewFunc.AddElement("111");
            Assert.IsFalse(tableWithNewFunc.Check("222"));
        }

        /// <summary>
        /// Delete elements and check their existence shall return false true false 
        /// (New hash function)
        /// </summary>
        [TestMethod]
        public void DeleteElementsAndCheckTheirExistenceShallReturnFalseTrueFalseNewFunc()
        {
            tableWithNewFunc.AddElement("111");
            tableWithNewFunc.AddElement("222");
            tableWithNewFunc.Delete("111");
            Assert.IsFalse(tableWithNewFunc.Check("111"));
            Assert.IsTrue(tableWithNewFunc.Check("222"));
            tableWithNewFunc.Delete("222");
            Assert.IsFalse(tableWithNewFunc.Check("222"));
        }

        /// <summary>
        /// Try to delete nonexistent elements (New hash function)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NonexistentElementException))]
        public void TryToDeleteNonexistentElementsNewFunc()
        {
            tableWithNewFunc.Delete("111");
        }

        /// <summary>
        /// Try to delete nonexistent elements (New hash function)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NonexistentElementException))]
        public void OneMoreTryToDeleteNonexistentElementsNewFunc()
        {
            tableWithNewFunc.AddElement("111");
            tableWithNewFunc.Delete("111");
            tableWithNewFunc.Delete("111");
        }

        /// <summary>
        /// Try to add empty string (New hash function)
        /// </summary>
        [TestMethod]
        public void AddEmptyStringNewFunc()
        {
            tableWithNewFunc.AddElement("");
        }

        /// <summary>
        /// Try to check presence of empty string (New hash function)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void CheckEmptyStringNewFunc()
        {
            tableWithNewFunc.Check("");
        }

        /// <summary>
        /// Try to delete empty string (New hash function)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void DeleteEmptyStringNewFunc()
        {
            tableWithNewFunc.Delete("");
        }
    }
}
