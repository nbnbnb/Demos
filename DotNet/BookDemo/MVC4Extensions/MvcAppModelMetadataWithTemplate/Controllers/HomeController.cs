using MvcAppModelMetadataWithTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcAppModelMetadataWithTemplate.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            Employee employee = new Employee
            {
                Name = "张三",
                Gender = "M",
                Education = "M",
                Departments = new[] { "HR", "AD" },
                Skills = new[] { "CSharp", "AdoNet" }
            };

            return View(employee);
        }

    }
}
