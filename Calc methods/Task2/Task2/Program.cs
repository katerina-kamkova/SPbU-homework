using System;
using System.Collections.Generic;
using System.Linq;

namespace Task2
{
  public class Program
  {
    public const double a = 0;
    public const double b = 1;
    public const double y = 0.65;
    public const int m = 15;
    public static List<(double, double)> table = new List<(double, double)>();

    public static double Function(double x)
    {
      return Math.Pow(Math.E, -x) - Math.Pow(x, 2) / 2;
    }

    public static double Lagrange(double x, int n)
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

    public static double Newton(double x, int n)
    {
      var newtonTable = new double[n + 2, n + 1];
      for (var i = 0; i <= n + 2; i++)
      {
        for (var j = 0; j < n + 2 - i; j++)
        {
          if (j == n + 1)
          {
            continue;
          }

          if (i == 0)
          {
            newtonTable[i, j] = table[j].Item1;
          }
          else if (i == 1)
          {
            newtonTable[i, j] = table[j].Item2;
          }
          else
          {
            newtonTable[i, j] = (newtonTable[i - 1, j + 1] - newtonTable[i - 1, j]) 
              / (newtonTable[0, i + j - 1] - newtonTable[0, j]);
          }
        }
      }
      double answer = 0.0;
      for (var i = 1; i <= n + 1; i++)
      {
        double temp = newtonTable[i, 0];
        for (var j = 1; j < i; j++)
        {
          temp *= x - table[j - 1].Item1;
        }
        answer += temp;
      }
      return answer;
    }

    public static void Main(string[] args)
    {
      var n = 7;
      var x = 0.0;
      var goOn = true;
      var sourceTableType = 2;
      var rand = new Random();

      // The beginning
      Console.WriteLine("Hi! This is LabWork 2: Algebraic interpolation problem");
      Console.WriteLine();
      Console.WriteLine("Variant 7");
      Console.WriteLine("Parameters: f(x) = exp(-x) - x^2 / 2,   [a, b] = [0, 1], m = 15");
      Console.WriteLine();
      Console.Write("Choose the way of creating the source table (1 - random, 2 - optimal): ");

      while (true)
      {
        if (!int.TryParse(Console.ReadLine(), out sourceTableType) || sourceTableType != 1 && sourceTableType != 2)
        {
          Console.WriteLine();
          Console.Write("You should enter 1 or 2, try again: ");
        }
        else
        {
          break;
        }
      }

      // Create and print source table
      Console.WriteLine("Source table:");
      for (var i = 0; i < m; i++)
      {
        if (sourceTableType == 1)
        {
          var temp = rand.NextDouble();
          table.Add((temp, Function(temp)));
        }
        else
        {
          double temp = a + i * (b - a) / m;
          table.Add((temp, Function(temp)));
        }
          Console.WriteLine($"f({table.Last().Item1}) = {table.Last().Item2}");
      }

      while (goOn)
      { 
        // Enter correct n
        Console.WriteLine();
        while (true)
        {
          if (sourceTableType == 2)
          {
            Console.WriteLine("You chose the second type, so n must be equal m - 1");
            n = m - 1;
            break;
          }

          Console.Write("Enter degree of interpolation polynomial n: ");
          if (!int.TryParse(Console.ReadLine(), out n))
          {
            Console.Write("The degree of interpolation polynomial n must be a natural number, try again: ");
          }
          else if (n >= m)
          {
            Console.Write("n must be < m, try again: ");
          }
          else 
          {
            break;
          }
        }

        // Enter correct x
        Console.Write("Enter interpolation point x: ");
        while (true)
        {
          if (!double.TryParse(Console.ReadLine(), out x))
          {
            Console.Write("The interpolation point x must be a double number, try again: ");
          }
          else
          {
            break;
          }
          Console.WriteLine();
        }

        // Sort table according to x and print it
        Console.WriteLine("Sorted table:");
        table.Sort(((double, double) first, (double, double) second) =>
        {
          if (Math.Abs(x - first.Item1) < Math.Abs(x - second.Item1)) return -1;
          else if (Math.Abs(x - first.Item1) == Math.Abs(x - second.Item1)) return 0;
          else return 1;
        });

        foreach (var element in table)
        {
          Console.WriteLine($"f({element.Item1}) = {element.Item2}");
        }
        Console.WriteLine();

        var lagrangeAnswer = Lagrange(x, n);
        var newtonAnswer = Newton(x, n);
        var funcAnswer = Function(x);

        Console.WriteLine("Lagrange method result = " + lagrangeAnswer);
        Console.WriteLine("Absolute value of the residual = " + Math.Abs(lagrangeAnswer - funcAnswer));
        Console.WriteLine();

        Console.WriteLine("Newton method result = " + newtonAnswer);
        Console.WriteLine("Absolute value of the residual = " + Math.Abs(newtonAnswer - funcAnswer));
        Console.WriteLine();

        // Ask whether user wants to count again
        Console.Write("If you want to count polynomials with new point, enter \"yes\", else enter \"no\": ");
        while (true)
        {
          var answer = Console.ReadLine();
          if (answer == "yes")
          {
            break;
          }
          else if (answer == "no")
          {
            goOn = false;
            Console.WriteLine("You`ve left the program");
            break;
          }
          else
          {
            Console.Write("Wrong input, try again: ");
          }
        }
      }
    }
  }
}
