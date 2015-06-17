using SimpleContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvCalculator
{
    public class Operation:IOperation
    {
        public string Name
        {
            get;
            internal set;
        }

        public int NumberOperands
        {
            get;
            internal set;
        }
    }
}
