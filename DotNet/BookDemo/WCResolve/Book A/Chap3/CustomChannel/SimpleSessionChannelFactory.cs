using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;

namespace CustomChannel
{
    public class SimpleSessionChannelFactory<TChannel> :
        SimpleChannelFactoryBase<TChannel>
    {
        public SimpleSessionChannelFactory(BindingContext context)
            : base(context)
        {
            this.Print("SimpleSessionChannelFactory()");
        }

        protected override TChannel OnCreateChannel(System.ServiceModel.EndpointAddress address, Uri via)
        {
            this.Print("OnCreateChannel()");

            IDuplexSessionChannel innerChannel =
                this.InnerChannelFactory.CreateChannel(address, via) as IDuplexSessionChannel;
            if (null != innerChannel)
            {
                return (TChannel)(object)new SimpleDuplexSessionChannel(this, innerChannel);
            }
            return default(TChannel);
        }
    }
}
