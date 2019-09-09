using static System.Console;

namespace ParseTree
{
    /// <summary>
    /// The node which contains number
    /// </summary>
    public class Number : INode
    {
        private int number;

        public Number(int number)
        {
            this.number = number;
        }

        /// <summary>
        /// Print the number
        /// </summary>
        public void PrintChar()
        {
            Write($"{number} ");
        }

        /// <summary>
        /// Get the number
        /// </summary>
        /// <returns>Wanted number</returns>
        public int Calculate() => number;
    }
}
