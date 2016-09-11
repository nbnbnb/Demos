using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;


namespace DerivativesCalculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Press any key when the service is ready.");
            Console.ReadKey(true);

            decimal result = 0;


            using (DerivativesCalculatorClient proxy =
                new DerivativesCalculatorClient("WebHostedHTTPSService"))
            {
                proxy.Open();
                result = proxy.CalculateDerivative(
                    new string[] { "MSFT" },
                    new decimal[] { 3 },
                    new string[] { });
                proxy.Close();
            }
            Console.WriteLine(string.Format("Result: {0}", result));

            Console.ReadKey(true);

            using (DerivativesCalculatorClient proxy =
                new DerivativesCalculatorClient("SelfHostedHTTPSService"))
            {
                proxy.Open();
                result = proxy.CalculateDerivative(
                    new string[] { "MSFT" },
                    new decimal[] { 3 },
                    new string[] { });
                proxy.Close();
            }
            Console.WriteLine(string.Format("Result: {0}", result));

            using (DerivativesCalculatorClient proxy =
                new DerivativesCalculatorClient("SelfHostedTCPService"))
            {
                proxy.Open();
                result = proxy.CalculateDerivative(
                    new string[] { "MSFT" },
                    new decimal[] { 3 },
                    new string[] { });
                proxy.Close();
            }
            Console.WriteLine(string.Format("Result: {0}", result));

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey(true);

        }
    }
}

