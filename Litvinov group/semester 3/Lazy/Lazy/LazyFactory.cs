using System;

namespace Lazy
{
    /// <summary>
    /// Class that creates Lazy classes based on function supplier
    /// </summary>
    public class LazyFactory
    {
        /// <summary>
        /// Create Lazy class without multithreading
        /// </summary>
        /// <typeparam name="T">Answer type</typeparam>
        /// <param name="supplier">Calculations function</param>
        /// <returns>Result of the calculations</returns>
        public static Lazy<T> CreateLazy<T>(Func<T> supplier) => new Lazy<T>(supplier);

        /// <summary>
        /// Create MultiThreadLazy class with semithreading
        /// </summary>
        /// <typeparam name="T">Answer type</typeparam>
        /// <param name="supplier">Calculations function</param>
        /// <returns>Result of the calculations</returns>
        public static MultiThreadLazy<T> CreateMultiThreadLazy<T>(Func<T> supplier) 
            => new MultiThreadLazy<T>(supplier);
    }
}
