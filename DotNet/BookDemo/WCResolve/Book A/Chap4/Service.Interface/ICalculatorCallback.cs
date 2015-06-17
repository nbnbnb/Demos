using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Service.Interface
{
    public interface ICalculatorCallback
    {
        [OperationContract(IsOneWay = true)]
        void DisplayResult(int result, int a, int b);
    }
}
