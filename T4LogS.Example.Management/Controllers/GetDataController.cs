using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using T4LogS.Core;

namespace T4LogS.Example.Management.Controllers
{
    [Route("api/[controller]")]
    public class GetDataController : Controller
    {
        [HttpGet("[action]")]
        public IActionResult GetPath()
        {
            //string folderRoot = Path.Combine(Directory.GetCurrentDirectory(), "T4LogS");
            var read = new T4LogSRead(true);
            var a = read.GetDirectoryFromRoot;
            StringBuilder sb = new StringBuilder();
            foreach (var item in read.GetDirectoryFromRoot)
            {
                for (int i = 0; i < item.Level; i++)
                {
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
                }
                sb.Append(item.Name + "<br>");
                foreach (var file in read.GetFiles(item))
                {
                    for (int i = 0; i < file.Level; i++)
                    {
                        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
                    }
                    sb.Append("File: " + file.Name + "<br>");
                }
            }
            return Content(sb.ToString(), "text/html");
        }
    }
}
