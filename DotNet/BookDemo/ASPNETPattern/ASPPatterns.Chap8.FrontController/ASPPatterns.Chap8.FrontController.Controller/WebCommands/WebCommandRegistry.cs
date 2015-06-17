using ASPPatterns.Chap8.FrontController.Controller.ActionCommands;
using ASPPatterns.Chap8.FrontController.Controller.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using ASPPatterns.Chap8.FrontController.Controller.Navigation;
using ASPPatterns.Chap8.FrontController.Controller.Routing;

namespace ASPPatterns.Chap8.FrontController.Controller.WebCommands
{
    public class WebCommandRegistry : IWebCommandRegistry
    {
        private IList<IWebCommand> _webCommands = new List<IWebCommand>();
        public WebCommandRegistry()
        {
            _webCommands.Add(CreateGetCategoryProductsCommand());
            _webCommands.Add(CreateGetHomePageCommand());
            _webCommands.Add(CreateGetProductDetailCommand());
        }

        public IWebCommand GetCommandFor(WebRequest webRequest)
        {
            return _webCommands.FirstOrDefault(wc => wc.CanHandle(webRequest)) ??
                new Display404PageCommand(NInjectFactory.BasicKernel.Get<IPageNavigator>());
        }
        public IWebCommand CreateGetCategoryProductsCommand()
        {
            List<IActionCommand> _categoryProductsActionCommands = new List<IActionCommand>();
            _categoryProductsActionCommands.Add(NInjectFactory.BasicKernel.Get<GetCategoryListCommand>());
            _categoryProductsActionCommands.Add(NInjectFactory.BasicKernel.Get<GetCategoryProductsCommand>());
            _categoryProductsActionCommands.Add(NInjectFactory.BasicKernel.Get<GetCategoryCommand>());

            return new WebCommand(
                NInjectFactory.BasicKernel.Get<IPageNavigator>(),
                _categoryProductsActionCommands,
                Routes.CategoryProducts,
                PageDirectory.CategoryProducts);
        }
        public IWebCommand CreateGetHomePageCommand()
        {
            List<IActionCommand> _homePageActionCommands = new List<IActionCommand>();
            _homePageActionCommands.Add(NInjectFactory.BasicKernel.Get<GetCategoryListCommand>());
            _homePageActionCommands.Add(NInjectFactory.BasicKernel.Get<GetTopSellingProductsCommand>());

            return new WebCommand(
                NInjectFactory.BasicKernel.Get<IPageNavigator>(),
                _homePageActionCommands,
                Routes.Home,
                PageDirectory.Home);
        }

        public IWebCommand CreateGetProductDetailCommand()
        {
            List<IActionCommand> _productDetailActionCommands = new List<IActionCommand>();
            _productDetailActionCommands.Add(NInjectFactory.BasicKernel.Get<GetCategoryListCommand>());
            _productDetailActionCommands.Add(NInjectFactory.BasicKernel.Get<GetProductDetailCommand>());

            return new WebCommand(
                NInjectFactory.BasicKernel.Get<IPageNavigator>(),
                _productDetailActionCommands,
                Routes.ProductDetail,
                PageDirectory.ProductDetail);
        }
    }
}