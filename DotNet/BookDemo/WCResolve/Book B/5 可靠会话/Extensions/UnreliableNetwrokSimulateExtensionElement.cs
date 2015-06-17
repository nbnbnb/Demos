using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Configuration;

namespace Extensions
{
    public class UnreliableNetwrokSimulateExtensionElement : BindingElementExtensionElement
    {
        [ConfigurationProperty("dropRate", IsRequired = false, DefaultValue = 20)]
        public int DropRate
        {
            get
            {
                return (int)this["dropRate"];
            }
            set
            {
                this["dropRate"] = value;
            }
        }

        public override Type BindingElementType
        {
            get
            {
                return typeof(UnreliableNetworkSimulateBindingElement);
            }
        }

        protected override System.ServiceModel.Channels.BindingElement CreateBindingElement()
        {
            return new UnreliableNetworkSimulateBindingElement(this.DropRate);
        }
    }
}
