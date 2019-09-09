namespace HashTable
{
    /// <summary>
    /// HashTable Interface
    /// </summary>
    public interface IHashTable
    {
        /// <summary>
        /// Add string to hash table
        /// </summary>
        /// <param name="value">Value to be added</param>
        void AddElement(string value);

        /// <summary>
        /// Check whether this string is in the table
        /// </summary>
        /// <param name="value">Checking value</param>
        bool Check(string value);

        /// <summary>
        /// Delte the string from the hash table
        /// </summary>
        /// <param name="value">Value to be deleted</param>
        void Delete(string value);
    }
}
