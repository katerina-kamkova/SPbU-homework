using System;

namespace Attributes
{
    /// <summary>
    /// Attribute for methods to be executed before EVERY TEST in the class
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class BeforeAttribute : Attribute { }
}
