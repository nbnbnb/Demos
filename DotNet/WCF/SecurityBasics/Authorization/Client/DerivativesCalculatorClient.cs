using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;

namespace DerivativesCalculator
{
    public class DerivativesCalculatorClient: ClientBase<IDerivativesCalculator>, IDerivativesCalculator
    {
        public DerivativesCalculatorClient(string endpointConfigurationName)
            : base(endpointConfigurationName)
        {
        }

        #region IDerivativesCalculator Members

        public decimal CalculateDerivative(string[] symbols, decimal[] parameters, string[] functions)
        {
            return base.Channel.CalculateDerivative(symbols, parameters, functions);
        }

        public void DoNothing()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}