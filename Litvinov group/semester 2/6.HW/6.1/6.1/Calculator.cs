using System;
using System.Collections.Generic;
using System.Linq;

namespace _6._1
{
    /// <summary>
    /// Class, that calculates the expression
    /// </summary>
    public class Calculator : ICalculator
    {
        private Stack<double> numbers = new Stack<double>();
        private Stack<string> operations = new Stack<string>();

        /// <summary>
        /// Count small expression with 2 numbers and 1 operation
        /// </summary>
        private void CountSmallPart()
        {
            var second = numbers.Pop();
            var first = numbers.Pop();
            switch (operations.Pop())
            {
                case ("+"):
                    numbers.Push(first + second);
                    return;
                case ("-"):
                    numbers.Push(first - second);
                    return;
                case ("*"):
                    numbers.Push(first * second);
                    return;
                case ("/"):
                    numbers.Push(first / second);
                    return;
            }
        }

        /// <summary>
        /// Actions in case the operation has the first priority
        /// </summary>
        /// <param name="element">The operation</param>
        private void FirstPriority(string element)
        {
            if (operations.Count() != 0 && (operations.Peek() == "*" || operations.Peek() == "/"))
            {
                CountSmallPart();
            }
            operations.Push(element);
        }

        /// <summary>
        /// Actions in case the operation has the second priority
        /// </summary>
        /// <param name="element">The operation</param>
        private void SecondPriority(string element)
        {
            while (operations.Count() != 0 && operations.Peek() != "(")
            {
                CountSmallPart();
            }
            operations.Push(element);
        }

        /// <summary>
        /// Calculate the expression
        /// In case the expression is wrong throw the exception
        /// </summary>
        /// <param name="input">The expression</param>
        /// <returns>The answer</returns>
        public double Calculate(List<string> input)
        {
            ExceptionCheck(input);

            foreach (var element in input)
            {
                if (double.TryParse(Convert.ToString(element), out var number))
                {
                    numbers.Push(number);
                }
                else
                {
                    switch (element)
                    {
                        case ("("):
                            operations.Push(element);
                            break;
                        case (")"):
                            while (operations.Peek() != "(")
                            {
                                CountSmallPart();
                            }
                            operations.Pop();
                            break;
                        case ("+"):
                            SecondPriority(element);
                            break;
                        case ("-"):
                            SecondPriority(element);
                            break;
                        case ("*"):
                            FirstPriority(element);
                            break;
                        case ("/"):
                            FirstPriority(element);
                            break;
                        default:
                            throw new WrongExpressionException("Wrong symbol");
                    }
                }
            }

            while (operations.Count() != 0)
            {
                CountSmallPart();
            }

            return numbers.Pop();
        }

        /// <summary>
        /// Check given expression whether the order of its elements is right 
        /// </summary>
        /// <param name="input">Expression</param>
        private void ExceptionCheck(List<string> input)
        {
            var check = 0;
            var counter = 0;
            var openBracket = 0;
            var closeBracket = 0;
            foreach (var element in input)
            {
                if (element == "(")
                {
                    check = (check + 1) % 2;
                    openBracket++;
                    if (openBracket < closeBracket)
                    {
                        throw new WrongExpressionException("Wrong amount of brackets");
                    }
                }
                else if (element == ")")
                {
                    check = (check + 1) % 2;
                    closeBracket++;
                    if (openBracket < closeBracket)
                    {
                        throw new WrongExpressionException("Wrong amount of brackets");
                    }
                }
                else if ((double.TryParse(Convert.ToString(element), out var number) && counter % 2 != check)
                      || (!double.TryParse(Convert.ToString(element), out number) && counter % 2 == check))
                {
                    throw new WrongExpressionException("Wrong order of elements");
                }
                counter++;
            }

            if (openBracket != closeBracket)
            {
                throw new WrongExpressionException("Wrong amount of brackets");
            }

            if (!double.TryParse(input[input.Count - 1], out var newNumber) && input[input.Count - 1] != "(" && input[input.Count - 1] != ")")
            {
                Console.WriteLine(input[input.Count - 1]);
                throw new WrongExpressionException("Wrong last symbol");
            }
        }
    }
}
