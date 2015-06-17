using CustomChannel;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
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
            EndpointAddress address =
                new EndpointAddress("http://127.0.0.1:9527/calculatorservice");

            using (ChannelFactory<ICalculator> factory =
                new ChannelFactory<ICalculator>(new SimpleDatagramBinding(), address))
            {
                factory.Open();
                ICalculator proxy = factory.CreateChannel();
             
                Console.WriteLine(proxy.Add(1, 2));
                Console.WriteLine("Done");
                Console.WriteLine(proxy.Add(1, 2));
                Console.WriteLine("Done");

                (proxy as ICommunicationObject).Close();
                proxy = factory.CreateChannel();
                Console.WriteLine(proxy.Add(1, 2));
                Console.WriteLine("Done");
                Console.WriteLine(proxy.Add(1, 2));
                Console.WriteLine("Done");


                Console.Read();
            }
        }
    }
}
