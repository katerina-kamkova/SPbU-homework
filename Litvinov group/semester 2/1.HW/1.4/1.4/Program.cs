using System;

namespace Spiral
{
    public class Program
    {
        private static int[,] Input()
        {
            Console.WriteLine("Enter your matrix: ");
            var input = Console.ReadLine().Split();
            int length = input.Length;
            int[,] matrix = new int[length, length];

            for (int i = 0; i < length; ++i)
            {
                for (int j = 0; j < length; ++j)
                {
                    matrix[i, j] = int.Parse(input[j]);
                }

                if (i != length - 1)
                {
                    input = Console.ReadLine().Split();
                }
            }

            return matrix;
        }

        private static void SpiralPrint(int[,] matrix)
        {
            Console.WriteLine("The matrix in spiral: ");

            int length = (int)Math.Sqrt(matrix.Length);
            Console.Write(matrix[length / 2, length / 2] + " ");

            for (int i = 3; i <= length; i += 2)
            {
                int y = length / 2 + i / 2;
                int x = length / 2 - i / 2;

                for (int j = 1; j < i; ++j)
                {  
                    Console.Write(matrix[x + j, y] + " ");
                }
                x += i - 1;

                for (int j = 1; j < i; ++j)
                {
                    Console.Write(matrix[x, y - j] + " ");
                }
                y -= i - 1;

                for (int j = 1; j < i; ++j)
                {
                    Console.Write(matrix[x - j, y] + " ");
                }
                x -= i - 1;

                for (int j = 1; j < i; ++j)
                {
                    Console.Write(matrix[x, y + j] + " ");
                }
            }
        }

        private static void Main(string[] args)
        {
            int[,] matrix = Input();
            SpiralPrint(matrix);
        }
    }
}