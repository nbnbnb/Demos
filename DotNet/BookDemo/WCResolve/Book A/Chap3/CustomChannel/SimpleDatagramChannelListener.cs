using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;

namespace CustomChannel
{
    public class SimpleDatagramChannelListener<TChannel> :
        SimpleChannelListenerBase<TChannel> where TChannel : class,IChannel
    {
        public SimpleDatagramChannelListener(BindingContext context)
            : base(context)
        {
            this.Print("SimpleDatagramChannelListener()");
        }

        protected override TChannel OnAcceptChannel(TimeSpan timeout)
        {
            this.Print("OnAcceptChannel()");
            IReplyChannel innerChannel =
                (IReplyChannel)this.InnerChannelListener.AcceptChannel(timeout);
            return new SimpleReplyChannel(this, innerChannel) as TChannel;
        }

        protected override TChannel OnEndAcceptChannel(IAsyncResult result)
        {
            this.Print("OnEndAcceptChannel()");
            IReplyChannel innerChannel =
                this.InnerChannelListener.EndAcceptChannel(result) as IReplyChannel;

            return innerChannel as TChannel;
        }
    }
}
