using System;
using System.Threading;
using System.Collections.Concurrent;

namespace MyThreadPool
{
    /// <summary>
    /// Thread pool
    /// Gives an ability to solve tasks in different threads
    /// </summary>
    public class MyThreadPool
    {
        private readonly int threadsAmount;
        private Thread[] threads;
        private CancellationTokenSource cts = new CancellationTokenSource();
        private ConcurrentQueue<Action> taskQueue = new ConcurrentQueue<Action>();
        private Object lockObject = new object();

        /// <summary>
        /// Notifies the thread that there is a new task to be done
        /// </summary>
        private AutoResetEvent takeNewTask = new AutoResetEvent(false);

        /// <summary>
        /// Fields for closing threads
        /// </summary>
        private int closedThreads = 0;
        private AutoResetEvent close = new AutoResetEvent(false);

        /// <summary>
        /// Checks whether threadPool is working
        /// </summary>
        public bool IsAlive { get => !cts.Token.IsCancellationRequested; }

        public MyThreadPool(int n)
        {
            threadsAmount = n;
            CreateThreads();
        }

        /// <summary>
        /// Creates the array of threads and says them to solve the tasks
        /// </summary>
        private void CreateThreads()
        {
            threads = new Thread[threadsAmount];
            for (var i = 0; i < threadsAmount; i++)
            {
                threads[i] = new Thread(() =>
                {
                    while (!cts.Token.IsCancellationRequested)
                    {
                        if (taskQueue.TryDequeue(out Action task))
                        {
                            task();
                        }
                        else
                        {
                            takeNewTask.WaitOne();
                        }
                    }

                    lock (lockObject)
                    {
                        closedThreads++;
                    }
                    close.Set();
                });

                threads[i].IsBackground = true;
                threads[i].Start();
            }
        }

        /// <summary>
        /// Create the task and direct it to adding function
        /// </summary>
        /// <typeparam name="TResult"Result type</typeparam>
        /// <param name="function">Function to be solved in the Task</param>
        /// <returns>New Task</returns>
        public IMyTask<TResult> AddTask<TResult>(Func<TResult> function)
        {
            var newTask = new MyTask<TResult>(function, this);
            return AddTaskToThreadPool(newTask);
        }

        /// <summary>
        /// Adds new task, only for MyTask.ContinueWith
        /// </summary>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="task">Task to be added</param>
        /// <returns>Task</returns>
        private IMyTask<TResult> AddTaskToThreadPool<TResult>(MyTask<TResult> task)
        {
            if (cts.Token.IsCancellationRequested)
            {
                throw new ThreadPoolClosedException();
            }

            taskQueue.Enqueue(task.Calculations);
            takeNewTask.Set();

            return task;
        }
        
        /// <summary>
        /// Cancels working processes
        /// </summary>
        public void Shutdown()
        {
            cts.Cancel();
            takeNewTask.Set();
            while (true)
            {
                close.WaitOne();
                takeNewTask.Set();

                lock (lockObject)
                {
                    if (threadsAmount == closedThreads)
                    {
                        break;
                    }
                }
            }

            taskQueue = null;
        }

        /// <summary>
        /// Represents the task, that can be fullfilled
        /// Can show the status of work, return result and start solving new task using old result
        /// </summary>
        /// <typeparam name="TResult">Result type</typeparam>
        private class MyTask<TResult> : IMyTask<TResult>
        {
            private volatile bool isCompleted;
            private TResult result;
            private Func<TResult> function;
            private Exception exception;
            private MyThreadPool threadPool;
            private AutoResetEvent getResult = new AutoResetEvent(false);
            private ManualResetEvent taskAddedToQueue = new ManualResetEvent(true);
            private ConcurrentQueue<Action> waitResultQueue = new ConcurrentQueue<Action>();
            private Object lockObject = new Object();

            public MyTask(Func<TResult> function, MyThreadPool threadPool)
            {
                this.function = function;
                this.threadPool = threadPool;
            }

            /// <summary>
            /// Wheather the task is completed
            /// </summary>
            public bool IsCompleted => isCompleted;

            /// <summary>
            /// Returns task result
            /// </summary>
            public TResult Result
            {
                get
                {
                    getResult.WaitOne();
                    
                    if (exception == null)
                    {
                        return result;
                    }

                    throw new AggregateException(exception);
                }
            }

            /// <summary>
            /// Completing the function
            /// </summary>
            public void Calculations()
            {
                try
                {
                    result = function();
                } 
                catch (Exception e)
                {
                    exception = e;
                }

                lock (lockObject)
                {
                    getResult.Set();
                    isCompleted = true;

                    FinishContinueWith();
                }
            }

            /// <summary>
            /// Creates new task using result of previous task
            /// </summary>
            /// <typeparam name="TNewResult">Type of the result of new function</typeparam>
            /// <param name="newFunc">Function for new task</param>
            /// <returns>new task</returns>
            public IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> newFunction)
            {
                var newTask = new MyTask<TNewResult>(() => newFunction(result), threadPool);
                lock (lockObject)
                { 
                    if (!IsCompleted)
                    {
                        waitResultQueue.Enqueue(() => threadPool.AddTaskToThreadPool(newTask));
                        return newTask;
                    }
                }
                return threadPool.AddTaskToThreadPool(newTask);
            }

            /// <summary>
            /// When the result`s calculated create waiting new tasks
            /// </summary>
            private void FinishContinueWith()
            {
                if (waitResultQueue.Count == 0)
                {
                    return;
                }

                if (exception == null)
                {
                    if (!threadPool.IsAlive)
                    {
                        waitResultQueue = null;
                        return;
                    }

                    foreach (Action action in waitResultQueue)
                    {
                        action();
                    }

                    waitResultQueue = null;
                }
                else
                {
                    throw new AggregateException(exception);
                }
            }
        }
    }
}