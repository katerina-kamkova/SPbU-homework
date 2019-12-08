using System;

namespace Task7
{
    public static class Tools
    {
        private const int tableWidth = 80;

        public static void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        public static void PrintRow(params string[] columns)
        {
            var width = (tableWidth - columns.Length) / columns.Length;
            var row = "|";

            foreach (var column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        private static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
        
        public static int FillIntVariable(string request, Func<int, bool> condition, string failMessage)
        {
            Console.Write(request);
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int answer) && condition(answer))
                {
                    return answer;
                }
                else
                {
                    Console.Write(failMessage);
                }
            }
        }

        public static double FillDoubleVariable(string request, Func<double, bool> condition, string failMessage)
        {
            Console.Write(request);
            while (true)
            {
                if (double.TryParse(Console.ReadLine(), out double answer) && condition(answer))
                {
                    return answer;
                }
                else
                {
                    Console.Write(failMessage);
                }
            }
        }
        
        public static void Swap<T>(ref T lhs, ref T rhs)
        {
            var temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
    }
}