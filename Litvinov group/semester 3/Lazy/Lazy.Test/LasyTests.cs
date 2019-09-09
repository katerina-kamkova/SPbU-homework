using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lazy
{
    [TestClass]
    public class LasyTests
    {
        [TestMethod]
        public void DoesItWorkAtAll_Lazy()
        {
            var lazy = LazyFactory.CreateLazy(() => 48);
            Assert.AreEqual(48, lazy.Get());
        }

        [TestMethod]
        public void DoesItWorkAtAll_MultiThreadLazy()
        {
            var lazy = LazyFactory.CreateMultiThreadLazy(() => 48);
            Assert.AreEqual(48, lazy.Get());
        }

        [TestMethod]
        public void CheckWhetherSupplierCalledOnce_Lazy()
        {
            var lazy = LazyFactory.CreateLazy(() => DateTime.Now);
            var result = lazy.Get();
            Assert.AreEqual(result, lazy.Get());
            Assert.AreEqual(result, lazy.Get());
        }

        [TestMethod]
        public void CheckWhetherSupplierCalledOnce_MultiThreadLazy()
        {
            var lazy = LazyFactory.CreateMultiThreadLazy(() => DateTime.Now);
            var result = lazy.Get();
            Assert.AreEqual(result, lazy.Get());
            Assert.AreEqual(result, lazy.Get());
        }

        [TestMethod]
        public void IsItAllRightWithNull_Lazy()
        {
            var lazy = LazyFactory.CreateLazy<string>(() => null);
            var result = lazy.Get();
            Assert.AreEqual(result, null);
            Assert.AreEqual(result, lazy.Get());
        }

        [TestMethod]
        public void IsItAllRightWithNull_MulriThreadLazy()
        {
            var lazy = LazyFactory.CreateMultiThreadLazy<string>(() => null);
            var result = lazy.Get();
            Assert.AreEqual(result, null);
            Assert.AreEqual(result, lazy.Get());
        }
    }
}
