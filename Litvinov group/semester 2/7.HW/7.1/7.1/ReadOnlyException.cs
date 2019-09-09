using System;

namespace _7._1
{
    /// <summary>
    /// The exception that is thrown only if the list is ReadOnly
    /// </summary>
    public class ReadOnlyException : Exception
    {
        public ReadOnlyException(string message = "You can only read this list")
            : base(message)
        { }
    }
}