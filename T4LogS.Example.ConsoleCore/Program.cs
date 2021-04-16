using System;

namespace T4LogS.Example.ConsoleCore
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int.Parse("a");
            }
            catch (Exception ex)
            {
                //Write log fast
                new T4LogS.Core.T4LogSWriteException(ex, Core.T4LogSType.Error, "Description (Optional, default String.Empty)").Dispose();
                //or write append detail
                using (var log = new T4LogS.Core.T4LogSWriteException(ex, Core.T4LogSType.Error))
                {
                    log.AppendDetail(new Core.T4LogSDetail()
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
