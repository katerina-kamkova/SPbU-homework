using System;
using static System.Console;

namespace List
{
    class List
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

        public void Add(string value, string position)
        {

            int number;
            if (!int.TryParse(position, out number) || number < 1 || number > size + 1)
            {
                Write("The wrong position, element isn`t added");
                return;
            }

            if (size == 0 || number == 1)
            {
                head = new Element(value, head);
            }
            else
            {
                Element pointer = head;
                for (int i = 2; i < number; ++i)
                {
                    pointer = pointer.next;
                }
                pointer.next = new Element(value, pointer.next);
            }

            ++size;
        }

        public void DeleteElement(string position)
        {
            int number;
            if (!int.TryParse(position, out number) || number < 1 || number > size)
            {
                Write("The wrong position, the element isn`t deleted");
                return;
            }

            if (size == 0)
            {
                WriteLine("List`s empty");
                return;
            }

            if (number == 1)
            {
                head = head.next;
            }
            else
            {
                Element pointer = head;
                for (int i = 1; i < number - 1; ++i)
                {
                    pointer = pointer.next;
                }
                pointer.next = pointer.next.next;
            }

            --size;
        }

        public string ReturnString(string position)
        {
            int number;
            if (!int.TryParse(position, out number) || number < 1 || number > size)
            {
                Write("The wrong position, element cannot be found");
                return "";
            }

            if (size == 0)
            {
                return "";
            }

            Element pointer = head;
            for (int i = 1; i < number; ++i)
            {
                pointer = pointer.next;
            }

            return pointer.str;
        }

        public bool IsEmpty() => head == null;

        public int Size() => size;

        public void Clean()
        {
            head = null;
            size = 0;
        }

        public void Print()
        {
            Write("The list: ");
            Element pointer = head;

            for (int i = 0; i < size; ++i)
            {
                Write($"{pointer.str} ");
                pointer = pointer.next;
            }

            WriteLine();
        }

        private Element head;
        private int size;
    }
}