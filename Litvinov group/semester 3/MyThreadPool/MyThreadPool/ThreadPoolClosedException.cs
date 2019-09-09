using System;

namespace MyThreadPool
{
    public class ThreadPoolClosedException : Exception
    {
        /// <summary>
        /// Exception that is thrown whe MyThreadPool`s off
        /// </summary>
        /// <param name="message"></param>
        public ThreadPoolClosedException(string message = "ThreadPool is off")
            : base(message)
        { }
    }
}
