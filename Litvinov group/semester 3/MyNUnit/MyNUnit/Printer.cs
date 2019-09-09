using System;
using System.Collections;

namespace MyNUnit
{
    /// <summary>
    /// Prints gathered information
    /// </summary>
    public class Printer
    {
        /// <summary>
        /// Print all the results
        /// <param name="infoArray"/>Array with info about tests</param>
        /// </summary>
        public static void PrintResults(Info[] infoArray)
        {
            Array.Sort(infoArray, Compare);

            string previousClass = null;
            foreach (var info in infoArray)
            {
                if (!Equals(previousClass, info.ClassType))
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine(info.ClassType);
                    previousClass = info.ClassType;
                }

                Console.Write("    " + info.TestName);
                Console.Write("    " + info.Result);

                if (!Equals(info.Result, "Passed"))
                {
                    Console.Write($" because: {info.Reason}");
                }

                var iTime = info.Time;
                string time = String.Format("{0:00}:{1:00}:{2:00}.{3:000000}",
                    iTime.Hours, iTime.Minutes, iTime.Seconds, iTime.Milliseconds / 10);
                Console.WriteLine($"  time: {time}");
            }
        }

        /// <summary>
        /// The rule for sorting the tests:
        /// if tests are from the same class, compare their names
        /// else compare classes
        /// Put this method outside PrintResults to be able to test it
        /// </summary>
        /// <param name="x">first element</param>
        /// <param name="y">second element</param>
        /// <returns>compare result</returns>
        public static int Compare(Info x, Info y)
        {
            if (Equals(x.ClassType, y.ClassType))
            {
                return new CaseInsensitiveComparer().Compare(x.TestName, y.TestName);
            }
            return new CaseInsensitiveComparer().Compare(x.ClassType, y.ClassType);
        }
    }
}
