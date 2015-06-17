using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Chap7.Controllers
{
    public class BController : Controller
    {
        protected override IActionInvoker CreateActionInvoker()
        {
            return new ControllerActionInvoker();
        }

        public void FooAsync() { }
        public void FooCompleted() { }
        public Task<ActionResult> Bar()
        {
            throw new NotImplementedException();
        }

        //
        // GET: /B/

        public ActionResult Index()
        {
            return View();
        }

    }
}
