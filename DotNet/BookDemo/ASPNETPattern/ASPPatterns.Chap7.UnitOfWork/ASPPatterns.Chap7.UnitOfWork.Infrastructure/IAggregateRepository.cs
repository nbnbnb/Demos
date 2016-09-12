using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap7.UnitOfWork.Infrastructure
{
    public interface IAggregateRepository<TAggregate> where TAggregate : IAggregateRoot
    {
        void Save(TAggregate item);
        void Add(TAggregate item);
        void Remove(TAggregate item);

        IAggregateUnitOfWork<TAggregate> UnitOfWork { get; }
    }
}
