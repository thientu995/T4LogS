using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace T4LogS.Example.ASPNet
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            string path = Path.Combine(Server.MapPath("~/"), "../T4LogS.Example.T4LogSManagement", "T4LogS", "T4LogS.Example.ASPNet");
            T4LogS.Core.T4LogSOptions option = new Core.T4LogSOptions()
            {
                LogsPath = path
            };
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            using (var t4log = new Core.T4LogSWriteException(Server.GetLastError(), Core.T4LogSType.Error))
            {
                System.Diagnostics.Debug.WriteLine(t4log.Exception);
            }
        }
    }
}
