using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace Service.Interface
{
    public static class EventMonitor
    {
        public const string ClientIdHeaderNamespace = "http://www.zhangjin.me";
        public const string ClientIdHeaderLocalName = "ClientId";
        public static event EventHandler<MonitorEventArgs> MonitoringNotificationSended;

        public static void Send(EventType eventType)
        {
            EventHandler<MonitorEventArgs> temp =
                Interlocked.CompareExchange(ref MonitoringNotificationSended, null, null);
            if (temp != null)
            {
                int clientId = OperationContext.Current.IncomingMessageHeaders
                    .GetHeader<int>(ClientIdHeaderLocalName, ClientIdHeaderNamespace);
                MonitoringNotificationSended(null, new MonitorEventArgs(clientId, eventType, DateTime.Now));
            }
        }

        public static void Send(int clientId, EventType eventType)
        {
            EventHandler<MonitorEventArgs> temp =
                Interlocked.CompareExchange(ref MonitoringNotificationSended, null, null);
            if (temp != null)
            {
                MonitoringNotificationSended(null, new MonitorEventArgs(clientId, eventType, DateTime.Now));
            }
        }
    }
}
