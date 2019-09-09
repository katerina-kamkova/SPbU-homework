using System;
using static System.Console;

namespace List
{
    class Program
    {
        private static void Menu()
        {
            WriteLine("Available options: ");
            WriteLine("1 - add element");
            WriteLine("2 - delete element");
            WriteLine("3 - get length");
            WriteLine("4 - find element");
            WriteLine("5 - print list");
            WriteLine("6 - check, whether the list is empty");
            WriteLine("7 - delete list");
            WriteLine("0 - exit the program");
        }

        private static void EventLoop(List list)
        {
            Menu();

            int choice = 1;
            while (choice != 0)
            {
                WriteLine();
                WriteLine("Enter your choice: ");
                string input = ReadLine();
                while (!int.TryParse(input, out choice))
                {
                    WriteLine("It`s not a number, try again: ");
                    input = ReadLine();
                }

                switch (choice)
                {
                    case 1:
                        {
                            WriteLine("Enter the element: ");
                            string str = ReadLine();
                            WriteLine("Enter the position: ");
                            string position = ReadLine();

                            list.Add(str, position);
                            break;
                        }
                    case 2:
                        {
                            WriteLine("Enter the element: ");
                            string str = ReadLine();

                            list.DeleteElement(str);
                            break;
                        }
                    case 3:
                        {
                            WriteLine($"The length of the list: {list.Size()}");
                            break;
                        }
                    case 4:
                        {
                            WriteLine("Enter the number: ");
                            string str = ReadLine();

                            string meaning = list.ReturnString(str);
                            if (meaning == "")
                            {
                                WriteLine("There`s no such element");
                            }
                            else
                            {
                                WriteLine($"The element number {str}: {meaning}");
                            }
                            break;
                        }
                    case 5:
                        {
                            list.Print();
                            break;
                        }
                    case 6:
                        {
                            if (list.IsEmpty())
                            {
                                WriteLine("The list is empty");
                            }
                            else
                            {
                                WriteLine("The list isn`t empty");
                            }
                            break;
                        }
                    case 7:
                        {
                            list.Clean();
                            WriteLine("The list is deleted");
                            break;
                        }
                    case 0:
                        {
                            break;
                        }
                    default:
                        {
                            WriteLine("The wrong number, try again");
                            break;
                        }
                }
            }
        }

        private static void Main(string[] args)
        {
            var list = new List();
            EventLoop(list);
        }
    }
}
