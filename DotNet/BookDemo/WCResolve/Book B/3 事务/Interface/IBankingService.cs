using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Interface
{
    [ServiceContract(Name = "http://www.zhangjin.me/interface")]
    public interface IBankingService
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void Transfer(string fromAccountId, string toAccountId, double amount);
    }
}
