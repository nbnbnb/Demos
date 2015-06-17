using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace Service.Interface
{
    [ServiceContract(Namespace = "http://www.zhangjin.me")]
    public interface IEventLog
    {
        [OperationContract]
        void WriteEntry(string source, string message,
            EventLogEntryType type, int eventId, short category);
    }
}
