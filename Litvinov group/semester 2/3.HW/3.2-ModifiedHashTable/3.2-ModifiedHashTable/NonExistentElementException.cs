using System;

namespace HashTable
{
    /// <summary>
    /// The exception that is thrown when user refers to the nonexistent element
    /// </summary>
    public class NonexistentElementException : Exception
    {
        public NonexistentElementException(string message = "There`s no such element in the list")
            : base(message)
        { }
    }
}