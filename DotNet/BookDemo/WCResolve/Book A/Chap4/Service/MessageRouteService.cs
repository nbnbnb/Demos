using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace Service
{
    public class MessageRouteService : IMessageRoute
    {
        public void DoRoute(System.ServiceModel.Channels.Message request)
        {
            Console.WriteLine(request.ToString());
        }

        public void Add(int a, int b)
        {
            Console.WriteLine("Sleep...");

            Thread.Sleep(5000);

            int result = a + b;
            ICalculatorCallback callback = 
                OperationContext.Current.GetCallbackChannel<ICalculatorCallback>();
            callback.DisplayResult(result, a, b);
        }

    }
}
