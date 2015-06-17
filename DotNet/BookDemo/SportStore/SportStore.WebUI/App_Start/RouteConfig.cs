using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null,
            "", // Only matches the empty URL (i.e. /) 
            new
            {
                controller = "Product",
                action = "List",
                category = (string)null,
                page = 1
            }
            );
            routes.MapRoute(null,
            "Page{page}", // Matches /Page2, /Page123, but not /PageXYZ 
            new { controller = "Product", action = "List", category = (string)null },
            new { page = @"\d+" } // Constraints: page must be numerical 
            );
            routes.MapRoute(null,
            "{category}", // Matches /Football or /AnythingWithNoSlash 
            new { controller = "Product", action = "List", page = 1 }
            );
            routes.MapRoute(null,
            "{category}/Page{page}", // Matches /Football/Page567 
            new { controller = "Product", action = "List" }, // Defaults 
            new { page = @"\d+" } // Constraints: page must be numerical 
            );

            routes.MapRoute(null, "{controller}/{action}"); 
        }
    }
}