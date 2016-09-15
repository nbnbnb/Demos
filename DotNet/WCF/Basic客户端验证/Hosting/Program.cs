using Service;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading;

namespace Hosting
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(CalculatorService)))
            {
                host.Opened += host_Opened;
                host.Open();
                Console.Read();
            }
        }

        static void host_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("服务已经启动，按任意键终止");
        }
    }
}
