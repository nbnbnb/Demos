using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap8.MVP.Presentation
{
    public interface IProductDetailPresenter
    {
        void Display();
        void AddProductToBasketAndShowBasketPage();
    }
}
