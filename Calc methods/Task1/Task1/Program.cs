using System;
using System.Collections.Generic;

namespace Task1
{
  public class Program
  {
    const double A = -8;
    const double B = 2;
    const double E = 1.0e-5;

    public static double Function(double x)
    {
      //return 10 * Math.Cos(x) - 0.1 * Math.Pow(x, 2);
      return Math.Pow(x, 3) - 1.0;
    }

    public static double Derivative(double x)
    {
      //return (-10) * Math.Sin(x) - 0.2 * x;
      return 3.0 * Math.Pow(x, 2);
    }

    public static List<(double, double)> Split(int stepNumber)
    {
      var step = (B - A) / stepNumber;

      var sections = new List<(double, double)>();
      for (double left = A, right = A + step; right <= B; left += step, right += step)
      {
        var fLeft = Function(left);
        var fRight = Function(right);
        if (fLeft * fRight <= 0)
        {
          sections.Add((left, right));
        }
      }
      return sections;
    }

    public static List<(double, int, double, double, double)> BisectionMethod(List<(double, double)> sections)
    {
      var answers = new List<(double, int, double, double, double)>();
      foreach (var section in sections)
      {
        var (a, b) = section;
        var counter = 0;
        while (b - a > 2 * E)
        {
          var c = (a + b) / 2;
          if (Function(a) * Function(c) <= 0)
          {
            b = c;
          }
          else
          {
            a = c;
          }
          counter++;
        }
        answers.Add(((section.Item2 + section.Item1) / 2, counter, (a + b) / 2, b - a, Math.Abs(Function((a + b) / 2))));
      }
      return answers;
    }

    public static List<(double, int, double, double, double)> NewtonMethod(List<(double, double)> sections, int p = 1)
    {
      var answers = new List<(double, int, double, double, double)>();
      foreach (var section in sections)
      {
        var counter = 0;
        var xStart = Derivative(section.Item1) != 0 ? section.Item1 : section.Item1 - E;
        var xNew = xStart - Function(xStart) * p / Derivative(xStart);
        while (Math.Abs(xStart - xNew) > E)
        {
          xStart = xNew;
          var der = Derivative(xStart);
          if (der == 0)
          {
            return NewtonMethod(sections, p + 1);
          }
          xNew = xStart - Function(xStart) * p / Derivative(xStart);
          counter++;
        }
        answers.Add((section.Item1, counter, xNew, Math.Abs(xStart - xNew), Math.Abs(Function(xNew))));
      }
      return answers;
    }

    public static List<(double, int, double, double, double)> ModifiedNewtonMethod(List<(double, double)> sections)
    {
      var answers = new List<(double, int, double, double, double)>();
      foreach (var section in sections)
      {
        var counter = 0;
        var xStart = Derivative(section.Item1) != 0 ? section.Item1 : section.Item1 + E;
        var xNew = xStart - Function(xStart) / Derivative(xStart);
        while (Math.Abs(xStart - xNew) > E)
        {
          xStart = xNew;
          xNew = xStart - Function(xStart) / Derivative(section.Item1);
          counter++;
        }
        answers.Add((section.Item1, counter, xNew, Math.Abs(xStart - xNew), Math.Abs(Function(xNew))));
      }
      return answers;
    }

    public static List<(double, int, double, double, double)> SecantMethod(List<(double, double)> sections)
    {
      var answers = new List<(double, int, double, double, double)>();
      foreach (var section in sections)
      {
        var counter = 0;
        var xStart = section.Item1;
        var xNew = section.Item2;
        while (Math.Abs(xStart - xNew) > E)
        {
          var temp = xNew;
          xNew = xStart - Function(xStart) * (xNew - xStart) / (Function(xNew) - Function(xStart));
          xStart = temp;
          counter++;
        }
        answers.Add((section.Item1, counter, xNew, Math.Abs(xStart - xNew), Math.Abs(Function(xNew))));
      }
      return answers;
    }

    public static void Print(List<(double, int, double, double, double)> answers)
    {
      foreach (var answer in answers)
      {
        Console.WriteLine();
        Console.WriteLine("Initial approximation = " + answer.Item1);
        Console.WriteLine("Number of steps = " + answer.Item2);
        Console.WriteLine("Approximate solution = " + answer.Item3);
        Console.WriteLine("Length of the last sector = " + answer.Item4);
        Console.WriteLine("Absolute value of the residual = " + answer.Item5);
      }
    }

    public static void Main(string[] args)
    {
      Console.WriteLine("Hi! This is LabWork 1 : Numerical methods for nonlinear equations.");
      Console.WriteLine();
      Console.WriteLine($"Parameters: [{A}; {B}], f(x) = 10 * cos(x) - 0.1 * x^2, E = 10^(-5)");
      Console.WriteLine();

      Console.WriteLine("Root separation");
      Console.Write("Enter the number of the sections: ");
      var stepNumber = Convert.ToInt32(Console.ReadLine());
      Console.WriteLine();
      var sections = Split(stepNumber);

      Console.WriteLine($"Subsectors of [{A}; {B}] that have 1 root inside:");
      foreach (var section in sections)
      {
        var (a, b) = section;
        Console.WriteLine($"[{a}; {b}]");
      }
      Console.WriteLine("Number of subsectors = " + sections.Count);

      Console.WriteLine();
      Console.WriteLine("Results of Bisection method:");
      Print(BisectionMethod(sections));

      Console.WriteLine();
      Console.WriteLine("Results of Newton`s method:");
      //Print(NewtonMethod(sections));

      Console.WriteLine();
      Console.WriteLine("Results of modified Newton`s method:");
      Print(ModifiedNewtonMethod(sections));

      Console.WriteLine();
      Console.WriteLine("Results of secant method:");
      Print(SecantMethod(sections));
    }
  }
}
