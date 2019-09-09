using static System.Console;

namespace UniqueList
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
            WriteLine("0 - exit the program");
        }

        private static void EventLoop(UniqueList list)
        {
            Menu();

            int choice = 1;
            while (choice != 0)
            {
                WriteLine();
                Write("Enter your choice: ");
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
                            Write("Enter the element: ");
                            string str = ReadLine();
                            Write("Enter the position: ");
                            int position = int.Parse(ReadLine());

                            try
                            {
                                list.Add(str, position);
                            }
                            catch (NotUniqueElementException e)
                            {
                                WriteLine(e.Message);
                            }
                            catch (WrongPositionException e)
                            {
                                WriteLine(e.Message);
                            }

                            break;
                        }
                    case 2:
                        {
                            Write("Enter the element: ");
                            string str = ReadLine();

                            try
                            {
                                list.DeleteElement(str);
                            }
                            catch (CannotDeleteNotExistedElementException e)
                            {
                                WriteLine(e.Message);
                            }

                            break;
                        }
                    case 3:
                        {
                            WriteLine($"The length of the list: {list.GetSize()}");
                            break;
                        }
                    case 4:
                        {
                            Write("Enter the number: ");
                            int number = int.Parse(ReadLine());

                            string meaning = list.GetValueByPosition(number);
                            if (meaning == "")
                            {
                                WriteLine("There`s no such element");
                            }
                            else
                            {
                                WriteLine($"The element number {number}: {meaning}");
                            }
                            break;
                        }
                    case 5:
                        {
                            Write("The list: ");
                            list.PrintList();
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
            UniqueList list = new UniqueList();
            EventLoop(list);
        }
    }
}