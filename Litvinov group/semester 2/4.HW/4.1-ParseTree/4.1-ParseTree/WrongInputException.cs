using System;

namespace ParseTree
{
    /// <summary>
    /// Exception that is thrown when user enters wrong expression
    /// </summary>
    public class WrongInputException : Exception
    {
        public WrongInputException(string message)
            : base(message)
        { }
    }
}
