using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MvcAppActionExecute.Controllers
{
    public class HomeController : AsyncController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public void ArticleAsync(string name)
        {
            AsyncManager.OutstandingOperations.Increment();

            Task.Factory.StartNew(() =>
            {
                string path = base.ControllerContext.HttpContext.Server
                    .MapPath(String.Format(@"\articles\{0}.html"));

                using (StreamReader reader = new StreamReader(path))
                {
                    AsyncManager.Parameters["content"] = reader.ReadToEnd();
                }

                AsyncManager.OutstandingOperations.Decrement();

            });
        }

        public ActionResult ArticleCompleted(string content)
        {
            return Content(content);
        }

        public Task<ActionResult> Article2(string name)
        {
            return Task.Factory.StartNew(() =>
            {
                string path = base.ControllerContext.HttpContext.Server
                    .MapPath(String.Format(@"\articles\{0}.html"));
                using (StreamReader reader = new StreamReader(path))
                {
                    AsyncManager.Parameters["content"] = reader.ReadToEnd();
                }
            }).ContinueWith<ActionResult>(task =>
            {
                string content = (string)AsyncManager.Parameters["content"];
                return Content(content);
            });
        }

    }
}
