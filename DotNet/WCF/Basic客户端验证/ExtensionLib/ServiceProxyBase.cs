using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionLib
{
    public abstract class ServiceProxyBase<TChannel>
    {
        public OperationInvoker<TChannel> Invoker { get; private set; }

        public ServiceProxyBase(string endpointName)
        {
            this.Invoker = new OperationInvoker<TChannel>(endpointName);
        }
    }
}
