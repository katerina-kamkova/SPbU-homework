using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNUnit
{
    public class Program
    {
        static void Main(string[] args)
        {
            var testsRunner = new TestsRunner();
            var array = testsRunner.ExecuteTests(AssemblyGetter.GetPath(args[0]));
            Printer.PrintResults(array);
        }
    }
}
