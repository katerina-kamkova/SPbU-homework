namespace StackCalculator
{
    /// <summary>
    /// Class to calculate given expression
    /// </summary>
    public interface ICalculator
    {
        /// <summary>
        /// Calculate the expression
        /// </summary>
        /// <param name="input">Given expression</param>
        void Calculate(string[] input);

        /// <summary>
        /// Print the answer
        /// </summary>
        /// <returns>Wanted answer</returns>
        int PrintAnswer();
    }
}
