using System;

namespace Attributes
{
    /// <summary>
    /// Attribute for methods to be executed after tests in the class
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AfterClassAttribute : Attribute { }
}
