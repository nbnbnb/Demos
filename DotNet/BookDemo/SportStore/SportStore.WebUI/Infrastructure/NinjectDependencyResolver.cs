using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        public IKernel Kernel { get; private set; }

        public NinjectDependencyResolver()
        {
            this.Kernel = new StandardKernel();
        }

        public object GetService(Type serviceType)
        {
            return Kernel.Get(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Kernel.GetAll(serviceType);
        }

        public void Register<TFrom, TTO>() where TTO : TFrom
        {
            this.Kernel.Bind<TFrom>().To<TTO>();
        }
    }
}