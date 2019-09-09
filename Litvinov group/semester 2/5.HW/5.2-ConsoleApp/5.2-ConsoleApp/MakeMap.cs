using System.IO;
using static System.Console;

namespace ConsoleApp
{
    public class MakeMap
    {
        /// <summary>
        /// Get the map from the file
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <returns>The map</returns>
        public char[,] Input(string path)
        {
            string input;
            using (var stream = new StreamReader(path))
            {
                input = stream.ReadToEnd();
            }

            int length = 0;
            int hight = 0;

            foreach (char element in input)
            {
                length++;
                if (element == '\n')
                {
                    break;
                }
            }
            hight = (input.Length + 2) / length;
            length -= 2;

            char[,] matrix = new char[hight, length];

            var index = 0;
            for (int i = 0; i < hight; ++i)
            {
                for (int j = 0; j < length; ++j)
                {
                    matrix[i, j] = input[index];
                    index++;
                }
                index += 2;
            }

            return matrix;
        }

        /// <summary>
        /// Print map with coords
        /// </summary>
        /// <param name="map">The map</param>
        public void PrintMapWithCoords(char[,] map)
        {
            WriteLine();

            WriteLine("The map: ");
            for (int i = 0; i < map.GetLength(0); ++i)
            {
                if ((map.GetLength(0) - i) % 10 == 0)
                {
                    Write((map.GetLength(0) - i) / 10);
                }
                else
                {
                    Write(" ");
                }

                Write((map.GetLength(0) - i) % 10);
                for (int j = 0; j < map.GetLength(1); ++j)
                {
                    Write(map[i, j]);
                    if (j == map.GetLength(1) - 1)
                    {
                        WriteLine();
                    }
                }
            }
            Write(" ");

            for (int i = 0; i < map.GetLength(1); ++i)
            {
                Write(i % 10);
            }
            WriteLine();
            Write(" ");

            for (int i = 0; i < map.GetLength(1); ++i)
            {
                if (i % 10 == 0)
                {
                    Write(i / 10);
                }
                else
                {
                    Write(" ");
                }
            }
        }
    }
}
