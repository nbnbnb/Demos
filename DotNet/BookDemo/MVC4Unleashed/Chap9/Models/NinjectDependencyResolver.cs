using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace Chap9.Models
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private List<IDisposable> _disposableServices = new List<IDisposable>();
        public IKernel Kernel { get; private set; }

        public NinjectDependencyResolver(NinjectDependencyResolver parent)
        {
            this.Kernel = parent.Kernel;
        }

        public NinjectDependencyResolver()
        {
            this.Kernel = new StandardKernel();
        }

        public void Register<TFrom, TTo>() where TTo : TFrom
        {
            this.Kernel.Bind<TFrom>().To<TTo>();
        }

        public IDependencyScope BeginScope()
        {
            // 返回一个新的 NinjectDependencyResolver 对象
            // 它拥有同一个Kernel 对象
            return new NinjectDependencyResolver(this);
        }

        public object GetService(Type serviceType)
        {
            var service = this.Kernel.TryGet(serviceType);
            this.AddDisposableService(service);
            return service;
        }

        private void AddDisposableService(object service)
        {
            IDisposable disposable = service as IDisposable;
            if (null != disposable &&
                !_disposableServices.Contains(disposable))
            {
                _disposableServices.Add(disposable);
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            foreach (var service in this.Kernel.GetAll(serviceType))
            {
                this.AddDisposableService(service);
                yield return service;
            }
        }

        public void Dispose()
        {
            foreach (IDisposable disposable in _disposableServices)
            {
                disposable.Dispose();
            }
        }
    }
}