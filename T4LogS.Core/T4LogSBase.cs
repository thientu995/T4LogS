using System;
using System.IO;

namespace T4LogS.Core
{
    public class T4LogSBase
    {
        internal T4LogSType status = T4LogSType.Error;
        public string FolderLogTypeToday
        {
            get
            {
                string pathLogs = Path.Combine(this.FolderLogToday, this.status.ToString());
                return CreateDirectory(pathLogs);
            }
        }

        public string FolderLogToday
        {
            get
            {
                string pathLogs = Path.Combine(T4LogSOptions.logsPath, DateTime.Now.ToString(T4LogSCommon.formatDateTimeFolder));
                return CreateDirectory(pathLogs);
            }
        }

        internal string CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
    }
}
