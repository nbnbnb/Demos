using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace DepositService
{
    public class DepositService : IDepositService
    {
        [OperationBehavior(TransactionScopeRequired = true)]
        public void Deposit(string accountId, double amount)
        {
            // 
        }
    }
}
