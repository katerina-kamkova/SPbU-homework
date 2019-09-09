using System;

namespace MyThreadPool
{
    /// <summary>
    /// Interface for MyTask
    /// Represents the task, that can be fullfilled
    /// Can show the status of work, return result and start solving new task using old result
    /// </summary>
    /// <typeparam name="TResult">Result type</typeparam>
    public interface IMyTask<TResult>
    {
        /// <summary>
        /// Wheather the task is completed
        /// </summary>
        bool IsCompleted { get; }

        /// <summary>
        /// Returns task result
        /// </summary>
        TResult Result { get; }

        /// <summary>
        /// Creates new task using result of previous task
        /// </summary>
        /// <typeparam name="TNewResult">Type of the result of new function</typeparam>
        /// <param name="newFunc">Function for new task</param>
        /// <returns>new task</returns>
        IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> newFunc);
    }
}
