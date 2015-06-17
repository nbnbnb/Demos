using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcAppModelBind.Models
{
    public class Contact
    {
        public string Name { get; set; }

        public string PhotoNo { get; set; }

        public string EmailAddress { get; set; }

        public Address Address { get; set; }
    }
}