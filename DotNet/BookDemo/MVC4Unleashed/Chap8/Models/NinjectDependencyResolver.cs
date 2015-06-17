using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chap8.Models
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        public IKernel Kernel { get; private set; }
        public NinjectDependencyResolver()
        {
            Kernel = new StandardKernel();
        }

        public void Register<TFrom, TTo>() where TTo : TFrom
        {
            Kernel.Bind<TFrom>().To<TTo>();
        }

        public object GetService(Type serviceType)
        {
            return Kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Kernel.GetAll(serviceType);
        }
    }
}