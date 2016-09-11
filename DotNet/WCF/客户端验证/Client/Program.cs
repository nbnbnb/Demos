using ExtensionLib;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.Remoting;
using System.Security.Cryptography.X509Certificates;
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
            using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("iisPoint"))
            {
                ICalculator proxy = channelFactory.CreateChannel();
                proxy.Multiply(1, 2);
                Console.WriteLine("iisPoint 服务调用完成");
            }

            using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("httpSelfPointMessage"))
            {
  
                ICalculator proxy = channelFactory.CreateChannel();
                proxy.Add(1, 2);
                Console.WriteLine("httpSelfPointMessage 服务调用完成");
            }

            using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("httpSelfPointTransport"))
            {
                ICalculator proxy = channelFactory.CreateChannel();
                proxy.Subtract(1, 2);
                Console.WriteLine("httpSelfPointTransport 服务调用完成");
            }

            using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("tcpPoint"))
            {
                ICalculator proxy = channelFactory.CreateChannel();
                proxy.Divide(1, 2);
                Console.WriteLine("tcpPoint 服务调用完成");
            }

            Console.ReadKey(true);
        }
    }
}
