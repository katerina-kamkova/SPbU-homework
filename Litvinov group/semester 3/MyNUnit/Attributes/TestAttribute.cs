using System;

namespace Attributes
{
    /// <summary>
    /// Attribute for tests
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class TestAttribute : Attribute
    {
        /// <summary>
        /// Is called when user expects the exception in the test,
        /// gets/sets the exception type
        /// </summary>
        public Type Expected { get; set; }

        /// <summary>
        /// Is called when the test needs to be ignored,
        /// gets/sets the message - reason of cancelation
        /// </summary>
        public string Ignore { get; set; }
    }
}