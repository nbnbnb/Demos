using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class UserInfo : IValidatableObject
    {
        public string UserName { get; set; }

        public int Age { get; set; }

        public Address UserAddress { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            ValidationResult res = new ValidationResult("abc");

            return new List<ValidationResult>
            {
                res
            };
        }
    }
}