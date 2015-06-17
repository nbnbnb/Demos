using CServiceStack.WebHost.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHelloWorldService
{
    public class AppHost : AppHostHttpListenerBase
    {
        public AppHost()
            : base(typeof(HelloServiceImpl).Assembly)
        {

        }

        public override void Configure(Funq.Container container)
        {
            
        }
    }
}
