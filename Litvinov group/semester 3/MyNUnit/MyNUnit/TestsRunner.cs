namespace MyNUnit
{
    using System;
    using Attributes;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Reflection;
    using System.Diagnostics;

    /// <summary>
    /// Runs all tests on given pathes
    /// </summary>
    public class TestsRunner
    {
        /// <summary>
        /// Information about each test
        /// </summary>
        private ConcurrentQueue<Info> infoQueue = new ConcurrentQueue<Info>();
        private Object lockObject = new Object();

        /// <summary>
        /// Executes all the tests on given path
        /// </summary>
        /// <param name="asmPath">Array of the pathes to the assemblies</param>
        /// <returns>Concurrent queue of test info</returns>
        public Info[] ExecuteTests(string[] asmPath)
        {
            Parallel.ForEach(asmPath, (path) =>
            {
                var assembly = Assembly.LoadFrom(Convert.ToString(path));
                foreach (var type in assembly.ExportedTypes)
                {
                    var lists = FillQueues(type);

                    var reason = ExecuteNotTestMethods(lists.BeforeClass, type);
                    if (!Equals(reason, ""))
                    {
                        foreach (var test in lists.Tests)
                        {
                            infoQueue.Enqueue(new Info(type.Name, test.Name, "Failed", reason, default(TimeSpan)));
                        }
                        continue;
                    }

                    var curQueue = new ConcurrentQueue<Info>();
                    Parallel.ForEach(lists.Tests, (method) => ExecuteTest(type, method, lists, curQueue));

                    reason = ExecuteNotTestMethods(lists.AfterClass, type);
                    while (!curQueue.IsEmpty)
                    {
                        curQueue.TryDequeue(out Info info);
                        if (Equals(info.Result, "Passed") && !Equals(reason, ""))
                        {
                            infoQueue.Enqueue(new Info(info.ClassType, info.TestName, "Failed", reason, info.Time));
                        }
                        else
                        {
                            infoQueue.Enqueue(info);
                        }
                    }
                }
            });

            return infoQueue.ToArray();
        }

        /// <summary>
        /// Find all methods in the class with needed attributes
        /// </summary>
        /// <param name="type">class to look in</param>
        /// <returns>listed methods according to their categories</returns>
        private Lists FillQueues(Type type)
        {
            var lists = new Lists();
            foreach (var methodInfo in type.GetMethods())
            {
                foreach (var attribute in Attribute.GetCustomAttributes(methodInfo))
                {
                    if (attribute.GetType() == typeof(BeforeClassAttribute))
                    {
                        lists.BeforeClass.Add(methodInfo);
                    }
                    if (attribute.GetType() == typeof(BeforeAttribute))
                    {
                        lists.Before.Add(methodInfo);
                    }
                    if (attribute.GetType() == typeof(TestAttribute))
                    {
                        lists.Tests.Add(methodInfo);
                    }
                    if (attribute.GetType() == typeof(AfterAttribute))
                    {
                        lists.After.Add(methodInfo);
                    }
                    if (attribute.GetType() == typeof(AfterClassAttribute))
                    {
                        lists.AfterClass.Add(methodInfo);
                    }
                }
            }
            return lists;
        }

        /// <summary>
        /// Execute not test methods, declare all tests failed if one of these metods throws the exception
        /// </summary>
        /// <param name="list">given list of metods</param>
        /// <param name="type">type of the class</param>
        /// <param name="instance">Instance in witch methods are run</param>
        /// <returns>reason of fail else ""</returns>
        private string ExecuteNotTestMethods(List<MethodInfo> list, Type type, object instance = null)
        {
            foreach (var method in list)
            {
                if (instance == null && !method.IsStatic)
                {
                    return $"{method.Name} must be static";
                }

                try
                {
                    lock (lockObject)
                    {
                        var result = method.Invoke(instance, Array.Empty<object>());
                    }
                }
                catch (Exception e)
                {
                    return $"{method.Name} method has thrown {e.InnerException.GetType().ToString()}, message: {e.InnerException.Message}";
                }
            }
            return "";
        }

        /// <summary>
        /// Executes all before methods, test and finally all after methods, enqueue results in current queue
        /// </summary>
        /// <param name="type">methods class type</param>
        /// <param name="methodInfo">wanted method</param>
        /// <param name="lists">listed methods according to their categories</param>
        /// <param name="curQueue">info queue of current class</param>
        private void ExecuteTest(Type type, MethodInfo methodInfo, Lists lists, ConcurrentQueue<Info> curQueue)
        {
            object instance = Activator.CreateInstance(type);

            var reason = ExecuteNotTestMethods(lists.Before, type, instance);
            if (!Equals(reason, ""))
            {
                curQueue.Enqueue(new Info(type.Name, methodInfo.Name, "Failed", reason, default(TimeSpan)));
                return;
            }

            var info = ExecuteTestMethod(type, methodInfo, instance);

            reason = ExecuteNotTestMethods(lists.After, type, instance);
            if (Equals(info.Result, "Passed") && !Equals(reason, ""))
            {
                curQueue.Enqueue(new Info(type.Name, methodInfo.Name, "Failed", reason, info.Time));
                return;
            }

            curQueue.Enqueue(info);
        }

        /// <summary>
        /// Executes the test, collects the results
        /// </summary>
        /// <param name="type">test class type</param>
        /// <param name="method">test</param>
        /// <returns>info about the given test</returns>
        private Info ExecuteTestMethod(Type type, MethodInfo method, object instance)
        {
            var typeName = Convert.ToString(type.Name);
            var testName = Convert.ToString(method.Name);

            var attributeProperties = method.GetCustomAttribute<TestAttribute>();
            if (attributeProperties.Ignore != null)
            {
                string message = attributeProperties.Ignore;
                return new Info(typeName, testName, "Ignored", message, default(TimeSpan));
            }

            var stopWatch = new Stopwatch();
            var result = "Passed";
            string reason = null;
            try
            {
                lock (lockObject)
                {
                    stopWatch.Start();
                    method.Invoke(instance, Array.Empty<object>());
                    stopWatch.Stop();
                }
            }
            catch (Exception e)
            {
                stopWatch.Stop();
                var exceptionType = e.InnerException.GetType();
                if (exceptionType != attributeProperties.Expected)
                {
                    result = "Failed";
                    reason = $"The test has thrown the {exceptionType.ToString()}. Exception message is: {e.Message}";
                }

                return new Info(typeName, testName, result, reason, stopWatch.Elapsed);
            }

            if (attributeProperties.Expected != null)
            {
                result = "Failed";
                reason = $"The test hasn`t thrown the {attributeProperties.Expected.ToString()}";
            }

            return new Info(typeName, testName, result, reason, stopWatch.Elapsed);
        }
    }
}
