using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;

namespace CustomChannel
{
    public class SimpleDatagramBindingElementExtensionElement : BindingElementExtensionElement
    {
        public override Type BindingElementType
        {
            get
            {
                return typeof(SimpleDatagramBindingElement);
            }
        }

        protected override System.ServiceModel.Channels.BindingElement CreateBindingElement()
        {
            return new SimpleDatagramBindingElement();
        }
    }
}
