using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NinjectDemo.Domain.Abstract;
using NinjectDemo.Domain.Entities;

namespace NinjectDemo.Domain.Concrete
{
    public class IterativeValueCalculator:IValueCalculator
    {

        public decimal ValueProducts(params Entities.Product[] products)
        {
            decimal totalValue=0;
            foreach(Product p in products)
            {
                totalValue += p.Price - 2M;
            }
            return totalValue;
        }
    }
}
