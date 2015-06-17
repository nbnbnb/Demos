using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace CustomFormater
{
    public class CompressionMessageFormatter : IDispatchMessageFormatter, IClientMessageFormatter
    {
        public MessageCompressor MessageCompressor { get; private set; }
        public IDispatchMessageFormatter InnerDispatchMessageFormater { get; private set; }
        public IClientMessageFormatter InnerClientMessageFormatter { get; private set; }

        public CompressionMessageFormatter(MessageCompressor messageCompressor,
            IDispatchMessageFormatter innerDispatchMessageFormatter,
            IClientMessageFormatter innerClientMessageFormatter)
        {
            this.MessageCompressor = messageCompressor;
            this.InnerClientMessageFormatter = InnerClientMessageFormatter;
            this.InnerDispatchMessageFormater = innerDispatchMessageFormatter;
        }

        #region Server

        public void DeserializeRequest(System.ServiceModel.Channels.Message message, object[] parameters)
        {
            message = this.MessageCompressor.DecompressMessage(message);
            this.InnerDispatchMessageFormater.DeserializeRequest(message, parameters);
        }

        public System.ServiceModel.Channels.Message SerializeReply(System.ServiceModel.Channels.MessageVersion messageVersion, object[] parameters, object result)
        {
            Message message = this.InnerDispatchMessageFormater.SerializeReply(messageVersion, parameters, result);
            return this.MessageCompressor.CompressMessage(message);
        }

        #endregion

        #region Client

        public object DeserializeReply(System.ServiceModel.Channels.Message message, object[] parameters)
        {
            message = this.MessageCompressor.DecompressMessage(message);
            return this.InnerClientMessageFormatter.DeserializeReply(message, parameters);
        }

        public System.ServiceModel.Channels.Message SerializeRequest(System.ServiceModel.Channels.MessageVersion messageVersion, object[] parameters)
        {
            Message message = this.InnerClientMessageFormatter.SerializeRequest(messageVersion, parameters);
            return this.MessageCompressor.CompressMessage(message);
        }

        #endregion
    }
}
