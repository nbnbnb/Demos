using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Async;

namespace MVC4.Extensions.ModelExtensions
{
    public class ParameterValidationAsyncActionInvoker : AsyncControllerActionInvoker
    {
        private const string PK = "ParameterDescriptor";

        protected override object GetParameterValue(System.Web.Mvc.ControllerContext controllerContext, System.Web.Mvc.ParameterDescriptor parameterDescriptor)
        {
            try
            {
                controllerContext.RouteData.DataTokens.Add(PK, parameterDescriptor);

                // 调用此方法时，将会完成 Model 的绑定以及验证
                return base.GetParameterValue(controllerContext, parameterDescriptor);
            }
            finally
            {
                controllerContext.RouteData.DataTokens.Remove(PK);
            }
        }
    }
}