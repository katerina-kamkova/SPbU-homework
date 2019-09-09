using System;

namespace Lazy
{
    /// <summary>
    /// Lazy calculation with the protection from mulrithreading
    /// </summary>
    /// <typeparam name="T">the type of the answer</typeparam>
    public class MultiThreadLazy<T> : ILazy<T>
    {
        private volatile bool calledBefore;
        private T result;
        private Func<T> supplier;
        private Object lockObject = new Object();

        /// <summary>
        /// Get the function
        /// </summary>
        /// <param name="supplier">function</param>
        public MultiThreadLazy(Func<T> supplier)
        {
            this.supplier = supplier;
        }

        /// <summary>
        /// If called for the first time - calculate and return the result
        /// and be shure, that only one thread will calculate and change calledBefore,
        /// else return already calculated result
        /// </summary>
        /// <returns>the result of the calculations</returns>
        public T Get()
        {
            if (!calledBefore)
            {
                lock (lockObject)
                {
                    if (!calledBefore)
                    {
                        result = supplier();
                        calledBefore = true;
                        supplier = null;
                    }
                }
            }

            return result;
        }
    }
}
