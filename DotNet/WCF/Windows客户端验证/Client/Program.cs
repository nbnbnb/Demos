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
            // Windows 身份验证
            // 客户端进程运行的 Windows 账号对应的 Windows 凭证自动作为调用服务的客户端凭证
            // 也可以显式进行指定

            // 服务地址要指定成 IP 才成功，域名会报错？？？

            using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("tcpPoint"))
            {
                ICalculator proxy = channelFactory.CreateChannel();
                proxy.Divide(1, 2);
                Console.WriteLine("tcpPoint 服务调用完成");
            }

            using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("httpSelfPointMessage"))
            {
                // 可以显式指定

                /*
                NetworkCredential credential =
                    channelFactory.Credentials.Windows.ClientCredential;

                credential.Domain = "";
                credential.UserName = "";
                credential.Password = "";
                */

                ICalculator proxy = channelFactory.CreateChannel();
                proxy.Add(1, 2);
                Console.WriteLine("httpSelfPointMessage 服务调用完成");
            }
            Console.ReadKey(true);
        }
    }
}
