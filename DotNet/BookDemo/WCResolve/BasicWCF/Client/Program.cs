using ExtensionLib;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ICalculator proxy = new CalculatorServiceProxy();
                for (int i = 0; i < 1000; i++)
                {
                    //ServiceProxy<ICalculator> serviceProxy = new ServiceProxy<ICalculator>("httpPoint");
                    proxy.Add(1, 2);
                    Console.WriteLine("服务调用成功 {0}",i);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("All Exception: {0}", ex.Message);
            }

            Console.Read();
        }

        static void Invoke(Action<ICalculator> action, ICalculator proxy)
        {
            try
            {
                action(proxy);
                Console.WriteLine("调用成功 !");
            }
            catch (TimeoutException ex)
            {
                (proxy as ICommunicationObject).Abort();
                Console.WriteLine("Single Exception: {0}", ex.Message);
            }
            catch (CommunicationException ex)
            {
                (proxy as ICommunicationObject).Abort();
                Console.WriteLine("Single Exception: {0}", ex.Message);
            }
        }
    }
}
