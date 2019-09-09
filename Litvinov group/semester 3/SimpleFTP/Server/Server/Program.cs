using System;
using System.Threading.Tasks;

namespace SimpleFTP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var port = 4242;
            var server = new Server(port);
            Task.Run(async () => await server.ServerWork());

            Console.ReadKey();
            server.Stop();
        }
    }
}
