using System;
using static System.Console;

namespace ConsoleApp
{
    /// <summary>
    /// The Game; reactions to users choices, display the situation
    /// </summary>
    public class Game
    {
        private Walker user;
        private char[,] map;

        public Game(char[,] map, int xPosition, int yPosition)
        {
            this.map = map;
            user.X = xPosition - 1;
            user.Y = map.GetLength(0) - yPosition;

            if (user.X >= map.GetLength(1) || user.X < 0
             || user.Y >= map.GetLength(0) || user.Y < 0)
            {
                throw new WrongCoordsException("Wrong coords");
            }
        }

        /// <summary>
        /// Struct for user
        /// </summary>
        private struct Walker
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        /// <summary>
        /// Print the map and the walker
        /// </summary>
        public void Print()
        {
            Clear();

            WriteLine();

            if (user.X >= map.GetLength(1) || user.X < 0
             || user.Y >= map.GetLength(0) || user.Y < 0)
            {
                throw new WrongWayException("You won!");
            }

            if (map[user.Y, user.X] == '@')
            {
                throw new WrongWayException("You lose!");
            }
            
            for (int i = 0; i < map.GetLength(0); ++i)
            {
                for (int j = 0; j < map.GetLength(1); ++j)
                {
                    if (user.X == j && user.Y == i && map[i, j] == ' ')
                    {
                        Write("*");
                    }
                    else
                    {
                        Write(map[i, j]);
                    }

                    if (j == map.GetLength(1) - 1)
                    {
                        WriteLine();
                    }
                }
            }
        }

        /// <summary>
        /// Reaction to the pressing left button
        /// </summary>
        public void OnLeft(object sender, EventArgs args)
        {
            --user.X;
            Print();
        }

        /// <summary>
        /// Reaction to the pressing right button
        /// </summary>
        public void OnRight(object sender, EventArgs args)
        {
            ++user.X;
            Print();
        }

        /// <summary>
        /// Reaction to the pressing up button
        /// </summary>
        public void Up(object sender, EventArgs args)
        {
            --user.Y;
            Print();
        }

        /// <summary>
        /// Reaction to the pressing dlwn button
        /// </summary>
        public void Down(object sender, EventArgs args)
        {
            ++user.Y;
            Print();
        }
    }
}
