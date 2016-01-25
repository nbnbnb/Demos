using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionLib;
using Service.Interface;

namespace Client
{
    public class CalculatorServiceProxy : ServiceProxyBase<ICalculator>, ICalculator
    {
        public CalculatorServiceProxy()
            : base("pipePoint") { }

        public double Add(double x, double y)
        {
            return this.Invoker.Invoke<double>(proxy => proxy.Add(x, y));
        }

        public double Subtract(double x, double y)
        {
            return this.Invoker.Invoke<double>(proxy => proxy.Subtract(x, y));
        }

        public double Multiply(double x, double y)
        {
            return this.Invoker.Invoke<double>(proxy => proxy.Multiply(x, y));
        }

        public double Divide(int x, int y)
        {
            return this.Invoker.Invoke<double>(proxy => proxy.Divide(x, y));
        }
    }
}
