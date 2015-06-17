using ConsoleApp.Concrete;
using ConsoleApp.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //ExportDemo();
            //CustomMEX();
        }

        static void ExportDemo()
        {
            ContractDescription contract =
                ContractDescription.GetContract(typeof(IOrderService));

            EndpointAddress address1 = new EndpointAddress("http://127.0.0.1/orderservice");
            EndpointAddress address2 = new EndpointAddress("net.tcp://127.0.0.1/orderservice");

            ServiceEndpoint endpoint1 = new ServiceEndpoint(contract, new WS2007HttpBinding(), address1);
            ServiceEndpoint endpoint2 = new ServiceEndpoint(contract, new NetTcpBinding(), address2);

            XmlQualifiedName serviceName = new XmlQualifiedName("OrderService", "http://www.zhangjin.me/services/");

            WsdlExporter exporter = new WsdlExporter();

            exporter.ExportEndpoints(new ServiceEndpoint[] { endpoint1, endpoint2 }, serviceName);

            MetadataSet metadata = exporter.GetGeneratedMetadata();

            using (XmlWriter writer =
                new XmlTextWriter("metadata.xml", Encoding.UTF8))
            {
                metadata.WriteTo(writer);
            }

            Process.Start("metadata.xml");
        }

        static void CustomMEX()
        {
            using (ServiceHost host = new ServiceHost(typeof(CalculatorService)))
            {
                host.Opened += host_Opened;
                host.Open();
                Console.ReadLine();
                host.Close();
            }
        }

        static void host_Opened(object sender, EventArgs e)
        {
            using (ChannelFactory<IMetadataProvisionService> channelFactory
                    = new ChannelFactory<IMetadataProvisionService>("mex"))
            {
                IMetadataProvisionService proxy = channelFactory.CreateChannel();
                string action = "http://schemas.xmlsoap.org/ws/2004/09/transfer/Get";
                Message request = Message.CreateMessage(MessageVersion.Default, action);
                Message reply = proxy.Get(request);  // 将会调用我们添加的单例实例

                MetadataSet metadata = reply.GetBody<MetadataSet>();
                using (XmlWriter writer = new XmlTextWriter("metadata.xml", Encoding.UTF8))
                {
                    metadata.WriteTo(writer);
                }

                Process.Start("metadata.xml");
            }
        }
    }
}
