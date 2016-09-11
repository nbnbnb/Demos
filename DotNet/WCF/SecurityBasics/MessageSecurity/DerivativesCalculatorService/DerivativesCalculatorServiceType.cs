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

        decimal IDerivativesCalculator.CalculateDerivative(
            string[] symbols, 
            decimal[] parameters, 
            string[] functions)
        {
            WindowsIdentity identity = ServiceSecurityContext.Current.WindowsIdentity;
            if (identity != null)
            {
                string name = identity.Name;
                if (!(string.IsNullOrEmpty(name)))
                {
                    Console.WriteLine("User is {0}.", name);
                }

            }
            return new Calculator().CalculateDerivative(
                symbols, parameters, functions);
        }

        void IDerivativesCalculator.DoNothing()
        {
            return;
        }

        #endregion
    }
}
