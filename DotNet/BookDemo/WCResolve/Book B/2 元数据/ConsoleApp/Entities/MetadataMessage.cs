using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Entities
{
    [MessageContract(IsWrapped=false)]
    public class MetadataMessage
    {
        public MetadataMessage(MetadataSet metadata)
        {
            this.Metadata = metadata;
        }

        [MessageBodyMember(Name="Metadata",
            Namespace="http://schemas.xmlsoap.org/ws/2004/09/mex")]
        public MetadataSet Metadata
        {
            get;
            set;
        }
    }
}
