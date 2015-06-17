using ConsoleApp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Concrete
{

    // 之所以实现 IMetadataProvisionService 接口，是为了在进行服务寄宿时
    // 满足“服务类型必须实现终结点契约接“的约束
    [ServiceMetadataBehavior(HttpGetEnabled = true,
        HttpGetUrl = "http://localhost:9999/calculatorservice/mex")]
    public class CalculatorService : ICalculator, IMetadataProvisionService
    {
        public Message Get(System.ServiceModel.Channels.Message request)
        {
            throw new NotImplementedException();
        }

        public double Add(double x, double y)
        {
            return x + y;
        }
    }
}
