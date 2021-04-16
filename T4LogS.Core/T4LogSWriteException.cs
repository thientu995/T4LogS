using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace T4LogS.Core
{
    public class T4LogSWriteException : T4LogSWriteBase, IDisposable
    {
        public readonly Exception Exception;
        public readonly T4LogSObject Object;

        private string strObjectFormatText;
        public string StrObjectFormatText
        {
            get
            {
                if (this.Object != null)
                {
                    if (string.IsNullOrWhiteSpace(this.strObjectFormatText) && !string.IsNullOrWhiteSpace(T4LogSOptions.formatTextCustom))
                    {
                        string[] strS = null;

                        strS = Regex.Split(T4LogSOptions.formatTextCustom, "<<details>>", RegexOptions.IgnoreCase);
                        string begin = replaceCommon(strS[0]);

                        strS = Regex.Split(strS[1], "<</details>>", RegexOptions.IgnoreCase);
                        string loop = strS[0];
                        string end = replaceCommon(strS[1]);

                        StringBuilder sb = new StringBuilder();
                        sb.Append(begin);
                        if (this.Object.Details != null)
                        {
                            foreach (T4LogSDetail item in this.Object.Details)
                            {
                                sb.Append(replaceLoop(loop, item));
                            }
                        }

                        sb.Append(end);
                        this.strObjectFormatText = sb.ToString();
                    }
                    return this.strObjectFormatText;
                }
                return string.Empty;
            }
        }

        private string strObject;
        public string StrObject
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.strObject))
                {
                    this.strObject = JsonConvert.SerializeObject(this.Object);
                }
                return this.strObject;
            }
        }

        public T4LogSWriteException(Exception ex, T4LogSType status, string description = "") : this(ex, ex.GetType(), status, description) { }
        public T4LogSWriteException(Exception ex, Type targetType, T4LogSType status, string description = "")
        {
            if (this.status == T4LogSType.Time)
            {
                throw new FormatException("T4LogSWriteException unsupported T4LogSType.Time. Please select T4LogSWriteTime");
            }
            this.isExited = false;
            this.status = status;
            this.Exception = ex;
            this.Object = new T4LogSObject()
            {
                Details = new List<T4LogSDetail>(),
                DateTime = DateTime.Now,
                Message = this.Exception.Message,
                StackTrace = this.Exception.StackTrace,
                Description = description,
            };
        }

        public void AppendDetail(T4LogSDetail value)
        {
            if (T4LogSOptions.saveDetails)
            {
                var lstTemp = new List<T4LogSDetail>(this.Object.Details);
                lstTemp.Add(value);
                this.Object.Details = new List<T4LogSDetail>(lstTemp);
            }
        }

        public void AppendRangeDetail(IEnumerable<T4LogSDetail> value)
        {
            if (T4LogSOptions.saveDetails)
            {
                foreach (var item in value)
                {
                    this.AppendDetail(item);
                }
            }
        }

        private void addPropertiesObjectDetails()
        {
            if (T4LogSOptions.saveDetails)
            {
                List<T4LogSDetail> prop = new List<T4LogSDetail>(this.Exception.GetType().GetProperties().ToT4LogObjects(this.Exception));
                prop.AddRange(new List<T4LogSDetail>(this.Object.Details));
                this.Object.Details = new List<T4LogSDetail>(prop);
            }
        }

        private void saveObject()
        {
            if (this.Object != null && !this.isExited)
            {
                this.isExited = true;
                this.addPropertiesObjectDetails();
                string fileName = System.IO.Path.Combine(this.PathLogs, DateTime.Now.Ticks.ToString() + ".");
                if (T4LogSOptions.saveFileJson)
                {
                    System.IO.File.WriteAllText(fileName + T4LogSOptions.extensionJson, this.StrObject);
                }
                if (T4LogSOptions.saveFileCustom)
                {
                    System.IO.File.WriteAllText(fileName + T4LogSOptions.extensionCustom, this.StrObjectFormatText);
                }
            }
        }

        private string replaceCommon(string obj)
        {

            return obj
                .Replace(T4LogSCommon.replaceFormatText(nameof(this.Object.Description)), T4LogSCommon.replaceNewLine(this.Object.Description))
                .Replace(T4LogSCommon.replaceFormatText(nameof(this.Object.Message)), T4LogSCommon.replaceNewLine(this.Object.Message))
                .Replace(T4LogSCommon.replaceFormatText(nameof(this.Object.StackTrace)), T4LogSCommon.replaceNewLine(this.Object.StackTrace))
                .Replace(T4LogSCommon.replaceFormatText(nameof(this.Object.DateTime)), this.Object.DateTime.ToString(T4LogSCommon.formatDateTimeFull));
        }

        private string replaceLoop(string obj, T4LogSDetail item)
        {
            return obj
                .Replace(T4LogSCommon.replaceFormatText(nameof(item.Name)), T4LogSCommon.replaceNewLine(item.Name))
                .Replace(T4LogSCommon.replaceFormatText(nameof(item.TargetName)), T4LogSCommon.replaceNewLine(item.TargetName))
                .Replace(T4LogSCommon.replaceFormatText(nameof(item.Value)), T4LogSCommon.replaceNewLine(item.Value));
        }

        ~T4LogSWriteException() { this.Dispose(); }
        public void Dispose()
        {
            this.saveObject();
        }
    }
}
