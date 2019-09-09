using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HashTable
{
    [TestClass]
    public class HashTableTest
    {
        private HashTable table;

        [TestInitialize]
        public void Init()
        {
            table = new HashTable();
        }

        [TestMethod]
        public void AddElementsAndCheckTheirPresenceShallReturnTrue()
        {
            table.AddElement("111");
            table.AddElement("111");
            table.AddElement("222");
            Assert.IsTrue(table.Check("111"));
            Assert.IsTrue(table.Check("222"));
        }

        [TestMethod]
        public void CheckNonexistentElements()
        {
            Assert.IsFalse(table.Check("111"));
            table.AddElement("111");
            Assert.IsFalse(table.Check("222"));
            Assert.IsFalse(table.Check(""));
        }

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

        [TestMethod]
        public void TryToDeleteNonexistentElements()
        {
            table.Delete("111");
            table.Delete("");
            table.AddElement("111");
            table.Delete("111");
            table.Delete("111");
        }
    }
}
