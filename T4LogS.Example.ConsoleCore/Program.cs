using System;

namespace T4LogS.Example.ConsoleCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(lstF.Count);
            try
            {
                int.Parse("ab");
            }
            catch (Exception ex)
            {
                new T4LogS.Core.T4LogSWriteException(ex, Core.T4LogSType.Error).Dispose();
            }
            Console.ReadLine();
        }
    }
}
