using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Ninject;
using ASPPatterns.Chap8.FrontController.Controller.Request;

namespace ASPPatterns.Chap8.FrontController.Controller
{
    public class CustomHTTPHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            NInjectFactory.BasicKernel.Get<FrontController>()
                .Handle(NInjectFactory.BasicKernel.Get<IWebRequestFactory>().CreateFrom(context));
        }
        public bool IsReusable
        {
            get { return true; }
        }
    }
}
