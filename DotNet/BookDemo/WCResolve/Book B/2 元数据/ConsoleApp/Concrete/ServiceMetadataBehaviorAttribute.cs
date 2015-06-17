using ConsoleApp.Helper;
using ConsoleApp.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp.Concrete
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceMetadataBehaviorAttribute : Attribute, IServiceBehavior
    {
        private const string MexContractName = "IMetadataProvisionService";
        private const string MexContractNamespace = "http://schemas.microsoft.com/2006/04/mex";

        private const string SingletonInstanceContextProvierType =
            "System.ServiceModel.Dispatcher.SingletonInstanceContextProvider,System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

        private const string SyncMethodInvokerType =
            "System.ServiceModel.Dispatcher.SyncMethodInvoker,System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

        private const string MessageOperationFormatterType =
            "System.ServiceModel.Dispatcher.MessageOperationFormatter,System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

        private static void CreateHttpGetChannelDispatcher(ServiceHostBase host,
            Uri listenUri, MetadataSet metadata)
        {
            // 创建 Binding
            // 用于并不是一个  SOAP
            // 所以需要将  MessageVersion 设置为 MessageVersion.None
            TextMessageEncodingBindingElement messageEncodingElement =
                new TextMessageEncodingBindingElement()
                {
                    MessageVersion = MessageVersion.None
                };

            HttpTransportBindingElement transportElement = new HttpTransportBindingElement();

            Utility.SetPropertyValue(transportElement, "Method", "GET");

            Binding binding = new CustomBinding(messageEncodingElement, transportElement);

            // 创建 ChannelListener
            IChannelListener listener =
                binding.BuildChannelListener<IReplyChannel>(listenUri, string.Empty,
                ListenUriMode.Explicit, new BindingParameterCollection());
            ChannelDispatcher dispatcher = new ChannelDispatcher(listener, "ServiceMetadataBehaviorHttpGetBinding", binding)
            {
                MessageVersion = binding.MessageVersion
            };

            // 创建 EndpointDispatcher
            EndpointDispatcher endpoint = new EndpointDispatcher(new EndpointAddress(listenUri),
                "IHttpGetMetadata", "http://www.zhangjin.me/interface");

            // 创建 DispatchOperation 并设置 DispatchMessageFormatter 和 OperationInvoker
            DispatchOperation operation = new DispatchOperation(endpoint.DispatchRuntime, "Get", "*", "*");
            operation.Formatter =
                Utility.CreateInstance<IDispatchMessageFormatter>(MessageOperationFormatterType, 
                Type.EmptyTypes, new object[0]);

            MethodInfo method = typeof(IHttpGetMetadata).GetMethod("Get");

            operation.Invoker = Utility.CreateInstance<IOperationInvoker>(SyncMethodInvokerType, 
                new Type[] { typeof(MethodInfo) }, 
                new object[] { method });

            endpoint.DispatchRuntime.Operations.Add(operation);

            // 设置 SingletonInstanceContext 和 InstanceContextProvider
            MetadataProvisionService serviceInstance = new MetadataProvisionService(metadata);
            endpoint.DispatchRuntime.SingletonInstanceContext =
                new InstanceContext(host, serviceInstance);
            endpoint.DispatchRuntime.InstanceContextProvider =
                Utility.CreateInstance<IInstanceContextProvider>(SingletonInstanceContextProvierType, 
                new Type[] { typeof(DispatchRuntime) }, 
                new object[] { endpoint.DispatchRuntime });

            // 待分发运行时被成功定制后，将创建的终结点分发器添加到信道分发器的 Endpoints 列表中
            dispatcher.Endpoints.Add(endpoint);

            // # 设置 ContractFilter 和 AddressFilter
            endpoint.ContractFilter = new MatchAllMessageFilter();
            endpoint.AddressFilter = new MatchAllMessageFilter();

            // 将信道分发器添加到 ServiceHost 的 ChannnelDispatchers 列表中国
            host.ChannelDispatchers.Add(dispatcher);
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            MetadataSet metadata = GetExportMetadata(serviceDescription);
            CustomizeMexEndpoints(serviceDescription, serviceHostBase, metadata);

            if (this.HttpGetEnabled)
            {
                CreateHttpGetChannelDispatcher(serviceHostBase, new Uri(this.HttpGetUrl), metadata);
            }
        }

        private void CustomizeMexEndpoints(ServiceDescription serviceDescription, ServiceHostBase host, MetadataSet metadata)
        {
            foreach (ChannelDispatcher channelDispatcher in host.ChannelDispatchers)
            {
                foreach (EndpointDispatcher endpoint in channelDispatcher.Endpoints)
                {
                    if (endpoint.ContractName == MexContractName &&
                        endpoint.ContractNamespace == MexContractNamespace)
                    {
                        DispatchRuntime dispatchRuntime = endpoint.DispatchRuntime;
                        dispatchRuntime.InstanceContextProvider =
                            Utility.CreateInstance<IInstanceContextProvider>(SingletonInstanceContextProvierType,
                            new Type[] { typeof(DispatchRuntime) },
                            new object[] { dispatchRuntime });

                        MetadataProvisionService serviceInstance =
                            new MetadataProvisionService(metadata);

                        dispatchRuntime.SingletonInstanceContext =
                            new InstanceContext(host, serviceInstance);
                    }
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {

        }

        private static MetadataSet GetExportMetadata(ServiceDescription serviceDescription)
        {
            Collection<ServiceEndpoint> endpoints = new Collection<ServiceEndpoint>();
            foreach (ServiceEndpoint endpoint in serviceDescription.Endpoints)
            {
                if (endpoint.Contract.ContractType == typeof(IMetadataProvisionService))
                {
                    continue;
                }

                ServiceEndpoint newEndpoint = new ServiceEndpoint(endpoint.Contract, endpoint.Binding, endpoint.Address);
                newEndpoint.Name = endpoint.Name;

                foreach (var behavior in endpoint.EndpointBehaviors)
                {
                    newEndpoint.EndpointBehaviors.Add(behavior);
                }

                endpoints.Add(newEndpoint);
            }

            WsdlExporter exporter = new WsdlExporter();
            XmlQualifiedName wsdlServieceQName = new XmlQualifiedName(serviceDescription.Name, serviceDescription.Namespace);
            exporter.ExportEndpoints(endpoints, wsdlServieceQName);
            MetadataSet metadata = exporter.GetGeneratedMetadata();
            return metadata;
        }

        public bool HttpGetEnabled { get; set; }

        public string HttpGetUrl { get; set; }


    }
}
