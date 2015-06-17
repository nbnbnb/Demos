using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chap8.Controllers
{
    public class FooController : Controller
    {
        //
        // GET: /Foo/

        public ActionResult Action1()
        {
            return View();
        }

        public ActionResult Action2()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
