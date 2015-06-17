using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Channels;
using System.Text;

namespace CustomChannel
{
    public class SimpleSessionChannelListener<TChannel> :
        SimpleChannelListenerBase<TChannel> where TChannel : class,IChannel
    {
        public SimpleSessionChannelListener(BindingContext context)
            : base(context)
        {
            this.Print("SimpleSessionChannelListener()");
        }

        protected override TChannel OnAcceptChannel(TimeSpan timeout)
        {
            this.Print("OnAcceptChannel()");

            IDuplexSessionChannel innerChannel =
                this.InnerChannelListener.AcceptChannel(timeout) as IDuplexSessionChannel;

            if (innerChannel != null)
            {
                return new SimpleDuplexSessionChannel(this, innerChannel) as TChannel;
            }

            return default(TChannel);
        }

        protected override TChannel OnEndAcceptChannel(IAsyncResult result)
        {
            this.Print("OnEndAcceptChannel()");

            IDuplexSessionChannel innerChannel =
              this.InnerChannelListener.EndAcceptChannel(result) as IDuplexSessionChannel;

            if (innerChannel != null)
            {
                return new SimpleDuplexSessionChannel(this, innerChannel) as TChannel;
            }

            return default(TChannel);

        }
    }
}
