using System;
using System.Collections.Generic;
using System.Reflection;

namespace T4LogS.Core
{
    public class T4LogSDetail
    {
        public string TargetName { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class T4LogSObject
    {
        public IEnumerable<T4LogSDetail> Details { get; internal set; }
        public string Message { get; internal set; }
        public string StackTrace { get; internal set; }
        public string Description { get; internal set; }
        public DateTime DateTime { get; internal set; }
    }

    public enum T4LogSType
    {
        Other = -1,
        Error,
        Warning,
        Info,
        Log,
        Debug,
        Audit,
        Time,
    }
}
