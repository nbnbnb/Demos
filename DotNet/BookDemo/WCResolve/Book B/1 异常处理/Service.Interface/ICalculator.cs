using Service.Interface.Entities;
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
        double Add(double x, double y);

        [OperationContract]
        double Subtract(double x, double y);

        [OperationContract]
        double Multiply(double x, double y);

        [OperationContract]
        [FaultContract(typeof(CalculationError))]
        double Divide(int x, int y);
    }
}
