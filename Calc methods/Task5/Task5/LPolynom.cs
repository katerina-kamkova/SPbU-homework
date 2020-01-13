using System;
using System.Collections.Generic;

namespace Task5
{
    public class LPolynom
    {
        private int n;
        private double[] polynom;

        public LPolynom(int n)
        {
            this.n = n >= 1 ? n : 0;
            Create();
        }

        private void Create()
        {
            polynom = new double[n + 1];

            if (n == 0)
            {
                polynom = new double[1] {1.0};
            }
            else
            {
                polynom = CreateRec(2, new double[1] {1.0}, new double[2] {0.0, 1.0});
            }
        }

        private double[] CreateRec(int counter, double[] first, double[] second)
        {
            if (counter == n + 1)
            {
                return second;
            }

            var newPolynom = new double[counter + 1];
            var fNumber = ((double)counter - 1) / counter;
            var sNumber = (2 * (double)counter - 1) / counter;

            newPolynom[counter] = sNumber * second[counter - 1];
            newPolynom[counter - 1] = sNumber * second[counter - 2];
            for (var i = counter - 2; i > 0; i--)
            {
                newPolynom[i] = sNumber * second[i - 1] - fNumber * first[i];
            }

            newPolynom[0] = fNumber * first[0] * (-1);

            return CreateRec(counter + 1, second, newPolynom);
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
        