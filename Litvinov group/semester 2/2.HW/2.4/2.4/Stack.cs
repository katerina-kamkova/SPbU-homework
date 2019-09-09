using static System.Console;

namespace StackCalculator
{
    /// <summary>
    /// Stack on pointers
    /// </summary>
    public class Stack : IStack
    {
        /// <summary>
        /// The ELement for stack
        /// </summary>
        private class Element
        {
            public Element(string str, Element next)
            {
                this.next = next;
                this.str = str;
            }

            public Element next;
            public string str;
        }

        /// <summary>
        /// Push the element into stack
        /// </summary>
        /// <param name="value">Element to be added</param>
        public void Push(string value)
        {
            top = new Element(value, top);
            ++size;
        }

        /// <summary>
        /// Get the last value and delete it from stack
        /// </summary>
        /// <returns>Wanted value</returns>
        public string Pop()
        {
            if (top == null)
            {
                WriteLine("Stack`s empty");
                return "";
            }

            Element element = top;
            top = top.next;
            --size;

            return element.str;
        }

        /// <summary>
        /// Check whether the stack`s empty
        /// </summary>
        /// <returns>Answer</returns>
        public bool IsEmpty() => top == null;

        /// <summary>
        /// get size of the stack
        /// </summary>
        /// <returns>Wanted size</returns>
        public int Size() => size;

        private Element top;
        private int size;
    }
}
