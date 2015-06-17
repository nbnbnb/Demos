using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public class InstrumentationService : IInstrumentation
    {
        public void IncreasePerformanceCounter(string catelogName, string counterName)
        {

        }

        public void SetWmiProperty(string propertyName, object value)
        {

        }

        public void WriteEntry(string source, string message, System.Diagnostics.EventLogEntryType type, int eventId, short category)
        {

        }
    }
}
