using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.ServiceModel;
using System.Text;

namespace AutoCloser
{
    public class ServiceProxy<TChannel> : RealProxy
    {
        public TChannel Channel { get; private set; }

        private ICommunicationObject innerChannel;

        public ServiceProxy(string endpointConfigName)
            : base(typeof(TChannel))
        {
            ChannelFactory<TChannel> channelFactory =
                ChannelFactories.GetFactory<TChannel>(endpointConfigName);
            this.innerChannel = (ICommunicationObject)channelFactory.CreateChannel();  // 真实代理
            this.Channel = (TChannel)this.GetTransparentProxy(); // 透明代理
        }

        public override IMessage Invoke(IMessage msg)
        {
            IMethodCallMessage methodCall = (IMethodCallMessage)msg;
            object[] args = new object[methodCall.ArgCount];
            //object[] args = (object[])Array.CreateInstance(typeof(object), methodCall.ArgCount);
            methodCall.Args.CopyTo(args, 0);
            try
            {
                object ret = methodCall.MethodBase.Invoke(this.innerChannel, args);
                this.innerChannel.Close();
                return new ReturnMessage(ret, args, methodCall.ArgCount, methodCall.LogicalCallContext, methodCall);
            }
            catch (Exception ex)
            {
                Exception innerEx = ex.InnerException;
                if (null == innerEx)
                {
                    return new ReturnMessage(ex, methodCall);
                }
                if (innerEx is TimeoutException || innerEx is CommunicationException)
                {
                    this.innerChannel.Abort();
                }

                return new ReturnMessage(innerEx, methodCall);
            }
        }
    }
}
