using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ASPPatterns.Chap8.FrontController.Controller.Request
{
    public interface IWebRequestFactory
    {
        WebRequest CreateFrom(HttpContext context);
    }
}
