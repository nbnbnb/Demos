using ASPPatterns.Chap7.UnitOfWork.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap7.UnitOfWork.Model
{
    public class AccountService
    {
        private IAccountRepository _accountRepository;
        private IUnitOfWork _unitOfWork;
        public AccountService(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }
        public void Transfer(Account from, Account to, decimal amount)
        {
            if (from.Balance >= amount)
            {
                from.Balance -= amount;
                to.Balance += amount;
                _accountRepository.Save(from);
                _accountRepository.Save(to);
                _unitOfWork.Commit();
            }
        }
    }
}
