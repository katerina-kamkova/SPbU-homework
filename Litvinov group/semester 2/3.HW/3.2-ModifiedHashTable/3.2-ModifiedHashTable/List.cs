namespace HashTable
{
    /// <summary>
    /// List of elements
    /// </summary>
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

        /// <summary>
        /// Add value to the list on certain position
        /// </summary>
        /// <param name="value">Value to be added</param>
        /// <param name="position">The position of the added value</param>
        public void Add(string value, int position)
        {
            if (position < 1 || position > size + 1)
            {
                throw new WrongIndexException();
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

        /// <summary>
        /// Delete value from the list
        /// </summary>
        /// <param name="str">Value to be deleted</param>
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

            throw new NonexistentElementException();
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
                throw new WrongIndexException();
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
                if (Equals(pointer.str, value))
                {
                    return true;
                }
                pointer = pointer.next;
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
                if (Equals(pointer.str, value))
                {
                    return counter;
                }

                pointer = pointer.next;
                ++counter;
            }

            return 0;
        }

        /// <summary>
        /// Get the length of the list
        /// </summary>
        /// <returns>Length of the list</returns>
        public int GetSize() => size;

        private Element head;
        private int size;
    }
}