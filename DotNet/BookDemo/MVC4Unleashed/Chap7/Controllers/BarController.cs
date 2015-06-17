using Chap7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chap7.Controllers
{
    public class BarController : Controller
    {
        //
        // GET: /Bar/

        protected override IActionInvoker CreateActionInvoker()
        {
            return new AsyncActionInvoker();
        }
        public ActionResult Index()
        {
            return View();
        }

    }
}
