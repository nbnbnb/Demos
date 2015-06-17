using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Service.Interface.Entities
{
    [DataContract(Namespace = "http://www.zhangjin.me")]
    public class CalculationError
    {
        public CalculationError(string operation, string message)
        {
            this.Operation = operation;
            this.Message = message;
        }

        [DataMember]
        public string Operation { get; private set; }

        [DataMember]
        public string Message { get; private set; }
    }
}
