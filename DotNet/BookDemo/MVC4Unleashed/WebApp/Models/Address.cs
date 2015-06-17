using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApp.Models
{

    public class Address
    {
        public string Province { get; set; }

        public string City { get; set; }

        public static Address ParseAddress(string fullAddress)
        {
            return new Address
            {
                Province = fullAddress.Split(',')[0],
                City = fullAddress.Split(',')[1]
            };
        }
    }
}