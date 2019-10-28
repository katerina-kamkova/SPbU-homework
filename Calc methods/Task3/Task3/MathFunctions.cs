using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
  public static class MathFunctions
  {
    public static double Function(double x)
    {
      return Math.Pow(Math.E, -x) - Math.Pow(x, 2) / 2;
    }

    public static List<(double, double)> Split(int stepNumber, double A, double B, List<(double, double)> table, int n, double F)
    {
      var step = (B - A) / stepNumber;

      var sections = new List<(double, double)>();
      for (double left = A, right = A + step; right <= B; left += step, right += step)
      {
        //var fLeft = CountLagrangePolynom(polynom, left);
        //var fRight = CountLagrangePolynom(polynom, right);
        var fLeft = Lagrange(left, n, table) - F;
        var fRight = Lagrange(right, n, table) - F;
        if (fLeft * fRight <= 0)
        {
          sections.Add((left, right));
        }
      }
      return sections;
    }

    public static List<double> BisectionMethod(List<(double, double)> sections, double E, List<(double, double)> table, int n, double F)
    {
      var answers = new List<double>();
      foreach (var section in sections)
      {
        var (a, b) = section;
        var counter = 0;
        while (b - a > 2 * E)
        {
          var c = (a + b) / 2;
          if ((Lagrange(a, n, table) - F) * (Lagrange(c, n, table) - F) <= 0)
          {
            b = c;
          }
          else
          {
            a = c;
          }
          counter++;
        }
        answers.Add((a + b) / 2);
      }
      return answers;
    }

    // Without fools proof
    public static double Combinations(int current, int k, int gapNumber, List<double> list, double temp, double result)
    {
      for (var i = current; i < list.Count; i++)
      {
        if (list.Count - i < k)
        {
          return result;
        }
        if (i != gapNumber)
        {
          if (k - 1 == 0)
          {
            result += (temp * list[i]);
          }
          else
          {
            result = Combinations(i + 1, k - 1, gapNumber, list, temp * list[i], result);
          }
        }
      }
      return result;
    }

    public static double[] GetLagrangePolynom(int n, List<(double, double)> table)
    {
      var answer = new double[n + 1];

      for (var i = 0; i <= n; i++)
      {
        double denominator = 1.0;
        var numerator = new double[n + 1];
        for (var j = 0; j <= n; j++)
        {
          if (i != j)
          {
            denominator *= (table[i].Item1 - table[j].Item1);
          }

          if (j == 0)
          {
            numerator[j] = 1;
          }
          else
          {
            numerator[j] = Combinations(0, j, i, table.Take(n + 1).Select(el => el.Item1 * (-1)).ToList(), 1, 0);
          }
        }

        var tempF = table[i].Item2;
        for (var j = 0; j <= n; j++)
        {
          answer[n - j] += numerator[j] * tempF / denominator;
        }
      }

      return answer;
    }

    public static double[] GetLagrangePolynom2(int n, List<(double, double)> table)
    {
      var answer = new double[n + 1];
      for (var i = 0; i <= n; i++)
      {
        var counter = 0;
        var denominator = 1.0;
        var numerator = new double[n + 1];
        for (var j = 0; j <= n; j++)
        {
          numerator[j] = 1;
        }
        for (var j = 0; j <= n ; j++)
        {
          if (i != j)
          {
            ++counter;
            denominator *= table[i].Item1 - table[j].Item1;
            for (var k = counter; k >= 0; k--)
            {
              if (k == counter) { numerator[k] = 1; }
              else if (k == 0) { numerator[k] *= (-1) * table[j].Item1; }
              else { numerator[k] = numerator[k] * (-1) * table[j].Item1 + numerator[k - 1]; }
            }
          }
        }

        for (var j = 0; j <= n; j++)
        {
          answer[j] += numerator[j] * table[i].Item2 / denominator;
        }
      }
      return answer;
    }

    public static double CountLagrangePolynom(double[] polynom, double x)
    {
      double answer = 0.0;
      int length = polynom.Length;
      for (var i = 0; i < length; i++)
      {
        answer += polynom[i] * Math.Pow(x, length - i);
      }
      return answer;
    }

    public static double Lagrange(double x, int n, List<(double, double)> table)
    {
      double answer = 0.0;
      for (var i = 0; i <= n; i++)
      {
        double temp = 1.0;
        for (var j = 0; j <= n; j++)
        {
          if (j != i)
          {
            var number = table[j].Item1;
            temp = temp * (x - number) / (table[i].Item1 - number);
          }
        }
        answer += temp * table[i].Item2;
      }
      return answer;
    }
  }
}
