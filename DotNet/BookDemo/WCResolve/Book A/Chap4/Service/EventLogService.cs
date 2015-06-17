using Service.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Service
{
    public class EventLogService : IEventLog
    {

        public void WriteEntry(string source, string message,
            EventLogEntryType type, int eventId, short category)
        {

        }
    }
}
