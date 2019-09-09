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
            Assert.AreEqual("333", list.ReturnString(2));
        }

        [TestMethod]
        public void Add3ElementsAndReturnThirdStringShallReturn222()
        {
            list.Add("111", 1);
            list.Add("222", 2);
            list.Add("333", 2);
            Assert.AreEqual("222", list.ReturnString(3));
        }

        [TestMethod]
        public void Add3ElementsAndReturnNumberOfString111ShallReturn1()
        {
            list.Add("111", 1);
            list.Add("222", 2);
            list.Add("333", 2);
            Assert.AreEqual(1, list.ReturnNumber("111"));
        }

        [TestMethod]
        public void Add3ElementsDeleteTheFirstFindNewFirstShallReturn333()
        {
            list.Add("111", 1);
            list.Add("222", 2);
            list.Add("333", 2);
            list.DeleteElement("111");
            Assert.AreEqual("333", list.ReturnString(1));
        }

        [TestMethod]
        public void TryToFindNonexistentElementShallReturnEmptyStringAnd0()
        {
            list.Add("111", 1);
            list.Add("222", 2);
            list.Add("333", 2);
            Assert.AreEqual("", list.ReturnString(0));
            Assert.AreEqual(0, list.ReturnNumber("444"));
        }

        [TestMethod]
        public void EnterWrongDataInAddString()
        {
            list.Add("111", 1);
            list.Add("222", 2);
            list.Add("333", 2);
            list.Add("444", -5);
            list.Add("555", 10);
        }

        [TestMethod]
        public void EnterWrongDataInReturnString()
        {
            list.ReturnString(1);
            list.Add("111", 1);
            list.ReturnString(0);
            list.ReturnString(3);
            list.ReturnString(-4);
        }

        [TestMethod]
        public void EnterWrongDataInDeleteString()
        {
            list.Add("111", 1);
            list.Add("222", 1);
            list.DeleteElement("3");
            list.DeleteElement("0");
            list.DeleteElement("");
            list.DeleteElement("-1");
            list.DeleteElement("aaa");
            list.DeleteElement("1.2");
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
            list.DeleteElement("111");
            Assert.AreEqual(1, list.GetSize());
            list.DeleteElement("222");
            Assert.AreEqual(0, list.GetSize());
        }
    }
}
