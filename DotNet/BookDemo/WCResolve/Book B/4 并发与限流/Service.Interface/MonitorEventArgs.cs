using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Interface
{
    public class MonitorEventArgs : EventArgs
    {

        public MonitorEventArgs(int clientId, EventType eventType, DateTime eventDate)
        {
            this.ClientId = clientId;
            this.EventType = eventType;
            this.EventDate = eventDate;
        }

        public int ClientId { get; private set; }
        public EventType EventType { get; private set; }
        public DateTime EventDate { get; private set; }
    }
}
