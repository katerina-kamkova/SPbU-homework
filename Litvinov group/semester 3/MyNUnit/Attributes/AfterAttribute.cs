using System;

namespace Attributes
{
    /// <summary>
    /// Attribute for methods to be executed after EVERY TEST in the class
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AfterAttribute : Attribute { }
}