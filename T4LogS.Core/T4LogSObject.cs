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
        public List<T4LogSDetail> Details { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
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
