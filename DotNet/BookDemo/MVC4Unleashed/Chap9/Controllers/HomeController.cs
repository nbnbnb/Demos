using Chap9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;

namespace Chap9.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            HttpControllerDescriptor controllerDescriptor = new HttpControllerDescriptor
            {
                ControllerType = typeof(FooController)
            };
            MethodInfo methodInfo = typeof(FooController).GetMethod("Bar");
            HttpActionDescriptor actionDescriptor =
                new ReflectedHttpActionDescriptor(controllerDescriptor, methodInfo)
                {
                    Configuration = GlobalConfiguration.Configuration
                };

            IActionValueBinder valueBinder = GlobalConfiguration.Configuration.Services.GetActionValueBinder();
            HttpActionBinding actionBinder = valueBinder.GetBinding(actionDescriptor);
            return View(actionBinder.ParameterBindings);
        }

        private string GetPipleline(HttpMessageHandler httpServer)
        {
            string pipleline = httpServer.GetType().Name;
            DelegatingHandler delegatingHandler = httpServer as DelegatingHandler;
            if (null != delegatingHandler && delegatingHandler.InnerHandler != null)
            {
                return pipleline + "=>" + GetPipleline(delegatingHandler.InnerHandler);
            }
            else
            {
                return pipleline;
            }
        }

    }
}
