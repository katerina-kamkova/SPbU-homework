using System;

namespace UniqueList
{
    /// <summary>
    /// The exception which is thrown when user`s trying to delete nonexistent element
    /// </summary>
    public class CannotDeleteNotExistedElementException : Exception
    {
        public CannotDeleteNotExistedElementException(string message = "There`s no such element")
            : base(message)
        { }
    }
}
