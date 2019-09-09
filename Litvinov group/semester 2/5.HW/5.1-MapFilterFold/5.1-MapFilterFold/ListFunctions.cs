using System;
using System.Collections.Generic;

namespace MapFilterFold
{
    /// <summary>
    /// Class for Map, Filter and Folder
    /// </summary>
    public class ListFunctions
    {
        /// <summary>
        /// The function which changes each element in list using given function
        /// </summary>
        /// <typeparam name="TIn">Type of input elements</typeparam>
        /// <typeparam name="TOut">Type of output elements</typeparam>
        /// <param name="list">List of elements which will be changed</param>
        /// <param name="func">Function that changes each element</param>
        /// <returns>New list contains changed elements</returns>
        public List<TOut> Map<TIn, TOut>(List<TIn> list, Func<TIn, TOut> func)
        {
            var newList = new List<TOut>();
            foreach (var element in list)
            {
                newList.Add(func(element));
            }
            return newList;
        }

        /// <summary>
        /// Function that chooses elements, suitable according to given function
        /// </summary>
        /// <typeparam name="T">Type of elements</typeparam>
        /// <param name="list">List of elements which will be filtered</param>
        /// <param name="func">The function which filters elements</param>
        /// <returns>Filtered list</returns>
        public List<T> Filter<T>(List<T> list, Func<T, bool> func)
        {
            var newList = new List<T>();
            foreach (var element in list)
            {
                if (func(element))
                {
                    newList.Add(element);
                }
            }
            return newList;
        }

        /// <summary>
        /// Count the number using list elements, given number and given function
        /// </summary>
        /// <typeparam name="TIn">Type of input elements</typeparam>
        /// <typeparam name="TOut">Type of output elements</typeparam>
        /// <param name="list">List of elements</param>
        /// <param name="current">Given number</param>
        /// <param name="func">Given function</param>
        /// <returns>Result</returns>
        public TOut Fold<TIn, TOut>(List<TIn> list, TOut current, Func<TOut, TIn, TOut> func)
        {
            foreach (var element in list)
            {
                current = func(current, element);
            }
            return current;
        }
    }
}
