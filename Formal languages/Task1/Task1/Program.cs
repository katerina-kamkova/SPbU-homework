using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
  public class Program
  {
    public static void Main(string[] args)
    {
      while (true)
      {
        var number = 0;
        var currentSum = 0;
        var i = 1;
        var j = 0;

        Console.Write("Enter the number: ");
        number = Convert.ToInt32(Console.ReadLine());

        while (number > currentSum)
        {
          ++j;
          currentSum += j;
        }

        if (currentSum != number)
        {
          while (number < currentSum)
          {
            currentSum--;
            ++i;
            --j;
          }
        }
        Console.WriteLine("i = " + i + ", j = " + j);
      }
    }
  }
}
