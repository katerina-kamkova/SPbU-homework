using static System.Console;

namespace UniqueList
{
    /// <summary>
    /// List of elements
    /// </summary>
    public class List : IList
    {
        /// <summary>
        /// Element for List
        /// </summary>
        private class Element
        {
            public Element(string value, Element next)
            {
                Value = value;
                Next = next;
            }

            /// <summary>
            /// Method to get and set the pointer to the next element
            /// </summary>
            public Element Next { get; set; }

            /// <summary>
            /// Method to get and set the value of the element
            /// </summary>
            public string Value { get; set; }
        }

        /// <summary>
        /// Add value to the list on certain position
        /// </summary>
        /// <param name="value">Value to be added</param>
        /// <param name="position">The position of the added value</param>
        public virtual void Add(string value, int position)
        {
            if (position < 1 || position > size + 1)
            {
                throw new WrongPositionException();
            }

            if (size == 0 || position == 1)
            {
                var next = head;
                head = new Element(value, next);
            }
            else
            {
                Element pointer = head;
                for (int i = 2; i < position; ++i)
                {
                    pointer = pointer.Next;
                }
                var next = pointer.Next;
                pointer.Next = new Element(value, next);
            }

            ++size;
        }

        /// <summary>
        /// Delete value from the list
        /// </summary>
        /// <param name="str">Value to be deleted</param>
        public void DeleteElement(string str)
        {
            if (size != 0)
            {
                if (Equals(head.Value, str))
                {
                    head = head.Next;
                    --size;
                    return;
                }

                Element pointer = head;
                while (pointer.Next != null)
                {
                    if (Equals(pointer.Next.Value, str))
                    {
                        pointer.Next = pointer.Next.Next;
                        --size;
                        return;
                    }
                    pointer = pointer.Next;
                }
            }

            throw new CannotDeleteNotExistedElementException();
        }

        /// <summary>
        /// Get the string by its position
        /// </summary>
        /// <param name="position">Position of the wanted element</param>
        /// <returns>Wanted value</returns>
        public string GetValueByPosition(int position)
        {
            if (position < 1 || position > size)
            {
                throw new WrongPositionException();
            }
            
            Element pointer = head;
            for (int i = 1; i < position; ++i)
            {
                pointer = pointer.Next;
            }

            return pointer.Value;
        }

        /// <summary>
        /// Check whether the list is empty
        /// </summary>
        /// <returns>True if the list empty, else false</returns>
        public bool IsEmpty() => head == null;

        /// <summary>
        /// Check whether the value is in the list
        /// </summary>
        /// <param name="value">Verifiable value</param>
        /// <returns>True if the value is in the list, else false</returns>
        public bool CheckPresence(string value)
        {
            Element pointer = head;
            while (pointer != null)
            {
                if (Equals(pointer.Value, value))
                {
                    return true;
                }
                pointer = pointer.Next;
            }
            return false;
        }

        /// <summary>
        /// Get the position of the value
        /// </summary>
        /// <param name="value">Value, which position we want to get</param>
        /// <returns>Wanted position</returns>
        public int GetPositionByValue(string value)
        {
            Element pointer = head;
            int counter = 1;
            while (pointer != null)
            {
                if (Equals(pointer.Value, value))
                {
                    return counter;
                }

                pointer = pointer.Next;
                ++counter;
            }

            return 0;
        }

        /// <summary>
        /// Get the length of the list
        /// </summary>
        /// <returns>Length of the list</returns>
        public int GetSize() => size;

        /// <summary>
        /// Print the list
        /// </summary>
        public void PrintList()
        {
            Element current = head;
            for (int i = 0; i < size; ++i)
            {
                Write($"{current.Value} ");
                current = current.Next;
            }
        } 

        private Element head;
        private int size;
    }
}