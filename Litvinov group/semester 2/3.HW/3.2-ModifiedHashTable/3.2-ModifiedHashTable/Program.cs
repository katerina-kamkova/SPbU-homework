using System;
using static System.Console;
using static System.Math;

namespace HashTable
{
    class Program
    {
        const ulong max = 18446744073709551615;

        /// <summary>
        /// Function counting hash code
        /// </summary>
        /// <param name="input">String, which hash code we want to find</param>
        /// <returns>Wanted hash code</returns>
        private static ulong HashCode(string input)
        {
            const int primeNumber = 13;
            ulong answer = 0;
            if (input == "")
            {
                return 0;
            }

            var str = input.Split();
            for (int i = 0; i < str.Length; ++i)
            {
                answer = (answer + ulong.Parse(str[i]) * (ulong)Pow(primeNumber, i)) % max;
            }
            return answer;
        }

        /// <summary>
        /// Let the user choose which way he want to count hash code
        /// </summary>
        /// <returns>Wanted hash code</returns>
        private static bool Menu1()
        {
            WriteLine("Choose the type of the hash function:");
            WriteLine("0 - GetHashCode()");
            WriteLine("1 - Another hash function");
            Write("Enter your choice: ");
            return int.Parse(ReadLine()) != 0;
        }

        /// <summary>
        /// Present available options
        /// </summary>
        private static void Menu2()
        {
            WriteLine("HashTable");
            WriteLine();
            WriteLine("Available options:");
            WriteLine("1 - add element");
            WriteLine("2 - check the presence");
            WriteLine("3 - delete element");
            WriteLine("0 - exit the program");
        }

        /// <summary>
        /// Make actions requested by user
        /// </summary>
        /// <param name="table">Used table</param>
        private static void EventLoop(HashTable table)
        {
            Menu2();

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
                            Write("Write the string: ");
                            string str = ReadLine();
                            try
                            {
                                table.AddElement(str);
                            }
                            catch (FormatException e)
                            {
                                WriteLine(e.Message);
                            }
                            break;
                        }
                    case 2:
                        {
                            Write("Write the string: ");
                            string str = ReadLine();
                            bool presence = false;
                            try
                            {
                                presence = table.Check(str);
                            }
                            catch (FormatException e)
                            {
                                WriteLine(e.Message);
                            }
                            if (presence)
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
                            Write("Write the string: ");
                            string str = ReadLine();

                            bool presence = false;
                            try
                            {
                                presence = table.Check(str);
                            }
                            catch (FormatException e)
                            {
                                WriteLine(e.Message);
                            }

                            if (presence)
                            {
                                table.Delete(str);
                            }
                            break;
                        }
                    case 0:
                        {
                            break;
                        }
                    default:
                        {
                            Write("Wrong number, try again: ");
                            break;
                        }
                }
            }
        }

        private static void Main(string[] args)
        {
            HashTable table;
            table = Menu1() ? new HashTable(HashCode) : new HashTable();

            EventLoop(table);
        }
    }
}
