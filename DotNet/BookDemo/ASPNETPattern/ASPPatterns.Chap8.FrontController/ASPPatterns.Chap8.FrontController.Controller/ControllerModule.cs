using ASPPatterns.Chap8.FrontController.Controller.Navigation;
using ASPPatterns.Chap8.FrontController.Controller.Request;
using ASPPatterns.Chap8.FrontController.Controller.Storage;
using ASPPatterns.Chap8.FrontController.Controller.WebCommands;
using ASPPatterns.Chap8.FrontController.Model;
using ASPPatterns.Chap8.FrontController.StubRepository;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap8.FrontController.Controller
{
    public class ControllerModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ICategoryRepository>().To<CategoryRepository>();
            this.Bind<IProductRepository>().To<ProductRepository>();
            this.Bind<IViewStorage>().To<ViewStorage>();
            this.Bind<IPageNavigator>().To<PageNavigator>();
            this.Bind<IWebCommandRegistry>().To<WebCommandRegistry>();
            this.Bind<IWebRequestFactory>().To<WebRequestFactory>();
        }
    }
}
