using System;
using static System.Console;

namespace ConsoleApp
{
    public class Program
    {
        private static void Main(string[] args)
        {
            WriteLine("Console App");
            WriteLine();
            WriteLine("Chose the wanted map: ");
            WriteLine("0 - small map");
            WriteLine("1 - big map");
            Write("Enter your choice: ");
            var choice = ReadLine();

            var makeMap = new MakeMap();
            var map = (choice == "1") ? makeMap.Input("Test2.txt") : makeMap.Input("Test.txt");

            makeMap.PrintMapWithCoords(map);

            WriteLine();
            WriteLine();
            Write("Enter x coord: ");
            int x = int.Parse(ReadLine());
            Write("Enter y coord: ");
            int y = int.Parse(ReadLine());

            Game game = default(Game);
            try
            {
                game = new Game(map, x, y);
            }
            catch (WrongCoordsException e)
            {
                WriteLine(e.Message);
                return;
            }
            game.Print();

            var eventLoop = new EventLoop();
            eventLoop.LeftHandler += game.OnLeft;
            eventLoop.RightHandler += game.OnRight;
            eventLoop.UpHandler += game.Up;
            eventLoop.DownHandler += game.Down;

            try
            {
                eventLoop.Move();
            }
            catch (WrongWayException e)
            {
                WriteLine(e.Message);
            }
        }
    }
}
