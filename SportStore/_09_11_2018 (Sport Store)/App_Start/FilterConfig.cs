using System.Web;
using System.Web.Mvc;

namespace _09_11_2018__Sport_Store_
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
