using MvcAppMetadataAware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcAppMetadataAware.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        // 将对象放在参数中，将会出发 ModelBind
        public ActionResult Index()
        {
            Employee employee = new Employee();
            return View(employee);
        }

    }
}
