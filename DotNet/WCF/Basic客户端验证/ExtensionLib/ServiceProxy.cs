using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionLib
{
    public class ServiceProxy<TChannel> : RealProxy
    {
        public TChannel Channel { get; private set; }

        private ICommunicationObject _innerChannel;

        public ServiceProxy(string endpointConfigName)
            : base(typeof(TChannel))
        {
            ChannelFactory<TChannel> channelFactory =
                ChannelFactories.GetFactory<TChannel>(endpointConfigName);

            _innerChannel = (ICommunicationObject)channelFactory.CreateChannel();

            // 创建透明代理
            this.Channel = (TChannel)base.GetTransparentProxy();
        }

        // 对透明代理的调用将会指向到 Invoke 方法
        // 在内部发起对真实代理的调用
        public override IMessage Invoke(IMessage msg)
        {
            IMethodCallMessage methodCall = (IMethodCallMessage)msg;
            object[] args = new object[methodCall.ArgCount];
            methodCall.Args.CopyTo(args, 0);
            try
            {
                object ret = methodCall.MethodBase.Invoke(_innerChannel, args);
                _innerChannel.Close(); // 关闭服务代理对象
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
                    _innerChannel.Abort();  // 终止出错的信道
                }

                return new ReturnMessage(innerEx, methodCall);
            }
        }
    }
}
