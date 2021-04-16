using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace T4LogS.Example.ASPNet
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            T4LogS.Core.T4LogSOptions option = new Core.T4LogSOptions() {
                LogsPath = Server.MapPath("T4LogS")
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
