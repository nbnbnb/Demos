using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Service
{
    [ServiceBehavior(ConfigurationName = "CalculatorService")]
    public class CalculatorService : ICalculator
    {
        public double Add(double x, double y)
        {
            Console.WriteLine("Add Operator");
            return x + y;
        }

        public double Subtract(double x, double y)
        {
            Console.WriteLine("Subtract Operator");
            return x - y;
        }

        public double Multiply(double x, double y)
        {
            Console.WriteLine("Multiply Operator");
            return x * y;
        }

        public double Divide(int x, int y)
        {
            Console.WriteLine("Divide Operator");
            return x / y;
        }

    }
}
