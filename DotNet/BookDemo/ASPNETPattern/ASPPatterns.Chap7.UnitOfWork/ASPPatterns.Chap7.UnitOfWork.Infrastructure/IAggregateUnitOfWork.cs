using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap7.UnitOfWork.Infrastructure
{
    public interface IAggregateUnitOfWork<TAggregate> where TAggregate : IAggregateRoot
    {
        void RegisterAmended(TAggregate entity, IAggregateUnitOfWorkRepository<TAggregate> unitofWorkRepository);
        void RegisterNew(TAggregate entity, IAggregateUnitOfWorkRepository<TAggregate> unitofWorkRepository);
        void RegisterRemoved(TAggregate entity, IAggregateUnitOfWorkRepository<TAggregate> unitofWorkRepository);
        void Commit();
    }
}
