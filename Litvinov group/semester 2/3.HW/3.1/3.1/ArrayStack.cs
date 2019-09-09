using static System.Console;

namespace StackCalculator
{
    /// <summary>
    /// Stack on array
    /// </summary>
    public class ArrayStack : IStack
    {
        public ArrayStack()
        {
            size = 0;
            arraySize = 20;
            top = -1;
            array = new string[arraySize];
        }

        /// <summary>
        /// Push the element into stack
        /// </summary>
        /// <param name="value">Element to be added</param>
        public void Push(string value)
        {
            top = (top + 1) % arraySize;
            array[top] = value;
            ++size;

            if (size == arraySize)
            {
                string[] oldArray = array;
                array = new string[2 * arraySize];
                for (int i = 0; i < arraySize; ++i)
                {
                    top = (top + 1) % arraySize;
                    array[i] = oldArray[top];
                }

                top = arraySize;
                arraySize *= 2;
            }
        }

        /// <summary>
        /// Get the last value and delete it from stack
        /// </summary>
        /// <returns>Wanted value</returns>
        public string Pop()
        {
            if (IsEmpty())
            {
                WriteLine("Error! Stack`s empty");
                return "";
            }

            string answer = array[top];
            array[top] = "";
            top = (top - 1) % arraySize;
            --size;
            return answer;
        }

        /// <summary>
        /// Check whether the stack`s empty
        /// </summary>
        /// <returns>Answer</returns>
        public bool IsEmpty() => size == 0;

        /// <summary>
        /// get size of the stack
        /// </summary>
        /// <returns>Wanted size</returns>
        public int Size() => size;
        
        private int size;
        private int arraySize;
        private int top;
        private string[] array;
    }
}
