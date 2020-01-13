namespace Task5
{
    using System;
    using System.IO;
    using OxyPlot;
    using OxyPlot.Axes;
    using OxyPlot.Series;

    public class Program
    {
        private static int FillIntVariable(string request, Func<int, bool> condition, string failMessage)
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

        private static double FillDoubleVariable(string request, Func<double, bool> condition, string failMessage)
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

        private static void Main(string[] args)
        {
            Console.WriteLine("Hi! This is LabWork 5");
            Console.WriteLine("Classic orthogonal polynomials");
            Console.WriteLine();

            var N = FillIntVariable("Enter N - polynom degree: ", 
                                    number => number >= 1,
                                    "You must enter a natural number, try again: ");
            Console.WriteLine();
            

            // Legender polynom
            Console.Write("Legendre polynom: P(x) = ");
            var lPolynom = new LPolynom(N);
            lPolynom.Print();

            var roots = lPolynom.GetRoots(-1.0, 1.0);
            Console.WriteLine("Legendre polynom roots: ");
            foreach (var root in roots)
            {
                Console.WriteLine(root);                
            }
            Console.WriteLine();
            
            var LegenderModel = new PlotModel{ Title = "Legender polynom" };
            Func<double, double> func = (x) => lPolynom.CountPolynom(x);
            LegenderModel.Series.Add(new FunctionSeries(func, -1, 1, 0.0001));
            using (var stream = File.Create("/home/katherine/RiderProjects/Task5/Task5/LegenderPolynom"))
            {
                var pdfExporter = new PdfExporter { Width = 600, Height = 400 };
                pdfExporter.Export(LegenderModel, stream);
            }
            
            
            // Chebyshev polynom
            Console.Write("Chebyshev polynom: T(x) = ");
            var chPolynom = new ChPolynom(N);
            chPolynom.Print();
            
            Console.WriteLine("Chebyshev polynom roots: ");
            foreach (var root in chPolynom.GetRoots())
            {
                Console.WriteLine(root);
            }
            Console.WriteLine();
            
            Console.WriteLine("Chebyshev polynom extremums: ");
            foreach (var extremum in chPolynom.GetExtremums())
            {
                Console.WriteLine(extremum);
            }
            Console.WriteLine();
            
            var ChebyshevModel = new PlotModel{ Title = "Chebyshev polynoms" };
            Func<double, double> func1 = (x) => chPolynom.CountPolynom(x);
            Func<double, double> func2 = (x) => chPolynom.CountCoercedPolynom(x);
            ChebyshevModel.Series.Add(new FunctionSeries(func1, -1, 1, 0.0001, "Chebyshev"));
            ChebyshevModel.Series.Add(new FunctionSeries(func2, -1, 1, 0.0001, "Coerced Chebyshev"));
            using (var stream = File.Create("/home/katherine/RiderProjects/Task5/Task5/ChebyshevPolynoms"))
            {
                var pdfExporter = new PdfExporter { Width = 600, Height = 400 };
                pdfExporter.Export(ChebyshevModel, stream);
            }
            
            
            // Hermite polynom
            Console.Write("Hermite polynom: H(x) = ");
            var hPolynom = new HPolynom(N);
            hPolynom.Print();

            var hRoots = hPolynom.GetRoots(-5.0, 5.0);
            Console.WriteLine("Hermite polynom roots: ");
            foreach (var root in hRoots)
            {
                Console.WriteLine(root);                
            }
            Console.WriteLine();
            
            var HermiteModel = new PlotModel{ Title = "Hermite polynom" };
            Func<double, double> hFunc = (x) => hPolynom.CountPolynom(x);
            HermiteModel.Series.Add(new FunctionSeries(hFunc, -4, 4, 0.0001));
            using (var stream = File.Create("/home/katherine/RiderProjects/Task5/Task5/HermitePolynom"))
            {
                var pdfExporter = new PdfExporter { Width = 600, Height = 400 };
                pdfExporter.Export(HermiteModel, stream);
            }
            
            
            // Laguerre polynoms
            var alpha = FillIntVariable("Enter alpha: ",
                                        number => number > -1,
                                        "Alpha must be > -1, try again: ");
            
            Console.Write("Laguerre polynom: L(x) = ");
            var lagPolynom = new LagPolynom(N, alpha);
            lagPolynom.Print();

            var lRoots = lagPolynom.GetRoots(0.0, 50.0);
            Console.WriteLine("Laguerre polynom roots: ");
            foreach (var root in lRoots)
            {
                Console.WriteLine(root);                
            }
            Console.WriteLine();
            
            var LaguerreModel = new PlotModel{ Title = "Laguerre polynom" };
            Func<double, double> lFunc = (x) => lagPolynom.CountPolynom(x);
            LaguerreModel.Series.Add(new FunctionSeries(lFunc, 0.0, 14.0, 0.0001));
            using (var stream = File.Create("/home/katherine/RiderProjects/Task5/Task5/LaguerrePolynom"))
            {
                var pdfExporter = new PdfExporter { Width = 600, Height = 400 };
                pdfExporter.Export(LaguerreModel, stream);
            }
        }
    }
}