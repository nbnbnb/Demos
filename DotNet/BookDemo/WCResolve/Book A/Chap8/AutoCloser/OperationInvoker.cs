using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace AutoCloser
{
    public class OperationInvoker<TChannel>
    {
        public string EndpointName { get; private set; }

        public OperationInvoker(string endpointName)
        {
            this.EndpointName = endpointName;
        }

        public void Invoke(Action<TChannel> serviceInvocation)
        {
            ChannelFactory<TChannel> channelFactory =
                ChannelFactories.GetFactory<TChannel>(this.EndpointName);

            TChannel channel = channelFactory.CreateChannel();

            OperationInvokerHelper.Invoke(serviceInvocation, channel);
        }

        public TResult Invoke<TResult>(Func<TChannel, TResult> serviceInvocation)
        {
            ChannelFactory<TChannel> channelFactory =
                ChannelFactories.GetFactory<TChannel>(this.EndpointName);
            TChannel channel = channelFactory.CreateChannel();
            return OperationInvokerHelper.Invoke(serviceInvocation, channel);
        }
    }
}
