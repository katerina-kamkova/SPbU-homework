using System;

namespace Task7
{
    public class Program
    {
        private const double x0 = 0.0;
        private const double y0 = 1.0;
        private static readonly double der0 = Equation(x0);
        private static readonly double der1 = Derivative1(x0);
        private static readonly double der2 = Derivative2(x0);
        private static readonly double der3 = Derivative3(x0);
        
        private static double Equation(double x)
        {
            return 0.5 * (Math.Exp(-x) + Math.Sin(x) + Math.Cos(x));
        }
        
        private static double Derivative(double x, double y)
        {
            return -1 * y + Math.Cos(x);
        }
        
        private static double Derivative1(double x)
        {
            return 0.5 * (-Math.Exp(-x) - Math.Sin(x) + Math.Cos(x));
        }
        
        private static double Derivative2(double x)
        {
            return 0.5 * (Math.Exp(-x) - Math.Sin(x) - Math.Cos(x));
        }
        
        private static double Derivative3(double x)
        {
            return 0.5 * (-Math.Exp(-x) + Math.Sin(x) - Math.Cos(x));
        }

        private static double Taylor(int n, double x)
        {
            var num = n / 4;
            var accumulator = 0.0;
            var factorial = 1;
            var pow = 1.0;
            
            for (var i = 0; i < num; i++)
            {
                accumulator += der0 * pow / factorial;
                factorial *= i * 4 + 1;
                pow *= x - x0;
                accumulator += der1 * pow / factorial;
                factorial *= i * 4 + 2;
                pow *= x - x0;
                accumulator += der2 * pow / factorial;
                factorial *= i * 4 + 3;
                pow *= x - x0;
                accumulator += der3 * pow / factorial;
                factorial *= (i + 1) * 4;
                pow *= x - x0;
            }

            accumulator += der0 * pow / factorial; 
            
            var dif = n - (num * 4);
            
            if (dif > 0)
            {
                factorial *= num * 4 + 1;
                pow *= x - x0;
                accumulator += der1 * pow / factorial;
            }

            if (dif > 1)
            {
                factorial *= num * 4 + 2;
                pow *= x - x0;
                accumulator += der2 * pow / factorial;
            }

            if (dif > 2)
            {
                factorial *= num * 4 + 3;
                pow *= x - x0;
                accumulator += der3 * pow / factorial;
            }

            return accumulator;
        }
        
