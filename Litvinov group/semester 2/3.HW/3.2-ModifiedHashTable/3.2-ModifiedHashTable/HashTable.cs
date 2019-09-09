using System;
using static System.Math;

namespace HashTable
{
    /// <summary>
    /// Hash table on list
    /// </summary>
    public class HashTable : IHashTable
    {
        private const int tableLength = 100;
        private List[] table;
        private Func<string, ulong> hashFunc;

        public HashTable(Func<string, ulong> hashFunc) : this()
        {
            this.hashFunc = hashFunc;
        }

        public HashTable()
        {
            table = new List[tableLength];
            for (int i = 0; i < tableLength; ++i)
            {
                table[i] = new List();
            }
        }

        /// <summary>
        /// Get hash code using the function, selected by user
        /// </summary>
        /// <param name="str">String to be added</param>
        /// <returns>Hash code, chosen by user</returns>
        private int HashCode(string str) => hashFunc != null ? (int)(hashFunc(str) % tableLength) : Abs(str.GetHashCode()) % tableLength;

        /// <summary>
        /// Add string to hash table
        /// </summary>
        /// <param name="value">Value to be added</param>
        public void AddElement(string value)
        {
            if (Equals(value, null))
            {
                throw new FormatException("Wrong input string");
            }

            int number = HashCode(value);
            table[number].Add(value, 1);
        }

        /// <summary>
        /// Check whether this string is in the table
        /// </summary>
        /// <param name="value">Checking value</param>
        /// <returns>True if string is n the table, else - false</returns>
        public bool Check(string value)
        {
            if (Equals(value, ""))
            {
                throw new FormatException("Wrong input string");
            }

            int number = HashCode(value);
            return table[number].CheckPresence(value);
        }

        /// <summary>
        /// Delte the string from the hash table
        /// </summary>
        /// <param name="value">Value to be deleted</param>
        public void Delete(string value)
        {
            if (Equals(value, ""))
            {
                throw new FormatException("Wrong input string");
            }

            int number = HashCode(value);
            table[number].DeleteElement(value);
        }
    }
}
