using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
  public class Program
  {
    public static ISet<char> Both(ISet<char> text1, ISet<char> text2)
    {
      text1.IntersectWith(text2);
      return text1;
    }

    public static ISet<char> FirstNotSecond(ISet<char> text1, ISet<char> text2)
    {
      text1.ExceptWith(text2);
      return text1;
    }

    public static IEnumerable<char> Unique(ISet<char> text)
    {
      return text.Distinct();
    }

    public static void Main(string[] args)
    {
      Console.WriteLine("Find symbols that are in both texts");
      Console.Write("Enter the first text: ");
      var text1 = Console.ReadLine().Split().;
      Console.Write("Enter the second text: ");
      var text2 = Console.ReadLine();


    }
  }
}
