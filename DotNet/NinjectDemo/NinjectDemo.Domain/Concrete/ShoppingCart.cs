using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NinjectDemo.Domain.Abstract;
using NinjectDemo.Domain.Entities;

namespace NinjectDemo.Domain.Concrete
{
    public class ShoppingCart
    {
        protected IValueCalculator calculator;

        protected Product[] products;

        public ShoppingCart(IValueCalculator calcParam)
        {
            calculator = calcParam;

            // define the set of products to sum 
            products = new Product[]{ 
                    new Product() { Name = "Kayak", Price = 199M}, 
                    new Product() { Name = "Lifejacket", Price = 48.95M}, 
                    new Product() { Name = "Soccer ball", Price = 19.50M}, 
                    new Product() { Name = "Stadium", Price = 1234M} 
               };
        }

        public virtual decimal CalculateStockValue()
        {
            decimal totalValue = calculator.ValueProducts(products);

            return totalValue;
        }

        public string UserName
        {
            get;
            set;
        }
    }
}
