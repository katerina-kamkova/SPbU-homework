using System.Collections.Generic;
using System.Collections;

namespace _7._1
{
    /// <summary>
    /// Generic List
    /// Collection of elements of T type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class List<T> : IList<T>
    {
        private Element head;
        private Element tail;

        /// <summary>
        /// The size of the list
        /// </summary>
        public int Count { get; private set; } = 0;

        /// <summary>
        /// Check whether we can only read the list
        /// </summary>
        public bool IsReadOnly { get; set; } = false;

        /// <summary>
        /// Get the value of the element by it`s index
        /// or change the value of the element by it`s index
        /// </summary>
        /// <param name="i">Index</param>
        /// <returns>The value of the element</returns>
        public T this[int i]
        {
            get
            {
                if (i >= 0 && i <= Count)
                {
                    var temp = head;
                    for (var j = 0; j < i; j++)
                    {
                        temp = temp.Next;
                    }
                    return temp.Value;
                }
                else
                {
                    throw new WrongIndexException();
                }
            }
            set
            {
                if (IsReadOnly)
                {
                    throw new ReadOnlyException();
                }

                if (i >= 0 && i <= Count)
                {
                    var temp = head;
                    for (var j = 0; j < i; j++)
                    {
                        temp = temp.Next;
                    }
                    temp.Value = value;
                }
                else
                {
                    throw new WrongIndexException();
                }
            }
        }

        /// <summary>
        /// Add the elemen into the end of the list
        /// </summary>
        /// <param name="element">The value of the element</param>
        public void Add(T element)
        {
            if (IsReadOnly)
            {
                throw new ReadOnlyException();
            }

            if (Count == 0)
            {
                head = new Element(null, element);
                tail = head;
            }
            else
            {
                tail.Next = new Element(null, element);
                tail = tail.Next;
            }

            Count++;
        }

        /// <summary>
        /// Delete all elements from the list
        /// </summary>
        public void Clear()
        {
            if (IsReadOnly)
            {
                throw new ReadOnlyException();
            }

            head = null;
            tail = null;
            Count = 0;
        }

        /// <summary>
        /// Check whether the element with this value exists
        /// </summary>
        /// <param name="element">The value of the element</param>
        /// <returns>The answer to the question</returns>
        public bool Contains(T element)
        {
            var temp = head;
            while (temp != null && !Equals(temp.Value, element))
            {
                temp = temp.Next;
            }

            return temp != null;
        }

        /// <summary>
        /// Copy every element starting with the concrete element
        /// </summary>
        /// <param name="array">The array that will be filled</param>
        /// <param name="index">Index of the first element in the array</param>
        public void CopyTo(T[] array, int index)
        {
            if (index < 0 || index >= array.Length)
            {
                throw new WrongIndexException();
            }

            if (array.Length - (index + Count) < 0)
            {
                throw new System.IndexOutOfRangeException();
            }

            foreach (var element in this)
            {
                array[index] = element;
                index++;
            }
        }

        /// <summary>
        /// Get the enumerator
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(head);
        }

        /// <summary>
        /// Get enumerator for the container
        /// </summary>
        /// <returns>Enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Defines the index of the first element with given value
        /// </summary>
        /// <param name="element">The element</param>
        /// <returns>The index</returns>
        public int IndexOf(T element)
        {
            var answer = 0;
            var temp = head;

            while (temp != null && !Equals(temp.Value, element))
            {
                temp = temp.Next;
                answer++;
            }

            if (temp == null)
            {
                throw new NonexistentElementException();
            }

            return answer;

        }

        /// <summary>
        /// Insert the element into the list by the given index
        /// </summary>
        /// <param name="index">Given index</param>
        /// <param name="element">Given element</param>
        public void Insert(int index, T element)
        {
            if (IsReadOnly)
            {
                throw new ReadOnlyException();
            }

            if (index == 0)
            {
                head = new Element(head, element);
                Count++;
                return;
            }

            if (index > Count + 1 || index < 0)
            {
                throw new WrongIndexException();
            }

            var temp = head;
            for (var i = 1; i < index; i++)
            {
                temp = temp.Next;
            }
            temp.Next = new Element(temp.Next, element);
            Count++;
        }


        /// <summary>
        /// Removes the first occurrence of a specific object from the list
        /// </summary>
        /// <param name="element">Given element</param>
        public bool Remove(T element)
        {
            if (IsReadOnly)
            {
                throw new ReadOnlyException();
            }

            if (Count == 0)
            {
                throw new EmptyListException();
            }

            if (Equals(head.Value, element))
            {
                head = head.Next;
                Count--;
                return true;
            }

            var temp = head;
            while (temp.Next != null && !Equals(temp.Next.Value, element))
            {
                temp = temp.Next;
            }

            if (temp.Next == null)
            {
                throw new NonexistentElementException();
            }

            if (temp.Next == tail)
            {
                tail = temp;
            }
            temp.Next = temp.Next.Next;
            Count--;
            return true;
        }

        /// <summary>
        /// Delete the element by index
        /// </summary>
        /// <param name="index">Given index</param>
        public void RemoveAt(int index)
        {
            if (IsReadOnly)
            {
                throw new ReadOnlyException();
            }

            if (Count == 0)
            {
                throw new EmptyListException();
            }

            if (index < 0 || index > Count)
            {
                throw new WrongIndexException();
            }

            if (index == 0)
            {
                head = head.Next;
                Count--;
                return;
            }

            var temp = head;
            for (int i = 1; i < index; i++)
            {
                temp = temp.Next;
            }

            if (index + 1 == Count)
            {
                tail = temp;
            }
            temp.Next = temp.Next.Next;
            Count--;
        }

        /// <summary>
        /// Element for List
        /// </summary>
        private class Element
        {
            /// <summary>
            /// Get and set the pointer to the next element
            /// </summary>
            public Element Next { get; set; }

            /// <summary>
            /// Get and set the value of the element
            /// </summary>
            public T Value { get; set; }

            /// <summary>
            /// Constructor, so user can add values during creating new copy of element
            /// </summary>
            /// <param name="next">Reference to the next element</param>
            /// <param name="value">Value</param>
            public Element(Element next, T value)
            {
                Next = next;
                Value = value;
            }
        }

        /// <summary>
        /// The class that supports a simple iteration over a generic collection
        /// </summary>
        private class Enumerator : IEnumerator<T>
        {
            private Element head;
            private Element current;

            /// <summary>
            /// Gets the reference to the first element of the list
            /// </summary>
            /// <param name="head">The first element of the list</param>
            public Enumerator(Element head)
            {
                this.head = head;
            }

            /// <summary>
            /// Get the value of the current 
            /// </summary>
            public T Current { get => current.Value; }
            object IEnumerator.Current { get => Current; }

            public void Dispose() { }

            /// <summary>
            /// Moves the reference to the current position to the next position
            /// </summary>
            /// <returns>Whether the current element isn`t the last</returns>
            public bool MoveNext()
            {
                if (head == null)
                {
                    return false;
                }

                if (current == null)
                {
                    current = head;
                    return true;
                }

                if (current.Next == null)
                {
                    return false;
                }

                current = current.Next;
                return true;
            }

            /// <summary>
            /// Start looking through the list feom the beginning
            /// </summary>
            public void Reset()
            {
                current = null;
            }
        }
    }
}
