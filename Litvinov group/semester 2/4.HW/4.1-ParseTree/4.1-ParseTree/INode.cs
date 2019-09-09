namespace ParseTree
{
    /// <summary>
    /// Inetrface for nodes of the Parse tree
    /// </summary>
    interface INode
    {
        /// <summary>
        /// Print the value
        /// </summary>
        void PrintChar();

        /// <summary>
        /// Get the number
        /// </summary>
        /// <returns>Wanted number</returns>
        int Calculate();
    }
}
