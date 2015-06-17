using MVC4.Extensions.ModelExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcAppModelValidation.Models
{
    public class Employee
    {
        public string Name { get; set; }

        public string Gender { get; set; }

        [RangeIf("Gender", "G7", 2000, 3000)]
        [RangeIf("Gender", "G8", 3000, 4000)]
        [RangeIf("Gender", "G9", 4000, 5000)]
        public decimal Salary { get; set; }
    }
}