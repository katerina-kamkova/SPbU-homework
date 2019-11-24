using System;
using System.Collections.Generic;

namespace Task5
{
    public class ChPolynom
    {
        private int n;
        private double[] polynom;

        public ChPolynom(int n)
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
            newPolynom[counter] = 2 * second[counter - 1];
            newPolynom[counter - 1] = 2 * second[counter - 2];
            for (var i = counter - 2; i > 0; i--)
            {
                newPolynom[i] = 2 * second[i - 1] - first[i];
            }

            newPolynom[0] = first[0] * (-1);

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

        public double CountCoercedPolynom(double x)
        {
            double answer = 0.0;
            for (var i = 0; i < polynom.Length; i++)
            {
                answer += polynom[i] * Math.Pow(x, i) / Math.Pow(2, n - 1);
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

        public List<double> GetRoots()
        {
            var roots = new List<double>();
            for (var k = 1; k <= n; k++)
            {
                var root = polynom.Length % 2 == 1 && k == (polynom.Length - 1) / 2
                    ? 0.0
                    : Math.Cos((2 * k - 1) * Math.PI / (2 * n));
                roots.Add(root);
            }

            return roots;
        }

        public List<double> GetExtremums()
        {
            var extremums = new List<double>();
            for (var k = 0; k <= n; k++)
            {
                extremums.Add(Math.Cos(Math.PI * k / n));
            }

            return extremums;
        }
    }
}