using System;

namespace BinaryTree
{
    /// <summary>
    /// Exception that is thrown when the user trys to delete or check smth in the empty tree
    /// </summary>
    public class EmptyTreeException : Exception
    {
        public EmptyTreeException(string message = "The tree is empty")
            : base(message)
        { }
    }
}