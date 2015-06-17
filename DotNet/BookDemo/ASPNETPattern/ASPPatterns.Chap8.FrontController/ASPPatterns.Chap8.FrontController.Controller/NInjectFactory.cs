using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap8.FrontController.Controller
{
    public class NInjectFactory
    {
        private static readonly IKernel _basicKernal;

        static NInjectFactory()
        {
            _basicKernal = new StandardKernel(new ControllerModule());
        }

        public static IKernel BasicKernel
        {
            get
            {
                return _basicKernal;
            }
        }
    }
}
