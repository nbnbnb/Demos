using MVC4.Extensions.ModelExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcAppModelValidation.Models
{
    [AlwaysFails(ErrorMessage = "Address")]
    public class Address
    {
        [AlwaysFails(ErrorMessage = "Address.Province")]
        public string Province { get; set; }

        [AlwaysFails(ErrorMessage = "Address.City")]
        public string City { get; set; }

        [AlwaysFails(ErrorMessage = "Address.District")]
        public string District { get; set; }

        [AlwaysFails(ErrorMessage = "Address.Street")]
        public string Street { get; set; }
    }
}