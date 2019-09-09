using System;
using static System.Console;

namespace ConsoleApp
{
    /// <summary>
    /// Class with events, which are used in this program
    /// </summary>
    public class EventLoop
    {
        /// <summary>
        /// Create necessary events 
        /// </summary>
        public event EventHandler<EventArgs> LeftHandler = (senger, args) => { };
        public event EventHandler<EventArgs> RightHandler = (senger, args) => { };
        public event EventHandler<EventArgs> UpHandler = (senger, args) => { };
        public event EventHandler<EventArgs> DownHandler = (senger, args) => { };

        /// <summary>
        /// Connect events and arrow buttons
        /// </summary>
        public void Move()
        {
            while (true)
            {
                var key = ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        LeftHandler(this, EventArgs.Empty);
                        break;
                    case ConsoleKey.RightArrow:
                        RightHandler(this, EventArgs.Empty);
                        break;
                    case ConsoleKey.UpArrow:
                        UpHandler(this, EventArgs.Empty);
                        break;
                    case ConsoleKey.DownArrow:
                        DownHandler(this, EventArgs.Empty);
                        break;
                    default:
                        WriteLine("There`s no such command");
                        break;
                }
            }
        }
    }
}
