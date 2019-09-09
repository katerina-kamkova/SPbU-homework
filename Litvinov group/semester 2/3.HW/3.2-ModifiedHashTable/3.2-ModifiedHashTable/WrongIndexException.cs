using System;

namespace HashTable
{
    /// <summary>
    /// The exception that is thrown when user refers to the element by the wrong index
    /// </summary>
    public class WrongIndexException : Exception
    {
        public WrongIndexException(string message = "Wrong index")
            : base(message)
        { }
    }
}