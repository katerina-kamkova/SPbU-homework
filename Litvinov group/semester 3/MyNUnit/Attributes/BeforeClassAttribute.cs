using System;

namespace Attributes
{
    /// <summary>
    /// Attribute for methods to be executed before tests in the class
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class BeforeClassAttribute : Attribute { }
}