using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Service.Interface;
using System.ServiceModel;
using System.Threading.Tasks;
using Service;

namespace WinFormClient
{
    public partial class Form1 : Form
    {
        private SynchronizationContext _syncContext;
        private DuplexChannelFactory<ICalculator> _channelFactory;
        private InstanceContext _callbackInstance;
        private static int clientIdIndex = 0;

        public Form1()
        {
            InitializeComponent();

            SubScribeForEvents();
        }

        private void SubScribeForEvents()
        {
            this.Load += Form1_Load;
            this.Disposed += Form1_Disposed;
        }

        void Form1_Disposed(object sender, EventArgs e)
        {
            EventMonitor.MonitoringNotificationSended -= ReceiveMonitoringNotification;
            _channelFactory.Close();
        }

        void Form1_Load(object sender, EventArgs e)
        {
            string header = string.Format("{0,-13}{1,-22}{2}", "Client", "Time", "Event");
            this.listBox1.Items.Add(header);
            _syncContext = SynchronizationContext.Current;
            _callbackInstance = new InstanceContext(new CalculatorCallbackService());
            _channelFactory = new DuplexChannelFactory<ICalculator>(_callbackInstance,"CalculatorService");
            EventMonitor.MonitoringNotificationSended += ReceiveMonitoringNotification;
          
            for (int i = 0; i < 2; i++)
            {
                ThreadPool.QueueUserWorkItem(state =>
                {
                    int clientId = Interlocked.Increment(ref clientIdIndex);
                    ICalculator proxy = _channelFactory.CreateChannel();
                  
                    EventMonitor.Send(clientId, EventType.StartCall);
                    using (OperationContextScope contextScope =
                        new OperationContextScope(proxy as IContextChannel))
                    {
                        MessageHeader<int> messageHeader = new MessageHeader<int>(clientId);
                        OperationContext.Current.OutgoingMessageHeaders.Add(messageHeader.GetUntypedHeader(EventMonitor.ClientIdHeaderLocalName, EventMonitor.ClientIdHeaderNamespace));
                        proxy.Add2(1, 2);
                    }
                    EventMonitor.Send(clientId, EventType.EndCall);
                });
            }

        }

        private void ReceiveMonitoringNotification(object sender, MonitorEventArgs e)
        {
            string message = string.Format("{0,-15}{1,-20}{2}", e.ClientId, e.EventDate.ToLocalTime(), e.EventType);
            _syncContext.Post(m => listBox1.Items.Add(message), null);
        }
    }
}
