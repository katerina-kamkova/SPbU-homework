using System;
using System.Collections.Generic;
using System.Linq;

namespace Task3
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Console.WriteLine("Hi! This is LabWork 3");
      Console.WriteLine("Variant 7");
      Console.WriteLine();

      var task = 1;
      while (task != 0)
      {
        task = Tools.FillIntVariable("Enter the number of the task you want to fulfil:\n" +
                                     "1 - Back interpolation task\n" +
                                     "2 - Search of derivatives of a table-defined function according to numerical differentiation formulas\n" +
                                     "0 - Leave the program\n" +
                                     "Your choice: ",
                                     number => number == 1 || number == 2 || number == 0,
                                     "You should enter 1, 2 or 0, try again: ");
        Console.WriteLine();

        if (task == 1)
        {
          Console.WriteLine("Back interpolation task");
          var subtask = new Subtask1(MathFunctions.Function);
          subtask.EnterParams();

          while (true)
          {
            subtask.Solve(Tools.FillIntVariable("Enter the way of counting: 1(only for strictly monotonous function) or 2: ",
                                                number => number == 1 || number == 2,
                                                "You should enter 1 or 2, try again: "));
            Console.Write("If you want to try another method on this parameters, print 'yes', else print any other letter: ");
            if (Console.ReadLine() != "yes")
            {
              break;
            }
          }
        }
        else if (task == 2)
        {
          Console.WriteLine("Search of derivatives of a table-defined function according to numerical differentiation formulas");
          var subtask = new Subtask2();
          subtask.EnterParams();
          subtask.Solve();
        }
      }
    }
  }
}
