using static System.Console;

namespace HashTable
{
    public class List
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

        public void Add(string value, int position)
        {
            if (position < 1 || position > size + 1)
            {
                WriteLine("The wrong position, element wasn`t added");
                return;
            }

            if (size == 0 || position == 1)
            {
                head = new Element(value, head);
            }
            else
            {
                Element pointer = head;
                for (int i = 2; i < position; ++i)
                {
                    pointer = pointer.next;
                }
                pointer.next = new Element(value, pointer.next);
            }

            ++size;
        }

        public void DeleteElement(string str)
        {
            if (size != 0)
            {
                if (Equals(head.str, str))
                { 
                    head = head.next;
                    --size;
                    return;
                }

                Element pointer = head;
                while (pointer.next != null)
                {
                    if (Equals(pointer.next.str, str))
                    {
                        pointer.next = pointer.next.next;
                        --size;
                        return;
                    }
                    pointer = pointer.next;
                }
            }

            WriteLine("There`s no such element");
        }

        public string ReturnString(int position)
        {
            if (position < 1 || position > size)
            {
                WriteLine("The wrong position, element wasn`t found");
                return "";
            }

            if (size == 0)
            {
                return "";
            }

            Element pointer = head;
            for (int i = 1; i < position; ++i)
            {
                pointer = pointer.next;
            }

            return pointer.str;
        }

        public bool IsEmpty() => head == null;

        public bool CheckPresence(string value)
        {
            Element pointer = head;
            while (pointer != null)
            {
                if (Equals(pointer.str, value))
                {
                    return true;
                }
                pointer = pointer.next;
            }
            return false;
        }

        public int ReturnNumber(string value)
        {
            Element pointer = head;
            int counter = 1;
            while (pointer != null)
            {
                if (Equals(pointer.str, value))
                {
                    return counter;
                }

                pointer = pointer.next;
                ++counter;
            }

            return 0;
        }

        public int GetSize() => size;

        private Element head;
        private int size;
    }
}