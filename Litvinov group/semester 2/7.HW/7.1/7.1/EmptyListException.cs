using System;

namespace _7._1
{
    /// <summary>
    /// Exception that is thrown when the user trys to delete or check smth in the empty list
    /// </summary>
    public class EmptyListException : Exception
    {
        public EmptyListException(string message = "The list is empty")
            : base(message)
        { }
    }
}