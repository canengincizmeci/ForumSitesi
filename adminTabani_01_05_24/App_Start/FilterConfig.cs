using System.Web;
using System.Web.Mvc;

namespace adminTabani_01_05_24
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
