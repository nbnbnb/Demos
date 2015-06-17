using Model;
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

                using (OperationContextScope opContextScope =
                    new OperationContextScope(proxy as IContextChannel))
                {
                    Dictionary<string, string> values = new Dictionary<string, string>();
                    values.Add("Foo", "123");
                    values.Add("Bar", "456");
                    values.Add("Baz", "789");

                    MessageHeader<Dictionary<string, string>> header =
                        new MessageHeader<Dictionary<string, string>>(values);

                    OperationContext.Current.OutgoingMessageHeaders
                        .Add(header.GetUntypedHeader("ApplicationContext", "http://www.zhangjin.me"));
                    Console.WriteLine(proxy.Add(1, 2));
                }
            }

            Console.Read();
        }
    }
}
