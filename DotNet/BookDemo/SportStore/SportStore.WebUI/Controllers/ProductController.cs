using SportStore.Domain.Abstract;
using SportStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;

        public int PageSize = 2;

        public ProductController(IProductRepository productRepository)
        {
            repository = productRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            var products = repository.Products
                .Where(m => category == null || m.Category == category);
                

            ProductsListViewModel viewModel = new ProductsListViewModel
            {
                Products = products.OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = products.Count()
                },
                CurrentCategory = category
            };
            return View(viewModel);
        }
    }
}
