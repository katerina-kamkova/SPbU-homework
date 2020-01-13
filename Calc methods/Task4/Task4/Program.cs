using System;

namespace Task4
{
    public class Program
    {
        private static double A;
        private static double B;
        private static int m;

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

        public static double wFunc(double x)
        {
            return 1.0;
        }

        public static double Func(double x)
        {
            //return Math.Cos(x);
            //return x;
            return Math.Pow(x, 7) + Math.Sin(x);
        }

        public static double Counted()
        {
            //return Math.Sin(B) - Math.Sin(A);
            //return (Math.Pow(B, 2) - Math.Pow(A, 2)) / 2;
            return (Math.Pow(B, 8) - Math.Pow(A, 8)) / 8 - Math.Cos(B) + Math.Cos(A);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hi! This is LabWork4: Approximate calculation of the integral by composite quadrature formulas");
            Console.WriteLine();
            A = FillDoubleVariable("Enter A - left edge of the sector: ",
                                   number => true,
                                   "You should enter a number, try again: ");
            B = FillDoubleVariable("Enter B - right edge of the sector: ",
                                   number => number > A,
                                   "You must enter a number > " + A);
            m = FillIntVariable("Enter the number of sectors: ",
                                number => number > 0,
                                "You must enter a natural number, try again: ");

            Console.WriteLine();
            var J = Counted();
            Console.WriteLine("J = " + J);
            Console.WriteLine();

            double h = (B - A) / m;
            double J_h = 0.0;

            double answer = 0.0;
            for (var i = 0; i < m; i++)
            {
                answer += Func(A + i * h);
            }
            J_h = answer * h;
            Console.WriteLine($"Left rectangle J(h) = {J_h}");
            Console.WriteLine($"|J - J(h)| = {Math.Abs(J - J_h)}");
            Console.WriteLine();

            answer = 0.0;
            for (var i = 0; i < m; i++)
            {
                answer += Func(A + (i + 1) * h);
            }
            J_h = answer * h;
            Console.WriteLine($"Right rectangle J(h) = {answer * h}");
            Console.WriteLine($"|J - J(h)| = {Math.Abs(J - J_h)}");
            Console.WriteLine();

            answer = 0.0;
            for (var i = 0; i < m; i++)
            {
                answer += Func(A + i * h + h / 2);
            }
            J_h = answer * h;
            Console.WriteLine($"Middle rectangle J(h) = {answer * h}");
            Console.WriteLine($"|J - J(h)| = {Math.Abs(J - J_h)}");
            Console.WriteLine();

            answer = 0.0;
            for (var i = 1; i < m; i++)
            {
                answer += Func(A + i * h);
            }
            J_h = ((Func(A) + Func(B)) / 2 + answer) * h;
            Console.WriteLine($"Trapeze J(h) = {((Func(A) + Func(B)) / 2 + answer) * h}");
            Console.WriteLine($"|J - J(h)| = {Math.Abs(J - J_h)}");
            Console.WriteLine();

            double sumInNodes = 0;
            double sumInCenters = 0;
            for (int i = 0; i < m; i++)
            {
                double x = A + i * h;
                double c = x + h / 2;
                sumInNodes += Func(x);
                sumInCenters += Func(c);
            }
            sumInNodes += Func(B);
            
            double summ = 2* sumInNodes-Func(A)-Func(B)+4*sumInCenters;
            summ *= h/6;
            Console.WriteLine($"Simpson's formula J(h) = {summ}");
            Console.WriteLine($"|J - J(h)| = {Math.Abs(J - summ)}");
        }
    }
}


