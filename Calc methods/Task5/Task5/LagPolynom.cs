using System;
using System.Collections.Generic;

namespace Task5
{
    public class LagPolynom
    {
        private int n;
        private int alpha;
        private double[] polynom;

        public LagPolynom(int n, int alpha)
        {
            this.n = n >= 1 ? n : 0;
            this.alpha = alpha;
            Create();
        }

        private int Factorial(int n, int result)
        {
            if (n == 0)
            {
                return result;
            }

            return Factorial(n - 1, result * n);
        }
        
        private void Create()
        {
            polynom = new double[n + 1];

            for (var m = 0; m <= n; m++)
            {
                polynom[m] = Math.Pow(-1, m) * (double)Factorial(alpha + n, 1) /
                             (Factorial(alpha + m, 1) * Factorial(n - m, 1) * Factorial(m, 1));
            }
        }

        public double[] GetPolynom() => polynom;

        public double CountPolynom(double x)
        {
            double answer = 0.0;
            for (var i = 0; i < polynom.Length; i++)
            {
                answer += polynom[i] * Math.Pow(x, i);
            }

            return answer;
        }

        public void Print()
        {
            if (n != 0)
            {
                Console.Write(polynom[polynom.Length - 1] + " * x ^ " + (polynom.Length - 1));
            }

            for (var i = polynom.Length - 2; i >= 0; i--)
            {
                if (polynom[i] > 0)
                {
                    Console.Write(" + ");
                }
                else if (polynom[i] < 0)
                {
                    Console.Write(" - ");
                }
                else
                {
                    continue;
                }
                
                Console.Write(Math.Abs(polynom[i]));
                if (i != 0)
                {
                    Console.Write(" * x ^ " + i);
                }
            }
            
            Console.WriteLine("\n");
        }

        public List<double> GetRoots(double a, double b)
        {
            return BisectionMethod(Split(1000, a, b));
        }

        private List<(double, double)> Split(int stepNumber, double A, double B)
        {
            var step = (B - A) / stepNumber;

            var sections = new List<(double, double)>();
            for (double left = A, right = A + step; right <= B; left += step, right += step)
            {
                var fLeft = CountPolynom(left);
                var fRight = CountPolynom(right);
                if (fLeft * fRight <= 0)
                {
                    sections.Add((left, right));
                }
            }
            return sections;
        }

        private List<double> BisectionMethod(List<(double, double)> sections)
        {
            var answers = new List<double>();
            foreach (var section in sections)
            {
                var (a, b) = section;
                while (b - a > 2 * 0.000000001)
                {
                    var c = (a + b) / 2;
                    if (CountPolynom(a) * CountPolynom(c) <= 0)
                    {
                        b = c;
                    }
                    else
                    {
                        a = c;
                    }
                }
                answers.Add((a + b) / 2);
            }
            return answers;
        }
    }
}