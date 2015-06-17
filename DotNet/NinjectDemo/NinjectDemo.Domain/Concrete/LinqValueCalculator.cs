using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NinjectDemo.Domain.Abstract;

namespace NinjectDemo.Domain.Concrete
{
    public class LinqValueCalculator:IValueCalculator
    {
        public decimal ValueProducts(params Entities.Product[] products)
        {
            return products.Sum(m => m.Price);
        }
    }
}
