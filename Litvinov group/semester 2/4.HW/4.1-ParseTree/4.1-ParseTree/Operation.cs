namespace ParseTree
{
    /// <summary>
    /// Class for operations
    /// </summary>
    abstract class Operation : INode
    {
        public INode Left { get; set; }
        public INode Right { get; set; }

        /// <summary>
        /// Print the operation
        /// </summary>
        public abstract void PrintChar();

        /// <summary>
        /// Print the answer to the part of the exersise below this node
        /// </summary>
        /// <returns>Wanted answer</returns>
        public abstract int Calculate();
    }
}
