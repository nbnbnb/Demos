using Chap9.Controllers;
using Chap9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Routing;

namespace Chap9
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

            GlobalConfiguration.Configuration.MessageHandlers.Add(new HttpMethodOverrideHandler());


            //var activator = new NinjectHttpControllerActivator();
            //activator.Register<IContactRepository, DefaultContactRepository>();
            //GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), activator);

            NinjectDependencyResolver dependencyResolver = new NinjectDependencyResolver();
            dependencyResolver.Register<IContactRepository, DefaultContactRepository>();
            GlobalConfiguration.Configuration.DependencyResolver = dependencyResolver;
            
             GlobalConfiguration.Configuration.Services.GetModelBinderProviders()
        }
    }
}