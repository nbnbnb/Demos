using Model;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Service
{
    [ServiceBehavior(ConfigurationName = "CalculatorService")]
    public class CalculatorService : ICalculator
    {
        public double Add(int x, int y)
        {
            Console.WriteLine("接收到来自客户端的上下文");

            Dictionary<string, string> values = OperationContext.Current.IncomingMessageHeaders
                .GetHeader<Dictionary<string, string>>("ApplicationContext", "http://www.zhangjin.me");

            foreach (var item in values)
            {
                Console.WriteLine("{0,-3}:{1}", item.Key, item.Value);
            }

            return x + y;
        }

        public double Subtract(int x, int y)
        {
            return x - y;
        }

        public double Multiply(int x, int y)
        {
            return x * y;
        }

        public double Divide(int x, int y)
        {
            return x / y;
        }
    }
}
