using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Security;

namespace DerivativesCalculator
{
    public class DerivativesCalculatorServiceType: IDerivativesCalculator
    {
        #region IDerivativesCalculator Members

        [OperationBehavior(Impersonation=ImpersonationOption.Required)]
        decimal IDerivativesCalculator.CalculateDerivative(
            string[] symbols, 
            decimal[] parameters, 
            string[] functions)
        {
            decimal result = 0;
            using (DerivativesCalculatorClient proxy =
                new DerivativesCalculatorClient("BackOfficeDerivativesCalculator"))
            {
                proxy.Open();
                result = proxy.CalculateDerivative(
                    new string[] { "MSFT" },
                    new decimal[] { 3 },
                    new string[] { });
                proxy.Close();
            }
            return result;
        }

        void IDerivativesCalculator.DoNothing()
        {
            return;
        }

        #endregion
    }
}
