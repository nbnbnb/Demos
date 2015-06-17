using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap8.FrontController.Controller.Request
{
    public class WebRequest
    {
        public string RequestedURL { get; set; }

        public NameValueCollection QueryArguments { get; set; }
    }
}
