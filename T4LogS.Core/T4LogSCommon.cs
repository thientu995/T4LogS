using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace T4LogS.Core
{
    public static class T4LogSCommon
    {
        internal const string formatDateTimeFolder = "yyyyMMdd";
        internal const string formatDateTimeFull = "yyyy/MM/dd hh:mm:ss tt";

        internal static string replaceFormatText(string name)
        {
            return "{{" + name + "}}";
        }

        internal static string replaceNewLine(string obj)
        {
            Regex regex_newline = new Regex("(\r\n|\r|\n)");// Environment.NewLine
            return regex_newline.Replace(obj, T4LogSOptions.breakLineCustom);
        }

        public static ICollection<T4LogSErrorDetail> ToT4LogObjects(this PropertyInfo[] properties, object obj)
        {
            ICollection<T4LogSErrorDetail> lst = new List<T4LogSErrorDetail>();
            foreach (PropertyInfo item in properties)
            {
                T4LogSErrorDetail objPro = item.ToT4LogObject(obj);
                if (objPro != null)
                {
                    lst.Add(objPro);
                }
            }
            return lst;
        }

        public static T4LogSErrorDetail ToT4LogObject(this PropertyInfo pi, object obj)
        {
            try
            {
                object value = pi.GetValue(obj, null);
                string strValue = JsonConvert.SerializeObject(value);
                return new T4LogSErrorDetail()
                {
                    TargetName = obj.GetType().FullName,
                    Name = pi.Name,
                    Value = strValue
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
