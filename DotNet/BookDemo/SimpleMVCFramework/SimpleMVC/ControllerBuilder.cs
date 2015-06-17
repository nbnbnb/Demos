using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleMVC
{
    public class ControllerBuilder
    {
        private Func<IControllerFactory> _factoryThunk;

        public static ControllerBuilder Current { get; private set; }

        static ControllerBuilder()
        {
            Current = new ControllerBuilder();
        }

        public IControllerFactory GetControllerFactory()
        {
            return _factoryThunk();
        }

        public void SetControllerFactory(IControllerFactory controllerFactory)
        {
            _factoryThunk = () => controllerFactory;
        }
    }
}