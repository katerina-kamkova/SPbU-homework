using System;
using static System.Console;

namespace Change
{
    class Program
    {
        private static int InputColumnLength()
        {
            WriteLine("Enter the length of the columns: ");
            var input = ReadLine();
            int columnLength = 0;
            bool incorrectData = true;

            while (incorrectData)
            {
                incorrectData = false;
                if (!int.TryParse(input, out columnLength) || columnLength < 1)
                {
                    incorrectData = true;
                    Write("The wrong number, try again: ");
                    input = ReadLine();
                }
            }

            return columnLength;
        }

        private static int[,] Input()
        {
            int columnLength = InputColumnLength();

            WriteLine("Enter your matrix: ");
            var input = ReadLine().Split();
            int length = input.Length;
            int[,] matrix = new int[columnLength, length];

            for (int i = 0; i < columnLength; ++i)
            {
                for (int j = 0; j < length; ++j)
                {
                    if (!int.TryParse(input[j], out matrix[i, j]) || input.Length != length)
                    {
                        j = -1;
                        WriteLine("The wrong string, try again: ");
                        input = ReadLine().Split();
                    }
                }

                if (i != columnLength - 1)
                {
                    bool incorrectData = true;
                    input = ReadLine().Split();
                    incorrectData = true;
                    while (incorrectData)
                    {
                        incorrectData = false;
                        if (input.Length != length)
                        {
                            incorrectData = true;
                            WriteLine("The wrong string, try again: ");
                            input = ReadLine().Split();
                        }
                    }
                }
            }

            return matrix;
        }

        private static void ChangeColumns(int a, int b, int[,] matrix)
        {
            for (int j = 0; j < matrix.GetLength(0); ++j)
            {
                int incorrectData = matrix[j, a];
                matrix[j, a] = matrix[j, b];
                matrix[j, b] = incorrectData;
            }
        }

        private static void SelectionSort(int[,] matrix)
        {
            for (int i = matrix.GetLength(1) - 1; i > 0; --i)
            {
                int maxElement = matrix[0, 0];
                int maxIndex = 0;

                for (int j = 1; j < i; ++j)
                {
                    if (matrix[0, j] > maxElement)
                    {
                        maxElement = matrix[0, j];
                        maxIndex = j;
                    }
                }

                if (maxElement > matrix[0, i])
                {
                    ChangeColumns(maxIndex, i, matrix);
                }
            }
        }

        private static void Print(int[,] matrix)
        {
            WriteLine();
            WriteLine("Matrix, sorted by elements of the first string: ");
            for (int i = 0; i < matrix.GetLength(0); ++i)
            {
                for (int j = 0; j < matrix.GetLength(1); ++j)
                {
                    Write(matrix[i, j] + " ");
                }
                WriteLine();
            }
        }
        
        private static void Main(string[] args)
        {
            int[,] matrix = Input();
            SelectionSort(matrix);
            Print(matrix);
        }
    }
}
