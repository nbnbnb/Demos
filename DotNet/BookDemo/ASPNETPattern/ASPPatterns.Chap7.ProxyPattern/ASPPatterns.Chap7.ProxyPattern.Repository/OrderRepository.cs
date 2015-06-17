using ASPPatterns.Chap7.ProxyPattern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap7.ProxyPattern.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public IEnumerable<Order> FindAllBy(Guid customerId)
        {
            IEnumerable<Order> customerOrders = new List<Order>();
            // Code to connect to the database and populate thecollection
            // of customers’ orders...
            return customerOrders;
        }
    }
}
