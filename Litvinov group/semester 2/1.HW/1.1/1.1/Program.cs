using System;

namespace Factorial
{
    class Program
    {
        private static int Factorial(int n)
        {
            if (n <= 1)
            {
                return 1;
            }
            return n * Factorial(n - 1);
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Enter the number: ");
            int number = int.Parse(Console.ReadLine());

            if (number >= 0)
            {
                Console.WriteLine("The factorial of the number: ");
                Console.WriteLine(Factorial(number));
            }
            else
            {
                Console.WriteLine("The wrong number");
            }

            Console.ReadKey();
        }
    }
}