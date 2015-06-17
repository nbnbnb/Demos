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
            // 对于 ASP.NET Web API 的寄宿来说，不论是 WebHost 还是 SelfHost
            // 我们都无需指定 HttpController 的类型
            // WCF 服务寄宿是针对某个具体服务类型的，而 ASP.NET Web API 的寄宿则是批量的
            Assembly.Load("WebApi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");

            HttpSelfHostConfiguration configuration =
                new HttpSelfHostConfiguration("http://localhost/selfhost");
            using (HttpSelfHostServer httpServer = 
                new HttpSelfHostServer(configuration))
            {
                httpServer.Configuration.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional });

                httpServer.OpenAsync();

                Console.WriteLine("服务寄宿已成功启动");

                Console.Read();
            }
        }
    }
}
