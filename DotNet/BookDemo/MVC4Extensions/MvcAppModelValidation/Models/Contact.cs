using MVC4.Extensions.ModelExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MvcAppModelValidation.Models
{
    [AlwaysFails(ErrorMessage = "Contact")]
    public class Contact : IDataErrorInfo
    {
        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Name":
                        return null;
                    case "PhoneNo":
                        return null;
                    case "EmailAddress":
                        return null;
                    default:
                        return null;
                }
            }
        }

        [AlwaysFails(ErrorMessage = "Contact.Name")]
        public string Name { get; set; }

        [AlwaysFails(ErrorMessage = "Contact.PhoneNo")]
        public string PhoneNo { get; set; }

        [AlwaysFails(ErrorMessage = "Contact.EmailAddress")]
        public string EmailAddress { get; set; }

        [AlwaysFails(ErrorMessage = "Contact.Address")]
        public Address Address { get; set; }
    }
}