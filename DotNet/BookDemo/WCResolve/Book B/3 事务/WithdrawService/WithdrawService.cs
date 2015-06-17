using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace WithdrawService
{
    public class WithdrawService : IWithdrawService
    {
        [OperationBehavior(TransactionScopeRequired = true)]
        public void Withdraw(string fromAccountId, string toAccountId, double amount)
        {
            //
        }
    }
}
