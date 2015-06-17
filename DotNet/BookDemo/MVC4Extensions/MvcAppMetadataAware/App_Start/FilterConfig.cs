using System.Web;
using System.Web.Mvc;

namespace MvcAppMetadataAware
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}