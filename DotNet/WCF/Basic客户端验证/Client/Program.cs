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
            using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("iisBasic1"))
            {
                // Digest 需要在域环境中测试
                // 由于不是 Windows，此处需要显式指定用户名和密码（使用 UserName 属性的 UserName 指定用户名）

                channelFactory.Credentials.UserName.UserName = "nbnbnb.zhang@live.com";
                channelFactory.Credentials.UserName.Password = "111111111";

                ICalculator proxy = channelFactory.CreateChannel();
                proxy.Add(1, 2);
                Console.WriteLine("httpSelfPointMessage 服务调用完成");
            }


            using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("iisBasic2"))
            {
                // Digest 需要在域环境中测试
                // 由于不是 Windows，此处需要显式指定用户名和密码（使用 UserName 属性的 UserName 指定用户名）

                channelFactory.Credentials.UserName.UserName = "nbnbnb.zhang@live.com";
                channelFactory.Credentials.UserName.Password = "111111111";

                ICalculator proxy = channelFactory.CreateChannel();
                proxy.Add(1, 2);
                Console.WriteLine("httpSelfPointMessage 服务调用完成");
            }

            using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("httpsSelfPoint"))
            {
                // Digest 需要在域环境中测试
                // 由于不是 Windows，此处需要显式指定用户名和密码（使用 UserName 属性的 UserName 指定用户名）

                channelFactory.Credentials.UserName.UserName = "nbnbnb.zhang@live.com";
                channelFactory.Credentials.UserName.Password = "111111111";

                ICalculator proxy = channelFactory.CreateChannel();
                proxy.Add(1, 2);
                Console.WriteLine("httpSelfPointMessage 服务调用完成");
            }

            Console.ReadKey(true);
        }
    }
}
