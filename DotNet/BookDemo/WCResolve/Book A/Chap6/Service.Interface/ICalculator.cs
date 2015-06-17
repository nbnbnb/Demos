using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Service.Interface
{
    [ServiceContract(
        ConfigurationName = "CalculatorContract",
        Namespace = "http://www.zhangjin.me")]
    public interface ICalculator
    {
        [OperationContract]
        double Add(int x, int y);

        [OperationContract]
        double Subtract(int x, int y);

        [OperationContract]
        double Multiply(int x, int y);

        [OperationContract]
        double Divide(int x, int y);
    }
}
