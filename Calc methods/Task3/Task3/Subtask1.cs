using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
  public class Subtask1
  {
    private Func<double, double> function;
    private List<(double, double)> sourceTable;
    private List<double> answers;
    private double a;
    private double b;
    private double F;
    private int m;
    private int n;

    public Subtask1(Func<double, double> function)
    {
      this.function = function;
      sourceTable = new List<(double, double)>();
    }

    public void EnterParams()
    {
      Console.WriteLine("Function: f(x) = exp(-x) - x^2 / 2");
      a = Tools.FillDoubleVariable("Enter a - left edge of the sector: ",
                                   number => true,
                                   "You should enter a number, try again: ");
      b = Tools.FillDoubleVariable("Enter b - right edge of the sector: ",
                                   number => number > a,
                                   $"You should enter a number bigger then {a}, try again: ");
      m = Tools.FillIntVariable("Enter the number of values in the table: ",
                                number => number > 0,
                                "You should enter a natural number, try again: ") - 1;

      sourceTable = new List<(double, double)>();
      Console.WriteLine("Source table:");
      for (var i = 0; i <= m; i++)
      {
        double temp = a + i * (b - a) / m;
        sourceTable.Add((temp, function(temp)));
        Console.WriteLine($"{i + 1}. f({sourceTable.Last().Item1}) = {sourceTable.Last().Item2}");
      }
      Console.WriteLine();
    }

    private void Solve1()
    {
      var newTable = sourceTable.Select(element => (element.Item2, element.Item1)).ToList();

      // Sort table according to F and print it
      Console.WriteLine("Source table, sorted according to F:");
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

      answers = new List<double> { MathFunctions.Lagrange(F, n, newTable) };
      //Print();
      //answers = new List<double> { MathFunctions.CountLagrangePolynom(MathFunctions.GetLagrangePolynom(n, newTable), F) };
    }

    private void Solve2()
    {
      var polynom = MathFunctions.GetLagrangePolynom(n, sourceTable);
      polynom[n] -= F;

      var stepsNum = Tools.FillIntVariable("Enter the number of steps: ",
                                           number => number > 0,
                                           "It must be a natural number, try again: ");
      var sections = MathFunctions.Split(stepsNum, a, b, polynom, sourceTable, n);

      var E = Tools.FillDoubleVariable("Enter E: ",
                                       e => e > 0,
                                       "E must be a positive number, try again: ");
      answers = MathFunctions.BisectionMethod(sections, E, sourceTable, n);
    }

    private void Print()
    {
      foreach (var element in answers)
      {
        Console.WriteLine("f^(-1)(" + F + ") = " + element);
        Console.WriteLine("The residual = " + Math.Abs(function(element) - F));
        Console.WriteLine();
      }
    }

    public void Solve(int way)
    {
      F = Tools.FillDoubleVariable("Enter value F: ",
                                   number => true,
                                   "The value F must be a double number, try again: ");
      n = Tools.FillIntVariable($"Enter value n (it must be <= then {m}): ",
                                number => number <= m,
                                $"The value n must be a natural number <= then {m + 1}, try again: ");

      if (way == 1)
      {
        Solve1();
      }
      else if (way == 2)
      {
        Solve2();
      }

      Print();
    }
  }
}
