using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    /// <summary>
    /// Interface for Priority Queue
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPriorityQueue<T>
    {
        /// <summary>
        /// Add element to the priority queue
        /// </summary>
        /// <param name="value">Element to be added</param>
        /// <param name="priority">Element`s priority</param>
        void Enqueue(T value, int priority);

        /// <summary>
        /// Get the element with the highest priority and delete it
        /// </summary>
        /// <returns>Wanted element</returns>
        T Dequeue();
    }
}