        public static void Main(string[] args)
        {
            Console.WriteLine("LabWork № 7");
            Console.WriteLine("Numerical solution of the Cauchy problem for an ordinary differential equation of the first order");
            Console.WriteLine("Variant № 7");
            Console.WriteLine();
            
            Console.WriteLine("Parameters:");
            Console.WriteLine("y'(x) = -y(x) + cos(x)");
            Console.WriteLine("y(0) = 1");
            Console.WriteLine("y(x) = 0.5 * (exp(-x) + sin(x) + cos(x))");
            Console.WriteLine();

            var n = Tools.FillIntVariable("Enter n: ", 
                                          number => number > 0, 
                                          "n must be a natural number");

            var h = Tools.FillDoubleVariable("Enter h: ", 
                                             number => number > 0, 
                                             "h must be > 0");

            ///////////////////////////////////////
            /// Exact answers
            ///////////////////////////////////////
            
            Console.WriteLine();
            Console.WriteLine("Exact answers: ");
            
            var answers = new double[2 * n + 1];
            for (var i = 0; i < 2 * n + 1; i++)
            {
                var temp = x0 + (i - n) * h;
                answers[i] = Equation(temp);
                Console.WriteLine($"f({temp}) = {answers[i]}");
            }
            
            ///////////////////////////////////////
            /// Taylor method
            ///////////////////////////////////////
            
            Console.WriteLine();
            Console.WriteLine("Taylor method");
            var taylorN = Tools.FillIntVariable("Enter Taylor elements number: ",
                                                number => number > 0,
                                                "Try again, it must be a natural number");
            Tools.PrintLine();
            Tools.PrintRow(new string[3]{"x", "Taylor method", "Absolute error"});
            Tools.PrintLine();
            
            for (var i = 0; i < 2 * n + 1; i++)
            {
                var temp = x0 + (i - n) * h;
                var taylor = Taylor(taylorN, temp);
                Tools.PrintRow(new string[3]{temp.ToString(), taylor.ToString(), Math.Abs(taylor - answers[i]).ToString()});
            }
            Tools.PrintLine();
            
            ///////////////////////////////////////
            /// Adams method
            ///////////////////////////////////////
            /// 
            Console.WriteLine();
            Console.WriteLine("Adams method: ");
            var array = new double[7, 2];
            var x = x0;
            var y = y0;
            array[0, 1] = x;
            array[1, 1] = y;
            array[2, 1] = h * Derivative(x, y);
            for (var i = 1; i < 5; i++)
            {
                array[0, 1] += h;                            // x
                array[1, 0] = array[1, 1];                   // y
                array[1, 1] = Equation(array[0, 1]);
                array[2, 0] = array[2, 1];                   // h * f(x, y)
                array[2, 1] = h * Derivative(array[0, 1], array[1, 1]);
                for (var j = 3; j < i + 3; j++)
                {
                    array[j, 0] = array[j, 1];
                    array[j, 1] = array[j - 1, 1] - array[j - 1, 0];
                }
                
                Console.WriteLine($"f({array[0, 1]}) = {array[1, 1]}");
            }

            array[1, 0] = array[1, 1];
            array[1, 1] += array[2, 1] + 
                           array[3, 1] / 2 + 
                           array[4, 1] * 5 / 12 + 
                           array[5, 1] * 3 / 8 +
                           array[6, 1] * 251 / 720;
            array[0, 1] += h;
            Console.WriteLine($"f({array[0, 1]}) = {array[1, 1]}");
            
            for (var i = 6; i <= n; i++)
            {
                array[2, 0] = array[2, 1];                   // h * f(x, y)
                array[2, 1] = h * Derivative(array[0, 1], array[1, 1]);
                for (var j = 3; j < 7; j++)
                {
                    array[j, 0] = array[j, 1];
                    array[j, 1] = array[j - 1, 1] - array[j - 1, 0];
                }
                
                array[1, 0] = array[1, 1];
                array[1, 1] += array[2, 1] + 
                               array[3, 1] / 2 + 
                               array[4, 1] * 5 / 12 + 
                               array[5, 1] * 3 / 8 +
                               array[6, 1] * 251 / 720;
                array[0, 1] += h;
                Console.WriteLine($"f({array[0, 1]}) = {array[1, 1]}");
            }
            
            Console.WriteLine("|y_n - y(x_n)| = " + Math.Abs(answers[2 * n] - array[1, 1]));
            Console.WriteLine();
            
            ///////////////////////////////////////
            /// Runge-Kutta method
            ///////////////////////////////////////
            
            Console.WriteLine();
            Console.WriteLine("Runge-Kutta method: ");
            x = x0;
            y = y0;
            Console.WriteLine($"f({x}) = {y}");
            for (var i = 0; i < n; i++)
            {
                var k1 = h * Derivative(x, y);
                var k2 = h * Derivative(x + h / 2, y + k1 / 2);
                var k3 = h * Derivative(x + h / 2, y + k2 / 2);
                
                y += (k1 + 2 * k2 + 2 * k3 + h * Derivative(x + h, y + k3)) / 6;
                x += h;
                
                Console.WriteLine($"f({x}) = {y}");
            }
            Console.WriteLine("|y_n - y(x_n)| = " + Math.Abs(y - answers[2 * n]));
            Console.WriteLine();
            
            ///////////////////////////////////////
            /// Euler method
            ///////////////////////////////////////

            Console.WriteLine("Euler method: ");
            Console.WriteLine("If you want to make different h, enter 'yes': ");
            var hArray = new double[n];
            if (Console.ReadLine() == "yes")
            {
                Console.WriteLine("Enter h (n numbers): ");
                for (var i = 0; i < n; i++)
                {
                    hArray[i] = Tools.FillDoubleVariable($"h{1} = ", 
                                                         number => number > 0,
                                                         $"h{1} must be positive, try again: h{1} = ");
                }
            }
            else
            {
                for (var i = 0; i < n; i++)
                {
                    hArray[i] = h;
                }
            }

            x = x0;
            y = y0;
            Console.WriteLine();
            Console.WriteLine($"f({x}) = {y}");
            for (var i = 0; i < n; i++)
            {
                y += hArray[i] * Derivative(x, y);
                x += hArray[i];
                Console.WriteLine($"f({x}) = {y}");
            }
            Console.WriteLine("|y_n - y(x_n)| = " + Math.Abs(y - answers[2 * n]));
            Console.WriteLine();
            
            ///////////////////////////////////////
            /// Improved Euler method
            ///////////////////////////////////////
            
            Console.WriteLine("Improved Euler method: ");
            x = x0;
            y = y0;
            Console.WriteLine();
            Console.WriteLine($"f({x}) = {y}");
            for (var i = 0; i < n; i++)
            {
                y += hArray[i] * Derivative(x + h / 2, y + hArray[i] * Derivative(x, y) / 2);
                x += hArray[i];
                Console.WriteLine($"f({x}) = {y}");
            }
            Console.WriteLine("|y_n - y(x_n)| = " + Math.Abs(y - answers[2 * n]));
            Console.WriteLine();
            
            ///////////////////////////////////////
            /// Euler-Cauchy method
            ///////////////////////////////////////
            
            Console.WriteLine("Euler-Cauchy method: ");
            x = x0;
            y = y0;
            Console.WriteLine();
            Console.WriteLine($"f({x}) = {y}");
            for (var i = 0; i < n; i++)
            {
                y += hArray[i] * (Derivative(x, y) + Derivative(x + hArray[i], y + hArray[i] * Derivative(x, y))) / 2;
                x += hArray[i];
                Console.WriteLine($"f({x}) = {y}");
            }
            Console.WriteLine("|y_n - y(x_n)| = " + Math.Abs(y - answers[2 * n]));
            Console.WriteLine();
        }
    }
}