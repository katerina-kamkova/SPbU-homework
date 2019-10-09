using System;
using System.Collections.Generic;
using System.Linq;

namespace Task3
{
  public class Program
  {
    const double E = 1.0e-5;

    public static double Function(double x)
    {
      return Math.Pow(Math.E, -x) - Math.Pow(x, 2) / 2;
    }

    public static List<(double, double)> Split(int stepNumber, double A, double B, double[] polynom)
    {
      var step = (B - A) / stepNumber;

      var sections = new List<(double, double)>();
      for (double left = A, right = A + step; right <= B; left += step, right += step)
      {
        var fLeft = CountLagrangePolynom(polynom, left);
        var fRight = CountLagrangePolynom(polynom, right);
        if (fLeft * fRight <= 0)
        {
          sections.Add((left, right));
        }
      }
      return sections;
    }

    public static List<double> BisectionMethod(List<(double, double)> sections, double[] polynom)
    {
      var answers = new List<double>();
      foreach (var section in sections)
      {
        var (a, b) = section;
        var counter = 0;
        while (b - a > 2 * E)
        {
          var c = (a + b) / 2;
          if (CountLagrangePolynom(polynom, a) * CountLagrangePolynom(polynom, c) <= 0)
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

    public static double[] GetLagrangePolynom(int n, List<(double, double)> table)
    {
      var answer = new double[n];

      for (var i = 0; i < n; i++)
      {
        double denominator = 1.0;
        var numerator = new double[n];
        for (var j = 0; j < n; j++)
        {
          numerator[j] = 1.0;
        }
        for (var j = 0; j < n; j++)
        {
          if (i != j)
          {
            denominator *= (table[i].Item1 - table[j].Item1);
            for (var k = n - 1; k >= 0; k--)
            {
              numerator[k] *= (-1) * table[j].Item1;
              if (k != 0)
              {
                numerator[k] += numerator[k - 1];
              }
            }
          }
        }

        var tempF = table[i].Item2;
        for (var j = 0; j < n; j ++)
        {
          answer[j] += numerator[j] * tempF / denominator;
        }
      }

      return answer;
    }

    public static double CountLagrangePolynom(double[] polynom, double x)
    {
      double answer = 0.0;
      for (var i = 0; i < polynom.Length; i++)
      {
        answer += polynom[i] * Math.Pow(x, i);
      }
      return answer;
    }

    public static void Main(string[] args)
    {
      // The beginning
      Console.WriteLine("Hi! This is LabWork 3");
      Console.WriteLine("Task 3.1 - Back interpolation task");
      Console.WriteLine();
      Console.WriteLine("Variant 7");
      Console.WriteLine("Parameters: f(x) = exp(-x) - x^2 / 2");
      Console.WriteLine();

      int m = 0;
      double a = 0.0;
      double b = 0.0;

      Console.Write("Enter the number of values in the table: ");
      while (true)
      {
        if (!int.TryParse(Console.ReadLine(), out m) || m < 1)
        {
          Console.Write("You should enter a natural number, try again: ");
        }
        else
        {
          m--;
          break;
        }
      }

      Console.Write("Enter a (left edge of the sector): ");
      while (true)
      {
        if (!double.TryParse(Console.ReadLine(), out a))
        {
          Console.Write("You should enter a number, try again: ");
        }
        else
        {
          break;
        }
      }

      Console.Write("Enter b (right edge of the sector): ");
      while (true)
      {
        if (!double.TryParse(Console.ReadLine(), out b) || b <= a)
        {
          Console.Write($"You should enter a number bigger then {a}, try again: ");
        }
        else
        {
          break;
        }
      }

      // Create and print source table
      var table = new List<(double, double)>();
      Console.WriteLine("Source table:");
      for (var i = 0; i <= m; i++)
      {
        double temp = a + i * (b - a) / m;
        table.Add((temp, Function(temp)));
        Console.WriteLine($"{i + 1}. f({table.Last().Item1}) = {table.Last().Item2}");
      }
      Console.WriteLine();
      
      while (true)
      {
        int countType = 0;
        Console.Write("Enter the way of counting: 1(only for monotonous function) or 2: ");
        while (true)
        {
          if (!int.TryParse(Console.ReadLine(), out countType) || (countType != 1 && countType != 2))
          {
            Console.Write("You should enter 1 or 2, try again: ");
          }
          else
          {
            break;
          }
        }

        // Enter correct F
        double F = 0.0;
        Console.Write("Enter value F: ");
        while (true)
        {
          if (!double.TryParse(Console.ReadLine(), out F))
          {
            Console.Write("The value F must be a double number, try again: ");
          }
          else
          {
            break;
          }
        }

        // Enter correct n
        int n = 0;
        Console.Write($"Enter value n (it must be <= then {m + 1}): ");
        while (true)
        {
          if (!int.TryParse(Console.ReadLine(), out n) || n > m + 1)
          {
            Console.Write($"The value n must be a natural number <= then {m + 1}, try again: ");
          }
          else
          {
            break;
          }
        }
        Console.WriteLine();

        var answer = new List<double>();
        if (countType == 1)
        {
          var newTable = table.Select(element => (element.Item2, element.Item1)).ToList();

          // Sort table according to F and print it
          Console.WriteLine("Sorted table:");
          newTable.Sort(((double, double) first, (double, double) second) =>
          {
            if (Math.Abs(F - first.Item1) < Math.Abs(F - second.Item1)) return -1;
            else if (Math.Abs(F - first.Item1) == Math.Abs(F - second.Item1)) return 0;
            else return 1;
          });

          var counter = 1;
          foreach (var element in newTable)
          {
            Console.WriteLine($"{counter}. f^(-1)({element.Item1}) = {element.Item2}");
            counter++;
          }
          Console.WriteLine();

          answer.Add(CountLagrangePolynom(GetLagrangePolynom(n, newTable), F));
        }
        else
        {
          var polynom = GetLagrangePolynom(n, table);
          polynom[0] -= F;

          Console.Write($"Enter the number of sections: ");
          var stepNumber = 0;
          while (true)
          {
            if (!int.TryParse(Console.ReadLine(), out stepNumber) || stepNumber <= 0)
            {
              Console.Write($"The value must be a natural number, try again: ");
            }
            else
            {
              break;
            }
          }
          answer = BisectionMethod(Split(stepNumber, a, b, polynom), polynom);
        }
        foreach (var element in answer)
        {
          Console.WriteLine("f^(-1)(" + F + ") = " + element);
          Console.WriteLine("The residual = " + Math.Abs(Function(element) - F));
          Console.WriteLine();
        }
      }
    }
  }
}
