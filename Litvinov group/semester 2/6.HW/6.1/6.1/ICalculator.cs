using System.Collections.Generic;

namespace _6._1
{
    /// <summary>
    /// Interface describing Calculator
    /// </summary>
    public interface ICalculator
    {
        /// <summary>
        /// Calculate the expression
        /// </summary>
        /// <param name="input">List of strings, input</param>
        /// <returns>Answer of the expression</returns>
        double Calculate(List<string> input);
    }
}
