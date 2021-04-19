using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace T4LogS.Core
{
    public class T4LogSWriteLog : T4LogSWriteBase
    {
        public readonly Stopwatch sw;
        private string group;
        private readonly string fileName;
        public T4LogSWriteLog(T4LogSType type, string group = "T4LogS")
        {
            switch (type)
            {
                case T4LogSType.Error:
                case T4LogSType.Time:
                    throw new FormatException(nameof(T4LogSWriteLog) + " unsupported " + nameof(T4LogSType) + "." + status.ToString() + "");
            }

            this.status = type;
            this.group = group;
            this.fileName = System.IO.Path.Combine(this.FolderLogTypeToday, group + "." + T4LogSOptions.extensionLog);
        }

        public void WriteLog(string content)
        {
            System.IO.File.AppendAllText(fileName, DateTime.Now + "\t:\t" + content + Environment.NewLine);
        }
    }
}
