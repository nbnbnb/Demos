using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Transactions;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ChannelFactory<IGreetingService> factory =
                new ChannelFactory<IGreetingService>("greetingService"))
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    IGreetingService proxy = factory.CreateChannel();
                    proxy.SayHello("Foo");
                    proxy.SayGoodBye("Bar");

                    (proxy as ICommunicationObject).Close();

                    scope.Complete();
                }

            }
        }
    }
}
