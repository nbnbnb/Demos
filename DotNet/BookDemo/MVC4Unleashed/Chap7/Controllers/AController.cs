using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Async;

namespace Chap7.Controllers
{
    public class AController : Controller
    {
        protected override IActionInvoker CreateActionInvoker()
        {
            return new AsyncControllerActionInvoker();
        }

        public void FooAsync() { }
        public void FooCompleted() { }
        public Task<ActionResult> Bar()
        {
            throw new NotImplementedException();
        }

        //
        // GET: /A/

        public ActionResult Index()
        {
            return View();
        }

    }
}
