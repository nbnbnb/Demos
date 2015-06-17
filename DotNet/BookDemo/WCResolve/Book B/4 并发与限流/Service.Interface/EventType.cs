using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Interface
{
    public enum EventType
    {
        StartCall,
        EndCall,
        StartExecute,
        EndExecute,
        StartCallback,
        EndCallback,
        StartExecuteCallback,
        EndExecuteCallback
    }
}
