using ASPPatterns.Chap8.FrontController.Controller.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap8.FrontController.Controller.Routing
{
    public class Route
    {
        private string _route;
        public Route(string route)
        {
            _route = route;
        }
        public bool Matches(WebRequest request)
        {
            return request.RequestedURL.ToLower().Contains(_route.ToLower());
        }
        public string URL
        {
            get { return _route; }
        }
    }
}
