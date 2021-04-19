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
        const bool lazyLoading = false;

        [HttpGet("[action]")]
        public IActionResult GetPath(T4LogSReadObject objFodler = null)
        {
            if (!objFodler.IsFile)
            {
                IEnumerable<T4LogSReadObject> dics;
                var read = new T4LogSRead(lazyLoading);
                if (objFodler != null)
                {
                    dics = read.GetDirectoryFromRoot;
                }
                else
                {
                    dics = read.GetDirectories(objFodler);
                }
                return Json(dics);
            }
            else
            {
                string contentFile = System.IO.File.ReadAllText(objFodler.Location);
                return Json(contentFile);
            }

        }

        //[HttpGet("[action]")]
        //public IActionResult GetPath(T4LogSReadObject onjFodler = null)
        //{
        //    var read = new T4LogSRead(false);
        //    StringBuilder sb = new StringBuilder();
        //    var dics = new List<T4LogSReadObject>();
        //    if (onjFodler != null)
        //    {
        //        dics = read.GetDirectoryFromRoot.ToList();
        //    }
        //    else
        //    {
        //        dics = read.GetDirectories(onjFodler).ToList();
        //    }
        //    //foreach (var item in dics)
        //    //{
        //    //    for (int i = 0; i < item.Level; i++)
        //    //    {
        //    //        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
        //    //    }
        //    //    foreach (var file in read.GetFiles(item))
        //    //    {
        //    //        for (int i = 0; i < file.Level; i++)
        //    //        {
        //    //            sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
        //    //        }
        //    //        sb.Append("File: " + file.Name + "<br>");
        //    //    }
        //    //}
        //    return Content(sb.ToString(), "text/html");
        //}
    }
}
