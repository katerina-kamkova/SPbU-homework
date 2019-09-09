using System;

namespace UniqueList
{
    /// <summary>
    /// The exception that is thrown when user`s trying to add not unique element
    /// </summary>
    public class NotUniqueElementException : Exception
    {
        public NotUniqueElementException(string message = "This element already is in the list")
            : base(message)
        { }
    }
}
