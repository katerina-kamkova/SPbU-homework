using System;

namespace ConsoleApp
{
    /// <summary>
    /// Exception that is thrown when user enters wrong coords
    /// </summary>
    public class WrongCoordsException : Exception
    {
        public WrongCoordsException(string message)
            : base(message)
        { }
    }
}
