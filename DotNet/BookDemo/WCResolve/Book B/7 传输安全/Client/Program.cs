using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ChannelFactory<ICalculator> factory =
                new ChannelFactory<ICalculator>("btking"))
            {
                
                UserNamePasswordClientCredential credential =
                    factory.Credentials.UserName;
                credential.UserName = "nbnbnb.zhang@live.com";
                credential.Password = "870615zjx";
                
                ICalculator proxy = factory.CreateChannel();
                Console.WriteLine(proxy.Add(1, 2));
                Console.Read();
            }
        }
    }
}
