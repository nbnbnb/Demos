using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Chap4.Models
{
    public class Address
    {
        [DisplayName("省")]
        public string Province { get; set; }

        [DisplayName("市")]
        public string City { get; set; }

        [DisplayName("区")]
        public string District { get; set; }

        [DisplayName("街道")]
        public string Street { get; set; }
    }
}