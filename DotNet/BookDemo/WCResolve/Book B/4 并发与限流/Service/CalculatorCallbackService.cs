using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace Service
{
    [CallbackBehavior(ConcurrencyMode=ConcurrencyMode.Multiple)]
    public class CalculatorCallbackService : ICalculatorCallback
    {
        public void ShowResult(double result)
        {
            EventMonitor.Send(EventType.StartExecuteCallback);
            Thread.Sleep(10000);
            EventMonitor.Send(EventType.EndExecuteCallback);
        }
    }
}
