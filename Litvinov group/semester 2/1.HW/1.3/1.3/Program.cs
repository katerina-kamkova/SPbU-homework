using System;

namespace Sort
{
    public class Program
    {
        private static int[] Input()
        {
            Console.WriteLine("Enter your array: ");
            string inputStr = Console.ReadLine();
            int length = inputStr.Length / 2 + 1;
            int[] array = new int[length];

            for (var i = 0; i < length; ++i)
            {
                array[i] = Convert.ToInt32(inputStr.Split(' ')[i]);
            }

            return array;
        }

        private static void BubbleSort(int[] array)
        {
            for (int i = array.Length - 1; i > 0; --i)
            {
                for (int j = 0; j < i; ++j)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }

        private static void Output(int[] array)
        {
            Console.WriteLine("The array in ascending order: ");
            foreach (var element in array)
            {
                Console.Write($"{element} ");
            }
        }

        private static void Main(string[] args)
        {
            int[] array = Input();
            BubbleSort(array);
            Output(array);
        }
    }
}
