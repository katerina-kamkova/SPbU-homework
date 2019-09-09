using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinaryTree
{
    [TestClass]
    public class BinaryTree
    {
        private BinaryTree<int> tree;
        private BinaryTree<int> anotherTree;
        private BinaryTree<int> bigTree;

        [TestInitialize]
        public void Init()
        {
            tree = new BinaryTree<int>((first, second) => first < second);
            anotherTree = new BinaryTree<int>((first, second) => first < second);
            bigTree = new BinaryTree<int>((first, second) => first < second);
            bigTree.Add(13);
            bigTree.Add(7);
            bigTree.Add(6);
            bigTree.Add(5);
            bigTree.Add(1);
            bigTree.Add(2);
            bigTree.Add(4);
            bigTree.Add(3);
            bigTree.Add(11);
            bigTree.Add(9);
            bigTree.Add(8);
            bigTree.Add(10);
            bigTree.Add(12);
            bigTree.Add(14);
            bigTree.Add(15);
            bigTree.Add(18);
            bigTree.Add(17);
            bigTree.Add(16);
        }

        /// <summary>
        /// Check Add and Enumerator
        /// </summary>
        [TestMethod]
        public void AddSomeElementsThen()
        {
            tree.Add(1);
            tree.Add(2);
            tree.Add(3);

            var count = 1;
            foreach (var element in tree)
            {
                Assert.AreEqual(count, element);
                count++;
            }
        }

        /// <summary>
        /// Add 18 elements and check Enumerator
        /// </summary>
        [TestMethod]
        public void Add18ElementsCheckTheOrder()
        {
            tree.Add(13);
            tree.Add(7);
            tree.Add(6);
            tree.Add(5);
            tree.Add(1);
            tree.Add(2);
            tree.Add(4);
            tree.Add(3);
            tree.Add(11);
            tree.Add(9);
            tree.Add(8);
            tree.Add(10);
            tree.Add(12);
            tree.Add(14);
            tree.Add(15);
            tree.Add(18);
            tree.Add(17);
            tree.Add(16);

            var count = 1;
            foreach (var element in tree)
            {
                Assert.AreEqual(count, element);
                count++;
            }
        }

        /// <summary>
        /// Try to add element in tree that is read only
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ReadOnlyException))]
        public void TryToAddSomethingInIsReadOnlyTree()
        {
            tree.IsReadOnly = true;
            tree.Add(1);
        }

        /// <summary>
        /// Check function SetEquals
        /// </summary>
        [TestMethod]
        public void CheckSetEquals()
        {
            Assert.IsTrue(tree.SetEquals(anotherTree));
            tree.Add(3);
            Assert.IsFalse(tree.SetEquals(anotherTree));
            anotherTree.Add(4);
            anotherTree.Add(5);
            anotherTree.Add(3);
            Assert.IsFalse(tree.SetEquals(anotherTree));
            tree.Add(5);
            tree.Add(4);
            Assert.IsTrue(tree.SetEquals(anotherTree));
        }

        /// <summary>
        /// Add some elements than clean and check whether the tree is empty
        /// </summary>
        [TestMethod]
        public void CheckClear()
        {
            tree.Add(3);
            tree.Add(1);
            tree.Add(2);
            tree.Clear();
            Assert.IsTrue(tree.SetEquals(anotherTree));
            tree.Clear();
            Assert.IsTrue(tree.SetEquals(anotherTree));
        }

        /// <summary>
        /// Check Contains in different situations
        /// </summary>
        [TestMethod]
        public void CheckContains()
        {
            Assert.IsFalse(tree.Contains(1));
            tree.Add(3);
            tree.Add(1);
            tree.Add(2);
            Assert.IsFalse(tree.Contains(0));
            Assert.IsTrue(tree.Contains(1));
            Assert.IsTrue(tree.Contains(2));
            Assert.IsTrue(tree.Contains(3));
            Assert.IsTrue(bigTree.Contains(13));
            Assert.IsTrue(bigTree.Contains(9));
            Assert.IsTrue(bigTree.Contains(1));
            Assert.IsTrue(bigTree.Contains(16));
            Assert.IsFalse(bigTree.Contains(33));
            Assert.IsFalse(bigTree.Contains(0));
            Assert.IsFalse(bigTree.Contains(-3));
        }

        /// <summary>
        /// Check CopyTo in different situations
        /// </summary>
        [TestMethod]
        public void CheckCopyTo()
        {
            var array = new int[20];
            bigTree.CopyTo(array, 1);
            for (var i = 1; i < 19; i++)
            {
                Assert.AreEqual(i, array[i]);
            }
        }

        /// <summary>
        /// Copy data from the wrong index, expected exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongIndexException))]
        public void TryCopyToWithWrongIndex()
        {
            var array = new int[4];
            bigTree.CopyTo(array, 20);
        }


        /// <summary>
        /// Try to copy to array that is smaller then the tree
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.IndexOutOfRangeException))]
        public void CopyToNotBigEnoughArray()
        {
            var array = new int[18];
            bigTree.CopyTo(array, 2);
        }

        /// <summary>
        /// Remove some nodes, check whether everything`s allright
        /// </summary>
        [TestMethod]
        public void RemoveSomeNodes()
        {
            Assert.IsTrue(bigTree.Remove(13));
            Assert.IsTrue(bigTree.Remove(16));
            Assert.IsTrue(bigTree.Remove(11));
            Assert.IsTrue(bigTree.Remove(5));
            var counter = 1;
            foreach (var element in bigTree)
            {
                if (counter == 5 || counter == 11 || counter == 13 || counter == 16)
                {
                    counter++;
                }
                Assert.AreEqual(counter, element);
                counter++;
            }
        }

        /// <summary>
        /// Try to remove some nodes from is read only tree
        /// </summary>
        [TestMethod]
        public void TryToRemoveSomeNodesFromIsReadOnlyTree()
        {
            bigTree.IsReadOnly = true;
            Assert.IsFalse(bigTree.Remove(3));
        }

        /// <summary>
        /// Try to Remove node from empty tree
        /// </summary>
        [TestMethod]
        public void TryToRemoveNodeFromEmptyTree()
        {
            Assert.IsFalse(tree.Remove(1));
        }

        /// <summary>
        /// Try to Remove Nonexistent element
        /// </summary>
        [TestMethod]
        public void TryToRemoveNonexistentNode()
        {
            Assert.IsFalse(bigTree.Remove(50));
        }

        /// <summary>
        /// Check SymmetricExceptWith in different situations
        /// </summary>
        [TestMethod]
        public void CheckSymmetricExceptWith()
        {
            tree.SymmetricExceptWith(anotherTree);
            tree.Add(3);
            tree.Add(4);
            tree.Add(1);
            tree.Add(2);
            tree.SymmetricExceptWith(anotherTree);
            var counter = 1;
            foreach (var element in tree)
            {
                Assert.AreEqual(counter, element);
                counter++;
            }

            anotherTree.Add(4);
            anotherTree.Add(1);
            tree.SymmetricExceptWith(anotherTree);
            Assert.AreEqual(2, tree.Count);
            Assert.IsFalse(tree.Contains(1));
            Assert.IsFalse(tree.Contains(4));
        }

        /// <summary>
        /// Check UnionWith in different situations
        /// </summary>
        [TestMethod]
        public void CheckUnionWith()
        {
            tree.UnionWith(anotherTree);
            tree.Add(3);
            tree.Add(4);
            tree.Add(1);
            tree.Add(2);
            tree.UnionWith(anotherTree);
            var counter = 1;
            foreach (var element in tree)
            {
                Assert.AreEqual(counter, element);
                counter++;
            }

            anotherTree.Add(6);
            anotherTree.Add(5);
            tree.UnionWith(anotherTree);
            Assert.AreEqual(6, tree.Count);
            Assert.IsTrue(tree.Contains(5));
            Assert.IsTrue(tree.Contains(6));
            counter = 1;
            foreach (var element in tree)
            {
                Assert.AreEqual(counter, element);
                counter++;
            }
        }

        /// <summary>
        /// Check IntersectWith in different situations
        /// </summary>
        [TestMethod]
        public void CheckIntersectWith()
        {
            tree.IntersectWith(anotherTree);
            tree.Add(3);
            tree.Add(4);
            anotherTree.Add(5);
            anotherTree.Add(4);
            tree.IntersectWith(anotherTree);
            Assert.IsFalse(tree.Contains(3));
        }

        /// <summary>
        /// Check function which defines belonging
        /// </summary>
        [TestMethod]
        public void CheckFunctionsWhichDefinesBelonging()
        {
            Assert.IsFalse(tree.IsProperSubsetOf(anotherTree));
            Assert.IsFalse(tree.IsProperSupersetOf(anotherTree));
            Assert.IsTrue(tree.IsSubsetOf(anotherTree));
            Assert.IsTrue(tree.IsSupersetOf(anotherTree));

            tree.Add(3);
            tree.Add(4);
            tree.Add(1);
            anotherTree.Add(3);
            Assert.IsFalse(tree.IsProperSubsetOf(anotherTree));
            Assert.IsTrue(tree.IsProperSupersetOf(anotherTree));
            Assert.IsFalse(tree.IsSubsetOf(anotherTree));
            Assert.IsTrue(tree.IsSupersetOf(anotherTree));

            anotherTree.Add(4);
            anotherTree.Add(1);
            anotherTree.Add(2);
            Assert.IsTrue(tree.IsProperSubsetOf(anotherTree));
            Assert.IsFalse(tree.IsProperSupersetOf(anotherTree));
            Assert.IsTrue(tree.IsSubsetOf(anotherTree));
            Assert.IsFalse(tree.IsSupersetOf(anotherTree));

            tree.Add(2);
            Assert.IsFalse(tree.IsProperSubsetOf(anotherTree));
            Assert.IsFalse(tree.IsProperSupersetOf(anotherTree));
            Assert.IsTrue(tree.IsSubsetOf(anotherTree));
            Assert.IsTrue(tree.IsSupersetOf(anotherTree));
        }
    }
}
