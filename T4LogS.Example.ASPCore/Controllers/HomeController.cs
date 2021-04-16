using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using T4LogS.Example.ASPCore.Models;

namespace T4LogS.Example.ASPCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            int.Parse("asdas");
            return View();
        }
    }
}
