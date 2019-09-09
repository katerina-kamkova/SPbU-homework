using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HashTable.Test
{
    [TestClass]
    public class ListTest
    {
        private List list;

        [TestInitialize]
        public void Init()
        {
            list = new List();
        }

        [TestMethod]
        public void AddElementAndCheckIsListEmpty()
        {
            Assert.IsTrue(list.IsEmpty());
            list.Add("111", 1);
            Assert.IsFalse(list.IsEmpty());
        }

        [TestMethod]
        public void Add3ElementsCheckPresenceAndReturnTheSecondShallReturnTrue333()
        {
            list.Add("111", 1);
            list.Add("222", 2);
            list.Add("333", 2);
            Assert.IsTrue(list.CheckPresence("333"));
            Assert.AreEqual("333", list.GetValueByPosition(2));
        }

        [TestMethod]
        public void Add3ElementsAndReturnThirdStringShallReturn222()
        {
            list.Add("111", 1);
            list.Add("222", 2);
            list.Add("333", 2);
            Assert.AreEqual("222", list.GetValueByPosition(3));
        }

        [TestMethod]
        public void Add3ElementsAndGetPositionByValueOfString111ShallReturn1()
        {
            list.Add("111", 1);
            list.Add("222", 2);
            list.Add("333", 2);
            Assert.AreEqual(1, list.GetPositionByValue("111"));
        }

        [TestMethod]
        public void Add3ElementsDeleteTheFirstFindNewFirstShallReturn333()
        {
            list.Add("111", 1);
            list.Add("222", 2);
            list.Add("333", 2);
            list.DeleteElement("111");
            Assert.AreEqual("333", list.GetValueByPosition(1));
        }

        [TestMethod]
        [ExpectedException(typeof(WrongIndexException))]
        public void TryToFindNonexistentElementShallReturnEmptyString()
        {
            list.Add("111", 1);
            list.Add("222", 2);
            list.Add("333", 2);
            Assert.AreEqual("", list.GetValueByPosition(0));
        }

        [TestMethod]
        public void TryToFindNonexistentElementShallReturn0()
        {
            list.Add("111", 1);
            list.Add("222", 2);
            list.Add("333", 2);
            Assert.AreEqual(0, list.GetPositionByValue("444"));
        }

        [TestMethod]
        [ExpectedException(typeof(WrongIndexException))]
        public void EnterWrongDataInAddStringExpectedException()
        {
            list.Add("111", 5);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongIndexException))]
        public void EnterWrongDataInGetValueByPosition()
        {
            list.GetValueByPosition(1);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongIndexException))]
        public void OneMoreEnterWrongDataInGetValueByPosition()
        {
            list.Add("111", 1);
            list.GetValueByPosition(0);
            list.GetValueByPosition(3);
        }

        [TestMethod]
        [ExpectedException(typeof(NonexistentElementException))]
        public void EnterWrongDataInDeleteString()
        {
            list.Add("111", 1);
            list.Add("222", 1);
            list.DeleteElement("3");
        }

        [TestMethod]
        [ExpectedException(typeof(NonexistentElementException))]
        public void OneMoreEnterWrongDataInDeleteString()
        {
            list.Add("111", 1);
            list.Add("222", 1);
            list.DeleteElement("");
        }

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

        [TestMethod]
        [ExpectedException(typeof(NonexistentElementException))]
        public void AddAndDeleteSeveralELementsAndReturnTheSizeOfTheListExpectedException()
        {
            Assert.AreEqual(0, list.GetSize());
            list.Add("111", 1);
            Assert.AreEqual(1, list.GetSize());
            list.Add("222", 1);
            Assert.AreEqual(2, list.GetSize());
            list.DeleteElement("111");
            Assert.AreEqual(1, list.GetSize());
            list.DeleteElement("111");
            Assert.AreEqual(1, list.GetSize());
            list.DeleteElement("222");
            Assert.AreEqual(0, list.GetSize());
        }
    }
}
