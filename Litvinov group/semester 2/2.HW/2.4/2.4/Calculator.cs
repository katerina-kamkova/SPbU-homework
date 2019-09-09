using System;
using static System.Console;

namespace StackCalculator
{
    /// <summary>
    /// Class to calculate given expression
    /// </summary>
    public class Calculator : ICalculator
    {
        public Calculator(IStack stack)
        {
            this.stack = stack;
        }

        private static (int second, int first) GetNumbers(IStack stack)
        {
            if (stack.Size() < 2)
            {
                throw new WrongInputStringException("The expressin is wrong");
            }
            return (int.Parse(stack.Pop()), int.Parse(stack.Pop()));
        }

        /// <summary>
        /// Calculate the expression
        /// </summary>
        /// <param name="input">Given expression</param>
        public void Calculate(string[] input)
        {
            for (int i = 0; i < input.Length; ++i)
            {
                if (input[i] == "*")
                {
                    var (second, first) = GetNumbers(stack);
                    stack.Push(Convert.ToString(first * second, 10));
                }
                else if (input[i] == "+")
                {
                    var (second, first) = GetNumbers(stack);
                    stack.Push(Convert.ToString(first + second, 10));
                }
                else if (input[i] == "-")
                {
                    var (second, first) = GetNumbers(stack);
                    stack.Push(Convert.ToString(first - second, 10));
                }
                else if (input[i] == "/")
                {
                    var (second, first) = GetNumbers(stack);
                    stack.Push(Convert.ToString(first / second, 10));
                }
                else if (int.TryParse(input[i], out int number))
                {
                    stack.Push(input[i]);
                }
                else
                {
                    throw new WrongInputStringException("The expressin is wrong");
                }
            }
            answer = int.Parse(stack.Pop());
        }

        /// <summary>
        /// Print the answer
        /// </summary>
        /// <returns>Wanted answer</returns>
        public int PrintAnswer()
        {
            Write($"The answer: {answer}");
            return answer;
        }
        
        private int answer;
        private IStack stack;
    }
}
