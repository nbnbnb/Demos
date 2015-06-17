using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;

namespace Service
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebServiceHost host = new WebServiceHost(typeof(EmployeeService)))
            {
                host.Opened += host_Opened;
                host.Open();
                Console.Read();
            }

            Message msg = null;
        }

        static void host_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("服务已成功开启，按任意键退出");
        }
    }
}
