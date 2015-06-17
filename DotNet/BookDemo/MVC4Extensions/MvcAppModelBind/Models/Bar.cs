using MvcAppModelBind.Binder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcAppModelBind.Models
{
    [ModelBinder(typeof(BarModelBinder))]
    public class Bar
    {
    }
}