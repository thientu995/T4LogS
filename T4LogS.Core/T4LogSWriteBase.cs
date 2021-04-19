using System;

namespace T4LogS.Core
{
    public class T4LogSWriteBase : T4LogSBase, IDisposable
    {
        internal bool isExited = false;
        public virtual void Dispose() { }
    }
}
