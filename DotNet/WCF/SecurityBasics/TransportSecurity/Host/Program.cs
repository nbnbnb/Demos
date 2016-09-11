using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.Text;

namespace DerivativesCalculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Type serviceType = typeof(DerivativesCalculatorServiceType);

            using(ServiceHost host = new ServiceHost(
                serviceType
                ))
            {
                host.Open();

                Console.WriteLine(
                    "The derivatives calculator service is available."
                );
                Console.ReadKey(true);

                host.Close();
            }
        }
    }
}
