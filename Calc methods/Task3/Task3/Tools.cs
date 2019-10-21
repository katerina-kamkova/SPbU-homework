using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
  public static class Tools
  {
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
  }
}
