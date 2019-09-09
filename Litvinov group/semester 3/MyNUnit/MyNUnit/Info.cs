using System;

namespace MyNUnit
{
    /// <summary>
    /// Information about the test
    /// </summary>
    public class Info
    {
        /// <summary>
        /// Run test constructor
        /// </summary>
        /// <param name="classType">type of the class containing the test</param>
        /// <param name="testName">The name of the test</param>
        /// <param name="result">whether the test is passed, ignored or failed</param>
        /// <param name="reason">if the test is failed, why</param>
        /// <param name="time">test run time</param>
        public Info(string classType, string testName, string result, string reason, TimeSpan time)
        {
            ClassType = classType;
            TestName = testName;
            Result = result;
            Reason = reason;
            Time = time;
        }

        /// <summary>
        /// Type of the class tested
        /// </summary>
        public string ClassType { get; }

        /// <summary>
        /// The Name of the test
        /// </summary>
        public string TestName { get; }

        /// <summary>
        /// Result of the test (passed, ignored or failed)
        /// </summary>
        public string Result { get; }

        /// <summary>
        /// Reason why the test was ignored
        /// </summary>
        public string Reason { get; }

        /// <summary>
        /// Time spent on the test
        /// </summary>
        public TimeSpan Time { get; }
    }
}
