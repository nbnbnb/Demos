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
            // ASP.NET Web API 的批量寄宿源直它对 HttpController 类型的智能解析
            // 他会从提供的程序集列表中解析出所有 HttpController 类型【实现了 IHttpController 接口】
            // 对于 Self Host 来说，HttpController 类型的解析在默认情况下只会针对加载到当前应用程序域的程序集列表
            // 所以此示例需要显式加载
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
