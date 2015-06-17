using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleMVC
{
    public interface IController
    {
        void Execute(RequestContext requestContext);
    }
}