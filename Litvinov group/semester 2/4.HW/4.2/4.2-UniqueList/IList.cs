namespace UniqueList
{
    /// <summary>
    /// Interface for List
    /// </summary>
    public interface IList
    {
        /// <summary>
        /// Add value to the list on certain position
        /// </summary>
        /// <param name="value">Value to be added</param>
        /// <param name="position">The position of the added value</param>
        void Add(string value, int position);

        /// <summary>
        /// Check whether the list is empty
        /// </summary>
        /// <returns>True if the list empty, else false</returns>
        bool IsEmpty();

        /// <summary>
        /// Check whether the value is in the list
        /// </summary>
        /// <param name="value">Verifiable value</param>
        /// <returns>True if the value is in the list, else false</returns>
        bool CheckPresence(string value);

        /// <summary>
        /// Get the string by its position
        /// </summary>
        /// <param name="position">Position of the wanted element</param>
        /// <returns>Wanted value</returns>
        string GetValueByPosition(int position);

        /// <summary>
        /// Get the position of the value
        /// </summary>
        /// <param name="value">Value, which position we want to get</param>
        /// <returns>Wanted position</returns>
        int GetPositionByValue(string value);

        /// <summary>
        /// Delete value from the list
        /// </summary>
        /// <param name="str">Value to be deleted</param>
        void DeleteElement(string str);

        /// <summary>
        /// Get the length of the list
        /// </summary>
        /// <returns>Length of the list</returns>
        int GetSize();

        /// <summary>
        /// Print the list
        /// </summary>
        void PrintList();
    }
}