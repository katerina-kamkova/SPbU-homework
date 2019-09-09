using System.Collections.Generic;
using System.Reflection;

namespace MyNUnit
{
    /// <summary>
    /// All lists, necessary for testing the class
    /// </summary>
    public class Lists
    {
        public List<MethodInfo> Tests { get; set; }
        public List<MethodInfo> Before { get; set; }
        public List<MethodInfo> After { get; set; }
        public List<MethodInfo> BeforeClass { get; set; }
        public List<MethodInfo> AfterClass { get; set; }

        public Lists()
        {
            Tests = new List<MethodInfo>();
            Before = new List<MethodInfo>();
            After = new List<MethodInfo>();
            BeforeClass = new List<MethodInfo>();
            AfterClass = new List<MethodInfo>();
        }
    }
}
