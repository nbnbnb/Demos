using Chap7.Models;
using System.Web;
using System.Web.Mvc;

namespace Chap7
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new BazAttribute());
        }
    }
}