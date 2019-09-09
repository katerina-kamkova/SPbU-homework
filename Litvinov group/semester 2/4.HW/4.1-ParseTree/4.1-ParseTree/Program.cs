using System.IO;
using static System.Console;

namespace ParseTree
{
    public class Program
    {
        /// <summary>
        /// Get the expression from the file
        /// </summary>
        /// <param name="filePass">The pass to the file</param>
        /// <returns>The expression</returns>
        public static string Input(string path)
        {
            using (var stream = new StreamReader(path))
            {
                return stream.ReadLine();
            }
        }

        static void Main(string[] args)
        {
            var tree = new Tree();

            WriteLine("Parse tree");
            WriteLine();
            Write("Enter the path: ");
            string path = ReadLine();

            string[] input = Input(path).Split();
            if (input != null)
            {
                var correctInput = true;
                try
                {
                    tree.FillTree(input);
                }
                catch (WrongInputException e)
                {
                    WriteLine(e.Message);
                    correctInput = false;
                }

                if (correctInput)
                {
                    tree.PrintTree();
                    WriteLine();
                    WriteLine($"The answer is: {tree.CalculateAnswer()}");
                }
            }
        }
    }
}