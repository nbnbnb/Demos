using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Permissions;
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
