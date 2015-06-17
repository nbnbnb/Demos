using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Async;

namespace Chap6.Models
{
    public class ParameterValidationAsyncActionInvoker : AsyncControllerActionInvoker
    {
        protected override object GetParameterValue(System.Web.Mvc.ControllerContext controllerContext,
            System.Web.Mvc.ParameterDescriptor parameterDescriptor)
        {
            try
            {
                controllerContext.RouteData.DataTokens.Add("ParameterDescriptor", parameterDescriptor);
                return base.GetParameterValue(controllerContext, parameterDescriptor);
            }
            finally
            {
                controllerContext.RouteData.DataTokens.Remove("ParameterDescriptor");
            }
        }
    }
}