using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionLib
{
    public class OperationInvoker<TChannel>
    {
        public string EndpointName { get; private set; }

        public OperationInvoker(string endpointName)
        {
            EndpointName = endpointName;
        }

        public void Invoke(Action<TChannel> serviceInvocation)
        {
            ChannelFactory<TChannel> channelFactory =
                ChannelFactories.GetFactory<TChannel>(this.EndpointName);

            TChannel channel = channelFactory.CreateChannel();
            Invoke(serviceInvocation, channel);
        }

        public TResult Invoke<TResult>(Func<TChannel, TResult> serviceInvocation)
        {
            ChannelFactory<TChannel> channelFactory =
                           ChannelFactories.GetFactory<TChannel>(this.EndpointName);
            TChannel channel = channelFactory.CreateChannel();
            return Invoke(serviceInvocation, channel);
        }

        // 无返回值的方法执行
        protected static void Invoke(Action<TChannel> serviceInvocation, TChannel channel)
        {
            ICommunicationObject communicationObject = (ICommunicationObject)channel;
            try
            {
                serviceInvocation(channel);
                // 关闭服务代理
                communicationObject.Close();
            }
            catch (Exception ex)
            {
                HandleException(ex, communicationObject);
            }
        }

        // 有返回值方法的执行
        protected static TResult Invoke<TResult>(Func<TChannel, TResult> serviceInvocation, TChannel channel)
        {
            ICommunicationObject communication = (ICommunicationObject)channel;
            TResult result = default(TResult);
            try
            {
                result = serviceInvocation(channel);
                // 关闭服务代理
                communication.Close();
            }
            catch (Exception ex)
            {
                HandleException(ex, communication);
            }
            return result;
        }

        private static void HandleException(Exception ex, ICommunicationObject channel)
        {
            if (ex is TimeoutException || ex is CommunicationException)
            {
                // 服务代理出现异常
                // 终止服务代理
                channel.Abort();
            }

            throw ex;
        }

    }
}
