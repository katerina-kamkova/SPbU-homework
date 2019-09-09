using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace SimpleFTP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var port = 4242;
            var client = new Client(port);
            while (true)
            {
                var input = Console.ReadKey();
                if (Equals('1', input))
                {
                    var list = client.List("ServerExample").Result;
                    if (list != null)
                    {
                        Console.WriteLine(list.Count);
                        Console.WriteLine();
                        for (var i = 0; i < list.Count; i++)
                        {
                            Console.WriteLine(list[i].Item1);
                            Console.WriteLine(list[i].Item2);
                        }
                    }
                }
                else
                {
                    client.Get("ServerExample/Геометрическая прогрессия.docx",
                        "C:/HW-2nd3rd-sem/Quiz.docx").Wait();
                    File.Delete("C:/HW-2nd3rd-sem/Quiz.docx");
                }

                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
