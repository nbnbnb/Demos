using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Transactions;

namespace Service
{
    [ServiceBehavior(ConfigurationName = "GreetingService", TransactionAutoCompleteOnSessionClose = true)]
    public class GreetingService : IGreetingService
    {
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = false)]
        public void SayHello(string name)
        {
            Console.WriteLine("[{1}]Hello, {0}", name,
                 Transaction.Current.TransactionInformation.DistributedIdentifier);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = false)]
        public void SayGoodBye(string name)
        {
            Console.WriteLine("[{1}]Goodbye, {0}", name,
                Transaction.Current.TransactionInformation.DistributedIdentifier);
        }
    }
}
