using System;

namespace UniqueList
{
    /// <summary>
    /// The exception that is thrown when user enters the wrong position
    /// </summary>
    public class WrongPositionException : Exception
    {
        public WrongPositionException(string message = "The position is wrong")
            : base(message)
        { }
    }
}
