using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackCalculator
{
    /// <summary>
    /// Stack interface
    /// </summary>
    public interface IStack
    {
        /// <summary>
        /// Push the element into stack
        /// </summary>
        /// <param name="value">Element to be added</param>
        void Push(string value);

        /// <summary>
        /// Get the last value and delete it from stack
        /// </summary>
        /// <returns>Wanted value</returns>
        string Pop();

        /// <summary>
        /// Check whether the stack`s empty
        /// </summary>
        /// <returns>Answer</returns>
        bool IsEmpty();

        /// <summary>
        /// get size of the stack
        /// </summary>
        /// <returns>Wanted size</returns>
        int Size();
    }
}
