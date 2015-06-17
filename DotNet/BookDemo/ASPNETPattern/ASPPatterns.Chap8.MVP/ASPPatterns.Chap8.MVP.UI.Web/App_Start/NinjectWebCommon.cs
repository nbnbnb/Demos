[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ASPPatterns.Chap8.MVP.UI.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(ASPPatterns.Chap8.MVP.UI.Web.App_Start.NinjectWebCommon), "Stop")]

namespace ASPPatterns.Chap8.MVP.UI.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using ASPPatterns.Chap8.MVP.Model;
    using ASPPatterns.Chap8.MVP.StubRepository;
    using ASPPatterns.Chap8.MVP.Presentation.Navigation;
    using ASPPatterns.Chap8.MVP.Presentation.Basket;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ICategoryRepository>().To<CategoryRepository>();
            kernel.Bind<IProductRepository>().To<ProductRepository>();
            kernel.Bind<IPageNavigator>().To<PageNavigator>();
            kernel.Bind<IBasket>().To<WebBasket>();

            CurrentKernel = kernel;
        }

        public static IKernel CurrentKernel
        {
            get;
            private set;
        }
    }
}
