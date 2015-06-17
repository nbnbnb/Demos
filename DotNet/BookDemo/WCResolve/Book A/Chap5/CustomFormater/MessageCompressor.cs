using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;

namespace CustomFormater
{
    public class MessageCompressor
    {
        public MessageCompressor(CompressionAlgorithm algorithm, int minMessageSize)
        {

        }

        public Message CompressMessage(Message sourceMessage)
        {
            return null;
        }

        public Message DecompressMessage(Message sourceMessage)
        {
            return null;
        }
    }
}
