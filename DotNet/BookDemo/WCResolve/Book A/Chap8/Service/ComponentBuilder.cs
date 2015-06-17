using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Web;

namespace Service
{
    public static class ComponentBuilder
    {
        public static object GerFormatter(OperationDescription operation, bool isProxy)
        {
            bool formatRequest = false;
            bool formatReply = false;

            DataContractSerializerOperationBehavior behavior =
                new DataContractSerializerOperationBehavior(operation);

            MethodInfo method = typeof(DataContractSerializerOperationBehavior)
                .GetMethod("GetFormatter", BindingFlags.Instance | BindingFlags.NonPublic);

            return method.Invoke(behavior, new object[] { operation, formatRequest, formatReply, isProxy });
        }

        public static MessageEncoderFactory GetMessageEncoderFactory(MessageVersion messageVersion, Encoding writeEncoding)
        {
            TextMessageEncodingBindingElement bindingElement =
                new TextMessageEncodingBindingElement(messageVersion, writeEncoding);

            return bindingElement.CreateMessageEncoderFactory();
        }

        public static IOperationInvoker GetOperationInvoker(MethodInfo method)
        {
            string syncMethodInvokerType = "System.ServiceModel.Dispatcher.SyncMethodInvoker,System.Service.Model,Version=4.0.0.0,Culture=nertual,PublickKey=b77a5c561934e089";
            Type type = Type.GetType(syncMethodInvokerType);
            return (IOperationInvoker)Activator.CreateInstance(type, new object[] { method });
        }
    }
}