using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap7.UnitOfWork.Infrastructure
{
    public interface IAggregateUnitOfWorkRepository<TAggregate> where TAggregate : IAggregateRoot
    {
        void PersistCreationOf(TAggregate entity);
        void PersistUpdateOf(TAggregate entity);
        void PersistDeletionOf(TAggregate entity);
    }
}
