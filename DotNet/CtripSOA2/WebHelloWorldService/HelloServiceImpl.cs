using CServiceStack.Common.Types;
using CServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHelloWorldService
{
    public class HelloServiceImpl : IHelloWorldService
    {
        //[Route("/sayHello")]
        public HelloResponseType Hello(HelloRequestType request)
        {
            return new HelloResponseType
            {
                IsSuccess = true,
                Result = request.OrderId.ToString() + " Is Process Success"
            };
        }

        //[Route("/check")]
        public CheckHealthResponseType CheckHealth(CheckHealthRequestType request)
        {
            return new CheckHealthResponseType();
        }
    }
}
