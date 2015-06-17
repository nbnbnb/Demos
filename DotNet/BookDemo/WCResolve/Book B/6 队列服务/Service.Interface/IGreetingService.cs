using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Service.Interface
{
    [ServiceContract(Namespace = "http://www.zhangjin.me/interface",
        SessionMode=SessionMode.Required,
        ConfigurationName = "IGreetingService")]
    public interface IGreetingService
    {
        [OperationContract(IsOneWay = true)]
        void SayHello(string name);

        [OperationContract(IsOneWay = true)]
        void SayGoodBye(string name);
    }
}
