using ExtensionLib;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
            // HTTPS 寄宿时，默认也是读取“受信任根证书颁发机构”存储区
            // 改变 HTTPS 寄宿时 证书的认证方式

            // 设置为 “受信任人”存取区时，删除此段代码
            // ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("tcpPoint"))
            {
                ICalculator proxy = channelFactory.CreateChannel();
                proxy.Add(1, 2);
                Console.WriteLine("服务调用完成");
            }

            using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("httpsSelfPoint"))
            {
                ICalculator proxy = channelFactory.CreateChannel();
                proxy.Subtract(1, 2);
                Console.WriteLine("服务调用完成");
            }
            
            
            using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("httpsIISPoint"))
            {
                ICalculator proxy = channelFactory.CreateChannel();
                proxy.Subtract(1, 2);
                Console.WriteLine("服务调用完成");
            }
            
            Console.ReadKey(true);
        }
    }
}
