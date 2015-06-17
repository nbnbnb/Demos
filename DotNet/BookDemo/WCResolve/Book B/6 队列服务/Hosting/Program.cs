using Service;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;

namespace Hosting
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @".\private$\queue4demo";
            if (!MessageQueue.Exists(path))
            {
                MessageQueue.Create(path, true);
            }

            using (ServiceHost host = new ServiceHost(typeof(GreetingService)))
            {
                host.Opened += host_Opened;
                host.Open();
                Console.ReadLine();
            }
        }

        static void host_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("服务已经启动，按任意键终止");
        }
    }
}
