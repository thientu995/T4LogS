using System;
using System.IO;
using T4LogS.Core;

namespace T4LogS.Example.ConsoleCore
{
    class Program
    {
        static void Main(string[] args)
        {
            string projectManagement = new DirectoryInfo(Environment.GetEnvironmentVariable("T4LogSManagementProject")).FullName;
            string folderRoot = Path.Combine(projectManagement, "T4LogS", System.Reflection.Assembly.GetEntryAssembly().GetName().Name);//Save sql or similar for high performance
            new T4LogS.Core.T4LogSOptions()
            {
                LogsPath = folderRoot
            };
            try
            {
                int.Parse("a");
            }
            catch (Exception ex)
            {
                new T4LogS.Core.T4LogSWriteLog(Core.T4LogSType.Log, "Group").WriteLog("123");
                //Write log fast
                new T4LogS.Core.T4LogSWriteException(ex, Core.T4LogSType.Error, "Description (Optional, default String.Empty)").Dispose();
                //or write append detail
                using (var log = new T4LogS.Core.T4LogSWriteException(ex, Core.T4LogSType.Error))
                {
                    log.AppendDetail(new Core.T4LogSErrorDetail()
                    {
                        Name = "Example Append Name",
                        TargetName = "Example Append Target Name",
                        Value = "Example Append Value",
                    });
                }
            }
            Console.ReadLine();
        }
    }
}
