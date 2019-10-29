using System;
using System.Collections.Generic;
using System.Linq;

namespace Task3
{
  public class Subtask2
  {
    private const double k = 4.5;
    private double a;
    private double b;
    private int m;
        private double h;
        private int numberPoints;

    private double function(double x)
    {
      return Math.Exp(k * x);
    }

    private double Derivative(double x)
    {
      return k * Math.Exp(k * x);
    }

    private double Derivative2(double x)
    {
      return Math.Pow(k, 2) * Math.Exp(x * k);
    }

    public void EnterParams()
    {
      a = Tools.FillDoubleVariable("Enter a - left edge of the sector: ",
                                   number => true,
                                   "You should enter a number, try again: ");
      /*b = Tools.FillDoubleVariable("Enter b - right edge of the sector: ",
                                   number => number > a,
                                   $"You should enter a number bigger then {a}, try again: ");*/
      m = Tools.FillIntVariable("Enter the number of values in the table: ",
                                number => number > 0,
                                "You should enter a natural number, try again: ") - 1;
      h = Tools.FillDoubleVariable("Enter h: ",
                                   number => number > 0,
                                   "You should enter a natural number, try again: ");
      b = a + h * m;

      }

        private const int tableWidth = 80;

    static void PrintLine()
    {
      Console.WriteLine(new string('-', tableWidth));
    }

    static void PrintRow(params string[] columns)
    {
      int width = (tableWidth - columns.Length) / columns.Length;
      string row = "|";

      foreach (string column in columns)
      {
        row += AlignCentre(column, width) + "|";
      }

      Console.WriteLine(row);
    }

    static string AlignCentre(string text, int width)
    {
      text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

      if (string.IsNullOrEmpty(text))
      {
        return new string(' ', width);
      }
      else
{
        return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
      }
    }

    public void Solve()
    {
      var list = new List<string[]>();
      list.Add(new string[6] { "x(i)", "f(x(i))", "f'(x(i))nd", "|f'(x(i))t - f'(x(i))nd|", "f''(x(i))nd", "|f''(x(i))t - f''(x(i))nd|" });

      var sourceTable = new List<(double, double, double)>(); // x, f(x), f'(x)
      for (var i = 0; i <= m; i++)
      {
        double temp = a + i * (b - a) / m;
        sourceTable.Add((temp, function(temp), Derivative(temp)));
      }

      //double h = (b - a) / m;
      for (var i = 0; i <= m; i++)
      {
        var x = sourceTable[i].Item1;
        double der = 0;
        double der2 = 0;
        if (i == 0)
        {
          der = (-3 * sourceTable[i].Item2 + 4 * sourceTable[i + 1].Item2 - sourceTable[i + 2].Item2) / (2 * h);
        }
        else if (i == m)
        {
          der = (3 * sourceTable[i].Item2 - 4 * sourceTable[i - 1].Item2 + sourceTable[i - 2].Item2) / (2 * h);
        }
        else
        {
          der = (sourceTable[i + 1].Item2 - sourceTable[i - 1].Item2) / (2 * h);
          der2 = (sourceTable[i + 1].Item2 - 2 * sourceTable[i].Item2 + sourceTable[i - 1].Item2) / (h * h);
        }

        if (i == 0 || i == m)
        {
          var str = new string[6] { Convert.ToString(x),
                                    Convert.ToString(sourceTable[i].Item2),
                                    Convert.ToString(der),
                                    Convert.ToString(Math.Abs(der - Derivative(x))),
                                    "",
                                    ""};
          list.Add(str);
        }
        else
        {
          var str = new string[6] { Convert.ToString(x),
                                    Convert.ToString(sourceTable[i].Item2),
                                    Convert.ToString(der),
                                    Convert.ToString(Math.Abs(der - Derivative(x))),
                                    Convert.ToString(der2),
                                    Convert.ToString(Math.Abs(der2 - Derivative2(x)))};
          list.Add(str);
        }
      }

      PrintRow(list[0].Take(2).ToArray());
      PrintLine();
      for (var i = 1; i < list.Count; i++)
      {
        PrintRow(list[i].Take(2).ToArray());
      }

      Console.WriteLine();

      PrintRow(list[0].Skip(2).Take(2).ToArray());
      PrintLine();
      for (var i = 1; i < list.Count; i++)
      {
        PrintRow(list[i].Skip(2).Take(2).ToArray());
      }

      Console.WriteLine();

      PrintRow(list[0].Skip(4).ToArray());
      PrintLine();
      for (var i = 1; i < list.Count; i++)
      {
        PrintRow(list[i].Skip(4).ToArray());
      }

      Console.WriteLine();
    }
  }
}
