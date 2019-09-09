using System;

namespace Fibonacci
{
    class Program
    {
        private static int Fibonacci(int n)
        {
            int first = 1;
            int second = 1;
            for (int i = 2; i < n; ++i)
            {
                int temp = second;
                second += first;
                first = temp;
            }
            return second;
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Enter the number: ");
            int number = int.Parse(Console.ReadLine());

            if (number > 0)
            {
                Console.WriteLine("The Fibonacci number: ");
                Console.WriteLine(Fibonacci(number));
            }
            else
            {
                Console.WriteLine("The wrong number");
            }

            Console.ReadKey();
        }
    }
}