using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;

namespace Extensions
{
    public class UnreliableNetworkSimulateBindingElement:BindingElement
    {
        public int DropRate { get; private set; }

        public UnreliableNetworkSimulateBindingElement(int dropRate)
        {
            this.DropRate = dropRate;
        }

        public override BindingElement Clone()
        {
            return new UnreliableNetworkSimulateBindingElement(this.DropRate);
        }

        public override T GetProperty<T>(BindingContext context)
        {
            return context.GetInnerProperty<T>();
        }

        public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
        {
            return (IChannelFactory<TChannel>)new UnreliableNetworkSimulateChannelFactory<TChannel>(context, this.DropRate);
        }
    }
}
