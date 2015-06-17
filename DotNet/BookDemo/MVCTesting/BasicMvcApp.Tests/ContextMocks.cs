using Moq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BasicMvcApp.Tests
{
    public class ContextMocks
    {
        public Mock<HttpContextBase> HttpContext { get; private set; }
        public Mock<HttpRequestBase> Request { get; private set; }
        public Mock<HttpResponseBase> Response { get; private set; }

        public ContextMocks(Controller controller)
        {
            // Define all the common context object, plus relationships between them
            HttpContext = new Mock<HttpContextBase>();
            Request = new Mock<HttpRequestBase>();
            Response = new Mock<HttpResponseBase>();

            HttpContext.Setup(m => m.Response).Returns(Response.Object);
            HttpContext.Setup(m => m.Request).Returns(Request.Object);
            HttpContext.Setup(m => m.Session).Returns(new FakeSessionState());

            Request.Setup(m => m.Cookies).Returns(new HttpCookieCollection());
            Response.Setup(m => m.Cookies).Returns(new HttpCookieCollection());
            Request.Setup(m => m.QueryString).Returns(new NameValueCollection());
            Request.Setup(m => m.Form).Returns(new NameValueCollection());

            // Apply the mock context to the supplied controller instance
            RequestContext rc = new RequestContext(HttpContext.Object, new RouteData());
            controller.ControllerContext = new ControllerContext(rc, controller);
        }

        private class FakeSessionState : HttpSessionStateBase
        {
            Dictionary<string, object> items = new Dictionary<string, object>();

            public override object this[string name]
            {
                get
                {
                    return items.ContainsKey(name) ? items[name] : null;
                }
                set
                {
                    items[name] = value;
                }
            }
        }
    }
}
