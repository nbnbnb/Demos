using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Web;

namespace WebHosting
{
    public class WcfHandler:IHttpHandler
    {
        public Type ServiceType { get; private set; }

        public MessageEncoderFactory MessageEncoderFactory { get; private set; }

        public IDictionary<string, MethodInfo> Methods { get; private set; }

        public IDictionary<string, IDispatchMessageFormatter> MessageFormatters { get; private set; }

        public IDictionary<string, IOperationInvoker> OperationInvokers { get; private set; }

        public bool IsReusable
        {
            get { return false; }
        }

        public WcfHandler(Type serviceType, MessageEncoderFactory messageEncoderFactory)
        {
            this.ServiceType = serviceType;
            this.MessageEncoderFactory = messageEncoderFactory;
            this.Methods = new Dictionary<string, MethodInfo>();
            this.MessageFormatters = new Dictionary<string, IDispatchMessageFormatter>();
            this.OperationInvokers = new Dictionary<string, IOperationInvoker>();
        }


        public void ProcessRequest(HttpContext context)
        {
            Message request = this.MessageEncoderFactory.Encoder.ReadMessage(context.Request.InputStream, 
                int.MaxValue, "application/soap+xml; charset=utf-8");

            string action = request.Headers.Action;

            MethodInfo method = this.Methods[action];

            int outArgsCount = 0;
            foreach (var parameter in method.GetParameters())
            {
                if (parameter.IsOut)
                {
                    outArgsCount++;
                }
            }

            int inputArgsCount = method.GetParameters().Length - outArgsCount;
            object[] parameters = new object[inputArgsCount];
            try
            {
                this.MessageFormatters[action].DeserializeRequest(request, parameters);
            }
            catch
            {

            }

            List<object> inputArgs = new List<object>();
            object[] outArgs = new object[outArgsCount];

            object serviceInstance = Activator.CreateInstance(this.ServiceType);
            object result = this.OperationInvokers[action].Invoke(serviceInstance, 
                parameters, out outArgs);

            Message reply = this.MessageFormatters[action]
                .SerializeReply(request.Version, outArgs, result);

            context.Response.ClearContent();
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "application/soap+xml; charset=utf-8";

            this.MessageEncoderFactory.Encoder.WriteMessage(reply, context.Response.OutputStream);

            context.Response.Flush();
        }
    }
}