using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace Chap9
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                // 针对 Action 名称的路由变量并没有包含在模板中，所以最终对目标 Action 的选择是根据 HTTP 方法完成的
                routeTemplate: "api/{controller}/{id}",  
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
               name: "ddd",
                // 针对 Action 名称的路由变量并没有包含在模板中，所以最终对目标 Action 的选择是根据 HTTP 方法完成的
               routeTemplate: "api/{controller}/{action}/{id}",
               defaults: new { id = RouteParameter.Optional }
           );
        }
    }
}
