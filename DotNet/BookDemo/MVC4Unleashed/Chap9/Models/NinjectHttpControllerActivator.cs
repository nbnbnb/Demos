using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dispatcher;
using Ninject;
using System.Web.Http.Controllers;
using System.Net.Http;

namespace Chap9.Models
{
    public class NinjectHttpControllerActivator : IHttpControllerActivator
    {
        public IKernel Kernel { get; private set; }

        public NinjectHttpControllerActivator()
        {
            Kernel = new StandardKernel();
        }

        public System.Web.Http.Controllers.IHttpController Create(HttpRequestMessage request,
            HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            return (IHttpController)Kernel.Get(controllerType);
        }

        public void Register<TFrom, TTO>() where TTO : TFrom
        {
            Kernel.Bind<TFrom>().To<TTO>();
        }
    }
}