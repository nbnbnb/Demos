using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NinjectDemo.Domain.Abstract;

namespace NinjectDemo.Domain.Concrete
{
    public class LimitShoppingCart:ShoppingCart
    {

        public LimitShoppingCart(IValueCalculator calcParam)
            : base(calcParam)
        {
            // nothing to do here
        }

        public override decimal CalculateStockValue()
        {
            var filteredPruducts = products.Where(e => { return e.Price < ItemLimit; });

            return calculator.ValueProducts(filteredPruducts.ToArray());           
        }

        public decimal ItemLimit { get; set; }
    }
}
