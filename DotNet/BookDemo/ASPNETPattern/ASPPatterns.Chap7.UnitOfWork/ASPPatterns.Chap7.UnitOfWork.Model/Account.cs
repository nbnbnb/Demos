using ASPPatterns.Chap7.UnitOfWork.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap7.UnitOfWork.Model
{
    public class Account : IAggregateRoot
    {
        public decimal Balance { get; set; }
    }
}
