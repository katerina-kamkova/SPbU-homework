using System;

namespace BinaryTree
{
    /// <summary>
    /// The exception that is thrown only if the tree is ReadOnly
    /// </summary>
    public class ReadOnlyException : Exception
    {
        public ReadOnlyException(string message = "You can only read this tree")
            : base(message)
        { }
    }
}