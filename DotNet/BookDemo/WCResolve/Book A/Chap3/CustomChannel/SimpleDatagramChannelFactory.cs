using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;

namespace CustomChannel
{
    public class SimpleDatagramChannelFactory<TChannel> :
        SimpleChannelFactoryBase<TChannel>
    {

        public SimpleDatagramChannelFactory(BindingContext context) :
            base(context)
        {
            this.Print("SimpleDatagramChannelFactory()");
        }

        protected override TChannel OnCreateChannel(System.ServiceModel.EndpointAddress address, Uri via)
        {
            this.Print("OnCreateChannel()");
            IRequestChannel innerChannel =
                this.InnerChannelFactory.CreateChannel(address, via) as IRequestChannel;

            return (TChannel)(object)new SimpleRequestChannel(this, innerChannel);
        }
    }
}
