using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Interface
{
    [ServiceContract(Namespace = "http://www.zhangjin.me/interface")]
    public interface IDepositService
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void Deposit(string accountId, double amount);
    }
}
