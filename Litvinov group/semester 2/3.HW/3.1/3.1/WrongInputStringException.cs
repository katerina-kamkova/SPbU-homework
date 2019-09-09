using System;

namespace StackCalculator
{
    /// <summary>
    /// Exception whic is thrown when the input string is wrong
    /// </summary>
    public class WrongInputStringException : Exception
    {
        public WrongInputStringException(string message)
            : base(message)
        { }
    }
}
