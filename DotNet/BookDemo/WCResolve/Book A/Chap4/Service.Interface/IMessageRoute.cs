using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace Service.Interface
{
    [ServiceContract(Namespace = "http://www.zhangjin.me",
        CallbackContract = typeof(ICalculatorCallback))]
    public interface IMessageRoute
    {
        [OperationContract(Action = "*")]
        void DoRoute(Message request);


        [OperationContract(IsOneWay = true)]
        void Add(int a, int b);
    }
}
