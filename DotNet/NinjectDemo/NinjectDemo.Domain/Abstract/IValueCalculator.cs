using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NinjectDemo.Domain.Entities;

namespace NinjectDemo.Domain.Abstract
{
    public interface IValueCalculator
    {
        decimal ValueProducts(params Product[] products);
    }
}
