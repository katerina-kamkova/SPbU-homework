using System;

namespace _6._1
{
    /// <summary>
    /// The exception that is thrown when the input expression is wrong
    /// </summary>
    public class WrongExpressionException : Exception
    {
        public WrongExpressionException(string message)
            : base(message)
        { }
    }
}
