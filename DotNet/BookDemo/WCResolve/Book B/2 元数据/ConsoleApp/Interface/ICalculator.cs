using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Interface
{
    [ServiceContract(Namespace="http://www.zhangjin.me/interface")]
    public interface ICalculator
    {
        [OperationContract]
        double Add(double x, double y);
    }
}
