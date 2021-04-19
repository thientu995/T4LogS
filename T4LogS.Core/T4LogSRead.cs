using System;
using System.Collections.Generic;
using System.IO;

namespace T4LogS.Core
{
    public class T4LogSRead : T4LogSBase
    {
        public bool lazyLoading { get; set; }
        public T4LogSRead(bool lazyLoading = true)
        {
            this.lazyLoading = lazyLoading;
        }

        public IEnumerable<T4LogSReadObject> GetDirectoryFromRoot => GetDirectories(new T4LogSReadObject()
        {
            Parent = null,
            Name = null,
            IsFile = false,
            Location = T4LogSOptions.logsPath,
            Level = 0,
        });

        public IEnumerable<T4LogSReadObject> GetDirectories(T4LogSReadObject folder)
        {
            if (folder.IsFile)
            {
                throw new FormatException("Input " + nameof(T4LogSReadObject) + " was not in a correct format.");
            }
            var paths = new DirectoryInfo(folder.Location);
            List<T4LogSReadObject> result = new List<T4LogSReadObject>();

            foreach (DirectoryInfo item in paths.GetDirectories())
            {
                var obj = new T4LogSReadObject()
                {
                    Parent = item.Parent.FullName,
                    Name = item.Name,
                    IsFile = false,
                    Location = item.FullName,
                    Level = folder.Level + 1,
                };
                result.Add(obj);
                if (!this.lazyLoading)
                {
                    result.AddRange(GetDirectories(obj));
                }
            }
            return result;
        }

        public IEnumerable<T4LogSReadObject> GetFiles(T4LogSReadObject folder)
        {
            if (folder.IsFile)
            {
                throw new FormatException("Input " + nameof(T4LogSReadObject) + " was not in a correct format.");
            }

            var paths = new DirectoryInfo(folder.Location);
            List<T4LogSReadObject> result = new List<T4LogSReadObject>();
            foreach (FileInfo item in paths.GetFiles())
            {
                if (item.Name.EndsWith(T4LogSOptions.extensionCustom))
                {
                    continue;
                }
                var obj = new T4LogSReadObject()
                {
                    Parent = item.DirectoryName,
                    Name = item.Name,
                    IsFile = true,
                    Location = item.FullName,
                    Level = folder.Level + 1,
                };
                result.Add(obj);
            }
            return result;
        }
    }
}
