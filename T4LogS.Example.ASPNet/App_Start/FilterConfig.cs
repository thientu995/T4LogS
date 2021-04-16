using System.Web;
using System.Web.Mvc;

namespace T4LogS.Example.ASPNet
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
