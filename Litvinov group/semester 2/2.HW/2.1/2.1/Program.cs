using System;
using static System.Console;

namespace Stack
{
    class Program
    {
        private static void Main(string[] args)
        {
            var stack = new Stack();
            stack.Push("111");
            stack.Push("222");
            WriteLine($"The length: {stack.Size()}");
            WriteLine($"The top: {stack.Pop()}");
            WriteLine($"The length: {stack.Size()}");
            stack.Push("333");
            stack.Clean();
            WriteLine($"The length: {stack.Size()}");
            if (!stack.IsEmpty())
            {
                WriteLine($"The top: {stack.Pop()}");
            }
            WriteLine(stack.Pop());
        }
    }
}
