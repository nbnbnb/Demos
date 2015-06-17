using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Async;

namespace Chap7.Models
{
    public class AsyncActionInvoker : AsyncControllerActionInvoker
    {
        public new ControllerDescriptor GetControllerDescriptor(ControllerContext controllerContext)
        {
            return base.GetControllerDescriptor(controllerContext);
        }
    }
}