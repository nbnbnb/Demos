using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chap7.Models
{
    public class FilterBaseAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
        }
    }
}