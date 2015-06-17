using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace BankingService
{
    public class BankingService : IBankingService
    {
        [OperationBehavior(TransactionScopeRequired = true)]
        public void Transfer(string fromAccountId, string toAccountId, double amount)
        {
            ServiceInvoker.Invoke<IWithdrawService>(proxy =>
                proxy.Withdraw(fromAccountId, toAccountId, amount),
                "withdrawservice");

            ServiceInvoker.Invoke<IDepositService>(proxy =>
                proxy.Deposit(toAccountId, amount),
                "depositservice");
        }
    }
}
