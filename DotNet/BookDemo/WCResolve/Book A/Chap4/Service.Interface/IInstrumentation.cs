using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Service.Interface
{
    [ServiceContract(Namespace = "http://www.zhangjin.me")]
    public interface IInstrumentation : IEventLog
    {
        [OperationContract]
        void IncreasePerformanceCounter(string catelogName, string counterName);

        [OperationContract]
        void SetWmiProperty(string propertyName, object value);
    }
}
