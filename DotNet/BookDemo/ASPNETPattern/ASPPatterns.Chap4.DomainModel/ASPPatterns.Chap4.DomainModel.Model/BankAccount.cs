using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap4.DomainModel.Model
{
    public class BankAccount
    {
        private decimal _balance;
        private Guid _accountNo;
        private string _customerRef;
        private IList<Transaction> _transaction;

        public BankAccount(Guid id, decimal balance,
            IList<Transaction> transactions, string customerRef)
        {
            AccountNo = id;
            _balance = balance;
            _transaction = transactions;
            _customerRef = customerRef;
        }

        public BankAccount()
            : this(Guid.NewGuid(), 0, new List<Transaction>(), "") { }

        public Guid AccountNo
        {
            get { return _accountNo; }
            internal set { _accountNo = value; }
        }

        public decimal Balance
        {
            get { return _balance; }
            internal set { _balance = value; }
        }

        public string CustomerRef
        {
            get { return _customerRef; }
            set { _customerRef = value; }
        }

        public bool CanWithdraw(decimal amount)
        {
            return Balance >= amount;
        }

        public void Withdraw(decimal amount, string reference)
        {
            if (CanWithdraw(amount))
            {
                Balance -= amount;
                _transaction.Add(new Transaction(0m, amount,
                    reference, DateTime.Now));
            }
            else
            {
                throw new InsufficientExecutionStackException();
            }
        }

        public void Deposit(decimal amount, string reference)
        {
            Balance += amount;
            _transaction.Add(new Transaction(amount, 0m, reference, DateTime.Now));
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            return _transaction;
        }
    }
}
