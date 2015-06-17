using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Interface
{
    [ServiceContract(ConfigurationName = "IMetadataProvisionService",
                        Name = "IMetadataProvisionService",
                        Namespace="http://schemas.microsoft.com/2006/04/mex")]
    public interface IMetadataProvisionService
    {
        [OperationContract(
            Action = "http://schemas.xmlsoap.org/ws/2004/09/transfer/Get",
            ReplyAction = "http://schemas.xmlsoap.org/ws/2004/09/transfer/GetResponse")]
        Message Get(Message request);
    }
}
