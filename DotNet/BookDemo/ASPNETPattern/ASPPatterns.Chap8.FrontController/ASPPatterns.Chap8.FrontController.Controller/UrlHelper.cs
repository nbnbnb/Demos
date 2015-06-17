using ASPPatterns.Chap8.FrontController.Controller.Request;
using ASPPatterns.Chap8.FrontController.Controller.Routing;
using ASPPatterns.Chap8.FrontController.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap8.FrontController.Controller
{
    public static class UrlHelper
    {
        public static string BuildHomePageLink()
        {
            return Routes.Home.URL;
        }
        public static string BuildProductDetailLinkFor(Product product)
        {
            return Routes.ProductDetail.URL + "?" +
                ActionArguments.ProductId.Key + "=" + product.Id;
        }
        public static string BuildProductCategoryLinkFor(Category category)
        {
            return Routes.CategoryProducts.URL + "?" +
               ActionArguments.CategoryId.Key + "=" + category.Id;
        }
    }
}
