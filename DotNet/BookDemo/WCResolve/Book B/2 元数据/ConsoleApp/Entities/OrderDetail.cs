using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Entities
{
    [DataContract(Namespace="http://www.zhangjin.me/entity/")]
    public class OrderDetail
    {
        public string OrderId { get; set; }

        public string ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
