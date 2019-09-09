using static System.Console;

namespace HashTable
{
    class Program
    {
        private static void Menu()
        {
            WriteLine("Available options: ");
            WriteLine("1 - add element");
            WriteLine("2 - check the presence");
            WriteLine("3 - delete element");
            WriteLine("0 - exit the program");
        }

        private static void EventLoop(HashTable table)
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
                            WriteLine("Write the string: ");
                            string str = ReadLine();
                            table.AddElement(str);
                            break;
                        }
                    case 2:
                        {
                            WriteLine("Write the string: ");
                            string str = ReadLine();
                            if (table.Check(str))
                            {
                                WriteLine("There`s such string in the hash table");
                            }
                            else
                            {
                                WriteLine("There`s no such string in the hash table");
                            }
                            break;
                        }
                    case 3:
                        {
                            WriteLine("Write the string: ");
                            string str = ReadLine();
                            while (!table.Check(str))
                            {
                                WriteLine("There`s no such string in hash table, try again: ");
                                str = ReadLine();
                            }
                            table.Delete(str);
                            break;
                        }
                    case 0:
                        {
                            break;
                        }
                    default:
                        {
                            WriteLine("Wrong number, try again: ");
                            break;
                        }
                }
            }
        }

        private static void Main(string[] args)
        {
            HashTable table = new HashTable();
            EventLoop(table);
        }
    }
}
