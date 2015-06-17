using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Text;

namespace CustomEncoding
{
    public class CompressionTextEncodingElement : BindingElementExtensionElement
    {
        [ConfigurationProperty("textEncoding")]
        public TextMessageEncodingElement TextEncoding
        {
            get
            {
                return (TextMessageEncodingElement)this["textEncoding"];
            }
            set
            {
                this["textEncoding"] = value;
            }
        }

        [ConfigurationProperty("algorithm", DefaultValue = CompressionAlgorithm.GZip)]
        public CompressionAlgorithm Algorithm
        {
            get { return (CompressionAlgorithm)this["algorithm"]; }
            set { this["algorithm"] = value; }
        }

        [ConfigurationProperty("minMessageSize", DefaultValue = "1024")]
        public int MinMessageSize
        {
            get { return (int)this["minMessageSize"]; }
            set { this["minMessageSize"] = value; }
        }

        public override Type BindingElementType
        {
            get
            {
                return typeof(CompressionTextMessageEncodingBindingElement);
            }
        }

        protected override System.ServiceModel.Channels.BindingElement CreateBindingElement()
        {
            TextMessageEncodingBindingElement textBindingElement = new TextMessageEncodingBindingElement();
            if (null != this.TextEncoding)
            {
                this.TextEncoding.ApplyConfiguration(textBindingElement);
            }

            return new CompressionTextMessageEncodingBindingElement(textBindingElement, this.Algorithm, this.MinMessageSize);
        }
    }
}
