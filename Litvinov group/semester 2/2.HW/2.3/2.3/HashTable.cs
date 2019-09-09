using static System.Math;

namespace HashTable
{
    public class HashTable
    {
        public void AddElement(string value)
        {
            int number = Abs(value.GetHashCode()) % length;
            table[number].Add(value, 1);
        }

        public bool Check(string value)
        {
            int number = Abs(value.GetHashCode()) % length;
            return table[number].CheckPresence(value);
        }

        public void Delete(string value)
        {
            int number = Abs(value.GetHashCode()) % length;
            table[number].DeleteElement(value);
        }

        public HashTable()
        {
            table = new List[length];
            for (int i = 0; i < length; ++i)
            {
                table[i] = new List();
            }
        }

        private const int length = 100;
        private List[] table;
        private const int primeNumber = 13;
    }
}
