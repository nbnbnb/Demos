using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Model
{
    public static class Extensions
    {
        public static void AttachApplicationContext(this OperationContext context)
        {
            if (null == ApplicationContext.Current)
            {
                return;
            }

            MessageHeader<ApplicationContext> header =
                new MessageHeader<ApplicationContext>(ApplicationContext.Current);
            OperationContext.Current.OutgoingMessageHeaders
                .Add(header.GetUntypedHeader(ApplicationContext.HeaderName,
                ApplicationContext.Namespace));
        }
    }
}
