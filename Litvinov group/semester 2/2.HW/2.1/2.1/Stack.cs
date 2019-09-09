using System;
using static System.Console;

namespace Stack
{
    class Stack
    {
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

        public Stack()
        {
        }

        public void Push(string meaning)
        {
            top = new Element(meaning, top);
            ++size;
        }

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

        public bool IsEmpty() => top == null;

        public int Size() => size;

        public void Clean()
        {
            size = 0;
            top = null;
        }

        private Element top;
        private int size;
    }
}
