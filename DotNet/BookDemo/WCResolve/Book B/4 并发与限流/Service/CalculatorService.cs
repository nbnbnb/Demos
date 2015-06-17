using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace Service
{
    [ServiceBehavior(ConfigurationName = "CalculatorService",
        ConcurrencyMode = ConcurrencyMode.Multiple,
        InstanceContextMode = InstanceContextMode.Single,
        UseSynchronizationContext = false)]
    public class CalculatorService : ICalculator
    {
        public double Add(double x, double y)
        {
            EventMonitor.Send(EventType.StartExecute);
            Thread.Sleep(5000);
            double result = x + y;
            EventMonitor.Send(EventType.EndExecute);
            return result;
        }

        public double Subtract(double x, double y)
        {
            return x - y;
        }

        public double Multiply(double x, double y)
        {
            return x * y;
        }

        public double Divide(double x, double y)
        {
            return x / y;
        }

        public void Add2(double x, double y)
        {
            EventMonitor.Send(EventType.StartCall);
            Thread.Sleep(5000);
            double result = x + y;

            EventMonitor.Send(EventType.StartCallback);
            int clientId = OperationContext.Current.IncomingMessageHeaders
                .GetHeader<int>(EventMonitor.ClientIdHeaderLocalName, EventMonitor.ClientIdHeaderNamespace);
            MessageHeader<int> messageHeader = new MessageHeader<int>(clientId);
            OperationContext.Current.OutgoingMessageHeaders.Add(
                messageHeader.GetUntypedHeader(EventMonitor.ClientIdHeaderLocalName, EventMonitor.ClientIdHeaderNamespace));
            OperationContext.Current.GetCallbackChannel<ICalculatorCallback>().ShowResult(result);
            EventMonitor.Send(EventType.EndCallback);

            Thread.Sleep(5000);
            EventMonitor.Send(EventType.EndExecute);
        }
    }
}
