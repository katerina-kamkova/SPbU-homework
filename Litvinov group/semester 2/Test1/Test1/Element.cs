using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    /// <summary>
    /// Priority queue element
    /// </summary>
    /// <typeparam name="T">Type of the Element</typeparam>
    public class Element<T>
    {
        public T Value { get; }
        public int Priority { get; }
        public Element<T> Next { get; set; } = null;

        public Element(T value, int priority)
        {
            Value = value;
            Priority = priority;
        }
    }
}
