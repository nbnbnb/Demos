using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExtensionLib
{
    public static class ChannelFactories
    {
        private static Dictionary<string, ChannelFactory> _channelFactories =
            new Dictionary<string, ChannelFactory>();

        private static object _lock = new object();

        public static ChannelFactory<TChannel> GetFactory<TChannel>
            (string endpointConfigName)
        {
            if (_channelFactories.ContainsKey(endpointConfigName))
            {
                return _channelFactories[endpointConfigName] as ChannelFactory<TChannel>;
            }

            Monitor.Enter(_lock);
            ChannelFactory<TChannel> channelFactory = null;
            if (_channelFactories.ContainsKey(endpointConfigName))
            {
                channelFactory= _channelFactories[endpointConfigName] as ChannelFactory<TChannel>;
            }
            else
            {
                channelFactory = new ChannelFactory<TChannel>(endpointConfigName);
                channelFactory.Open();
                _channelFactories[endpointConfigName] = channelFactory;
            }
            Monitor.Exit(_lock);

            return channelFactory;
        }
    }
}
