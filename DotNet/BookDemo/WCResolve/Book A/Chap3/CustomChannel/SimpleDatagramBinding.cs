using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;

namespace CustomChannel
{
    public class SimpleDatagramBinding : Binding
    {
        private TransportBindingElement transportBindingElement;
        private BindingElementCollection bindingElementCollection;

        public SimpleDatagramBinding()
        {
            BindingElement[] bindingElements = {
                new SimpleDatagramBindingElement(),
                new TextMessageEncodingBindingElement(),
                new HttpTransportBindingElement()
            };

            bindingElementCollection = new BindingElementCollection(bindingElements);

            transportBindingElement = (TransportBindingElement)bindingElements[2];
        }

        public override BindingElementCollection CreateBindingElements()
        {
            return bindingElementCollection;
        }

        public override string Scheme
        {
            get
            {
                return transportBindingElement.Scheme;
            }
        }
    }
}
