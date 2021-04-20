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
    public class ParameterGetDataController
    {
        public T4LogSReadObject obj { get; set; }
        public string lazyLoading { get; set; }
    }
    [Route("api/[controller]")]
    public class GetDataController : Controller
    {

        [HttpPost("[action]")]
        public IActionResult GetPath(bool lazyLoading, [FromBody] T4LogSReadObject param)
        {
            var read = new T4LogSRead(lazyLoading);
            IEnumerable<T4LogSReadObject> dics;
            if (param == null)
            {
                dics = read.GetDirectoryFromRoot;
            }
            else
            {
                if (!param.IsFile)
                {
                    dics = read.GetDirectories(param);
                }
                else
                {

                    dics = new List<T4LogSReadObject>()
                    {
                        read.GetContent(param)
                    };
                }
            }
            return Json(dics);
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
