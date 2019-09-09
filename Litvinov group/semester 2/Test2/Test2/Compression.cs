using System.Collections.Generic;

namespace Test2
{
    /// <summary>
    /// Class to compress and uncompress the array
    /// </summary>
    public static class Compression
    {
        /// <summary>
        /// Compress the array
        /// </summary>
        /// <param name="array">Given array</param>
        public static void Compress(ref byte[] array)
        {
            if (array.Length == 0)
            {
                return;
            }

            var list = new List<byte>();

            foreach (var element in array)
            {
                if (list.Count == 0 || element != list[list.Count - 1])
                {
                    list.Add(1);
                    list.Add(element);
                }
                else
                {
                    list[list.Count - 2]++;
                }
            }

            array = new byte[list.Count];
            for (var i = 0; i < list.Count; i++)
            {
                array[i] = list[i];
            }
        }

        /// <summary>
        /// Uncompress array
        /// </summary>
        /// <param name="array">Given array</param>
        public static void Uncompress(ref byte[] array)
        {
            if (array.Length == 0)
            {
                return;
            }

            var newLength = 0;
            for (var i = 0; i < array.Length; i += 2)
            {
                newLength += array[i];
            }

            var newArray = new byte[newLength];
            var counter = 0;
            for (var i = 1; i < array.Length; i += 2)
            {
                for (var j = 0; j < array[i - 1]; j++)
                {
                    newArray[counter] = array[i];
                    counter++;
                }
            }

            array = newArray;
        }
    }
}

