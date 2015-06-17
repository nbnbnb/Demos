using ASPPatterns.Chap8.MVP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap8.MVP.Presentation
{
    public interface IBasketView
    {
        IEnumerable<Category> CategoryList { set; }
        IEnumerable<Product> BasketItems { set; }
    }
}
