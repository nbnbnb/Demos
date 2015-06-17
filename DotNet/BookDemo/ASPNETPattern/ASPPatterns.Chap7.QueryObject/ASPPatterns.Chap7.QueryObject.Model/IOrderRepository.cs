using ASPPatterns.Chap7.QueryObject.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap7.QueryObject.Model
{
    public interface IOrderRepository
    {
        IEnumerable<Order> FindBy(Query query);
    }
}
