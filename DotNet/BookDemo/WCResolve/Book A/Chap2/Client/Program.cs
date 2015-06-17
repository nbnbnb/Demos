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
            using (ChannelFactory<ICalculator> factory =
                new ChannelFactory<ICalculator>("httpPoint"))
            {
                ICalculator proxy = factory.CreateChannel();

                Console.WriteLine(proxy.Add(1, 2));
                Console.WriteLine(proxy.Subtract(1, 2));
                Console.WriteLine(proxy.Multiply(1, 2));
                Console.WriteLine(proxy.Divide(1, 2));

                Console.Read();
            }
        }
    }
}
