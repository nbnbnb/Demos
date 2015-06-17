using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NinjectDemo.Domain.Abstract;

namespace NinjectDemo.Domain.Concrete
{
    public class CustomValueCalculator:IValueCalculator
    {
        private decimal _discount = 10M;

        public CustomValueCalculator(decimal discount)
        {
            this._discount = discount;
        }

        public decimal DisCount
        {
            get { return _discount; }
            set { _discount = value; }
        }

        public decimal ValueProducts(params Entities.Product[] products)
        {
            return products.Sum(p => p.Price) - DisCount;
        }

        public CustomValueCalculator()
        {

        }

    }
}
