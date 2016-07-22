using ASPPatterns.Chap7.UnitOfWork.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap7.UnitOfWork.Model
{
    public class AggregateService<TAggregate> where TAggregate : IAggregateRoot
    {
        private IAggregateRepository<TAggregate> _repository;
        public AggregateService(IAggregateRepository<TAggregate> repository)
        {
            _repository = repository;
        }

        public void DoSomething(TAggregate toAdd, TAggregate toUpdate, TAggregate toDelete)
        {
            _repository.Save(toUpdate);
            _repository.Add(toAdd);
            _repository.Remove(toDelete);

            _repository.UnitOfWork.Commit();
        }
    }
}
