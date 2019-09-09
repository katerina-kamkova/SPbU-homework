using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test2
{
    /// <summary>
    /// Test Compression
    /// </summary>
    [TestClass]
    public class Test2Tests
    {
        /// <summary>
        /// Try Compress on empty array, shall not fall
        /// </summary>
        [TestMethod]
        public void TryCompressOnEmptyArray()
        {
            var array = new byte[0];
            Compression.Compress(ref array);
        }

        /// <summary>
        /// Try Uncompress on empty array, shall not fall
        /// </summary>
        [TestMethod]
        public void TryUncompressOnEmptyArray()
        {
            var array = new byte[0];
            Compression.Uncompress(ref array);
        }

        /// <summary>
        /// Try funcs Compress and Uncopress on sorted array
        /// </summary>
        [TestMethod]
        public void TryOnSortedArray()
        {
            var array = new byte[6] { 2, 3, 3, 3, 6, 7 };
            var sameArray = new byte[6] { 2, 3, 3, 3, 6, 7 };
            var compressedArray = new byte[8] { 1, 2, 3, 3, 1, 6, 1, 7 };

            Compression.Compress(ref array);
            for (var i = 0; i < 8; i++)
            {
                Assert.AreEqual(array[i], compressedArray[i]);
            }

            Compression.Uncompress(ref array);
            for (var i = 0; i < 6; i++)
            {
                Assert.AreEqual(array[i], sameArray[i]);
            }
        }

        /// <summary>
        /// Try on not sorted array with many repeating elements
        /// </summary>
        [TestMethod]
        public void TryOnArrayWithManyRepeatingElements()
        {
            var array = new byte[17] { 3, 3, 4, 4, 4, 4, 4, 7, 7, 7, 5, 5, 6, 2, 1, 1, 1 };
            var compressedArray = new byte[14] { 2, 3, 5, 4, 3, 7, 2, 5, 1, 6, 1, 2, 3, 1 };
            var uncompressedArray = new byte[17] { 3, 3, 4, 4, 4, 4, 4, 7, 7, 7, 5, 5, 6, 2, 1, 1, 1 };

            Compression.Compress(ref array);
            for (var i = 0; i < 14; i++)
            {
                Assert.AreEqual(array[i], compressedArray[i]);
            }

            Compression.Uncompress(ref array);
            for (var i = 0; i < 17; i++)
            {
                Assert.AreEqual(array[i], uncompressedArray[i]);
            }
        }

        /// <summary>
        /// Try on array that contains mane copies of one number
        /// </summary>
        [TestMethod]
        public void TryOnArrayWichContainsSameNumbers()
        {
            var array = new byte[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var compressedArray = new byte[2] { 10, 0 };
            var uncompressedArray = new byte[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            Compression.Compress(ref array);
            for (var i = 0; i < 2; i++)
            {
                Assert.AreEqual(array[i], compressedArray[i]);
            }

            Compression.Uncompress(ref array);
            for (var i = 0; i < 10; i++)
            {
                Assert.AreEqual(array[i], uncompressedArray[i]);
            }
        }

        /// <summary>
        /// Try compress and uncompress on not sorted array
        /// </summary>
        [TestMethod]
        public void TryOnNotSortedArray()
        {
            var array = new byte[6] { 3, 3, 7, 3, 6, 2 };
            var compressedArray = new byte[10] { 2, 3, 1, 7, 1, 3, 1, 6, 1, 2 };
            var uncompressedArray = new byte[6] { 3, 3, 7, 3, 6, 2 };

            Compression.Compress(ref array);
            for (var i = 0; i < 10; i++)
            {
                Assert.AreEqual(array[i], compressedArray[i]);
            }

            Compression.Uncompress(ref array);
            for (var i = 0; i < 6; i++)
            {
                Assert.AreEqual(array[i], uncompressedArray[i]);
            }
        }

        /// <summary>
        /// Try on not sorted array with many repeating elements
        /// </summary>
        [TestMethod]
        public void TryOnArrayWithManyRepeatingElementsUnsorted()
        {
            var array = new byte[17] { 3, 3, 4, 4, 7, 5, 6, 2, 1, 1, 1, 4, 5, 7, 4, 4, 7 };
            var compressedArray = new byte[24] { 2, 3, 2, 4, 1, 7, 1, 5, 1, 6, 1, 2, 3, 1, 1, 4, 1, 5, 1, 7, 2, 4, 1, 7 };
            var uncompressedArray = new byte[17] { 3, 3, 4, 4, 7, 5, 6, 2, 1, 1, 1, 4, 5, 7, 4, 4, 7 };

            Compression.Compress(ref array);
            for (var i = 0; i < 24; i++)
            {
                Assert.AreEqual(array[i], compressedArray[i]);
            }

            Compression.Uncompress(ref array);
            for (var i = 0; i < 17; i++)
            {
                Assert.AreEqual(array[i], uncompressedArray[i]);
            }
        }
    }
}
