using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace NerdDinner.Models
{

    public class Product
    {

            [StringLength(10, ErrorMessage = "不能超过10个字符"), Required]
            public string Name { get; set; }
            [StringLength(15)]
            public string Color { get; set; }
            [Range(0, 9999)]
            public string Weight { get; set; }

    }
}