using ConsoleHelloWorldClient.Client;
using CServiceStack.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHelloWorldClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var webHost = "http://127.0.0.1:9636";
            var selfHost = "http://127.0.0.1:3721";
            var client = HelloWorldServiceClient.GetInstance(webHost);
            var response = client.Hello(new HelloRequestType
            {
                OrderId = 1234566
            });

            if (response.ResponseStatus.Ack == AckCodeType.Success)
            {
                Console.WriteLine("Response : {0}", response.Result);
            }
            else
            {
                Console.WriteLine("Error : {0}",response.ResponseStatus.Errors[0].Message);
            }
          
            Console.ReadKey();
        }
    }
}
