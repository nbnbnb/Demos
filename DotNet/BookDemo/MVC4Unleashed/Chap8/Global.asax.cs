using Chap8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Globalization;
using System.Threading;

namespace Chap8
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            NinjectDependencyResolver dependencyResolver =
                new Models.NinjectDependencyResolver();
            dependencyResolver.Register<ResourceReader, DefaultResourceReader>();
            DependencyResolver.SetResolver(dependencyResolver);

           // ViewEngines.Engines.Insert(0, new StaticFileViewEngine());
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new SimpleRazorViewEngine());
        }

        protected void Application_BeginRequest()
        {
            HttpContextBase contextWrapper =
                new HttpContextWrapper(HttpContext.Current);

            string culture = RouteTable.Routes.GetRouteData(contextWrapper)
                .Values["culture"] as string;

            if (!string.IsNullOrEmpty(culture))
            {
                try
                {
                    CultureInfo cultureInfo = new CultureInfo(culture);
                    Thread.CurrentThread.CurrentCulture = cultureInfo;
                    Thread.CurrentThread.CurrentUICulture = cultureInfo;
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }
        }
    }
}