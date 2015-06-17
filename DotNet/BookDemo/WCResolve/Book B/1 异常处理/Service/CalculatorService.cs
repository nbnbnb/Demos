using Service.Interface;
using Service.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel;
using System.Text;

namespace Service
{
    [ServiceBehavior(ConfigurationName = "CalculatorService")]
    public class CalculatorService : ICalculator
    {
        public double Add(double x, double y)
        {
            return x + y;
        }

        public double Subtract(double x, double y)
        {
            return x - y;
        }

        public double Multiply(double x, double y)
        {
            return x * y;
        }

        public double Divide(int x, int y)
        {
            if (y == 0)
            {
                //ExceptionDetail ex = new ExceptionDetail(new DivideByZeroException());
                //throw new FaultException<ExceptionDetail>(ex, ex.Message);

                // 如果抛出的为系统异常，则 Host 将会直接关闭
                // throw new OutOfMemoryException("测试内存异常");

                // 如果抛出的为应用程序异常
                // WCF 框架将会捕获此异常，然后保证成 FaultException<ExceptionDetail> 对象
                // 序列化后返回给客户端
                // 如果采用会话，会话信道将会终止，实例上下文将会回收
            }

            return x / y;
        }
    }
}
