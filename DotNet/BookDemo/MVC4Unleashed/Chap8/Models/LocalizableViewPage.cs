using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chap8.Models
{
    public abstract class LocalizableViewPage<TMode> : WebViewPage<TMode>
    {
        [Inject]
        public ResourceReader ResourceReader { get; set; }
    }
}