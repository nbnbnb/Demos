using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chap4.Models
{
    public class DemoModel
    {

        [DisplayText(DisplayName = "BirthDate")]
        public DateTime BirthDate { get; set; }
    }
}