using Chap4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Chap4.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            DemoModel model = new DemoModel();
            model.BirthDate = new DateTime(1987, 6, 15);

            return View(model);
        }
    }
}
