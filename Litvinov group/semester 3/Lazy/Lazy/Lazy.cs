using System;

namespace Lazy
{
    /// <summary>
    /// Lazy calculations
    /// </summary>
    /// <typeparam name="T">The type of the answer</typeparam>
    public class Lazy<T> : ILazy<T>
    {
        private bool calledBefore;
        private T result;
        private Func<T> supplier;

        /// <summary>
        /// Get the function
        /// </summary>
        /// <param name="supplier">function</param>
        public Lazy(Func<T> supplier)
        {
            this.supplier = supplier;
        }

        /// <summary>
        /// If called for the first time - calculate and return the result,
        /// else return already calculated result
        /// </summary>
        /// <returns>the result of the calculations</returns>
        public T Get()
        {
            if (calledBefore)
            {
                return result;
            }

            calledBefore = true;
            result = supplier();
            supplier = null;
            return result;
        }
    }
}
