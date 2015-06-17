using Chap8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;

namespace Chap8.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            Contact contact = new Contact
            {
                Name = "张三",
                PhoneNo = "123456789",
                EmailAddress = "zhangsan@gmail.com"
            };
            return View(contact);
        }

        public ActionResult MyIoC()
        {
            return View();
        }

        public ActionResult Foo()
        {
            return new RedirectResult("http://www.asp.net");
        }

        public void Bar()
        {

        }

        public ActionResult Baz()
        {
            return null;
        }

        public double Qux()
        {
            return 1.00;
        }

        public ActionResult ShowNonExistentView()
        {
            return View("NonExistentView");
        }

        public ActionResult ShowStaticFileView()
        {
            return View();
        }
    }
}
