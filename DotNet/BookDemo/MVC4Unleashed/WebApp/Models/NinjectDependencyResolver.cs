using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Models
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        IKernel kernel = new StandardKernel();

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public void Register<TTo, TFrom>() where TTo : TFrom
        {
            kernel.Bind<TFrom>().To<TTo>();
        }
    }
}