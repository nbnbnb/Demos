using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace Service
{
    public class ServiceChannelProxy<TChannel> : RealProxy
    {
        public Uri Address { get; private set; }

        public MessageVersion MessageVersion { get; private set; }

        public IDictionary<string, IClientMessageFormatter> MessageFormatters { get; private set; }

        public MessageEncoderFactory MessageEncoderFactory { get; private set; }

        public ServiceChannelProxy(Uri address,MessageVersion messageVersion,MessageEncoderFactory encoderFactory)
            : base(typeof(TChannel))
        {
            this.Address = address;
            this.MessageVersion = messageVersion;
            this.MessageEncoderFactory = encoderFactory;
            this.MessageFormatters = new Dictionary<string, IClientMessageFormatter>();
        }

        public override IMessage Invoke(IMessage msg)
        {
            IMethodCallMessage methodCall = (IMethodCallMessage)msg;

            object[] attributes = methodCall.MethodBase.GetCustomAttributes(typeof(OperationContractAttribute), true);

            OperationContractAttribute attribute = (OperationContractAttribute)attributes[0];
            string operationName = string.IsNullOrEmpty(attribute.Name) ? methodCall.MethodName : attribute.Name;

            return null;
        }
    }
}