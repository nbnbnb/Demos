using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace AutoCloser
{
    public static class OperationInvokerHelper
    {
        public static void Invoke<TChannel>(Action<TChannel> serviceInvocation,
           TChannel channel)
        {
            ICommunicationObject communicationObject = (ICommunicationObject)channel;
            try
            {
                serviceInvocation(channel);
                communicationObject.Close();
            }
            catch (Exception ex)
            {
                HandleException(ex, communicationObject);
            }
        }

        public static TResult Invoke<TChannel, TResult>(Func<TChannel, TResult> serviceInvocation,
            TChannel channel)
        {
            ICommunicationObject communicationObject = (ICommunicationObject)channel;
            TResult result = default(TResult);
            try
            {
                result = serviceInvocation(channel);
                communicationObject.Close();
            }
            catch (Exception ex)
            {
                HandleException(ex, communicationObject);
            }
            return result;
        }

        private static void HandleException(Exception ex, ICommunicationObject channel)
        {
            if (ex is TimeoutException || ex is CommunicationException)
            {
                channel.Abort();
            }

            throw ex;
        }
    }
}
