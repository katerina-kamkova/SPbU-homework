using static System.Console;

namespace StackCalculator
{
    public class Program
    {
        private static void EventLoop(Calculator calculator)
        {
            WriteLine("Calculator");
            WriteLine();
            WriteLine("Available options: ");
            WriteLine("0 - to exit");
            WriteLine("1 - count expression");
            bool choice = true;
            while (choice)
            {
                WriteLine();
                WriteLine("Enter your choice: ");
                choice = int.Parse(ReadLine()) == 1;

                if (choice)
                {
                    WriteLine("Choose the type of the stack:");
                    WriteLine("0 - list stack");
                    WriteLine("1 - array stack");
                    if (ReadLine() == "1")
                    {
                        calculator = new Calculator(new ArrayStack());
                    }
                    else
                    {
                        calculator = new Calculator(new Stack());
                    }

                    WriteLine("Enter the expression in postfix form:");
                    var input = ReadLine().Split();

                    calculator.Calculate(input);
                    calculator.PrintAnswer();
                }
            }
        }
        
        private static void Main(string[] args)
        {
            Calculator calculator = null;
            EventLoop(calculator);
        }
    }
}
