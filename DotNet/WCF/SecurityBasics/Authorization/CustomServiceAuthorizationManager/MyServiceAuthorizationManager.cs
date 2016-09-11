using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Claims;
using System.ServiceModel;
using System.Text;

namespace DerivativesCalculator
{
    public class MyServiceAuthorizationManager: ServiceAuthorizationManager
    {
        public override bool CheckAccess(OperationContext operationContext)
        {
            if (string.Compare(operationContext.IncomingMessageHeaders.Action, "CalculateDerivative", true) == 0)
            {
                ReadOnlyCollection<ClaimSet> claimSets = operationContext.ServiceSecurityContext.AuthorizationContext.ClaimSets;
                ClaimSet claimSet = claimSets[0];
                foreach (Claim claim in claimSet)
                {
                    if(string.Compare(claim.ClaimType,"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name",true)==0)
                    {
                        if (string.Compare(claim.Right, "http://schemas.xmlsoap.org/ws/2005/05/identity/right/identity", true) == 0)
                        {
                            if (string.Compare((string)claim.Resource, "don", true) == 0)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}
