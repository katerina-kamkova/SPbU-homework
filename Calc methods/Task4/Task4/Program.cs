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

    public static double fFunc(double x)
    {
      return Math.Exp(x * 2);
    }

    public static double Counted()
    {
      return (Math.Exp(2 * B) - Math.Exp(2 * A)) / 2;
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
        answer += fFunc(A + i * h);
      }
      J_h = answer * h;
      Console.WriteLine($"Left rectangle J(h) = {J_h}");
      Console.WriteLine($"|J - J(h)| = {Math.Abs(J - J_h)}");
      Console.WriteLine();

      answer = 0.0;
      for (var i = 0; i < m; i++)
      {
        answer += fFunc(A + (i + 1) * h);
      }
      J_h = answer * h;
      Console.WriteLine($"Right rectangle J(h) = {answer * h}");
      Console.WriteLine($"|J - J(h)| = {Math.Abs(J - J_h)}");
      Console.WriteLine();

      answer = 0.0;
      for (var i = 0; i < m; i++)
      {
        answer += fFunc(A + i * h + h / 2);
      }
      J_h = answer * h;
      Console.WriteLine($"Middle rectangle J(h) = {answer * h}");
      Console.WriteLine($"|J - J(h)| = {Math.Abs(J - J_h)}");
      Console.WriteLine();

      answer = 0.0;
      for (var i = 1; i < m; i++)
      {
        answer += fFunc(A + i * h);
      }
      J_h = ((fFunc(A) + fFunc(B)) / 2 + answer) * h;
      Console.WriteLine($"Trapeze J(h) = {((fFunc(A) + fFunc(B)) / 2 + answer) * h}");
      Console.WriteLine($"|J - J(h)| = {Math.Abs(J - J_h)}");
      Console.WriteLine();

      answer = 0.0;
      double answer1 = 0.0;
      for (var i = 1; i < m; i++)
      {
        if (i % 2 == 0)
        {
          answer1 += fFunc(A + i * h);
        }
        else
        {
          answer += fFunc(A + i * h);
        }
      }
      J_h = (fFunc(A) + fFunc(B) + 4 * answer + 2 * answer1) * h / 6;
      Console.WriteLine($"Simpson's formula J(h) = {(fFunc(A) + fFunc(B) + 4 * answer + 2 * answer1) * h / 6}");
      Console.WriteLine($"|J - J(h)| = {Math.Abs(J - ((fFunc(A) + fFunc(B) + 4 * answer + 2 * answer1) * h / 6))}");
    }
  }
}

