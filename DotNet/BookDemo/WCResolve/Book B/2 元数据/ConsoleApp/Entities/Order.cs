using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Entities
{
    [DataContract(Namespace = "http://www.zhangjin.me/entity/")]
    public class Order
    {
        [DataMember]
        public string OrderId { get; set; }

        [DataMember]
        public string CustomerId { get; set; }

        public Collection<OrderDetail> Details { get; set; }
    }
}
