using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;

namespace Model
{
    [CollectionDataContract(Namespace = "http://www.zhangjin.me",
        ItemName = "context",
        KeyName = "name",
        ValueName = "value")]
    public class ApplicationContext : Dictionary<string, object>
    {
        public const string HeaderName = "ApplicationContext";
        public const string Namespace = "http://www.zhangjin.me";
        public const string PropertyName = "ApplicationContext";

        [ThreadStatic]
        private static ApplicationContext current;

        public static ApplicationContext Current
        {
            get
            {
                if (OperationContext.Current == null)
                {
                    return current;
                }

                MessageProperties incommingProperties =
                    OperationContext.Current.IncomingMessageProperties;

                if (null != incommingProperties &&
                    incommingProperties.ContainsKey(PropertyName))
                {
                    return (ApplicationContext)incommingProperties[PropertyName];
                }

                MessageHeaders incommingHeaders =
                    OperationContext.Current.IncomingMessageHeaders;

                if (null != incommingHeaders &&
                    incommingHeaders.FindHeader(HeaderName, Namespace) > -1)
                {
                    ApplicationContext context =
                        incommingHeaders.GetHeader<ApplicationContext>(HeaderName, Namespace);

                    incommingProperties.Add(PropertyName, context);

                    return context;
                }

                return current;
            }
            set
            {
                current = value;
            }
        }
    }
}
