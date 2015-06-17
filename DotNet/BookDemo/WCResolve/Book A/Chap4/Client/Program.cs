using Service;
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
            Console.WriteLine("Press any key to start.");
            Console.ReadLine();

            InstanceContext callback = new InstanceContext(new CalculatorCallbackService());
            using (DuplexChannelFactory<IMessageRoute> factory =
               new DuplexChannelFactory<IMessageRoute>(callback,"MessageRouteService"))
            {
                IMessageRoute proxy = factory.CreateChannel();
                proxy.Add(1, 2);

                Console.ReadLine();
            }

            Console.WriteLine("Test OK.");
           
        }
    }
}
