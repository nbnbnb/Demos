using ASPPatterns.Chap7.UnitOfWork.Infrastructure;
using ASPPatterns.Chap7.UnitOfWork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap7.UnitOfWork.Repository
{
    public class AggregateRepository<TAggregate> : IAggregateRepository<TAggregate>, IAggregateUnitOfWorkRepository<TAggregate> where TAggregate : IAggregateRoot
    {
        private IAggregateUnitOfWork<TAggregate> _unitOfWork;

        public IAggregateUnitOfWork<TAggregate> UnitOfWork
        {
            get
            {
                return _unitOfWork;
            }
        }

        public AggregateRepository(IAggregateUnitOfWork<TAggregate> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void PersistUpdateOf(TAggregate entity)
        {
            // ADO.NET code to update the entity...
        }
        public void PersistCreationOf(TAggregate entity)
        {
            // ADO.NET code to add the entity...
        }
        public void PersistDeletionOf(TAggregate entity)
        {
            // ADO.NET code to delete the entity...
        }

        public void Save(TAggregate item)
        {
            _unitOfWork.RegisterAmended(item, this);
        }

        public void Add(TAggregate item)
        {
            _unitOfWork.RegisterNew(item, this);
        }

        public void Remove(TAggregate item)
        {
            _unitOfWork.RegisterRemoved(item, this);
        }
    }
}
