using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Chap7.Models;
using System.Web.Mvc.Async;
using System.Reflection;
using System.Collections.Concurrent;

namespace Chap7.Controllers
{
    [Foo]
    public class HomeController : AsyncController
    {
        [Bar]
        public void DemoAction()
        {

        }

        protected override IActionInvoker CreateActionInvoker()
        {
            return new AsyncActionInvoker();
        }

        //
        // GET: /Home/

        byte[] buf = null;

        public Task<ActionResult> Article(string name)
        {
            string path = ControllerContext.HttpContext.Server
                .MapPath(string.Format(@"\articles\{0}.html", name));

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read,
                FileShare.Read, 32, FileOptions.Asynchronous);
            buf = new byte[fs.Length];


            return Task.Factory
                     .FromAsync<byte[], int, int, int>(fs.BeginRead, fs.EndRead, buf, 0, buf.Length, fs, TaskCreationOptions.None)
                     .ContinueWith<ActionResult>(task =>
                     {
                         string content = Encoding.UTF8.GetString(buf, 0, task.Result);
                         return Content(content);
                     });
        }

        public ActionResult Index()
        {
            ReflectedControllerDescriptor controllerDescriptor =
                new ReflectedControllerDescriptor(typeof(HomeController));

            ActionDescriptor actionDescriptor = controllerDescriptor
                .FindAction(ControllerContext, "DemoAction");

            IEnumerable<Filter> filters = FilterProviders.Providers
                .GetFilters(ControllerContext, actionDescriptor);

            return View(filters);
        }

        public void Foo() { }
        public void BarAsync() { }
        public void BarCompleted() { }
        public Task<ActionResult> Baz()
        {
            throw new NotImplementedException();
        }

        private IEnumerable<ControllerDescriptor>
            GetControllerDescriptors(params Controller[] controllers)
        {
            controllers = controllers ?? new Controller[0];

            foreach (Controller controller in controllers)
            {
                ControllerContext.Controller = controller;
                SyncActionInvoker syncActionInvoker =
                    controller.ActionInvoker as SyncActionInvoker;

                AsyncActionInvoker asyncActionInvoker =
                   controller.ActionInvoker as AsyncActionInvoker;

                if (null != syncActionInvoker)
                {
                    yield return syncActionInvoker
                        .GetControllerDescriptor(ControllerContext);
                }

                if (null != asyncActionInvoker)
                {
                    yield return asyncActionInvoker
                        .GetControllerDescriptor(ControllerContext);
                }
            }
        }

        private IEnumerable<IActionInvoker> GetActionInvokers()
        {
            // 默认的 ActionInvoker
            NinjectDependencyResolver dependencyResolver = (NinjectDependencyResolver)DependencyResolver.Current;
            yield return this.CreateActionInvoker();

            // 为 Dependency 注册针对  IActionInvoker 的类型映射
            ClearCachedActionInvokers();
            dependencyResolver.Register<IActionInvoker, SyncActionInvoker>();
            yield return this.CreateActionInvoker();

            // 为 Dependency 注册针对 IAsyncActionInvoker 的类型映射
            ClearCachedActionInvokers();
            dependencyResolver.Register<IAsyncActionInvoker, AsyncActionInvoker>();
            yield return this.CreateActionInvoker();
        }

        private void ClearCachedActionInvokers()
        {
            PropertyInfo property = typeof(DependencyResolver)
                .GetProperty("CurrentCache", BindingFlags.NonPublic | BindingFlags.Static);

            var cachedActionInvoker = property.GetValue(null, null);

            FieldInfo field = cachedActionInvoker.GetType()
                .GetField("_cache", BindingFlags.NonPublic | BindingFlags.Instance);

            ConcurrentDictionary<Type, object> dictionary =
                (ConcurrentDictionary<Type, object>)field.GetValue(cachedActionInvoker);

            dictionary.Clear();
        }

    }
}
