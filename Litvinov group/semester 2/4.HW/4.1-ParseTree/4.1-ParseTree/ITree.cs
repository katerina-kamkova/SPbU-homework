namespace ParseTree
{
    /// <summary>
    /// Interface for tree
    /// </summary>
    interface ITree
    {
        /// <summary>
        /// Fill the tree with expression
        /// </summary>
        /// <param name="input">Expression</param>
        void FillTree(string[] input);

        /// <summary>
        /// Print the expression by tree
        /// </summary>
        void PrintTree();

        /// <summary>
        /// Calculate the answer
        /// </summary>
        /// <returns>Wanted answer</returns>
        int CalculateAnswer();
    }
}
