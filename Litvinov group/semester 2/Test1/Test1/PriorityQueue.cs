using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    /// <summary>
    /// The priority queue
    /// </summary>
    /// <typeparam name="T">Type of the queue</typeparam>
    public class PriorityQueue<T> : IPriorityQueue<T>
    {
        private int size;
        private Element<T> head;
        private Element<T> tail;

        public PriorityQueue()
        {
            size = 0;
            head = null;
            tail = null;
        }

        /// <summary>
        /// Add element to the priority queue
        /// </summary>
        /// <param name="value">Element to be added</param>
        /// <param name="priority">Element`s priority</param>
        public void Enqueue(T value, int priority)
        {
            if (head == null)
            {
                head = new Element<T>(value, priority);
                tail = head;
            }
            else
            {
                tail.Next = new Element<T>(value, priority);
                tail = tail.Next;
            }

            ++size;
        }

        /// <summary>
        /// Get the element with the highest priority and delete it
        /// </summary>
        /// <returns>Wanted element</returns>
        public T Dequeue()
        {
            if (head == null)
            {
                throw new EmptyQueueException("The queue is empty");
            }

            var higherPriority = head.Priority;
            int position = 0;
            var temp = head.Next;
            for (int i = 1; i < size; ++i)
            {
                if (temp.Priority < higherPriority)
                {
                    higherPriority = temp.Priority;
                    position = i;
                }

                temp = temp.Next;
            }

            temp = head;
            if (position == 0)
            {
                head = temp.Next;
                --size;
                return temp.Value;
            }

            temp = head.Next;
            var prev = head;
            for (int i = 1; i < position; ++i)
            {
                temp = temp.Next;
                prev = prev.Next;
            }
            prev.Next = temp.Next;
            --size;
            return temp.Value;
        }
    }
}
