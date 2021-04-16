using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace T4LogS.Example.ASPNet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var time = new T4LogS.Core.T4LogSWriteTime("HomeController");
            time.Start();
            time.Stop();
            time.Dispose();
            System.IO.File.Open(@"D:\NGUYEN THIEN TU\VSCode\W3Blog\T4LogS\ASDASD",System.IO.FileMode.Open);
            //int.Parse("23123asdasds");
            return View();
        }
    }
}