using System.IO;

namespace T4LogS.Core
{
    public class T4LogSOptions
    {
        internal static string extensionJson = "json";
        internal static string extensionLog = "log";
        internal static string extensionCustom = "html";

        internal static string breakLineCustom = "<br>";
        internal static string formatTextCustom = @"
<table>
<caption style=""text-align:left"">
    <p><b>Date Time:</b> {{DateTime}}</p>
    <p><b>Description:</b> {{Description}}</p>
    <p><b>Message:</b> {{Message}}</p>
    <p><b>Stack Trace:</b> {{StackTrace}}</p>
</caption>
<tr>
   <th>Name</th>
   <th>Target Name</th>
   <th>Value</th>
</tr>
<<details>>
<tr>
   <td>{{Name}}</td>
   <td>{{TargetName}}</td>
   <td>{{Value}}</td>
</tr>
<</details>>
<table>
";
        internal static bool saveFileCustom = true;
        internal static bool saveDetails = true;
        internal static bool saveFileJson = true;

        internal static string logsPath = Path.Combine(Directory.GetCurrentDirectory(), "T4LogS");

        public bool SaveDetails
        {
            get
            {
                return saveDetails;
            }
            set
            {
                saveDetails = value;
            }
        }
        public string LogsPath
        {
            get
            {
                return LogsPath;
            }
            set
            {
                logsPath = value;
            }
        }
        public bool SaveFileJson
        {
            get
            {
                return saveFileJson;
            }
            set
            {
                saveFileJson = value;
            }
        }

        public bool SaveFileCustom
        {
            get
            {
                return saveFileCustom;
            }
            set
            {
                saveFileCustom = value;
            }
        }
        public string ExtensionCustom
        {
            get
            {
                return "." + extensionCustom;
            }
            set
            {
                extensionCustom = value;
            }
        }
        public string BreakLineCustom
        {
            get
            {
                return breakLineCustom;
            }
            set
            {
                breakLineCustom = value;
            }
        }
        public string FormatTextCustom
        {
            get
            {
                return formatTextCustom;
            }
            set
            {
                formatTextCustom = value;
            }
        }
    }
}
