using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public class CalculatorCallbackService : ICalculatorCallback
    {
        public void DisplayResult(int result, int a, int b)
        {
            Console.WriteLine("a + b = {2} when a ={0} and b = {1}", a, b, result);
        }
    }
}
