using static System.Console;

namespace ParseTree
{
    /// <summary>
    /// Class for "-"
    /// </summary>
    class Subtract : Operation
    {
        /// <summary>
        /// Print the operation with brackets and start Print for children
        /// </summary>
        public override void PrintChar()
        {
            Write("( - ");
            Left.PrintChar();
            Right.PrintChar();
            Write(") ");
        }

        /// <summary>
        /// Print the answer to the part of the exersise below this node
        /// </summary>
        /// <returns>Wanted answer</returns>
        public override int Calculate() => Left.Calculate() - Right.Calculate();
    }
}
