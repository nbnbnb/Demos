using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Http.SelfHost;
using System.Web.Http;

namespace SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly.Load("WebApi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");

            HttpSelfHostConfiguration configuration =
                new HttpSelfHostConfiguration("http://127.0.0.1/selfhost");

            using (HttpSelfHostServer httpServer = new HttpSelfHostServer(configuration))
            {
                httpServer.Configuration.Routes.MapHttpRoute(
                    name: "DefaultApp",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );

                httpServer.OpenAsync().Wait();
                Console.WriteLine("服务已成功启动");
                Console.Read();
            }
        }
    }
}
