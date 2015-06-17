using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC4.Extensions.ModelExtensions
{
    public class HttpHeaderValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            NameValueCollection requestData = new NameValueCollection();
            var headers = controllerContext.RequestContext
                .HttpContext.Request.Headers;
            foreach (string key in headers)
            {
                requestData.Add(key.Replace("-", ""), headers[key]);
            }

            return new NameValueCollectionValueProvider(requestData, CultureInfo.InvariantCulture);
        }
    }
}