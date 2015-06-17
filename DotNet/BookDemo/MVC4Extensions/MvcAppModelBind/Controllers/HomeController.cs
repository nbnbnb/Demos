using MvcAppModelBind.Binder;
using MvcAppModelBind.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcAppModelBind.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            NameValueCollection datasource = new NameValueCollection();
            datasource.Add("foo.Name", "Foo");
            datasource.Add("foo.PhoneNo", "123456789");
            datasource.Add("foo.EmailAddress", "Foo@gmail.com");
            datasource.Add("foo.Address.Province", "江苏");
            datasource.Add("foo.Address.City", "苏州");
            datasource.Add("foo.Address.District", "工业园区");
            datasource.Add("foo.Address.Street", "星湖街 328 号");

            NameValueCollectionValueProvider valueProvider =
                new NameValueCollectionValueProvider(datasource, CultureInfo.InvariantCulture);

            return View(valueProvider);
        }

        // 
        // GET: /Home/Header
        public ActionResult Header(CommonHttpHeaders headers)
        {
            return View(headers);
        }

        // GET: /Home/CustomBinder
        public ActionResult CustomBinder()
        {
            ControllerDescriptor controllerDescription =
                new ReflectedControllerDescriptor(typeof(HomeController));
            ActionDescriptor actionDescription =
                controllerDescription.FindAction(ControllerContext, "DemoAction");

            return View(actionDescription);
        }

        public void DemoAction(
            [ModelBinder(typeof(FooModelBinder))]Foo foo,
            Bar bar,
            Baz baz)
        {

        }

        // GET: /Home/ModelStateBind
        public ActionResult ModelStateBind()
        {
            return View();
        }

        // POST: /Home/ModelStateBind
        [HttpPost]
        public ActionResult ModelStateBind(Contact contact)
        {
            return View("ModelState", this.ViewData.ModelState);
        }
    }
}
