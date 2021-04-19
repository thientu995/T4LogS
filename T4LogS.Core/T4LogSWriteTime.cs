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
    public class T4LogSWriteTime : T4LogSWriteBase, IDisposable
    {
        public readonly Stopwatch sw;
        private string funcName;
        private string desciption;
        private DateTime dtStart;
        public T4LogSWriteTime(string funcName, string description = "")
        {
            this.status = T4LogSType.Time;
            this.isExited = false;
            this.funcName = funcName;
            this.desciption = description;
            this.sw = new Stopwatch();
        }

        public void Start()
        {
            sw.Start();
            dtStart = DateTime.Now;
        }

        public void Stop(bool autoDispose = true)
        {
            sw.Stop();
            if (autoDispose)
            {
                this.Dispose();
            }
        }

        ~T4LogSWriteTime() => this.Dispose();
        public void Dispose()
        {
            if (!this.isExited)
            {
                this.isExited = true;
                string fileName = System.IO.Path.Combine(this.FolderLogTypeToday, funcName + "." + T4LogSOptions.extensionLog);

                StringBuilder sb = new StringBuilder();
                sb.Append("Start " + dtStart);
                sb.Append(Environment.NewLine + "\t- Description: " + desciption);
                sb.Append(Environment.NewLine + "\t- Time elapsed: ");
                sb.Append(Environment.NewLine + "\t\t+ ns: " + sw.Elapsed.TotalMilliseconds * 1000000);
                sb.Append(Environment.NewLine + "\t\t+ ms: " + sw.Elapsed.TotalMilliseconds);
                sb.Append(Environment.NewLine + "\t\t+  s: " + sw.Elapsed.TotalSeconds);
                sb.Append(Environment.NewLine + "----------------------------" + Environment.NewLine + Environment.NewLine);
                
                System.IO.File.AppendAllText(fileName, sb.ToString());
            }
        }
    }
}
