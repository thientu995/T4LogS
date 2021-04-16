using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace T4LogS.Core
{
    public class T4LogSWriteBase
    {
        internal bool isExited = false;
        internal T4LogSType status = T4LogSType.Error;
        public string PathLogs
        {
            get
            {
                string pathLogs = System.IO.Path.Combine(T4LogSOptions.logsPath, DateTime.Now.ToString(T4LogSCommon.formatDateTimeFolder), this.status.ToString());
                if (!Directory.Exists(pathLogs))
                {
                    Directory.CreateDirectory(pathLogs);
                }
                return pathLogs;
            }
        }

    }
}
