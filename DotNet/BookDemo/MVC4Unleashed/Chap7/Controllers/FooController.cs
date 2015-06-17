using Chap7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chap7.Controllers
{
    public class FooController : Controller
    {
        //
        // GET: /Foo/

        protected override IActionInvoker CreateActionInvoker()
        {
            return new SyncActionInvoker();
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
