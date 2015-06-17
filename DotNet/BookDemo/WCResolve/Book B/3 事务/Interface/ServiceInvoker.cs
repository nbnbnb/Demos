using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Interface
{
    public static class ServiceInvoker
    {
        public static void Invoke<TChannel>(Action<TChannel> action,
            string endpointConfigurationName)
        {
            using (ChannelFactory<TChannel> channelFactory =
                new ChannelFactory<TChannel>(endpointConfigurationName))
            {
                TChannel channel = channelFactory.CreateChannel();
                using (channel as IDisposable)
                {
                    try
                    {
                        action(channel);
                    }
                    catch (TimeoutException)
                    {
                        (channel as ICommunicationObject).Abort();
                        throw;
                    }
                    catch (CommunicationException)
                    {
                        (channel as ICommunicationObject).Abort();
                        throw;
                    }
                }
            }
        }
    }
}
