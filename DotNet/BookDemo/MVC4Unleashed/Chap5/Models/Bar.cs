using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chap5.Models
{
    // 只会解析应用在参数上的特性
    // 应用在类上的特性 ParameterDescriptor 不会进行解析
    //[ModelBinder(typeof(BarModelBinder))]
    public class Bar
    {
    }
}