using System.Web;
using System.Web.Mvc;

namespace Soindus.Interfaces.ApiRest
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
