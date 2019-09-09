using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    /// <summary>
    /// Exception which is thrown when user`s trying to delete the element from the empty queue
    /// </summary>
    public class EmptyQueueException : Exception
    {
        public EmptyQueueException(string message)
            : base(message)
        { }
    }
}
