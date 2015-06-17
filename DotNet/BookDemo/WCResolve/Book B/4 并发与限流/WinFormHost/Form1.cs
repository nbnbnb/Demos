using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.ServiceModel;
using Service.Interface;
using Service;

namespace WinFormHost
{
    public partial class Form1 : Form
    {
        private SynchronizationContext _syncContext;
        private ServiceHost _serviceHost;

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
            _serviceHost.Close();
        }

        void Form1_Load(object sender, EventArgs e)
        {
            string header = string.Format("{0,-13}{1,-22}{2}", "Client", "Time", "Event");
            listBox1.Items.Add(header);
            _syncContext = SynchronizationContext.Current;
            EventMonitor.MonitoringNotificationSended += ReceiveMonitoringNotification;
            _serviceHost = new ServiceHost(typeof(CalculatorService));
            _serviceHost.Open();
        }

        void ReceiveMonitoringNotification(object sender, MonitorEventArgs e)
        {
            string message = string.Format("{0,-15}{1,-20}{2}", e.ClientId, e.EventDate.ToLocalTime(), e.EventType);
            _syncContext.Post(m => listBox1.Items.Add(message), null);
        }

    }
}
