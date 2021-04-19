using System;
using System.Collections.Generic;
using System.Reflection;

namespace T4LogS.Core
{
    public class T4LogSErrorDetail
    {
        public string TargetName { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class T4LogSErrorObject
    {
        public IEnumerable<T4LogSErrorDetail> Details { get; internal set; }
        public string Message { get; internal set; }
        public string StackTrace { get; internal set; }
        public string Description { get; internal set; }
        public DateTime DateTime { get; internal set; }
    }

    public class T4LogSReadObject
    {
        //public IEnumerable<T4LogSReadObject> Subdirectories { get; internal set; }
        public string Parent { get; internal set; }
        //public string Content { get; internal set; }
        public string Name { get; internal set; }
        public string NameFormat { get; internal set; }
        public string Location { get; internal set; }
        public bool IsFile { get; internal set; }
        public int Level { get; internal set; }
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
