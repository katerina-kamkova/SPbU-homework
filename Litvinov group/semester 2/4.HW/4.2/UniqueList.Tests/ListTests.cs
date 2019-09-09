using static System.Console;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniqueList
{
    /// <summary>
    /// Tests for class List
    /// </summary>
    [TestClass]
    public class ListTest
    {
        private List list;

        /// <summary>
        /// Initiolize the list
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            list = new List();
        }

        /// <summary>
        /// Add element and check whether the list is empty
        /// </summary>
        [TestMethod]
        public void AddElementAndCheckIsListEmpty()
        {
            Assert.IsTrue(list.IsEmpty());
            list.Add("111", 1);
            Assert.IsFalse(list.IsEmpty());
        }

        /// <summary>
        /// Add 3 elements, then check their presence and conformity of the positions
        /// </summary>
        [TestMethod]
        public void Add3ElementsCheckPresenceAndReturnTheSecondShallReturnTrue333()
        {
            list.Add("111", 1);
            list.Add("222", 2);
            list.Add("333", 2);
            Assert.IsTrue(list.CheckPresence("333"));
            Assert.AreEqual("333", list.GetValueByPosition(2));
        }

        /// <summary>
        /// Add 3 elements and check the third element to be 222
        /// </summary>
        [TestMethod]
        public void Add3ElementsAndReturnThirdStringShallReturn222()
        {
            list.Add("111", 1);
            list.Add("222", 2);
            list.Add("333", 2);
            Assert.AreEqual("222", list.GetValueByPosition(3));
        }

        /// <summary>
        /// Add 3 elements and get position by value "111", shall return 1
        /// </summary>
        [TestMethod]
        public void Add3ElementsAndGetPositionByValueOfString111ShallReturn1()
        {
            list.Add("111", 1);
            list.Add("222", 2);
            list.Add("333", 2);
            Assert.AreEqual(1, list.GetPositionByValue("111"));
        }

        /// <summary>
        /// Add 3 elements, delete the first element, then check that new first element = 333
        /// </summary>
        [TestMethod]
        public void Add3ElementsDeleteTheFirstFindNewFirstShallReturn333()
        {
            list.Add("111", 1);
            list.Add("222", 2);
            list.Add("333", 2);
            list.DeleteElement("111");
            Assert.AreEqual("333", list.GetValueByPosition(1));
        }

        /// <summary>
        /// Try to find nonexistant element, shall return 0
        /// </summary>
        [TestMethod]
        public void TryToFindNonexistentElementShallReturn0()
        {
            Assert.AreEqual(0, list.GetPositionByValue("444"));
            list.Add("111", 1);
            list.Add("222", 2);
            list.Add("333", 2);
            Assert.AreEqual(0, list.GetPositionByValue("444"));
        }

        /// <summary>
        /// Try to find element from wrong position
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongPositionException))]
        public void TryToFindElementFromWrongPosition()
        {
            list.GetValueByPosition(0);
            list.GetValueByPosition(1);
            list.Add("111", 1);
            list.Add("222", 2);
            list.Add("333", 2);
            list.GetValueByPosition(7);
        }

        /// <summary>
        /// Enter wrong position in GetValueByPosition
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongPositionException))]
        public void EnterWrongDataInGetValueByPosition()
        {
            list.GetValueByPosition(1);
        }

        /// <summary>
        /// Enter wrong position in GetValueByPosition
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongPositionException))]
        public void EnterWrongDataInGetValueByPosition2()
        {
            list.Add("111", 1);
            list.GetValueByPosition(0);
        }

        /// <summary>
        /// Enter wrong Value in DeleteElement
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CannotDeleteNotExistedElementException))]
        public void EnterWrongDataInDeleteString()
        {
            list.Add("111", 1);
            list.Add("222", 1);
            list.DeleteElement("-1");
        }

        /// <summary>
        /// Add and delete several elements and return the size of the list
        /// </summary>
        [TestMethod]
        public void AddAndDeleteSeveralELementsAndReturnTheSizeOfTheList()
        {
            Assert.AreEqual(0, list.GetSize());
            list.Add("111", 1);
            Assert.AreEqual(1, list.GetSize());
            list.Add("222", 1);
            Assert.AreEqual(2, list.GetSize());
            list.DeleteElement("111");
            Assert.AreEqual(1, list.GetSize());
            list.DeleteElement("222");
            Assert.AreEqual(0, list.GetSize());
        }

        /// <summary>
        /// Try to delete nonexistent element, then get size
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CannotDeleteNotExistedElementException))]
        public void TryToDeleteNonexistentElementThenGetSize()
        {
            list.Add("111", 1);
            list.Add("222", 1);
            list.DeleteElement("111");
            list.DeleteElement("111");
            Assert.AreEqual(1, list.GetSize());
        }

        /// <summary>
        /// Print empty list, shall not fall
        /// </summary>
        [TestMethod]
        public void PrintEmptyListShallNotFall()
        {
            list.PrintList();
        }
    }
}