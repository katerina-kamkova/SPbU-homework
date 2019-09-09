using System;

namespace ConsoleApp
{
    /// <summary>
    /// Exception that is thrown when the user goes the wrong way
    /// </summary>
    public class WrongWayException : Exception
    {
        public WrongWayException(string message)
            : base(message)
        { }
    }
}
