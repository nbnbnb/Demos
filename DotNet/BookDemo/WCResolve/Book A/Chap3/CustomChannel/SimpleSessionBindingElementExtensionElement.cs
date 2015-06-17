using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;

namespace CustomChannel
{
    public class SimpleSessionBindingElementExtensionElement : BindingElementExtensionElement
    {
        public override Type BindingElementType
        {
            get
            {
                return typeof(SimpleSessionBindingElement);
            }
        }

        protected override System.ServiceModel.Channels.BindingElement CreateBindingElement()
        {
            return new SimpleSessionBindingElement();
        }
    }
}
