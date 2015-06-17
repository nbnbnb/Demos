using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service
{
    public class RouteMapping
    {
        public string Address { get; private set; }

        public Type ServiceType { get; private set; }

        public RouteMapping(String address, Type serviceType)
        {
            this.Address = address;
            this.ServiceType = serviceType;
        }
    }
}